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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConnection frm = new FrmConnection();
            frm.ShowDialog();
        }
    }
}
