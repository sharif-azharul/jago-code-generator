namespace AmarCodeGenerator
{
    partial class FrmClassSP
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
            this.gboCreateStoredProcedure.SuspendLayout();
            this.gboCreateClass.SuspendLayout();
            this.gboDataBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateClassAndSP
            // 
            this.btnCreateClassAndSP.Location = new System.Drawing.Point(899, 306);
            this.btnCreateClassAndSP.Name = "btnCreateClassAndSP";
            this.btnCreateClassAndSP.Size = new System.Drawing.Size(75, 25);
            this.btnCreateClassAndSP.TabIndex = 31;
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
            this.gboCreateStoredProcedure.Location = new System.Drawing.Point(463, 164);
            this.gboCreateStoredProcedure.Name = "gboCreateStoredProcedure";
            this.gboCreateStoredProcedure.Size = new System.Drawing.Size(511, 127);
            this.gboCreateStoredProcedure.TabIndex = 30;
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
            this.gboCreateClass.Location = new System.Drawing.Point(463, 29);
            this.gboCreateClass.Name = "gboCreateClass";
            this.gboCreateClass.Size = new System.Drawing.Size(511, 115);
            this.gboCreateClass.TabIndex = 29;
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
            this.gboDataBase.Location = new System.Drawing.Point(25, 19);
            this.gboDataBase.Name = "gboDataBase";
            this.gboDataBase.Size = new System.Drawing.Size(258, 312);
            this.gboDataBase.TabIndex = 28;
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
            // FrmClassSP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 356);
            this.Controls.Add(this.btnCreateClassAndSP);
            this.Controls.Add(this.gboCreateStoredProcedure);
            this.Controls.Add(this.gboCreateClass);
            this.Controls.Add(this.gboDataBase);
            this.Name = "FrmClassSP";
            this.Text = "FrmClassSP";
            this.gboCreateStoredProcedure.ResumeLayout(false);
            this.gboCreateStoredProcedure.PerformLayout();
            this.gboCreateClass.ResumeLayout(false);
            this.gboCreateClass.PerformLayout();
            this.gboDataBase.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateClassAndSP;
        private System.Windows.Forms.GroupBox gboCreateStoredProcedure;
        private System.Windows.Forms.CheckBox chkSelectbySearchSP;
        private System.Windows.Forms.CheckBox chkSelectAllSP;
        private System.Windows.Forms.CheckBox chkDeleteSP;
        private System.Windows.Forms.CheckBox chkInsertSP;
        private System.Windows.Forms.CheckBox chkSelectbyKeySP;
        private System.Windows.Forms.GroupBox gboCreateClass;
        private System.Windows.Forms.CheckBox chkModel;
        private System.Windows.Forms.CheckBox chkIBLL;
        private System.Windows.Forms.CheckBox chkBusinessLogicLayerClasses;
        private System.Windows.Forms.CheckBox chkDataAccessLayerClasses;
        private System.Windows.Forms.GroupBox gboDataBase;
        private System.Windows.Forms.Button btnCreateClass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboDatabaseNames;
        private System.Windows.Forms.CheckedListBox chkListBoxDataBaseTables;
        private System.Windows.Forms.Label label9;
    }
}