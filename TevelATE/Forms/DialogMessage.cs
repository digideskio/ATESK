using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TevelATE.Forms
{
    public partial class DialogMessage : Form
    {
        public DialogMessage(string l1, string l2 = "")
        {
            InitializeComponent();
            label2.Text = l2;
            label1.Text = l1;
        }
    }
}
