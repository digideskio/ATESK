using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DmitryBrant.CustomControls;
using System.Threading;

namespace Clock
{
    public partial class ClockControl : UserControl
    {
        Thread m_clockThread  = null;
        DateTime m_stat;
        bool m_runningClock = false;
        TimeSpan m_time;
        public delegate void ClockCallback(TimeSpan time);
        ClockCallback pCallback;
        AutoResetEvent m_eventTime = new AutoResetEvent(false);

        public ClockControl()
        {
            InitializeComponent();
            
        }
        public void SetCallback(ClockCallback p)
        {
            pCallback = p;
        }
        public void Start()
        {
            if (m_clockThread == null || m_clockThread.IsAlive == false)
            {
                m_clockThread = new Thread(ClockThread);
                m_stat = DateTime.Now;
                m_clockThread.Start();
            }
        }
        public void Stop()
        {
            m_runningClock = false;
            m_eventTime.Set();
            if (m_clockThread != null)
                m_clockThread.Join();
        }
        void ClockThread()
        {
            m_runningClock = true;
            while (m_runningClock)
            {
                m_time = DateTime.Now - m_stat;
                if (pCallback != null)
                    pCallback(m_time);
                ssHour.Value = m_time.Hours.ToString("00");
                ssMin.Value = m_time.Minutes.ToString("00");
                ssSec.Value = m_time.Seconds.ToString("00");
                m_eventTime.WaitOne(1000);
            }
        }
    }
}
