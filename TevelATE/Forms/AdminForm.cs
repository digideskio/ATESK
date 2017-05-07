using System;
using System.Windows.Forms;
 

namespace TevelATE
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            usersDb nu = new usersDb();

            if (txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Please enter user name");
                return;
            }

            if (txtUserName.Text == string.Empty)
            {
                MessageBox.Show("Please enter password");
                return;
            }

            if (txtFirstName.Text == string.Empty)
            {
                MessageBox.Show("Please enter first name");
                return;
            }

            if (txtLastName.Text == string.Empty)
            {
                MessageBox.Show("Please enter last name");
                return;
            }
             

            try
            {
                usersDb.User u = new usersDb.User
                {
                    typeOfUser = (int)usersDb.USERS_TYPES.TECHNICIAN,
                    userName = txtUserName.Text,
                    password = txtPassword.Text,
                    firstName = txtFirstName.Text,
                    employeeId = txtEmployeeId.Text,
                    lastName = txtLastName.Text,
                    phoneNumber = txtPhoneNumber.Text,
                    active = true
                };

                nu.CreateNewUser(u);
              
                MessageBox.Show("User created");
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text != string.Empty && txtUserName.Text != string.Empty)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text != string.Empty && txtUserName.Text != string.Empty)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
