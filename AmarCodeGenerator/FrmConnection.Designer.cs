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
            this.gboConnectionString.Location = new System.Drawing.Point(45, 9);
            this.gboConnectionString.Name = "gboConnectionString";
            this.gboConnectionString.Size = new System.Drawing.Size(315, 312);
            this.gboConnectionString.TabIndex = 23;
            this.gboConnectionString.TabStop = false;
            this.gboConnectionString.Text = "Connection String";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(30, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(231, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "1) Select or enter Server Name/IP Address";
            // 
            // rboUsernamePasswordSecurity
            // 
            this.rboUsernamePasswordSecurity.AutoSize = true;
            this.rboUsernamePasswordSecurity.Checked = true;
            this.rboUsernamePasswordSecurity.Location = new System.Drawing.Point(48, 157);
            this.rboUsernamePasswordSecurity.Name = "rboUsernamePasswordSecurity";
            this.rboUsernamePasswordSecurity.Size = new System.Drawing.Size(213, 17);
            this.rboUsernamePasswordSecurity.TabIndex = 20;
            this.rboUsernamePasswordSecurity.TabStop = true;
            this.rboUsernamePasswordSecurity.Text = "Use a specific user name and password";
            this.rboUsernamePasswordSecurity.UseVisualStyleBackColor = true;
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(66, 58);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(195, 20);
            this.txtServerName.TabIndex = 10;
            this.txtServerName.Text = "AZHAR-PC\\SQLEXPRESS";
            //this.txtServerName.Text = "DESKTOP-BPLNFDB";
            // 
            // btnConnectDisconnect
            // 
            this.btnConnectDisconnect.Location = new System.Drawing.Point(201, 251);
            this.btnConnectDisconnect.Name = "btnConnectDisconnect";
            this.btnConnectDisconnect.Size = new System.Drawing.Size(75, 25);
            this.btnConnectDisconnect.TabIndex = 0;
            this.btnConnectDisconnect.Text = "Connect";
            this.btnConnectDisconnect.UseVisualStyleBackColor = true;
            this.btnConnectDisconnect.Click += new System.EventHandler(this.btnConnectDisconnect_Click);
            // 
            // rboWindowsSecurity
            // 
            this.rboWindowsSecurity.AutoSize = true;
            this.rboWindowsSecurity.Location = new System.Drawing.Point(48, 134);
            this.rboWindowsSecurity.Name = "rboWindowsSecurity";
            this.rboWindowsSecurity.Size = new System.Drawing.Size(198, 17);
            this.rboWindowsSecurity.TabIndex = 19;
            this.rboWindowsSecurity.Text = "Use Windows NT integrated security";
            this.rboWindowsSecurity.UseVisualStyleBackColor = true;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(136, 180);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtUserName.Size = new System.Drawing.Size(140, 20);
            this.txtUserName.TabIndex = 11;
            this.txtUserName.Text = "injurycloud";
            //this.txtUserName.Text = "sa";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(30, 115);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(211, 16);
            this.label10.TabIndex = 18;
            this.label10.Text = "2) Enter Information to log on to server";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(136, 208);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(140, 20);
            this.txtPassword.TabIndex = 12;
            this.txtPassword.Text = "Gr8Pe0ple!*$";
            //this.txtPassword.Text = "sa2016";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(62, 183);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "User Name";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(62, 208);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 15;
            this.label8.Text = "Password";
            // 
            // gboConnectionStatus
            // 
            this.gboConnectionStatus.BackColor = System.Drawing.Color.Transparent;
            this.gboConnectionStatus.Controls.Add(this.label23);
            this.gboConnectionStatus.Controls.Add(this.lblConnectionStatus);
            this.gboConnectionStatus.Controls.Add(this.btnDisconnect);
            this.gboConnectionStatus.Location = new System.Drawing.Point(45, 327);
            this.gboConnectionStatus.Name = "gboConnectionStatus";
            this.gboConnectionStatus.Size = new System.Drawing.Size(315, 53);
            this.gboConnectionStatus.TabIndex = 43;
            this.gboConnectionStatus.TabStop = false;
            this.gboConnectionStatus.Text = "Connection Status";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(11, 25);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(100, 13);
            this.label23.TabIndex = 37;
            this.label23.Text = "Connection Status :";
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.Red;
            this.lblConnectionStatus.Location = new System.Drawing.Point(117, 25);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(79, 13);
            this.lblConnectionStatus.TabIndex = 36;
            this.lblConnectionStatus.Text = "Not Connected";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(201, 17);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 25);
            this.btnDisconnect.TabIndex = 4;
            this.btnDisconnect.Text = "Disconnect";
            // 
            // FrmConnection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 391);
            this.Controls.Add(this.gboConnectionStatus);
            this.Controls.Add(this.gboConnectionString);
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