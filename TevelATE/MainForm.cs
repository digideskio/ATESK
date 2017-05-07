using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using RegistryClassApi;


namespace TevelATE
{

    public partial class MainForm : Form
    {
        StationInfo station;
        usersDb m_users = new usersDb();
        ATEForm m_ateForm;
        AutoResetEvent m_dayliEvent = new AutoResetEvent(false);
        Thread m_threadSend = null;
        List<usersDb.User> m_userList;
        string m_serverIpAddress;
        
        string m_fieldGuid = string.Empty;
        public MainForm()
        {
            InitializeComponent();

            string password = ""; 
            try
            {
                using (StreamReader sr = new StreamReader("dbserver.txt"))
                {
                    m_serverIpAddress = sr.ReadLine();
                    password = sr.ReadLine();
                    if (password == null)
                    {
                        throw (new SystemException("No password"));
                    }
                }
            }
            catch (Exception )
            {              
               DataBaseServerSettings s = new DataBaseServerSettings();
               if (s.ShowDialog() == System.Windows.Forms.DialogResult.OK)
               {
                   m_serverIpAddress = s.IpAddress;
                   password = s.Password;
                   Properties.Settings.Default.DatabaseServer = s.IpAddress;
                   Properties.Settings.Default.Save();
               }
            }

            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;

            Point ptPanel = new Point();
            ptPanel.X = screenWidth / 2 - panel1.Size.Width / 2;
            ptPanel.Y = screenHeight / 2 - panel1.Size.Height / 2; ;
            panel1.Location = ptPanel;
        
            try
            {
                MySQLConnector.Initialize(m_serverIpAddress, password);
            }
            catch (Exception err)
            {
                _MessageBox(err.Message);
            }

            
            cmbUserType.SelectedIndex = 0;
            btnEnterBTM.Enabled = false;
 
 
            llCreateTech.Enabled = false;
            llCreateTech.Visible = true;

            try
            {
                ShowUsers();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            loadSettings();
            try
            {
                string guid = Guid.NewGuid().ToString();
                clsRegistry reg = new clsRegistry();
                m_fieldGuid = reg.GetStringValue(Registry.LocalMachine, "SOFTWARE\\LusterTech\\Field", "Guid");
                if (reg.strRegError != null)
                {
                    reg.SetStringValue(Registry.LocalMachine, "SOFTWARE\\LusterTech\\Field", "Guid", guid);
                    AddNewStation(guid, "Station first");
                }
                else
                {
                    if (m_fieldGuid == string.Empty || m_fieldGuid == null)
                    {
                        reg.SetStringValue(Registry.LocalMachine, "SOFTWARE\\LusterTech\\Field", "Guid", guid);
                        AddNewStation(guid, "Station first");
                    }
                }
                m_fieldGuid = reg.GetStringValue(Registry.LocalMachine, "SOFTWARE\\LusterTech\\Field", "Guid");
                label5.Text = m_fieldGuid;

                station = MySQLConnector.getStationInfo(m_fieldGuid);
                if (station.guid == null)
                {
                    AddNewStation(m_fieldGuid, "Station first");
                    station = MySQLConnector.getStationInfo(m_fieldGuid);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }


        }
        void AddNewStation(string guid, string Alias)
        {
            MySQLConnector.AddNewStation(guid, Alias);
        }
         
        void loadSettings()
        {
            try
            {
                comboUserName.Text = Properties.Settings.Default.LoginName;
                cmbUserType.SelectedIndex = Properties.Settings.Default.UserType;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }

        DialogResult _MessageBox(string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
        {

            string caption = "ATE Jig";
            DialogResult result;
            // Displays the MessageBox.
            result = MessageBox.Show(this, message, caption, buttons);
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            if (cmbUserType.SelectedIndex == -1)
            {
                _MessageBox("Please select type of user");
                return;
            }
            if (comboUserName.Text == string.Empty)
            {
                MessageBox.Show("Enter user name please");
                return;
            }
            if (txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Enter password please");
                return;
            }

            try
            {
                
                if (cmbUserType.SelectedIndex == 0)
                {
                   
                    if (m_users.CheckAuthtintication(comboUserName.Text, 
                                                     txtPassword.Text, 
                                                     cmbUserType.SelectedIndex) == true)
                    {

                      

                        m_ateForm = new ATEForm(comboUserName.Text, txtPassword.Text, false, station);
                        this.Hide();
                        m_ateForm.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        _MessageBox("User name or password are incorrect");
                    }
                }
                else
                {
                     
                    if (m_users.CheckAuthtintication(comboUserName.Text,                                         
                                                     txtPassword.Text, 
                                                     cmbUserType.SelectedIndex) == true)
                    {



                        m_ateForm = new ATEForm(comboUserName.Text, txtPassword.Text, true, station);
                        this.Hide();
                        m_ateForm.ShowDialog();
                        this.Show();
                    }
                    else
                    {
                        _MessageBox("User name or password are incorrect");
                    }
                }
            }
            catch (Exception err)
            {
                _MessageBox("Error #100:" + err.Message);
                this.Show();


                if (err.Message.Contains("KMTronics HW Switches Initializtion error: Kmtronic : A device attached to the system is not functioning"))
                {                                       
                    RmoveKMTronic();
                    Thread.Sleep(2000);
                    RescanUSB();
                    Thread.Sleep(2000);
                } else 
                if (err.Message.Contains("KMTronics HW Switches Initializtion error: Kmtronic : The PortName cannot be empty"))
                {
                    RmoveKMTronic();
                    Thread.Sleep(2000);
                    RescanUSB();
                    Thread.Sleep(2000);
                }
            }
        }

        void RescanUSB()
        {
            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = "devcon.exe";
                proc.StartInfo.Arguments = "rescan";
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        void RmoveKMTronic()
        {
            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = "devcon.exe";
                proc.StartInfo.Arguments = @"/r remove USB\VID_04D8&PID_FEF9";
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {      
            m_dayliEvent.Set();
            if (m_threadSend != null)
                m_threadSend.Join();

            Properties.Settings.Default.LoginName = comboUserName.Text;
            Properties.Settings.Default.UserType = cmbUserType.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //onChangeInputs();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            onChangeInputs();
        }

        void onChangeInputs()
        {
          
            if (cmbUserType.Text == "Admin" && txtPassword.Text == usersDb.GetAdminPassword())
            {
                llShowUsers.Enabled = true;
                llCreateTech.Enabled = true;
            }
            else
            {
                llShowUsers.Enabled = false;
                llCreateTech.Enabled = false;
            }

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUserType.SelectedIndex == 0)
            {
                llShowUsers.Enabled = false;
                llCreateTech.Enabled = false;
                llSettings.Enabled = false;
                btnEnterBTM.Enabled = true;
            } else 
            if (cmbUserType.SelectedIndex == 1)
            {
                llShowUsers.Enabled = false;
                llCreateTech.Enabled = false;
                llSettings.Enabled = true;
                btnEnterBTM.Enabled = true;
            } else
            if (cmbUserType.SelectedIndex == 2)
            {
                llShowUsers.Enabled = true;
                llCreateTech.Enabled = true;
                llSettings.Enabled = true;
                btnEnterBTM.Enabled = false;
            }
            ShowUsers();
        }
        
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            if (cmbUserType.SelectedIndex == -1)
            {
                _MessageBox("Please select type of user");
                return;
            }
            if (comboUserName.Text == string.Empty)
            {
                _MessageBox("Enter user name please");
                return;
            }
            if (txtPassword.Text == string.Empty)
            {
                _MessageBox("Enter password please");
                return;
            }
            try
            {
                if (m_users.CheckAuthtintication(comboUserName.Text, txtPassword.Text, cmbUserType.SelectedIndex) == true)
                {
                    SettingsForm f = new SettingsForm();
                    f.ShowDialog();
                }
                else
                {
                    _MessageBox("User name or password are incorrect");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                this.Show();
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (cmbUserType.SelectedIndex != 2)
            {
                _MessageBox("Only admin can create new users");
                return;
            }
            
            if (txtPassword.Text == string.Empty)
            {
                _MessageBox("Enter password please");
                return;
            }
            try
            {

                string apas = usersDb.GetAdminPassword();
                 

                if (cmbUserType.Text == "Admin" && txtPassword.Text == apas)
                {
                    AdminForm f = new AdminForm();
                    this.Hide();
                    f.ShowDialog();
                    this.Show();

                    ShowUsers();

                    return;
                }
                 
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                this.Show();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboUserName.Text.ToLower() == "admin" && txtPassword.Text == usersDb.GetAdminPassword())
            {
                llCreateTech.Enabled = true;
            }
            else
            {
                llCreateTech.Enabled = false;
            }
        }
        void ShowUsers()
        {

            comboUserName.Items.Clear();
            if (cmbUserType.SelectedIndex == 2)
                return;
            m_userList = m_users.ReadAllUsers();
            foreach (usersDb.User u in m_userList)
            {
                if (cmbUserType.SelectedIndex == 0 && u.typeOfUser == 0)
                    comboUserName.Items.Add(u.userName);

                if (cmbUserType.SelectedIndex == 1 && u.typeOfUser == 1)
                    comboUserName.Items.Add(u.userName);
            }

        }

        private void llShowUsers_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
                        
            if (cmbUserType.SelectedIndex == -1)
            {
                _MessageBox("Please select type of user");
                return;
            }
            if (cmbUserType.SelectedIndex == 0)
                return;
             
            if (txtPassword.Text == string.Empty)
            {
                _MessageBox("Enter password please");
                return;
            }
            try
            {
                string userName = string.Empty;
                if (cmbUserType.SelectedIndex == 2)
                {
                    userName = "Admin";
                }
                if (cmbUserType.SelectedIndex == 1)
                {
                    userName = comboUserName.Text;
                }
                if (cmbUserType.SelectedIndex > 0)
                {
                    if (m_users.CheckAuthtintication(userName, txtPassword.Text, cmbUserType.SelectedIndex) == true)
                    {
                        ListUsersOperators f = new ListUsersOperators();
                        f.ShowDialog();

                        ShowUsers();
                    }
                    else
                    {
                        _MessageBox("User name or password are incorrect");
                    }
                }
                else
                {
                    MessageBox.Show("Only technicisan can view users");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
                this.Show();
            }
        }
    }
}
