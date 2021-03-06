﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TevelATE.IATETest;

namespace TevelATE.AllTests
{


    public class TestHandler
    {
        
        List<IATETest> m_test = new List<IATETest>();
        private  const int m_testCount = 4;

        ATEMsgCallback pCallback;

        public TestHandler(ATEMsgCallback p)
        {
            pCallback = p;
            m_test.Add(new ATETest1(p, 1));
            m_test.Add(new ATETest2(p, 1));
            m_test.Add(new ATETest3(p, 1));
            m_test.Add(new ATETest4(p, 1));

        }

        public List<IATETest> GetTests()
        {
            return m_test;
        }
        public static int GetTestCount()
        {
            return m_testCount;
        }

        public bool Start()
        {
            pCallback(ATECBCodes.ATE_STARTED, "", 0);

            m_test[0].Start();
            return false;
        }
        public bool Stop()
        {
            m_test[0].Stop();
            return false;
        }
    }
}
