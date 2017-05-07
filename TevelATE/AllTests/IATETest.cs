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
            ATE_ERROR           
        }
        int m_testNum = -1;
        Task m_task;
        public delegate void ATEMsgCallback(ATECBCodes code, string msg, int testnum);
        protected List<Tuple<string, int>> m_datagridHeader = new List<Tuple<string, int>>();
        protected bool m_running = false;

        private ATEMsgCallback pCallback;

        protected void SendMessage(ATECBCodes code, string msg, int testNum)
        {
            if (pCallback != null)
            {
                pCallback(code, msg, testNum);
            }
        }
        public static int GetTestCount()
        {
            return 1;
        }
        public abstract string TestName
        {
            get;
        }
        public IATETest(ATEMsgCallback p)
        {
            pCallback = p;
        }        
        public List<Tuple<string, int>>  GetHeader()
        {
            return m_datagridHeader;
        }
        protected abstract void TaskProcess(CancellationToken ct);

        public virtual string Start(int testNum)
        {
            m_testNum = testNum;
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            m_task = Task.Factory.StartNew(() => TaskProcess(token), token);

            m_running = true;
            return "ok";
        }
        public virtual string Stop()
        {
            m_running = false;
            return "ok";
        }
        public virtual string Initialize()
        {
            return "ok";
        }


    }
}
