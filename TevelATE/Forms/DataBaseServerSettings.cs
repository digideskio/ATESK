using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TevelATE
{
    public partial class DataBaseServerSettings : Form
    {
        string m_ip;
        string m_password;

        public DataBaseServerSettings()
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            try
            {
                using (StreamReader sr = new StreamReader("dbserver.txt"))
                {
                    txtUserName.Text = sr.ReadLine();                    
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
             
        }

        public string IpAddress
        {
            get
            {
                return m_ip;
            }
        }
        public string Password
        {
            get
            {
                return m_password;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.DatabaseServer = txtUserName.Text;
                Properties.Settings.Default.Save();
                m_password = txtPassword.Text;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                return;
            }

            try
            {
                using (StreamWriter sw = new StreamWriter("dbserver.txt"))
                {
                    sw.WriteLine(txtUserName.Text);
                    sw.WriteLine(m_password);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            m_ip = txtUserName.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
