using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TevelATE
{
    public class ATETest1 : IATETest
    {
        string m_testName = "TEST1";
        public ATETest1(ATEMsgCallback p) : base(p)
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
            m_datagridHeader.Add(new Tuple<string, int>("voltage1", width));
            m_datagridHeader.Add(new Tuple<string, int>("voltage2", width));
            m_datagridHeader.Add(new Tuple<string, int>("voltage3", width));
            m_datagridHeader.Add(new Tuple<string, int>("voltage4", width));
            m_datagridHeader.Add(new Tuple<string, int>("PassFail", width));
        }
        protected override void TaskProcess(CancellationToken ct)
        {
            while (ct.IsCancellationRequested == false)
            {

                Thread.Sleep(1000);
            }                
        }

        public override string Start(int testNum)
        {
            base.Start(testNum);

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
