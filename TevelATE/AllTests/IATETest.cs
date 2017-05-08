using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TevelATE
{
    public abstract class IATETest
    {
        public enum ATECBCodes
        {
            ATE_STARTED,
            ATE_STOPPED,
            ATE_ERROR,
            ATE_TEST_STARTED,
            ATE_TEST_STOPPED,
            ATE_DATA,
            ATE_COLOR_DATA
        }
        int m_testNum = -1;
        Task m_task;
        public delegate void ATEMsgCallback(ATECBCodes code, string msg, int testnum);
        protected List<Tuple<string, int>> m_datagridHeader = new List<Tuple<string, int>>();
        protected bool m_running = false;
        CancellationTokenSource tokenSource;
        private ATEMsgCallback pCallback;

        protected void SendMessage(ATECBCodes code, string msg)
        {
            if (pCallback != null)
            {
                pCallback(code, msg, m_testNum);
            }
        }
       
        public abstract string TestName
        {
            get;
        }
        public IATETest(ATEMsgCallback p, int testNum)
        {
            pCallback = p;
            m_testNum = testNum;
        }        
        public List<Tuple<string, int>>  GetHeader()
        {
            return m_datagridHeader;
        }
        protected abstract void TaskProcess(CancellationToken ct);

        public virtual string Start()
        {
            tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            m_task = Task.Factory.StartNew(() => TaskProcess(token), token);

            pCallback(ATECBCodes.ATE_TEST_STARTED, "", m_testNum);

            m_running = true;
            return "ok";
        }
        public virtual string Stop()
        {
            m_running = false;
            if (tokenSource != null)
                tokenSource.Cancel();
            if (m_task != null)
                m_task.Wait();
            pCallback(ATECBCodes.ATE_TEST_STOPPED, "", m_testNum);
            return "ok";
        }
        public virtual string Initialize()
        {
            return "ok";
        }


    }
}
