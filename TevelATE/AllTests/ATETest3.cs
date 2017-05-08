using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TevelATE
{
    public class ATETest3 : IATETest
    {
        string m_testName = "TEST3";
        public ATETest3(ATEMsgCallback p, int testNum) : base(p, testNum)
        {
            SetDataGridHeader();
        }
        public  override string TestName
        {
            get
            {
                return m_testName;
            }
        }
        private void SetDataGridHeader()
        {
            int width = 100;
            m_datagridHeader.Add(new Tuple<string, int>("xxx", width));
            m_datagridHeader.Add(new Tuple<string, int>("xxx", width));
            m_datagridHeader.Add(new Tuple<string, int>("xx", width));
            m_datagridHeader.Add(new Tuple<string, int>("x", width));
            m_datagridHeader.Add(new Tuple<string, int>("PassFail", width));
        }
        protected override void TaskProcess(CancellationToken ct)
        {
            while (ct.IsCancellationRequested == false)
            {

                Thread.Sleep(1000);
            }                
        }

        public override string Start()
        {
            base.Start();

            return "ok";
        }
        public override string Stop()
        {
            base.Stop();

            return "ok";
        }
        public override string Initialize()
        {
            base.Initialize();

            return "ok";
        }
    }
}
