using System.Drawing;
using System.Windows.Forms;

namespace AmarCodeGenerator
{
    partial class ClassGenerator
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
            this.tabAmarCodeGenerator = new System.Windows.Forms.TabControl();
            this.tbLogin = new System.Windows.Forms.TabPage();
            this.txtLoginPass = new System.Windows.Forms.TextBox();
            this.lblLoginPass = new System.Windows.Forms.Label();
            this.txtUserId = new System.Windows.Forms.TextBox();
            this.lblUserId = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbConnectionString = new System.Windows.Forms.TabPage();
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
            this.tbClassGenerator = new System.Windows.Forms.TabPage();
            this.btnCreateClassAndSP = new System.Windows.Forms.Button();
            this.gboCreateStoredProcedure = new System.Windows.Forms.GroupBox();
            this.chkSelectbySearchSP = new System.Windows.Forms.CheckBox();
            this.chkSelectAllSP = new System.Windows.Forms.CheckBox();
            this.chkDeleteSP = new System.Windows.Forms.CheckBox();
            this.chkInsertSP = new System.Windows.Forms.CheckBox();
            this.chkSelectbyKeySP = new System.Windows.Forms.CheckBox();
            this.gboCreateClass = new System.Windows.Forms.GroupBox();
            this.chkModel = new System.Windows.Forms.CheckBox();
            this.chkIBLL = new System.Windows.Forms.CheckBox();
            this.chkBusinessLogicLayerClasses = new System.Windows.Forms.CheckBox();
            this.chkDataAccessLayerClasses = new System.Windows.Forms.CheckBox();
            this.gboDataBase = new System.Windows.Forms.GroupBox();
            this.btnCreateClass = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cboDatabaseNames = new System.Windows.Forms.ComboBox();
            this.chkListBoxDataBaseTables = new System.Windows.Forms.CheckedListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbModelFirst = new System.Windows.Forms.TabPage();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.gboProperty = new System.Windows.Forms.GroupBox();
            this.chkShowList = new System.Windows.Forms.CheckBox();
            this.chkShowUpdate = new System.Windows.Forms.CheckBox();
            this.chkShowCreate = new System.Windows.Forms.CheckBox();
            this.ddlDDLDataValue = new System.Windows.Forms.ComboBox();
            this.ddlDDLTextValue = new System.Windows.Forms.ComboBox();
            this.lblDDLDataValue = new System.Windows.Forms.Label();
            this.lblDDLTextValue = new System.Windows.Forms.Label();
            this.chkForeignTables = new System.Windows.Forms.CheckedListBox();
            this.txtPigValues = new System.Windows.Forms.TextBox();
            this.ddlOriDBType = new System.Windows.Forms.ComboBox();
            this.ddlDBType = new System.Windows.Forms.ComboBox();
            this.ddlSystemType = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.ddlUIControl = new System.Windows.Forms.ComboBox();
            this.chkIsNull = new System.Windows.Forms.CheckBox();
            this.chkSkip = new System.Windows.Forms.CheckBox();
            this.chkFK = new System.Windows.Forms.CheckBox();
            this.chkIsPK = new System.Windows.Forms.CheckBox();
            this.txtDBLength = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDBName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPropertyName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.gboModel = new System.Windows.Forms.GroupBox();
            this.txtOriginalTableName = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txtControllerName = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.txtAPIControllerName = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.txtCreateViewPageName = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.txtEditViewPageName = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txtDotNetDataContextName = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtIndexViewPageName = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtDotNetIBLLIntName = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtDotNetBLLName = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtTableNameAsTitle = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtDotNetInterfaceName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtDotNetModelName = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtModelDisplayName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grdPropertyList = new System.Windows.Forms.DataGridView();
            this.gboConnectionStatus = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLogout = new System.Windows.Forms.Button();
            this.tabAmarCodeGenerator.SuspendLayout();
            this.tbLogin.SuspendLayout();
            this.tbConnectionString.SuspendLayout();
            this.gboConnectionString.SuspendLayout();
            this.tbClassGenerator.SuspendLayout();
            this.gboCreateStoredProcedure.SuspendLayout();
            this.gboCreateClass.SuspendLayout();
            this.gboDataBase.SuspendLayout();
            this.tbModelFirst.SuspendLayout();
            this.gboProperty.SuspendLayout();
            this.gboModel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPropertyList)).BeginInit();
            this.gboConnectionStatus.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabAmarCodeGenerator
            // 
            this.tabAmarCodeGenerator.Controls.Add(this.tbLogin);
            this.tabAmarCodeGenerator.Controls.Add(this.tbConnectionString);
            this.tabAmarCodeGenerator.Controls.Add(this.tbClassGenerator);
            this.tabAmarCodeGenerator.Controls.Add(this.tbModelFirst);
            this.tabAmarCodeGenerator.Location = new System.Drawing.Point(31, 38);
            this.tabAmarCodeGenerator.Name = "tabAmarCodeGenerator";
            this.tabAmarCodeGenerator.SelectedIndex = 0;
            this.tabAmarCodeGenerator.Size = new System.Drawing.Size(1215, 640);
            this.tabAmarCodeGenerator.TabIndex = 23;
            this.tabAmarCodeGenerator.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabAmarCodeGenerator_Selected);
            // 
            // tbLogin
            // 
            this.tbLogin.Controls.Add(this.txtLoginPass);
            this.tbLogin.Controls.Add(this.lblLoginPass);
            this.tbLogin.Controls.Add(this.txtUserId);
            this.tbLogin.Controls.Add(this.lblUserId);
            this.tbLogin.Controls.Add(this.btnLogin);
            this.tbLogin.Location = new System.Drawing.Point(4, 22);
            this.tbLogin.Name = "tbLogin";
            this.tbLogin.Padding = new System.Windows.Forms.Padding(3);
            this.tbLogin.Size = new System.Drawing.Size(1128, 614);
            this.tbLogin.TabIndex = 2;
            this.tbLogin.Text = "Login";
            this.tbLogin.UseVisualStyleBackColor = true;
            // 
            // txtLoginPass
            // 
            this.txtLoginPass.Location = new System.Drawing.Point(599, 194);
            this.txtLoginPass.Name = "txtLoginPass";
            this.txtLoginPass.Size = new System.Drawing.Size(100, 20);
            this.txtLoginPass.TabIndex = 4;
            this.txtLoginPass.UseSystemPasswordChar = true;
            // 
            // lblLoginPass
            // 
            this.lblLoginPass.AutoSize = true;
            this.lblLoginPass.Location = new System.Drawing.Point(516, 194);
            this.lblLoginPass.Name = "lblLoginPass";
            this.lblLoginPass.Size = new System.Drawing.Size(53, 13);
            this.lblLoginPass.TabIndex = 3;
            this.lblLoginPass.Text = "Password";
            // 
            // txtUserId
            // 
            this.txtUserId.Location = new System.Drawing.Point(599, 149);
            this.txtUserId.Name = "txtUserId";
            this.txtUserId.Size = new System.Drawing.Size(100, 20);
            this.txtUserId.TabIndex = 2;
            // 
            // lblUserId
            // 
            this.lblUserId.AutoSize = true;
            this.lblUserId.Location = new System.Drawing.Point(516, 149);
            this.lblUserId.Name = "lblUserId";
            this.lblUserId.Size = new System.Drawing.Size(47, 13);
            this.lblUserId.TabIndex = 1;
            this.lblUserId.Text = "User Id: ";
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(702, 243);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "Log in";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tbConnectionString
            // 
            this.tbConnectionString.Controls.Add(this.gboConnectionString);
            this.tbConnectionString.Location = new System.Drawing.Point(4, 22);
            this.tbConnectionString.Name = "tbConnectionString";
            this.tbConnectionString.Padding = new System.Windows.Forms.Padding(3);
            this.tbConnectionString.Size = new System.Drawing.Size(1128, 614);
            this.tbConnectionString.TabIndex = 0;
            this.tbConnectionString.Text = "Connection String";
            this.tbConnectionString.UseVisualStyleBackColor = true;
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
            this.gboConnectionString.Location = new System.Drawing.Point(23, 18);
            this.gboConnectionString.Name = "gboConnectionString";
            this.gboConnectionString.Size = new System.Drawing.Size(1026, 312);
            this.gboConnectionString.TabIndex = 22;
            this.gboConnectionString.TabStop = false;
            this.gboConnectionString.Text = "Connection String";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(394, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(231, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "1) Select or enter Server Name/IP Address";
            // 
            // rboUsernamePasswordSecurity
            // 
            this.rboUsernamePasswordSecurity.AutoSize = true;
            this.rboUsernamePasswordSecurity.Checked = true;
            this.rboUsernamePasswordSecurity.Location = new System.Drawing.Point(412, 173);
            this.rboUsernamePasswordSecurity.Name = "rboUsernamePasswordSecurity";
            this.rboUsernamePasswordSecurity.Size = new System.Drawing.Size(213, 17);
            this.rboUsernamePasswordSecurity.TabIndex = 20;
            this.rboUsernamePasswordSecurity.TabStop = true;
            this.rboUsernamePasswordSecurity.Text = "Use a specific user name and password";
            this.rboUsernamePasswordSecurity.UseVisualStyleBackColor = true;
            this.rboUsernamePasswordSecurity.CheckedChanged += new System.EventHandler(this.rboUsernamePasswordSecurity_CheckedChanged);
            // 
            // txtServerName
            // 
            this.txtServerName.Location = new System.Drawing.Point(430, 74);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(195, 20);
            this.txtServerName.TabIndex = 10;
            this.txtServerName.Text = "DESKTOP-STQHPU5";
            // 
            // btnConnectDisconnect
            // 
            this.btnConnectDisconnect.Location = new System.Drawing.Point(585, 266);
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
            this.rboWindowsSecurity.Location = new System.Drawing.Point(412, 150);
            this.rboWindowsSecurity.Name = "rboWindowsSecurity";
            this.rboWindowsSecurity.Size = new System.Drawing.Size(198, 17);
            this.rboWindowsSecurity.TabIndex = 19;
            this.rboWindowsSecurity.Text = "Use Windows NT integrated security";
            this.rboWindowsSecurity.UseVisualStyleBackColor = true;
            this.rboWindowsSecurity.CheckedChanged += new System.EventHandler(this.rboWindowsSecurity_CheckedChanged);
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(500, 196);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtUserName.Size = new System.Drawing.Size(140, 20);
            this.txtUserName.TabIndex = 11;
            this.txtUserName.Text = "sa";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(394, 131);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(211, 16);
            this.label10.TabIndex = 18;
            this.label10.Text = "2) Enter Information to log on to server";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(500, 224);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(140, 20);
            this.txtPassword.TabIndex = 12;
            this.txtPassword.Text = "sa1234";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(426, 199);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "User Name";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(426, 224);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 15;
            this.label8.Text = "Password";
            // 
            // tbClassGenerator
            // 
            this.tbClassGenerator.Controls.Add(this.btnCreateClassAndSP);
            this.tbClassGenerator.Controls.Add(this.gboCreateStoredProcedure);
            this.tbClassGenerator.Controls.Add(this.gboCreateClass);
            this.tbClassGenerator.Controls.Add(this.gboDataBase);
            this.tbClassGenerator.Location = new System.Drawing.Point(4, 22);
            this.tbClassGenerator.Name = "tbClassGenerator";
            this.tbClassGenerator.Padding = new System.Windows.Forms.Padding(3);
            this.tbClassGenerator.Size = new System.Drawing.Size(1128, 614);
            this.tbClassGenerator.TabIndex = 1;
            this.tbClassGenerator.Text = "Create Class And Stored Procedure";
            this.tbClassGenerator.UseVisualStyleBackColor = true;
            // 
            // btnCreateClassAndSP
            // 
            this.btnCreateClassAndSP.Location = new System.Drawing.Point(967, 312);
            this.btnCreateClassAndSP.Name = "btnCreateClassAndSP";
            this.btnCreateClassAndSP.Size = new System.Drawing.Size(75, 25);
            this.btnCreateClassAndSP.TabIndex = 27;
            this.btnCreateClassAndSP.Text = "Create";
            this.btnCreateClassAndSP.UseVisualStyleBackColor = true;
            this.btnCreateClassAndSP.Click += new System.EventHandler(this.btnCreateClassAndSP_Click);
            // 
            // gboCreateStoredProcedure
            // 
            this.gboCreateStoredProcedure.Controls.Add(this.chkSelectbySearchSP);
            this.gboCreateStoredProcedure.Controls.Add(this.chkSelectAllSP);
            this.gboCreateStoredProcedure.Controls.Add(this.chkDeleteSP);
            this.gboCreateStoredProcedure.Controls.Add(this.chkInsertSP);
            this.gboCreateStoredProcedure.Controls.Add(this.chkSelectbyKeySP);
            this.gboCreateStoredProcedure.Location = new System.Drawing.Point(453, 163);
            this.gboCreateStoredProcedure.Name = "gboCreateStoredProcedure";
            this.gboCreateStoredProcedure.Size = new System.Drawing.Size(511, 127);
            this.gboCreateStoredProcedure.TabIndex = 26;
            this.gboCreateStoredProcedure.TabStop = false;
            this.gboCreateStoredProcedure.Text = "Create Stored Procedure";
            // 
            // chkSelectbySearchSP
            // 
            this.chkSelectbySearchSP.AutoSize = true;
            this.chkSelectbySearchSP.Checked = true;
            this.chkSelectbySearchSP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectbySearchSP.Location = new System.Drawing.Point(179, 104);
            this.chkSelectbySearchSP.Name = "chkSelectbySearchSP";
            this.chkSelectbySearchSP.Size = new System.Drawing.Size(107, 17);
            this.chkSelectbySearchSP.TabIndex = 45;
            this.chkSelectbySearchSP.Text = "Select by Search";
            this.chkSelectbySearchSP.UseVisualStyleBackColor = true;
            // 
            // chkSelectAllSP
            // 
            this.chkSelectAllSP.AutoSize = true;
            this.chkSelectAllSP.Checked = true;
            this.chkSelectAllSP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectAllSP.Location = new System.Drawing.Point(346, 104);
            this.chkSelectAllSP.Name = "chkSelectAllSP";
            this.chkSelectAllSP.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAllSP.TabIndex = 44;
            this.chkSelectAllSP.Text = "Select All";
            this.chkSelectAllSP.UseVisualStyleBackColor = true;
            // 
            // chkDeleteSP
            // 
            this.chkDeleteSP.AutoSize = true;
            this.chkDeleteSP.Checked = true;
            this.chkDeleteSP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeleteSP.Location = new System.Drawing.Point(24, 59);
            this.chkDeleteSP.Name = "chkDeleteSP";
            this.chkDeleteSP.Size = new System.Drawing.Size(57, 17);
            this.chkDeleteSP.TabIndex = 43;
            this.chkDeleteSP.Text = "Delete";
            this.chkDeleteSP.UseVisualStyleBackColor = true;
            // 
            // chkInsertSP
            // 
            this.chkInsertSP.AutoSize = true;
            this.chkInsertSP.Checked = true;
            this.chkInsertSP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkInsertSP.Location = new System.Drawing.Point(24, 31);
            this.chkInsertSP.Name = "chkInsertSP";
            this.chkInsertSP.Size = new System.Drawing.Size(92, 17);
            this.chkInsertSP.TabIndex = 42;
            this.chkInsertSP.Text = "Insert/Update";
            this.chkInsertSP.UseVisualStyleBackColor = true;
            // 
            // chkSelectbyKeySP
            // 
            this.chkSelectbyKeySP.AutoSize = true;
            this.chkSelectbyKeySP.Checked = true;
            this.chkSelectbyKeySP.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectbyKeySP.Location = new System.Drawing.Point(24, 104);
            this.chkSelectbyKeySP.Name = "chkSelectbyKeySP";
            this.chkSelectbyKeySP.Size = new System.Drawing.Size(91, 17);
            this.chkSelectbyKeySP.TabIndex = 40;
            this.chkSelectbyKeySP.Text = "Select by Key";
            this.chkSelectbyKeySP.UseVisualStyleBackColor = true;
            // 
            // gboCreateClass
            // 
            this.gboCreateClass.Controls.Add(this.chkModel);
            this.gboCreateClass.Controls.Add(this.chkIBLL);
            this.gboCreateClass.Controls.Add(this.chkBusinessLogicLayerClasses);
            this.gboCreateClass.Controls.Add(this.chkDataAccessLayerClasses);
            this.gboCreateClass.Location = new System.Drawing.Point(453, 28);
            this.gboCreateClass.Name = "gboCreateClass";
            this.gboCreateClass.Size = new System.Drawing.Size(511, 115);
            this.gboCreateClass.TabIndex = 25;
            this.gboCreateClass.TabStop = false;
            this.gboCreateClass.Text = "Create Class";
            // 
            // chkModel
            // 
            this.chkModel.AutoSize = true;
            this.chkModel.Checked = true;
            this.chkModel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkModel.Location = new System.Drawing.Point(24, 43);
            this.chkModel.Name = "chkModel";
            this.chkModel.Size = new System.Drawing.Size(112, 17);
            this.chkModel.TabIndex = 39;
            this.chkModel.Text = "Model Layer Class";
            this.chkModel.UseVisualStyleBackColor = true;
            // 
            // chkIBLL
            // 
            this.chkIBLL.AutoSize = true;
            this.chkIBLL.Checked = true;
            this.chkIBLL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIBLL.Location = new System.Drawing.Point(271, 85);
            this.chkIBLL.Name = "chkIBLL";
            this.chkIBLL.Size = new System.Drawing.Size(142, 17);
            this.chkIBLL.TabIndex = 38;
            this.chkIBLL.Text = "Business Logic Interface";
            this.chkIBLL.UseVisualStyleBackColor = true;
            // 
            // chkBusinessLogicLayerClasses
            // 
            this.chkBusinessLogicLayerClasses.AutoSize = true;
            this.chkBusinessLogicLayerClasses.Checked = true;
            this.chkBusinessLogicLayerClasses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBusinessLogicLayerClasses.Location = new System.Drawing.Point(271, 43);
            this.chkBusinessLogicLayerClasses.Name = "chkBusinessLogicLayerClasses";
            this.chkBusinessLogicLayerClasses.Size = new System.Drawing.Size(154, 17);
            this.chkBusinessLogicLayerClasses.TabIndex = 37;
            this.chkBusinessLogicLayerClasses.Text = "Business Logic Layer Class";
            this.chkBusinessLogicLayerClasses.UseVisualStyleBackColor = true;
            // 
            // chkDataAccessLayerClasses
            // 
            this.chkDataAccessLayerClasses.AutoSize = true;
            this.chkDataAccessLayerClasses.Checked = true;
            this.chkDataAccessLayerClasses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDataAccessLayerClasses.Location = new System.Drawing.Point(24, 85);
            this.chkDataAccessLayerClasses.Name = "chkDataAccessLayerClasses";
            this.chkDataAccessLayerClasses.Size = new System.Drawing.Size(144, 17);
            this.chkDataAccessLayerClasses.TabIndex = 36;
            this.chkDataAccessLayerClasses.Text = "Data Access Layer Class";
            this.chkDataAccessLayerClasses.UseVisualStyleBackColor = true;
            // 
            // gboDataBase
            // 
            this.gboDataBase.Controls.Add(this.btnCreateClass);
            this.gboDataBase.Controls.Add(this.label1);
            this.gboDataBase.Controls.Add(this.cboDatabaseNames);
            this.gboDataBase.Controls.Add(this.chkListBoxDataBaseTables);
            this.gboDataBase.Controls.Add(this.label9);
            this.gboDataBase.Location = new System.Drawing.Point(15, 18);
            this.gboDataBase.Name = "gboDataBase";
            this.gboDataBase.Size = new System.Drawing.Size(258, 312);
            this.gboDataBase.TabIndex = 23;
            this.gboDataBase.TabStop = false;
            this.gboDataBase.Text = "Database";
            // 
            // btnCreateClass
            // 
            this.btnCreateClass.Location = new System.Drawing.Point(182, 350);
            this.btnCreateClass.Name = "btnCreateClass";
            this.btnCreateClass.Size = new System.Drawing.Size(75, 25);
            this.btnCreateClass.TabIndex = 26;
            this.btnCreateClass.Text = "Create";
            this.btnCreateClass.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 16);
            this.label1.TabIndex = 22;
            this.label1.Text = "2) Check tables to create class/stored procedure";
            // 
            // cboDatabaseNames
            // 
            this.cboDatabaseNames.DisplayMember = "DatabaseName";
            this.cboDatabaseNames.FormattingEnabled = true;
            this.cboDatabaseNames.Location = new System.Drawing.Point(24, 53);
            this.cboDatabaseNames.Name = "cboDatabaseNames";
            this.cboDatabaseNames.Size = new System.Drawing.Size(197, 21);
            this.cboDatabaseNames.TabIndex = 21;
            this.cboDatabaseNames.ValueMember = "DatabaseName";
            this.cboDatabaseNames.SelectedIndexChanged += new System.EventHandler(this.cboDatabaseNames_SelectedIndexChanged);
            // 
            // chkListBoxDataBaseTables
            // 
            this.chkListBoxDataBaseTables.FormattingEnabled = true;
            this.chkListBoxDataBaseTables.Location = new System.Drawing.Point(24, 115);
            this.chkListBoxDataBaseTables.Name = "chkListBoxDataBaseTables";
            this.chkListBoxDataBaseTables.Size = new System.Drawing.Size(197, 184);
            this.chkListBoxDataBaseTables.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(12, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(147, 16);
            this.label9.TabIndex = 17;
            this.label9.Text = "1) Select Database on server";
            // 
            // tbModelFirst
            // 
            this.tbModelFirst.Controls.Add(this.btnGenerate);
            this.tbModelFirst.Controls.Add(this.gboProperty);
            this.tbModelFirst.Controls.Add(this.gboModel);
            this.tbModelFirst.Controls.Add(this.grdPropertyList);
            this.tbModelFirst.Location = new System.Drawing.Point(4, 22);
            this.tbModelFirst.Name = "tbModelFirst";
            this.tbModelFirst.Padding = new System.Windows.Forms.Padding(3);
            this.tbModelFirst.Size = new System.Drawing.Size(1207, 614);
            this.tbModelFirst.TabIndex = 3;
            this.tbModelFirst.Text = "Create Class from Model";
            this.tbModelFirst.UseVisualStyleBackColor = true;
            // 
            // btnGenerate
            // 
            this.btnGenerate.BackColor = System.Drawing.Color.YellowGreen;
            this.btnGenerate.ForeColor = System.Drawing.Color.Red;
            this.btnGenerate.Location = new System.Drawing.Point(1094, 555);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 23);
            this.btnGenerate.TabIndex = 3;
            this.btnGenerate.Text = "GENERATE";
            this.btnGenerate.UseVisualStyleBackColor = false;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // gboProperty
            // 
            this.gboProperty.Controls.Add(this.chkShowList);
            this.gboProperty.Controls.Add(this.chkShowUpdate);
            this.gboProperty.Controls.Add(this.chkShowCreate);
            this.gboProperty.Controls.Add(this.ddlDDLDataValue);
            this.gboProperty.Controls.Add(this.ddlDDLTextValue);
            this.gboProperty.Controls.Add(this.lblDDLDataValue);
            this.gboProperty.Controls.Add(this.lblDDLTextValue);
            this.gboProperty.Controls.Add(this.chkForeignTables);
            this.gboProperty.Controls.Add(this.txtPigValues);
            this.gboProperty.Controls.Add(this.ddlOriDBType);
            this.gboProperty.Controls.Add(this.ddlDBType);
            this.gboProperty.Controls.Add(this.ddlSystemType);
            this.gboProperty.Controls.Add(this.label12);
            this.gboProperty.Controls.Add(this.ddlUIControl);
            this.gboProperty.Controls.Add(this.chkIsNull);
            this.gboProperty.Controls.Add(this.chkSkip);
            this.gboProperty.Controls.Add(this.chkFK);
            this.gboProperty.Controls.Add(this.chkIsPK);
            this.gboProperty.Controls.Add(this.txtDBLength);
            this.gboProperty.Controls.Add(this.label13);
            this.gboProperty.Controls.Add(this.label14);
            this.gboProperty.Controls.Add(this.label15);
            this.gboProperty.Controls.Add(this.label11);
            this.gboProperty.Controls.Add(this.txtDisplayName);
            this.gboProperty.Controls.Add(this.label5);
            this.gboProperty.Controls.Add(this.txtDBName);
            this.gboProperty.Controls.Add(this.label4);
            this.gboProperty.Controls.Add(this.txtPropertyName);
            this.gboProperty.Controls.Add(this.btnAdd);
            this.gboProperty.Controls.Add(this.label3);
            this.gboProperty.Location = new System.Drawing.Point(77, 144);
            this.gboProperty.Name = "gboProperty";
            this.gboProperty.Size = new System.Drawing.Size(1077, 239);
            this.gboProperty.TabIndex = 2;
            this.gboProperty.TabStop = false;
            this.gboProperty.Text = "Property Info";
            // 
            // chkShowList
            // 
            this.chkShowList.AutoSize = true;
            this.chkShowList.Checked = true;
            this.chkShowList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowList.Location = new System.Drawing.Point(1301, 47);
            this.chkShowList.Name = "chkShowList";
            this.chkShowList.Size = new System.Drawing.Size(83, 17);
            this.chkShowList.TabIndex = 39;
            this.chkShowList.Text = "Is Show List";
            this.chkShowList.UseVisualStyleBackColor = true;
            // 
            // chkShowUpdate
            // 
            this.chkShowUpdate.AutoSize = true;
            this.chkShowUpdate.Checked = true;
            this.chkShowUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowUpdate.Location = new System.Drawing.Point(1187, 46);
            this.chkShowUpdate.Name = "chkShowUpdate";
            this.chkShowUpdate.Size = new System.Drawing.Size(102, 17);
            this.chkShowUpdate.TabIndex = 38;
            this.chkShowUpdate.Text = "Is Show Update";
            this.chkShowUpdate.UseVisualStyleBackColor = true;
            // 
            // chkShowCreate
            // 
            this.chkShowCreate.AutoSize = true;
            this.chkShowCreate.Checked = true;
            this.chkShowCreate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowCreate.Location = new System.Drawing.Point(1082, 44);
            this.chkShowCreate.Name = "chkShowCreate";
            this.chkShowCreate.Size = new System.Drawing.Size(98, 17);
            this.chkShowCreate.TabIndex = 37;
            this.chkShowCreate.Text = "Is Show Create";
            this.chkShowCreate.UseVisualStyleBackColor = true;
            // 
            // ddlDDLDataValue
            // 
            this.ddlDDLDataValue.FormattingEnabled = true;
            this.ddlDDLDataValue.Location = new System.Drawing.Point(471, 42);
            this.ddlDDLDataValue.Name = "ddlDDLDataValue";
            this.ddlDDLDataValue.Size = new System.Drawing.Size(121, 21);
            this.ddlDDLDataValue.TabIndex = 36;
            this.ddlDDLDataValue.Visible = false;
            // 
            // ddlDDLTextValue
            // 
            this.ddlDDLTextValue.FormattingEnabled = true;
            this.ddlDDLTextValue.Location = new System.Drawing.Point(597, 42);
            this.ddlDDLTextValue.Name = "ddlDDLTextValue";
            this.ddlDDLTextValue.Size = new System.Drawing.Size(121, 21);
            this.ddlDDLTextValue.TabIndex = 35;
            // 
            // lblDDLDataValue
            // 
            this.lblDDLDataValue.AutoSize = true;
            this.lblDDLDataValue.Location = new System.Drawing.Point(471, 25);
            this.lblDDLDataValue.Name = "lblDDLDataValue";
            this.lblDDLDataValue.Size = new System.Drawing.Size(91, 13);
            this.lblDDLDataValue.TabIndex = 34;
            this.lblDDLDataValue.Text = "DDL Data Value :";
            // 
            // lblDDLTextValue
            // 
            this.lblDDLTextValue.AllowDrop = true;
            this.lblDDLTextValue.AutoSize = true;
            this.lblDDLTextValue.Location = new System.Drawing.Point(595, 25);
            this.lblDDLTextValue.Name = "lblDDLTextValue";
            this.lblDDLTextValue.Size = new System.Drawing.Size(83, 13);
            this.lblDDLTextValue.TabIndex = 33;
            this.lblDDLTextValue.Text = "DDL Text Value";
            // 
            // chkForeignTables
            // 
            this.chkForeignTables.FormattingEnabled = true;
            this.chkForeignTables.Location = new System.Drawing.Point(266, 39);
            this.chkForeignTables.Name = "chkForeignTables";
            this.chkForeignTables.Size = new System.Drawing.Size(191, 49);
            this.chkForeignTables.TabIndex = 32;
            this.chkForeignTables.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chkForeignTables_ItemCheck);
            // 
            // txtPigValues
            // 
            this.txtPigValues.Location = new System.Drawing.Point(273, 37);
            this.txtPigValues.Multiline = true;
            this.txtPigValues.Name = "txtPigValues";
            this.txtPigValues.Size = new System.Drawing.Size(192, 79);
            this.txtPigValues.TabIndex = 31;
            // 
            // ddlOriDBType
            // 
            this.ddlOriDBType.FormattingEnabled = true;
            this.ddlOriDBType.Location = new System.Drawing.Point(496, 150);
            this.ddlOriDBType.Name = "ddlOriDBType";
            this.ddlOriDBType.Size = new System.Drawing.Size(121, 21);
            this.ddlOriDBType.TabIndex = 30;
            // 
            // ddlDBType
            // 
            this.ddlDBType.FormattingEnabled = true;
            this.ddlDBType.Location = new System.Drawing.Point(243, 151);
            this.ddlDBType.Name = "ddlDBType";
            this.ddlDBType.Size = new System.Drawing.Size(121, 21);
            this.ddlDBType.TabIndex = 29;
            this.ddlDBType.SelectedValueChanged += new System.EventHandler(this.ddlDBType_SelectedValueChanged);
            // 
            // ddlSystemType
            // 
            this.ddlSystemType.FormattingEnabled = true;
            this.ddlSystemType.Location = new System.Drawing.Point(369, 150);
            this.ddlSystemType.Name = "ddlSystemType";
            this.ddlSystemType.Size = new System.Drawing.Size(121, 21);
            this.ddlSystemType.TabIndex = 28;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(126, 14);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "UI Control Name:";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // ddlUIControl
            // 
            this.ddlUIControl.FormattingEnabled = true;
            this.ddlUIControl.Location = new System.Drawing.Point(129, 37);
            this.ddlUIControl.Name = "ddlUIControl";
            this.ddlUIControl.Size = new System.Drawing.Size(121, 21);
            this.ddlUIControl.TabIndex = 26;
            this.ddlUIControl.SelectedValueChanged += new System.EventHandler(this.ddlUIControl_SelectedValueChanged);
            // 
            // chkIsNull
            // 
            this.chkIsNull.AutoSize = true;
            this.chkIsNull.Location = new System.Drawing.Point(994, 46);
            this.chkIsNull.Name = "chkIsNull";
            this.chkIsNull.Size = new System.Drawing.Size(75, 17);
            this.chkIsNull.TabIndex = 25;
            this.chkIsNull.Text = "Is Nullable";
            this.chkIsNull.UseVisualStyleBackColor = true;
            // 
            // chkSkip
            // 
            this.chkSkip.AutoSize = true;
            this.chkSkip.Location = new System.Drawing.Point(909, 45);
            this.chkSkip.Name = "chkSkip";
            this.chkSkip.Size = new System.Drawing.Size(84, 17);
            this.chkSkip.TabIndex = 24;
            this.chkSkip.Text = "Is Skippable";
            this.chkSkip.UseVisualStyleBackColor = true;
            // 
            // chkFK
            // 
            this.chkFK.AutoSize = true;
            this.chkFK.Location = new System.Drawing.Point(814, 44);
            this.chkFK.Name = "chkFK";
            this.chkFK.Size = new System.Drawing.Size(93, 17);
            this.chkFK.TabIndex = 23;
            this.chkFK.Text = "Is Foreign Key";
            this.chkFK.UseVisualStyleBackColor = true;
            // 
            // chkIsPK
            // 
            this.chkIsPK.AutoSize = true;
            this.chkIsPK.Location = new System.Drawing.Point(737, 42);
            this.chkIsPK.Name = "chkIsPK";
            this.chkIsPK.Size = new System.Drawing.Size(71, 17);
            this.chkIsPK.TabIndex = 22;
            this.chkIsPK.Text = "Is Primary";
            this.chkIsPK.UseVisualStyleBackColor = true;
            // 
            // txtDBLength
            // 
            this.txtDBLength.Location = new System.Drawing.Point(621, 152);
            this.txtDBLength.Name = "txtDBLength";
            this.txtDBLength.Size = new System.Drawing.Size(100, 20);
            this.txtDBLength.TabIndex = 15;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(618, 136);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(61, 13);
            this.label13.TabIndex = 14;
            this.label13.Text = "DB Length:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(494, 136);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 13);
            this.label14.TabIndex = 12;
            this.label14.Text = "Ori DB Type:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(238, 136);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 13);
            this.label15.TabIndex = 10;
            this.label15.Text = "DB Type:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(374, 134);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 13);
            this.label11.TabIndex = 8;
            this.label11.Text = "System Type:";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(15, 39);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(100, 20);
            this.txtDisplayName.TabIndex = 7;
            this.txtDisplayName.Leave += new System.EventHandler(this.txtDisplayName_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Display Name:";
            // 
            // txtDBName
            // 
            this.txtDBName.Location = new System.Drawing.Point(18, 151);
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.Size = new System.Drawing.Size(100, 20);
            this.txtDBName.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "DB Name:";
            // 
            // txtPropertyName
            // 
            this.txtPropertyName.Location = new System.Drawing.Point(137, 151);
            this.txtPropertyName.Name = "txtPropertyName";
            this.txtPropertyName.Size = new System.Drawing.Size(100, 20);
            this.txtPropertyName.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(1315, 195);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add to Grid";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(134, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "System Name";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // gboModel
            // 
            this.gboModel.Controls.Add(this.txtOriginalTableName);
            this.gboModel.Controls.Add(this.label39);
            this.gboModel.Controls.Add(this.txtControllerName);
            this.gboModel.Controls.Add(this.label24);
            this.gboModel.Controls.Add(this.txtAPIControllerName);
            this.gboModel.Controls.Add(this.label25);
            this.gboModel.Controls.Add(this.txtCreateViewPageName);
            this.gboModel.Controls.Add(this.label26);
            this.gboModel.Controls.Add(this.txtEditViewPageName);
            this.gboModel.Controls.Add(this.label27);
            this.gboModel.Controls.Add(this.txtDotNetDataContextName);
            this.gboModel.Controls.Add(this.label19);
            this.gboModel.Controls.Add(this.txtIndexViewPageName);
            this.gboModel.Controls.Add(this.label20);
            this.gboModel.Controls.Add(this.txtDotNetIBLLIntName);
            this.gboModel.Controls.Add(this.label21);
            this.gboModel.Controls.Add(this.txtDotNetBLLName);
            this.gboModel.Controls.Add(this.label22);
            this.gboModel.Controls.Add(this.txtTableNameAsTitle);
            this.gboModel.Controls.Add(this.label18);
            this.gboModel.Controls.Add(this.txtDotNetInterfaceName);
            this.gboModel.Controls.Add(this.label17);
            this.gboModel.Controls.Add(this.txtDotNetModelName);
            this.gboModel.Controls.Add(this.label16);
            this.gboModel.Controls.Add(this.txtModelDisplayName);
            this.gboModel.Controls.Add(this.label2);
            this.gboModel.Location = new System.Drawing.Point(27, 17);
            this.gboModel.Name = "gboModel";
            this.gboModel.Size = new System.Drawing.Size(1140, 121);
            this.gboModel.TabIndex = 1;
            this.gboModel.TabStop = false;
            this.gboModel.Text = "Model Info";
            // 
            // txtOriginalTableName
            // 
            this.txtOriginalTableName.Location = new System.Drawing.Point(15, 80);
            this.txtOriginalTableName.Name = "txtOriginalTableName";
            this.txtOriginalTableName.Size = new System.Drawing.Size(100, 20);
            this.txtOriginalTableName.TabIndex = 25;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(12, 64);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(97, 13);
            this.label39.TabIndex = 24;
            this.label39.Text = "OriginalTableName";
            // 
            // txtControllerName
            // 
            this.txtControllerName.Location = new System.Drawing.Point(903, 89);
            this.txtControllerName.Name = "txtControllerName";
            this.txtControllerName.Size = new System.Drawing.Size(100, 20);
            this.txtControllerName.TabIndex = 23;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(900, 73);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(79, 13);
            this.label24.TabIndex = 22;
            this.label24.Text = "ControllerName";
            // 
            // txtAPIControllerName
            // 
            this.txtAPIControllerName.Location = new System.Drawing.Point(1197, 41);
            this.txtAPIControllerName.Name = "txtAPIControllerName";
            this.txtAPIControllerName.Size = new System.Drawing.Size(100, 20);
            this.txtAPIControllerName.TabIndex = 21;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(1194, 25);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(96, 13);
            this.label25.TabIndex = 20;
            this.label25.Text = "APIControllerName";
            // 
            // txtCreateViewPageName
            // 
            this.txtCreateViewPageName.Location = new System.Drawing.Point(985, 41);
            this.txtCreateViewPageName.Name = "txtCreateViewPageName";
            this.txtCreateViewPageName.Size = new System.Drawing.Size(100, 20);
            this.txtCreateViewPageName.TabIndex = 19;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(982, 25);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(114, 13);
            this.label26.TabIndex = 18;
            this.label26.Text = "CreateViewPageName";
            // 
            // txtEditViewPageName
            // 
            this.txtEditViewPageName.Location = new System.Drawing.Point(876, 41);
            this.txtEditViewPageName.Name = "txtEditViewPageName";
            this.txtEditViewPageName.Size = new System.Drawing.Size(100, 20);
            this.txtEditViewPageName.TabIndex = 17;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(873, 25);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(101, 13);
            this.label27.TabIndex = 16;
            this.label27.Text = "EditViewPageName";
            // 
            // txtDotNetDataContextName
            // 
            this.txtDotNetDataContextName.Location = new System.Drawing.Point(660, 41);
            this.txtDotNetDataContextName.Name = "txtDotNetDataContextName";
            this.txtDotNetDataContextName.Size = new System.Drawing.Size(100, 20);
            this.txtDotNetDataContextName.TabIndex = 15;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(657, 25);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(128, 13);
            this.label19.TabIndex = 14;
            this.label19.Text = "DotNetDataContextName";
            // 
            // txtIndexViewPageName
            // 
            this.txtIndexViewPageName.Location = new System.Drawing.Point(766, 41);
            this.txtIndexViewPageName.Name = "txtIndexViewPageName";
            this.txtIndexViewPageName.Size = new System.Drawing.Size(100, 20);
            this.txtIndexViewPageName.TabIndex = 13;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(787, 25);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(109, 13);
            this.label20.TabIndex = 12;
            this.label20.Text = "IndexViewPageName";
            // 
            // txtDotNetIBLLIntName
            // 
            this.txtDotNetIBLLIntName.Location = new System.Drawing.Point(554, 41);
            this.txtDotNetIBLLIntName.Name = "txtDotNetIBLLIntName";
            this.txtDotNetIBLLIntName.Size = new System.Drawing.Size(100, 20);
            this.txtDotNetIBLLIntName.TabIndex = 11;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(551, 25);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(103, 13);
            this.label21.TabIndex = 10;
            this.label21.Text = "DotNetIBLLIntName";
            // 
            // txtDotNetBLLName
            // 
            this.txtDotNetBLLName.Location = new System.Drawing.Point(445, 41);
            this.txtDotNetBLLName.Name = "txtDotNetBLLName";
            this.txtDotNetBLLName.Size = new System.Drawing.Size(100, 20);
            this.txtDotNetBLLName.TabIndex = 9;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(442, 25);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 13);
            this.label22.TabIndex = 8;
            this.label22.Text = "DotNetBLLName";
            // 
            // txtTableNameAsTitle
            // 
            this.txtTableNameAsTitle.Location = new System.Drawing.Point(230, 41);
            this.txtTableNameAsTitle.Name = "txtTableNameAsTitle";
            this.txtTableNameAsTitle.Size = new System.Drawing.Size(100, 20);
            this.txtTableNameAsTitle.TabIndex = 7;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(227, 25);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(94, 13);
            this.label18.TabIndex = 6;
            this.label18.Text = "TableNameAsTitle";
            // 
            // txtDotNetInterfaceName
            // 
            this.txtDotNetInterfaceName.Location = new System.Drawing.Point(336, 41);
            this.txtDotNetInterfaceName.Name = "txtDotNetInterfaceName";
            this.txtDotNetInterfaceName.Size = new System.Drawing.Size(100, 20);
            this.txtDotNetInterfaceName.TabIndex = 5;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(333, 25);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(111, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "DotNetInterfaceName";
            // 
            // txtDotNetModelName
            // 
            this.txtDotNetModelName.Location = new System.Drawing.Point(124, 41);
            this.txtDotNetModelName.Name = "txtDotNetModelName";
            this.txtDotNetModelName.Size = new System.Drawing.Size(100, 20);
            this.txtDotNetModelName.TabIndex = 3;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(121, 25);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(98, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "DotNetModelName";
            // 
            // txtModelDisplayName
            // 
            this.txtModelDisplayName.Location = new System.Drawing.Point(15, 41);
            this.txtModelDisplayName.Name = "txtModelDisplayName";
            this.txtModelDisplayName.Size = new System.Drawing.Size(100, 20);
            this.txtModelDisplayName.TabIndex = 1;
            this.txtModelDisplayName.MouseLeave += new System.EventHandler(this.txtModelDisplayName_MouseLeave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Model Display Name:";
            // 
            // grdPropertyList
            // 
            this.grdPropertyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdPropertyList.Location = new System.Drawing.Point(77, 412);
            this.grdPropertyList.Name = "grdPropertyList";
            this.grdPropertyList.Size = new System.Drawing.Size(1413, 136);
            this.grdPropertyList.TabIndex = 0;
            // 
            // gboConnectionStatus
            // 
            this.gboConnectionStatus.BackColor = System.Drawing.Color.Transparent;
            this.gboConnectionStatus.Controls.Add(this.label23);
            this.gboConnectionStatus.Controls.Add(this.lblConnectionStatus);
            this.gboConnectionStatus.Controls.Add(this.btnDisconnect);
            this.gboConnectionStatus.Location = new System.Drawing.Point(31, 658);
            this.gboConnectionStatus.Name = "gboConnectionStatus";
            this.gboConnectionStatus.Size = new System.Drawing.Size(477, 53);
            this.gboConnectionStatus.TabIndex = 42;
            this.gboConnectionStatus.TabStop = false;
            this.gboConnectionStatus.Text = "Connection Status";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(28, 25);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(100, 13);
            this.label23.TabIndex = 37;
            this.label23.Text = "Connection Status :";
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.ForeColor = System.Drawing.Color.Red;
            this.lblConnectionStatus.Location = new System.Drawing.Point(134, 25);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(79, 13);
            this.lblConnectionStatus.TabIndex = 36;
            this.lblConnectionStatus.Text = "Not Connected";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false;
            this.btnDisconnect.Location = new System.Drawing.Point(380, 19);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 25);
            this.btnDisconnect.TabIndex = 4;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1269, 24);
            this.menuStrip1.TabIndex = 43;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.homeToolStripMenuItem.Text = "Home";
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Red;
            this.btnLogout.ForeColor = System.Drawing.Color.Chartreuse;
            this.btnLogout.Location = new System.Drawing.Point(1171, 27);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 44;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // ClassGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1269, 728);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.gboConnectionStatus);
            this.Controls.Add(this.tabAmarCodeGenerator);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "ClassGenerator";
            this.Text = "Create Class and Stored Procedure script for DB table";
            this.Load += new System.EventHandler(this.ClassGenerator_Load);
            this.tabAmarCodeGenerator.ResumeLayout(false);
            this.tbLogin.ResumeLayout(false);
            this.tbLogin.PerformLayout();
            this.tbConnectionString.ResumeLayout(false);
            this.gboConnectionString.ResumeLayout(false);
            this.gboConnectionString.PerformLayout();
            this.tbClassGenerator.ResumeLayout(false);
            this.gboCreateStoredProcedure.ResumeLayout(false);
            this.gboCreateStoredProcedure.PerformLayout();
            this.gboCreateClass.ResumeLayout(false);
            this.gboCreateClass.PerformLayout();
            this.gboDataBase.ResumeLayout(false);
            this.tbModelFirst.ResumeLayout(false);
            this.gboProperty.ResumeLayout(false);
            this.gboProperty.PerformLayout();
            this.gboModel.ResumeLayout(false);
            this.gboModel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPropertyList)).EndInit();
            this.gboConnectionStatus.ResumeLayout(false);
            this.gboConnectionStatus.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabAmarCodeGenerator;
        private System.Windows.Forms.TabPage tbConnectionString;
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
        private System.Windows.Forms.TabPage tbClassGenerator;
        private System.Windows.Forms.GroupBox gboDataBase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDatabaseNames;
        private System.Windows.Forms.CheckedListBox chkListBoxDataBaseTables;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox gboConnectionStatus;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnCreateClass;
        private System.Windows.Forms.GroupBox gboCreateStoredProcedure;
        private System.Windows.Forms.GroupBox gboCreateClass;
        private System.Windows.Forms.Button btnCreateClassAndSP;
        private System.Windows.Forms.CheckBox chkDeleteSP;
        private System.Windows.Forms.CheckBox chkInsertSP;
        private System.Windows.Forms.CheckBox chkSelectbyKeySP;
        private System.Windows.Forms.CheckBox chkBusinessLogicLayerClasses;
        private System.Windows.Forms.CheckBox chkDataAccessLayerClasses;
        private System.Windows.Forms.CheckBox chkSelectbySearchSP;
        private System.Windows.Forms.CheckBox chkSelectAllSP;
        private System.Windows.Forms.CheckBox chkModel;
        private System.Windows.Forms.CheckBox chkIBLL;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.TabPage tbLogin;
        private System.Windows.Forms.TabPage tbModelFirst;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.TextBox txtLoginPass;
        private System.Windows.Forms.Label lblLoginPass;
        private System.Windows.Forms.TextBox txtUserId;
        private System.Windows.Forms.Label lblUserId;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.GroupBox gboProperty;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.GroupBox gboModel;
        private System.Windows.Forms.TextBox txtModelDisplayName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView grdPropertyList;
        private System.Windows.Forms.TextBox txtPropertyName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDBLength;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDBName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chkIsNull;
        private System.Windows.Forms.CheckBox chkSkip;
        private System.Windows.Forms.CheckBox chkFK;
        private System.Windows.Forms.CheckBox chkIsPK;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox ddlUIControl;
        private System.Windows.Forms.ComboBox ddlOriDBType;
        private System.Windows.Forms.ComboBox ddlDBType;
        private System.Windows.Forms.ComboBox ddlSystemType;
        private System.Windows.Forms.TextBox txtPigValues;
        private System.Windows.Forms.CheckedListBox chkForeignTables;
        private System.Windows.Forms.ComboBox ddlDDLDataValue;
        private System.Windows.Forms.ComboBox ddlDDLTextValue;
        protected internal System.Windows.Forms.Label lblDDLDataValue;
        private System.Windows.Forms.Label lblDDLTextValue;
        private System.Windows.Forms.TextBox txtOriginalTableName;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox txtControllerName;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtAPIControllerName;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox txtCreateViewPageName;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtEditViewPageName;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox txtDotNetDataContextName;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtIndexViewPageName;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtDotNetIBLLIntName;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtDotNetBLLName;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtTableNameAsTitle;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtDotNetInterfaceName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtDotNetModelName;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.CheckBox chkShowList;
        private System.Windows.Forms.CheckBox chkShowUpdate;
        private System.Windows.Forms.CheckBox chkShowCreate;
    }
}

