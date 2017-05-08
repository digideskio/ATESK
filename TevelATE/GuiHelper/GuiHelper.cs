using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TevelATE.GuiHelper
{
    public static class GuiDataGridHelper 
    {
        static List<DataGridView> m_dataGridView;
        public static void SetGrids(List<DataGridView> dataGridView)
        {
            m_dataGridView = dataGridView;
        }

        public static void UpdateDataGridWith(string[] sdata, int testNum)
        {
            testNum--;
            if (m_dataGridView[testNum].InvokeRequired == true)
                m_dataGridView[testNum].Invoke((MethodInvoker)delegate
                {

                    m_dataGridView[testNum].Rows.Add();

                    Int32 index = m_dataGridView[testNum].Rows.Count - 2;

                    for (int i = 0; i < sdata.Length;i++)
                    {
                        m_dataGridView[testNum].Rows[index].Cells[i].Value = sdata[i];
                    }
                });
        }
        public static void ClearAll()
        {
            foreach (DataGridView d in m_dataGridView)
            {
                d.Rows.Clear();
            }
        }
        static Color stringToColor(string colorString)
        {
            try
            {
                string[] colors = colorString.Split(':');
                return Color.FromArgb(int.Parse(colors[3]), int.Parse(colors[0]), int.Parse(colors[1]), int.Parse(colors[2]));
            }
            catch
            {
                return Color.Black;
            }
        }

        public static void UpdateColorGridWith(string[] cdata, int testNum)
        {
            testNum--;
            if (m_dataGridView[testNum].InvokeRequired == true)
                m_dataGridView[testNum].Invoke((MethodInvoker)delegate
                {

                    Int32 index = m_dataGridView[testNum].Rows.Count - 2;

                    for (int i = 0; i < cdata.Length; i++)
                    {
                        m_dataGridView[testNum].Rows[index].Cells[i].Style.ForeColor = stringToColor(cdata[i]);
                    }
                });
        }
    }
}
