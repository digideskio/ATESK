using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DmitryBrant.CustomControls;
using static TevelATE.IATETest;
using System.Threading;
using System.Collections.Concurrent;
using System.Drawing.Drawing2D;
using TevelATE.Forms;
using Bulb;
using System.Reflection;
using TevelATE.AllTests;

namespace TevelATE
{
    public partial class ATEForm : Form
    {
        bool m_ledBlink = false;
        enum GUI_STATE
        {
            STARTED,
            STOPPED
        }
        List<DataGridView> m_dataGridView = new List<DataGridView>();
        Task m_task;
        Thread m_ledBlinkThread;
        CancellationTokenSource tokenSource;
        bool m_running = false;
        TestHandler m_tHandler;
        AutoResetEvent m_event = new AutoResetEvent(false);
        List<LedBulb> m_leds = new List<LedBulb>(); 

        struct MsgType
        {
            public ATECBCodes code;
            public string msg;
            public int testnum;
        }
        ConcurrentQueue<MsgType> m_msgQueue = new ConcurrentQueue<MsgType>();
        public ATEForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            Initialize();

           
        }
        public ATEForm(string userName, string password, bool technical, StationInfo station)
        {
            InitializeComponent();
            Initialize();
        }
        void Initialize()
        {
            ATEMsgCallback p = new ATEMsgCallback(HostMsgCallback);
            m_tHandler = new TestHandler(p);
            InitializeGui();
            InitializeDataGrid();

            txtSerialNumber.Text = "1";
            txtPartNumber.Text = "1";
        }
        void InitializeGui()
        {

            m_leds.Add(led1);
            m_leds.Add(led2);
            m_leds.Add(led3);
            m_leds.Add(led4);
            m_leds.Add(led5);

            for (int i = 0; i < TestHandler.GetTestCount(); i++)
            {
                //m_dataGridView.Add(dataGridView1);
                m_dataGridView.Add(CreateDataGridForTests(i));

                
            }
        }
 
        void InitializeDataGrid()
        {
             
            int j = 0;
            List<IATETest> tests = m_tHandler.GetTests();
            foreach (IATETest test in tests)
            {
                DataGridView d = m_dataGridView[j];
                List<Tuple<string, int>> header = test.GetHeader();
                d.ColumnCount = header.Count;
                for (int i = 0; i < header.Count; i++)
                {
                    d.Columns[i].Name = header[i].Item1;
                    d.Columns[i].Width = header[i].Item2;
                }
                d.Invalidate();
                j++;
            } 
        }
        string CheckStartConditions()
        {
            if (txtPartNumber.Text == string.Empty)
            {
                return "Part number is missing";
            }

            if (txtSerialNumber.Text == string.Empty)
            {
                return "Serial number is missing";
            }

            return "ok";
        }
        string Start()
        {
            lock (this)
            {
                string r = string.Empty;
                if (m_running == false)
                {
                    if ((r = CheckStartConditions()) != "ok")
                    {
                        ShowDialogMessage(r);
                        return "error on start";
                    }
                    StartMsgHandler();
                    m_tHandler.Start();
                    return "pending";
                }
                else
                {
                    return "in process";
                }
            }
        }
        string Stop()
        {
            lock (this)
            {
                if (m_running == true)
                {
                    m_tHandler.Stop();
                    m_event.Set();
                    if (tokenSource != null)
                        tokenSource.Cancel();
                    m_task.Wait();
                    StopBlinkLed();
                    GuiState(GUI_STATE.STOPPED);
                    return "ok";
                }
                else
                {
                    return "stopped";
                }
            }
        }
        string Pause()
        {
            return "not supported";
        }

        void StartMsgHandler()
        {
            if (m_task != null)
                m_task.Wait();
            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;
          
            m_task = Task.Factory.StartNew(() => HostMsgProcessing(token), token);
            
        }
        void HostMsgCallback(ATECBCodes code, string msg, int testnum)
        {
            MsgType m = new MsgType();
            m.msg = msg;
            m.testnum = testnum;
            m.code = code;
            m_msgQueue.Enqueue(m);
            m_event.Set();
        }
        void GuiState(GUI_STATE state)
        {

            switch (state)
            {
                case GUI_STATE.STARTED:
                {
                    m_running = true;
                    btnStart.BackColor = Color.LightGreen;
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                }
                break;
                case GUI_STATE.STOPPED:
                {
                   btnStart.BackColor = Color.White;
                   btnStart.Enabled = true;
                   btnStop.Enabled = false;
                   m_running = false;                        
                }
                break;                
            }
        }
        void HostMsgProcessing(CancellationToken ct)
        {
            MsgType msgType;
            while (ct.IsCancellationRequested == false)
            {
                if (m_msgQueue.Count == 0)
                    m_event.WaitOne();
                if (ct.IsCancellationRequested == true && m_msgQueue.Count == 0)
                    return;
                if (m_msgQueue.TryDequeue(out msgType) == false)
                    continue;

                switch (msgType.code)
                {

                    case ATECBCodes.ATE_STARTED:
                    {
                        GuiState(GUI_STATE.STARTED);
                    }
                    break;
                    case ATECBCodes.ATE_STOPPED:
                    {

                    }
                    break;
                    case ATECBCodes.ATE_ERROR:
                    {

                    }
                    break;
                    case ATECBCodes.TEST_STARTED:
                    {
                        StartBlinkLed(msgType.testnum);
                    }
                    break;
                    case ATECBCodes.TEST_STOPPED:
                    {
                        StopBlinkLed();
                    }
                    break;
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {            
            Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Stop();
        }
        void ShowDialogMessage(string l1 , string l2 = "")
        {
            DialogMessage d = new DialogMessage(l1, l2);
            d.ShowDialog();
        }
        void BlinkLed(int testNum)
        {
          
            while (m_ledBlink)
            {
                m_leds[testNum].On = !m_leds[testNum].On;
                Thread.Sleep(800);
            }
        }

        void StopBlinkLed()
        {
            m_ledBlink = false;
            if (m_ledBlinkThread != null)
                m_ledBlinkThread.Join();
        }

        void StartBlinkLed(int testNum)
        {
            if (m_ledBlinkThread == null || m_ledBlinkThread.IsAlive == false)
            {
                m_ledBlink = true;
                m_ledBlinkThread = new Thread(() => BlinkLed(testNum - 1));
                m_ledBlinkThread.Start();
            }            
        }
        DataGridView CreateDataGridForTests(int i)
        {
          
            DataGridView dg = new DataGridView();

            dg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dg.Location = new System.Drawing.Point(244, 188);
            dg.Name = "dataGridView" + i;
            dg.Size = new System.Drawing.Size(960, 661);
            dg.TabIndex = 30;
            this.Controls.Add(dg);

            return dg;
        }
        private void ATEForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_running == true)
            {
                ShowDialogMessage("Still running");
                return;
            }
        }
    }
}
