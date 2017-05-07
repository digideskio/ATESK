using System;
using System.Collections.Generic;
using System.Windows.Forms;
 

namespace TevelATE
{
    public partial class ListUsersOperators : Form
    {
        usersDb u = new usersDb();

        usersDb.User m_currentSelectedUser;
        public ListUsersOperators()
        {
            InitializeComponent();

            try
            {
                dataGridView1.ColumnCount = 8;
                dataGridView1.Columns[0].Name = "ID";
                dataGridView1.Columns[1].Name = "user name";
                dataGridView1.Columns[2].Name = "QMS User name";
                dataGridView1.Columns[3].Name = "First Name";
                dataGridView1.Columns[4].Name = "Last Name";
                dataGridView1.Columns[5].Name = "Phone number";
                dataGridView1.Columns[6].Name = "Active";
                dataGridView1.Columns[7].Name = "Type";

                ShowUsers();
               
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        void ShowUsers()
        {
            try
            {
                this.dataGridView1.Rows.Clear();
                List<usersDb.User> l = u.ReadAllUsers();
                if (l != null)
                {
                    for (int i = 0; i < l.Count; i++)
                    {
                        usersDb.User x = l[i];
                        this.dataGridView1.Rows.Add(x.ID , x.userName,  x.employeeId, x.firstName, x.lastName, x.phoneNumber, x.active, x.typeOfUser == 1 ? "Technician" : "Operator");
                    }
                }
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             

            try
            {/*
                Users nu = new Users();

                //string s = listBox1.Items[listBox1.SelectedIndex].ToString();
                string[] splitedLine = s.Split(new Char[] { ',' });

                Users.User u = new Users.User
                {
                    typeOfUser = splitedLine[1].Trim() == "Technician" ? 1 : 0,
                    userName = splitedLine[0],
                    active = splitedLine[2] == "active" ? true : false
                };
             
                nu.DeleteUser(u);
                MessageBox.Show("User deleted");*/
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            usersDb nu = new usersDb();
              
            try
            {
                nu.SuspendUser(m_currentSelectedUser.ID, !m_currentSelectedUser.active);
                ShowUsers();
                MessageBox.Show("User suspended");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (m_currentSelectedUser != null)
            {
                SaveUserChangesForm f = new SaveUserChangesForm(m_currentSelectedUser);
                f.ShowDialog();
                ShowUsers();
            }
            else
            {
                MessageBox.Show("Please select a row to edit");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                CreateUserOperator f = new CreateUserOperator();
                f.ShowDialog();           
                ShowUsers();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                ShowUsers();
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;// get the Row Index
            if (index > -1)
            {
                m_currentSelectedUser = new usersDb.User();
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                m_currentSelectedUser.ID = int.Parse(selectedRow.Cells[0].Value.ToString());
                m_currentSelectedUser.userName = selectedRow.Cells[1].Value.ToString();
                try
                {
                    m_currentSelectedUser.employeeId = selectedRow.Cells[2].Value.ToString();
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                }
                m_currentSelectedUser.firstName = selectedRow.Cells[3].Value.ToString();
                m_currentSelectedUser.lastName = selectedRow.Cells[4].Value.ToString();
                m_currentSelectedUser.phoneNumber = selectedRow.Cells[5].Value.ToString();
                m_currentSelectedUser.active = selectedRow.Cells[6].Value.ToString() == "True" ? true : false;
                m_currentSelectedUser.typeOfUser = selectedRow.Cells[7].Value.ToString().ToLower() == "technician" ? 1 : 0;
            }
            else
            {
                m_currentSelectedUser = null;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (m_currentSelectedUser != null)
            {
                DialogResult d = MessageBox.Show("Do you want to delete this user?", "PA ATE", MessageBoxButtons.YesNo);
                if (d == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                       usersDb nu = new usersDb();
                        nu.DeleteUser(m_currentSelectedUser.ID);
                        ShowUsers();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                }

            }
        }
    }
}
