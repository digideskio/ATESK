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

namespace TevelATE
{
    public partial class ATEForm : Form
    {

        enum GUI_STATE
        {
            STARTED,
            STOPPED
        }

        Task m_task;
        CancellationTokenSource tokenSource;
        bool m_running = false;
        List<IATETest> m_test = new List<IATETest>();
        AutoResetEvent m_event = new AutoResetEvent(false);
        
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
            m_test.Add(new ATETest1(p));
            InitializeGui();
            InitializeDataGrid();
        }
        void InitializeGui()
        {
             
        }
        void InitializeDataGrid()
        {
            DataGridView[] d = { dataGridView1 };

            foreach (IATETest test in m_test)
            {
                List<Tuple<string, int>> header = test.GetHeader();
                d[0].ColumnCount = header.Count;
                for (int i = 0; i < header.Count; i++)
                {
                    d[0].Columns[i].Name = header[i].Item1;
                    d[0].Columns[i].Width = header[i].Item2;
                }
            }
        }
        string Start()
        {
            if (m_running == false)
            {
                StartMsgHandler();
                return "pending";
            } else
            {
                return "in process";
            }
        }
        string Stop()
        {
            if (m_running == false)
            {                                
                m_event.Set();
                tokenSource.Cancel();
                m_task.Wait();
                return "ok";
            } 
            else
            {
                return "stopped";
            }
        }
        string Pause()
        {
            return "not supported";
        }

        void StartMsgHandler()
        {
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
                    //btnStart.Enabled = false;
                }
                break;
                case GUI_STATE.STOPPED:
                {
                    //btnStart.Enabled = true;
                }
                break;
            }
        }
        void HostMsgProcessing(CancellationToken ct)
        {
            MsgType msgType;
            while (ct.IsCancellationRequested == false)
            {
                m_event.WaitOne();
                if (ct.IsCancellationRequested == true)
                    return;
                if (m_msgQueue.TryDequeue(out msgType) == false)
                    continue;

                switch (msgType.code)
                {

                    case ATECBCodes.ATE_STARTED:
                    {
                        m_running = true;
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
