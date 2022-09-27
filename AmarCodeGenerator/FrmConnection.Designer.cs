namespace AmarCodeGenerator
{
    partial class FrmConnection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gboConnectionString = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.rboUsernamePasswordSecurity = new System.Windows.Forms.RadioButton();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.btnConnectDisconnect = new System.Windows.Forms.Button();
            this.rboWindowsSecurity = new System.Windows.Forms.RadioButton();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.gboConnectionStatus = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.gboConnectionString.SuspendLayout();
            this.gboConnectionStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // gboConnectionString
            // 
            this.gboConnectionString.Controls.Add(this.label6);
            this.gboConnectionString.Controls.Add(this.rboUsernamePasswordSecurity);
            this.gboConnectionString.Controls.Add(this.txtServerName);
            this.gboConnectionString.Controls.Add(this.btnConnectDisconnect);
            this.gboConnectionString.Controls.Add(this.rboWindowsSecurity);
            this.gboConnectionString.Controls.Add(this.txtUserName);
            this.gboConnectionString.Controls.Add(this.label10);
            this.gboConnectionString.Controls.Add(this.txtPassword);
            this.gboConnectionString.Controls.Add(this.label7);
            this.gboConnectionString.Controls.Add(this.label8);
            this.gboConnectionString.Location = new System.Drawing.Point(60, 11);
            this.gboConnectionString.Margin = new System.Windows.Forms.Padding(4);
            this.gboConnectionString.Name = "gboConnectionString";
            this.gboConnectionString.Padding = new System.Windows.Forms.Padding(4);
            this.gboConnectionString.Size = new System.Drawing.Size(420, 384);
            this.gboConnectionString.TabIndex = 23;
            this.gboConnectionString.TabStop = false;
            this.gboConnectionString.Text = "Connection String";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(40, 48);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(308, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "1) Select or enter Server Name/IP Address";
            // 
            // rboUsernamePasswordSecurity
            // 
            this.rboUsernamePasswordSecurity.AutoSize = true;
            this.rboUsernamePasswordSecurity.Checked = true;
            this.rboUsernamePasswordSecurity.Location = new System.Drawing.Point(64, 193);
            this.rboUsernamePasswordSecurity.Margin = new System.Windows.Forms.Padding(4);
            this.rboUsernamePasswordSecurity.Name = "rboUsernamePasswordSecurity";
            this.rboUsernamePasswordSecurity.Size = new System.Drawing.Size(267, 20);
            this.rboUsernamePasswordSecurity.TabIndex = 20;
            this.rboUsernamePasswordSecurity.TabStop = true;
            this.rboUsernamePasswordSecurity.Text = "Use a specific user name and password";
            this.rboUsernamePasswordSecurity.UseVisualStyleBackColor = true;
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(88, 71);
            this.txtServerName.Margin = new System.Windows.Forms.Padding(4);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(259, 22);
            this.txtServerName.TabIndex = 10;
            this.txtServerName.Text = "DESKTOP-BPLNFDB";
            // 
            // btnConnectDisconnect
            // 
            this.btnConnectDisconnect.Location = new System.Drawing.Point(268, 309);
            this.btnConnectDisconnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnConnectDisconnect.Name = "btnConnectDisconnect";
            this.btnConnectDisconnect.Size = new System.Drawing.Size(100, 31);
            this.btnConnectDisconnect.TabIndex = 0;
            this.btnConnectDisconnect.Text = "Connect";
            this.btnConnectDisconnect.UseVisualStyleBackColor = true;
            this.btnConnectDisconnect.Click += new System.EventHandler(this.btnConnectDisconnect_Click);
            // 
            // rboWindowsSecurity
            // 
            this.rboWindowsSecurity.AutoSize = true;
            this.rboWindowsSecurity.Location = new System.Drawing.Point(64, 165);
            this.rboWindowsSecurity.Margin = new System.Windows.Forms.Padding(4);
            this.rboWindowsSecurity.Name = "rboWindowsSecurity";
            this.rboWindowsSecurity.Size = new System.Drawing.Size(245, 20);
            this.rboWindowsSecurity.TabIndex = 19;
            this.rboWindowsSecurity.Text = "Use Windows NT integrated security";
            this.rboWindowsSecurity.UseVisualStyleBackColor = true;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(181, 222);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtUserName.Size = new System.Drawing.Size(185, 22);
            this.txtUserName.TabIndex = 11;
            this.txtUserName.Text = "sa";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(40, 142);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(281, 20);
            this.label10.TabIndex = 18;
            this.label10.Text = "2) Enter Information to log on to server";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(181, 256);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(185, 22);
            this.txtPassword.TabIndex = 12;
            this.txtPassword.Text = "sa2016";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(83, 225);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "User Name";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(83, 256);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "Password";
            // 
            // gboConnectionStatus
            // 
            this.gboConnectionStatus.BackColor = System.Drawing.Color.Transparent;
            this.gboConnectionStatus.Controls.Add(this.label23);
            this.gboConnectionStatus.Controls.Add(this.lblConnectionStatus);
            this.gboConnectionStatus.Controls.Add(this.btnDisconnect);
            this.gboConnectionStatus.Location = new System.Drawing.Point(60, 402);
            this.gboConnectionStatus.Margin = new System.Windows.Forms.Padding(4);
            this.gboConnectionStatus.Name = "gboConnectionStatus";
            this.gboConnectionStatus.Padding = new System.Windows.Forms.Padding(4);
            this.gboConnectionStatus.Size = new System.Drawing.Size(420, 65);
            this.gboConnectionStatus.TabIndex = 43;
            this.gboConnectionStatus.TabStop = false;
            this.gboConnectionStatus.Text = "Connection Status";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(15, 31);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(120, 16);
            this.label23.TabIndex = 37;
            this.label23.Text = "Connection Status :";
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.Red;
            this.lblConnectionStatus.Location = new System.Drawing.Point(156, 31);
            this.lblConnectionStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(96, 16);
            this.lblConnectionStatus.TabIndex = 36;
            this.lblConnectionStatus.Text = "Not Connected";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(268, 21);
            this.btnDisconnect.Margin = new System.Windows.Forms.Padding(4);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(100, 31);
            this.btnDisconnect.TabIndex = 4;
            this.btnDisconnect.Text = "Disconnect";
            // 
            // FrmConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 481);
            this.Controls.Add(this.gboConnectionStatus);
            this.Controls.Add(this.gboConnectionString);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmConnection";
            this.Text = "FrmConnection";
            this.gboConnectionString.ResumeLayout(false);
            this.gboConnectionString.PerformLayout();
            this.gboConnectionStatus.ResumeLayout(false);
            this.gboConnectionStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gboConnectionString;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RadioButton rboUsernamePasswordSecurity;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Button btnConnectDisconnect;
        private System.Windows.Forms.RadioButton rboWindowsSecurity;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gboConnectionStatus;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.Button btnDisconnect;
    }
}