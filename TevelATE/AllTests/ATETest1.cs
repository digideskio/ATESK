﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TevelATE
{
    public class ATETest1 : IATETest
    {
        string m_testName = "TEST1";
        public ATETest1(ATEMsgCallback p, int testNum) : base(p, testNum)
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
        public string CTS(Color color)
        {
            return color.R + ":" + color.G + ":" + color.B + ":" + color.A;
        }
        protected override void TaskProcess(CancellationToken ct)
        {
            while (ct.IsCancellationRequested == false)
            {
                SendMessage(ATECBCodes.ATE_DATA, "3.2, 4.2 , 1.8 , 10.121, PASS");
                string[] colors = { CTS(Color.Green), CTS(Color.Red), CTS(Color.Green), CTS(Color.Orange), CTS(Color.Red)};
                var result = string.Join(",", colors);
                SendMessage(ATECBCodes.ATE_COLOR_DATA, result);
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
