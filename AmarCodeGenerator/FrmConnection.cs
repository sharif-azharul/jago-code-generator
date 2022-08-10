using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmarCodeGenerator
{
    public partial class FrmConnection : Form
    {
        public FrmConnection()
        {
            InitializeComponent();
        }

        private void btnConnectDisconnect_Click(object sender, EventArgs e)
        {
            try
            {

                if ((this.rboWindowsSecurity.Checked) && (this.txtServerName.Text != ""))
                {
                    this.lblConnectionStatus.Text = "Connected to " + this.txtServerName.Text; ;

                    #region Windows Security
                    if (CommonTask.ConnectToDatabase(CreateConnectionStringWithoutDatabase()))
                    {
                        Reset(false);
                        FrmClassSP frm = new FrmClassSP();
                        this.Hide();

                        frm.GetAllDatabaseNames();
                        frm.ShowDialog();
                    }

                    #endregion
                }
                else if ((this.rboUsernamePasswordSecurity.Checked) && (this.txtServerName.Text != ""))
                {
                    this.lblConnectionStatus.Text = "Connected to " + this.txtServerName.Text; ;

                    #region User Name Password security
                    if ((this.txtUserName.Text != "") && (this.txtPassword.Text != ""))
                    {
                        SessionUtility.DB_USER = txtUserName.Text.Trim();
                        SessionUtility.DB_PASSWORD = txtPassword.Text.Trim();
                        SessionUtility.DB_SERVER_NAME = txtServerName.Text.Trim();
                        if (CommonTask.ConnectToDatabase(CreateConnectionStringWithoutDatabase()))
                        {
                            Reset(false);
                            FrmClassSP frm = new FrmClassSP();
                            this.Hide();

                            frm.GetAllDatabaseNames();
                            frm.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter user name and password");
                    }
                    #endregion
                }
                else
                {
                    MessageBox.Show("Please enter server name");
                }

            }
            catch (Exception ex)
            {
                CommonTask.LogError(ex);
            }
        }




        //........... Reset form controls ......................
        private void Reset(bool status)
        {
            try
            {
                if (status)
                {
                    //.................... True .................................
                    this.gboConnectionStatus.Enabled = !status;
                    this.btnDisconnect.Enabled = !status;
                    //this.tabAmarCodeGenerator.TabPages.Add(this.tbConnectionString);
                    //this.tabAmarCodeGenerator.TabPages.Remove(this.tbClassGenerator);
                }
                else
                {
                    //.................... False .................................
                    this.gboConnectionStatus.Enabled = !status;
                    this.btnDisconnect.Enabled = !status;
                    //this.tabAmarCodeGenerator.TabPages.Remove(this.tbConnectionString);
                    //this.tabAmarCodeGenerator.TabPages.Add(this.tbClassGenerator);
                }


                //this.gboConnectionStatus.Enabled = !status;
                //this.gboConnectionString.Enabled = status;                
                //this.btnDisconnect.Enabled = !status;
                //this.gboCreateClass.Enabled = !status;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void ClearAll()
        {
            try
            {
                this.txtServerName.Text = "";
                this.rboWindowsSecurity.Checked = true;
                this.rboUsernamePasswordSecurity.Checked = false;
                this.txtUserName.Text = "";
                this.txtPassword.Text = "";
                this.lblConnectionStatus.Text = "Not Connected";

                //this.cboDatabaseNames.DataSource = null;
                //this.cboDatabaseNames.Items.Clear();
                //this.chkListBoxDataBaseTables.DataSource = null;
                //this.chkListBoxDataBaseTables.Items.Clear();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //...........Create connection string .................
        private string CreateConnectionStringWithoutDatabase()
        {

            try
            {
                string strDataSource = this.txtServerName.Text;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                if (this.rboUsernamePasswordSecurity.Checked)
                {
                    //........................... user name password security mode............................
                    string strUserName = this.txtUserName.Text;
                    string strPassword = this.txtPassword.Text;
                    sb.AppendFormat("Data Source=" + strDataSource + ";Persist Security Info=True;User ID=" + strUserName + ";Password=" + strPassword);
                }
                else if (this.rboWindowsSecurity.Checked)
                {
                    //......................... windows authentication security mode..........................
                    sb.AppendFormat("Data Source=" + strDataSource + ";Integrated Security=SSPI;Persist Security Info=False");
                }

                SessionUtility.SQL_CONN_STRING = sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SessionUtility.SQL_CONN_STRING;
        }

        public string CreateConnectionStringWithDatabase(string pDBName)
        {
            try
            {
                string strDataSource = this.txtServerName.Text;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                if (this.rboUsernamePasswordSecurity.Checked)
                {
                    //........................... user name password security mode............................
                    //string strUserName = this.txtUserName.Text;
                    //string strPassword = this.txtPassword.Text;
                    sb.AppendFormat("Data Source=" + SessionUtility.DB_SERVER_NAME + ";Persist Security Info=True;User ID=" + SessionUtility.DB_USER + ";Password=" + SessionUtility.DB_PASSWORD + ";Initial Catalog=" + pDBName);
                }
                else if (this.rboWindowsSecurity.Checked)
                {
                    //......................... windows authentication security mode..........................
                    sb.AppendFormat("Data Source=" + SessionUtility.DB_SERVER_NAME + ";Integrated Security=SSPI;Persist Security Info=False" + ";Initial Catalog=" + pDBName);
                }

                SessionUtility.SQL_CONN_STRING = sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SessionUtility.SQL_CONN_STRING;
        }

    }
}
