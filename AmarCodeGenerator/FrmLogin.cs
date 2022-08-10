using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmarCodeGenerator
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserId.Text) && !string.IsNullOrEmpty(txtLoginPass.Text))
            {
                if (txtUserId.Text == txtLoginPass.Text)
                {
                    FrmMain frm = new FrmMain();
                    this.Hide();

                    frm.ShowDialog();
                }
                else
                    MessageBox.Show("Please enter valid user id or password");
            }
            else
                MessageBox.Show("Please enter user id or password");
        }
    }
}
