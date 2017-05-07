using System;
using System.Windows.Forms;


namespace TevelATE
{
    public partial class SaveUserChangesForm : Form
    {
        usersDb.User u;
        public SaveUserChangesForm(usersDb.User u)
        {
            InitializeComponent();
            this.u = u;

            textBox1.Text = u.userName;
            textBox3.Text = u.firstName;
            textBox4.Text = u.lastName;
            textBox5.Text = u.phoneNumber;
            checkBox1.Checked = u.active;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            usersDb nu = new usersDb();
            

            if (textBox2.Text == string.Empty || textBox2.Text == null)
            {
                MessageBox.Show("Please enter password");
                return;
            }

            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Please enter user name");
                return;
            }

            if (textBox6.Text == string.Empty)
            {
                MessageBox.Show("Please enter your QMS user name");
                return;
            }

            if (textBox3.Text == string.Empty)
            {
                MessageBox.Show("Please enter first name");
                return;
            }

            if (textBox4.Text == string.Empty)
            {
                MessageBox.Show("Please enter last name");
                return;
            }

            if (textBox5.Text == string.Empty)
            {
                MessageBox.Show("Please enter phone number");
                return;
            }

            try
            {
                usersDb.User u1 = new usersDb.User
                {
                    ID = u.ID,
                    typeOfUser = u.typeOfUser,
                    userName = textBox1.Text,
                    employeeId = textBox6.Text,
                    password = textBox2.Text,
                    firstName = textBox3.Text,
                    lastName = textBox4.Text,
                    phoneNumber = textBox5.Text,
                    active = checkBox1.Checked
                };
                nu.SaveUserChanges(u1);
                MessageBox.Show("Save changed ok");
            }
            catch (Exception err)
            {
                throw (new SystemException(err.Message));
            }
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.ToLower() == "admin")
            {
                button1.Enabled = false;
                return;
            }

            if (textBox2.Text != string.Empty && textBox1.Text != string.Empty)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
     
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.ToLower() == "admin")
            {
                button1.Enabled = false;
                return;
            }

            if (textBox2.Text != string.Empty && textBox1.Text != string.Empty)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
    }
}
