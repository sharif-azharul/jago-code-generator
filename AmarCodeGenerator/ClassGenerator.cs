
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Linq;

namespace AmarCodeGenerator
{
    public partial class ClassGenerator : Form
    {

        #region Constructor
        public ClassGenerator()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Variables
        private static string SQL_CONN_STRING = string.Empty;
        private static SqlConnection connection;
        private static string strTable = string.Empty;
        #endregion

        //public  string ColumnsToSkip = ConfiguratonManager.;
        public string ColumnsToSkip = string.Empty;
        public string DeleteColumn = string.Empty;
        public string DeleteSupportingColumns = string.Empty;
        public string InsertSupportingColumns = string.Empty;
        public string UpdateSupportingColumns = string.Empty;
        public string RootFolderName = string.Empty;
        public string SPFolderName = string.Empty;
        public string ModelInterfaceFolder = string.Empty;
        public string ModelFolder = string.Empty;
        public string BLLFolder = string.Empty;
        public string IBLLFolder = string.Empty;
        public string DataContextFolder = string.Empty;
        public string ViewsFolder = string.Empty;
        public string ControllerFolder = string.Empty;
        public Boolean IsModelInterfaceRequired = false;
        public Boolean IsBLLInterfaceRequired = false;

        public string NameSpaceFirstPart = string.Empty;
        public string ModuleName = string.Empty;

        public string DBConnectionString = string.Empty;

        public string ParentChildTables = string.Empty;
        public string ParentTables = string.Empty;
        public string ChildTables = string.Empty;


        #region Common Methods

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

                SQL_CONN_STRING = sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SQL_CONN_STRING;
        }
        private string CreateConnectionStringWithDatabase()
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
                    sb.AppendFormat("Data Source=" + strDataSource + ";Persist Security Info=True;User ID=" + strUserName + ";Password=" + strPassword + ";Initial Catalog=" + this.cboDatabaseNames.SelectedValue);
                }
                else if (this.rboWindowsSecurity.Checked)
                {
                    //......................... windows authentication security mode..........................
                    sb.AppendFormat("Data Source=" + strDataSource + ";Integrated Security=SSPI;Persist Security Info=False" + ";Initial Catalog=" + this.cboDatabaseNames.SelectedValue);
                }

                SQL_CONN_STRING = sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return SQL_CONN_STRING;
        }

        //........... Connection with database .................
        private bool ConnectToDatabase(string SQL_CONN_STRING)
        {
            bool isConnected = false;
            try
            {
                connection = new SqlConnection(SQL_CONN_STRING);
                connection.Open();

                if (connection != null)
                {
                    isConnected = true;
                }

            }
            catch
            {
                isConnected = false;
                MessageBox.Show("Login failed");
            }

            return isConnected;
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
                    this.tabAmarCodeGenerator.TabPages.Add(this.tbConnectionString);
                    this.tabAmarCodeGenerator.TabPages.Remove(this.tbClassGenerator);
                }
                else
                {
                    //.................... False .................................
                    this.gboConnectionStatus.Enabled = !status;
                    this.btnDisconnect.Enabled = !status;
                    this.tabAmarCodeGenerator.TabPages.Remove(this.tbConnectionString);
                    this.tabAmarCodeGenerator.TabPages.Add(this.tbClassGenerator);
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

        //............ Get Database , Tables and Attributes Names ................
        private void GetAllDatabaseNames()
        {
            try
            {
                //Get all database names from selected computer
                string strSQLquery = "select name as DatabaseName from master.dbo.sysdatabases";

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(strSQLquery, SQL_CONN_STRING))
                {
                    // create the DataSet 
                    DataSet dataSet = new DataSet();
                    // fill the DataSet using our DataAdapter 
                    dataAdapter.Fill(dataSet);

                    if (dataSet.Tables.Count >= 1)
                    {
                        if (dataSet.Tables[0].Rows.Count >= 1)
                        {
                            this.cboDatabaseNames.DataSource = dataSet.Tables[0];
                        }
                    }
                }

                this.cboDatabaseNames.SelectedIndex = 0;

                //Get all table names from selected database
                if (ConnectToDatabase(CreateConnectionStringWithDatabase()))
                {
                    GetAllTableNames();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetAllTableNames()
        {
            try
            {
                string strSQLquery = "select table_name from Information_Schema.Tables where Table_Type='Base Table' order by table_name";
                Database db = new SqlDatabase(SQL_CONN_STRING);
                DbCommand cmd = db.GetSqlStringCommand(strSQLquery);
                using (DbConnection cn = db.CreateConnection())
                {
                    cn.Open();

                    DataSet ds = db.ExecuteDataSet(cmd);
                    if (ds.Tables.Count >= 1)
                    {
                        //Fill check list box with database tables
                        if (ds.Tables[0].Rows.Count >= 1)
                        {
                            chkListBoxDataBaseTables.DataSource = ds.Tables[0];
                            chkListBoxDataBaseTables.DisplayMember = "table_name";
                            chkListBoxDataBaseTables.ValueMember = "table_name";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private TableModel GetTableAttributes(string strTableName)
        {
            //SqlDataReader dr = null;
            TableModel objTableModel = new TableModel();
            objTableModel.PropetyList = new List<ColumnModel>();
            objTableModel.OriginalTableName = strTableName;
            objTableModel.TableNameAsTitle = strTableName.Substring(2, strTableName.Length - 2);//GenerateModelName(strTableName);

            objTableModel.DisplayName = AddSpacesBeforeUppercaseLetter(objTableModel.TableNameAsTitle);
            objTableModel.ControllerName = objTableModel.TableNameAsTitle + "Controller";
            objTableModel.IndexViewPageName = "Index";
            objTableModel.EditViewPageName = "Edit";
            objTableModel.CreateViewPageName = "Create";

            objTableModel.DotNetModelName = objTableModel.TableNameAsTitle + "Model";
            objTableModel.DotNetBLLName = objTableModel.TableNameAsTitle + "BLL";
            objTableModel.DotNetDataContextName = objTableModel.TableNameAsTitle + "DC";
            objTableModel.DotNetIBLLIntName = "I" + objTableModel.TableNameAsTitle + "BLL";
            objTableModel.DotNetInterfaceName = (IsModelInterfaceRequired ? "" : "I") + objTableModel.TableNameAsTitle;
            List<ColumnModel> objColumnList = new List<ColumnModel>();
            try
            {
                //Get table attributes
                //string strSQLquery = "SELECT table_name,column_name,data_type FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '" + strTableName + " ' ";

                StringBuilder sbSQL = new StringBuilder();

                sbSQL.AppendLine("   SELECT ");
                sbSQL.AppendLine("  c.name 'Column_Name', ");
                sbSQL.AppendLine("  t.Name 'Data_Type', ");
                sbSQL.AppendLine("  c.max_length 'Max_Length', ");
                sbSQL.AppendLine("  c.precision , ");
                sbSQL.AppendLine("  c.scale , ");
                sbSQL.AppendLine("  c.is_nullable, ");
                sbSQL.AppendLine("  ISNULL(i.is_primary_key, 0) 'is_primary_key' ");
                sbSQL.AppendLine(" FROM     ");
                sbSQL.AppendLine("     sys.columns c ");
                sbSQL.AppendLine(" INNER JOIN  ");
                sbSQL.AppendLine("     sys.types t ON c.user_type_id = t.user_type_id ");
                sbSQL.AppendLine(" LEFT OUTER JOIN  ");
                sbSQL.AppendLine("     sys.index_columns ic ON ic.object_id = c.object_id AND ic.column_id = c.column_id ");
                sbSQL.AppendLine(" LEFT OUTER JOIN  ");
                sbSQL.AppendLine("     sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id ");
                sbSQL.AppendLine(" WHERE ");
                sbSQL.AppendLine("     c.object_id = OBJECT_ID('" + strTableName + "') ");

                Database db = new SqlDatabase(SQL_CONN_STRING);
                DbCommand cmd = db.GetSqlStringCommand(sbSQL.ToString());
                using (DbConnection cn = db.CreateConnection())
                {
                    cn.Open();

                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {


                        //return dr;
                        while (dr.Read())
                        {
                            ColumnModel objColumn = new ColumnModel();
                            objColumn = BuildModel(objColumn, dr);
                            objColumnList.Add(objColumn);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            objTableModel.PropetyList.AddRange(objColumnList);
            if (objColumnList.FindAll(c => c.IsPrimayKey == true).Count > 1)
            {
                objTableModel.HasCompositeKey = true;
            }
            else
                objTableModel.HasCompositeKey = false;
            return objTableModel;
        }

        private string GenerateModelName(string strTableName)
        {
            return TitleCaseString(strTableName);
        }

        private ColumnModel BuildModel(ColumnModel objColumnModel, IDataReader dr)
        {
            DataTable dt = dr.GetSchemaTable();
            foreach (DataRow dRow in dt.Rows)
            {
                string col = dRow.ItemArray[0].ToString();
                switch (col)
                {

                    case "Column_Name":
                        objColumnModel.DBName = dr["Column_Name"].Equals(DBNull.Value) ? "" : dr["Column_Name"].ToString();
                        objColumnModel.SYSName = dr["Column_Name"].Equals(DBNull.Value) ? "" : dr["Column_Name"].ToString();
                        objColumnModel.IsSkippable = IsSkippable(objColumnModel.DBName);
                        break;
                    case "Data_Type":
                        {
                            objColumnModel.OriginalDBType = dr["Data_Type"].Equals(DBNull.Value) ? "" : dr["Data_Type"].ToString();
                            objColumnModel.DBType = SqlDatabseType(dr["Data_Type"].Equals(DBNull.Value) ? "" : dr["Data_Type"].ToString());
                            objColumnModel.SYSType = GetSystemType(dr["Data_Type"].Equals(DBNull.Value) ? "" : dr["Data_Type"].ToString());
                        }
                        break;
                    case "Max_Length":
                        {
                            objColumnModel.DBLength = dr["Max_Length"].Equals(DBNull.Value) ? "" : dr["Max_Length"].ToString();
                            if (objColumnModel.DBType.ToLower() == "nchar" || objColumnModel.DBType.ToLower() == "nvarchar" || objColumnModel.DBType.ToLower() == "ntext")
                                objColumnModel.DBLength = ((Convert.ToInt32(objColumnModel.DBLength)) / 2).ToString();
                        }
                        break;
                    case "is_primary_key":
                        objColumnModel.IsPrimayKey = dr["is_primary_key"].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr["is_primary_key"].ToString());
                        break;
                    case "is_nullable":
                        {
                            objColumnModel.IsNullable = dr["is_nullable"].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr["is_nullable"].ToString());
                            if ((!objColumnModel.IsNullable && objColumnModel.SYSType == "DateTime?") || objColumnModel.IsSkippable)
                            {
                                objColumnModel.SYSType = objColumnModel.SYSType.Replace('?', ' ');
                            }
                        }
                        break;
                    default:
                        break;

                }

            }
            //if (objColumnModel.IsNullable)
            //{
            //    objColumnModel.SYSType += "?";
            //}
            objColumnModel.DisplayName = AddSpacesBeforeUppercaseLetter(objColumnModel.SYSName);
            return objColumnModel;
        }

        private string AddSpacesBeforeUppercaseLetter(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }

        private bool IsSkippable(string p)
        {
            Boolean IsSkip = false;
            if (ColumnsToSkip.ToUpper().Contains(p.ToUpper()))
                IsSkip = true;
            return IsSkip;
        }
        //Get system datatype from sql datatype
        public string GetSystemType(string tstrSqlType)
        {
            string _Type = string.Empty;
            try
            {

                #region Sql to dot net Conversion
                switch (tstrSqlType)
                {
                    case "bigint":
                        {
                            _Type = "Int64?";
                        }
                        break;
                    case "smallint":
                        {
                            _Type = "Int16?";
                        }
                        break;
                    case "tinyint":
                        {
                            _Type = "Int16?";
                        }
                        break;
                    case "int":
                        {
                            _Type = "Int32?";
                        }
                        break;
                    case "bit":
                        {
                            _Type = "Boolean";
                        }
                        break;
                    case "decimal":
                    case "numeric":
                        {
                            _Type = "decimal?";
                        }
                        break;
                    case "money":
                    case "smallmoney":
                        {
                            _Type = "decimal?";
                        }
                        break;
                    case "float":
                    case "real":
                        {
                            _Type = "float?";
                        }
                        break;
                    case "timestamp":
                    case "date":
                    case "datetime":
                    case "smalldatetime":
                    case "time":

                        {
                            _Type = "DateTime?";
                        }
                        break;

                    case "sql_variant":
                        {
                            _Type = "object";
                        }
                        break;
                    case "char":
                    case "varchar":
                    case "text":
                    case "nchar":
                    case "nvarchar":
                    case "ntext":
                        {
                            _Type = "String";
                        }
                        break;
                    case "binary":
                    case "varbinary":
                    case "image":

                        {
                            _Type = "byte[]";
                        }
                        break;
                    //case "image":
                    //    {
                    //        _Type = "System.Drawing.Image";
                    //    }
                    //    break;
                    case "uniqueidentifier":
                        {
                            _Type = "Guid?";
                        }
                        break;
                    default:
                        {
                            _Type = "unknown";
                        }
                        break;
                }

                #endregion

            }
            catch (Exception ex)
            {
                _Type = null;
                throw ex;

            }
            return _Type;
        }

        //Sql databse type conversion
        private string SqlDatabseType(string tstrSqlType)
        {
            string _Type = string.Empty;
            try
            {

                #region Sql Conversion
                switch (tstrSqlType)
                {
                    case "uniqueidentifier":
                        {
                            _Type = "Guid";
                        }
                        break;
                    case "bigint":
                        {
                            _Type = "BigInt";
                        }
                        break;
                    case "smallint":
                        {
                            _Type = "SmallInt";
                        }
                        break;
                    case "tinyint":
                        {
                            _Type = "TinyInt";
                        }
                        break;
                    case "int":
                        {
                            _Type = "Int";
                        }
                        break;
                    case "bit":
                        {
                            _Type = "Bit";
                        }
                        break;
                    case "decimal":
                    case "numeric":
                        {
                            _Type = "Decimal";
                        }
                        break;
                    case "money":
                        {
                            _Type = "Money";
                        }
                        break;
                    case "smallmoney":
                        {
                            _Type = "SmallMoney";
                        }
                        break;
                    case "float":
                    case "real":
                        {
                            _Type = "Float";
                        }
                        break;
                    case "datetime":
                    case "time":
                        {
                            _Type = "System.DateTime.DateTime";
                        }
                        break;
                    case "smalldatetime":
                        {
                            _Type = "System.DateTime.SmallDateTime";
                        }
                        break;
                    case "char":
                        {
                            _Type = "char";
                        }
                        break;
                    case "sql_variant":
                        {
                            _Type = "object";
                        }
                        break;
                    case "nvarchar":
                        {
                            _Type = "NVarChar";
                        }
                        break;
                    case "varchar":
                        {
                            _Type = "VarChar";
                        }
                        break;
                    case "text":
                        {
                            _Type = "Text";
                        }
                        break;
                    case "nchar":
                        {
                            _Type = "NChar";
                        }
                        break;
                    case "binary":
                        {
                            _Type = "Binary";
                        }
                        break;
                    case "varbinary":
                        {
                            _Type = "byte[]";
                        }
                        break;
                    case "image":
                        {
                            _Type = "Byte";
                        }
                        break;
                    //case "image":
                    //    {
                    //        _Type = "System.Drawing.Image";
                    //    }
                    //    break;
                    default:
                        {
                            _Type = "unknown";
                        }
                        break;
                }

                #endregion

            }
            catch (Exception ex)
            {
                _Type = null;
                throw ex;

            }
            return _Type;
        }

        private void LogError(Exception ex)
        {

            string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
            string sPathName = @"ErrorLog\";
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            string sErrorTime = sYear + sMonth + sDay;
            if (!Directory.Exists(sPathName))
            {
                Directory.CreateDirectory(sPathName);
            }
            StreamWriter sw = new StreamWriter(sPathName + "ErrorLog" + sErrorTime + ".txt", true);
            sw.WriteLine(sLogFormat + ex.Message);
            sw.Flush();
            sw.Close();
            MessageBox.Show(ex.Message);
        }

        #endregion

        #region Common Events

        private void btnCreateClassAndSP_Click(object sender, EventArgs e)
        {
            try
            {
                bool isCreated = false;

                if ((this.chkDataAccessLayerClasses.Checked) || (this.chkBusinessLogicLayerClasses.Checked) || (this.chkSelectbyKeySP.Checked) || (this.chkDeleteSP.Checked) || (this.chkInsertSP.Checked))
                {
                    InitializeConfiguration();
                    if (this.chkDataAccessLayerClasses.Checked)
                    {
                        var objTableModelList = DefineParentChildTables();

                        #region Create DataAccessLayer Class

                        //foreach (int indexChecked in chkListBoxDataBaseTables.CheckedIndices)
                        //{

                        //    strTable = chkListBoxDataBaseTables.GetItemText(chkListBoxDataBaseTables.Items[indexChecked]);
                        //    TableModel objTableModel = new TableModel();
                        //    objTableModel = GetTableAttributes(strTable);
                        //    // Generating C# Classes Model, DC, Interface
                        //    GenerateModelClass(objTableModel);
                        //    if (IsModelInterfaceRequired)
                        //    {
                        //        GenerateInterfaceClass(objTableModel);
                        //    }
                        //    GenerateBLLClass(objTableModel);
                        //    if (IsBLLInterfaceRequired)
                        //    {
                        //        GenerateINTERFACEBLLClass(objTableModel);
                        //    }
                        //    GenerateDataContextClass(objTableModel);

                        //    //  Generating SP------------
                        //    GenerateStoredProcedures(objTableModel);

                        //    GenerateAllViewPages(objTableModel);

                        //    GenerateController(objTableModel);

                        //}

                        foreach (var pTable in objTableModelList)
                        {
                            GenerateModelClass(pTable);
                            GenerateBLLClass(pTable);
                            GenerateDataContextClass(pTable);
                            GenerateStoredProcedures(pTable);
                            GenerateController(pTable);
                            GenerateAllViewPages(pTable);

                        }

                        #endregion

                        isCreated = true;
                    }
                    //if (this.chkBusinessLogicLayerClasses.Checked)
                    //{
                    //    #region Create BusinessLogicLayer Class

                    //    foreach (int indexChecked in chkListBoxDataBaseTables.CheckedIndices)
                    //    {
                    //        strTable = chkListBoxDataBaseTables.GetItemText(chkListBoxDataBaseTables.Items[indexChecked]);
                    //        GenerateClass(GetTableAttributes(strTable), "BusinessLayer", "cls" + strTable + "Controller");
                    //        //GenerateClass(GetTableAttributes(strTable));
                    //    }

                    //    #endregion

                    //    isCreated = true;
                    //}
                    //if ((this.chkSelectSP.Checked) || (this.chkDeleteSP.Checked) || (this.chkInsertSP.Checked) || (this.chkUpdateSP.Checked))
                    //{
                    //    #region Create Stored Procedure

                    //    foreach (int indexChecked in chkListBoxDataBaseTables.CheckedIndices)
                    //    {
                    //        strTable = chkListBoxDataBaseTables.GetItemText(chkListBoxDataBaseTables.Items[indexChecked]);
                    //        //GenerateStoredProcedures(GetTableAttributes(strTable));
                    //    }

                    //    #endregion

                    //    isCreated = true;
                    //}
                    if (isCreated)
                    {
                        MessageBox.Show("Classes/StoredProcedures have been created successfully");
                    }
                }
                else
                {
                    MessageBox.Show("Please Check and then press Create");
                }

            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private List<TableModel> DefineParentChildTables()
        {

            List<TableModel> objTableList = new List<TableModel>();
            List<TableModel> objHeirarchyTableList = new List<TableModel>();
            foreach (int indexChecked in chkListBoxDataBaseTables.CheckedIndices)
            {
                //DefineParentChildTables();

                strTable = chkListBoxDataBaseTables.GetItemText(chkListBoxDataBaseTables.Items[indexChecked]);
                TableModel objTableModel = new TableModel();
                objTableModel = GetTableAttributes(strTable);
                objTableList.Add(objTableModel);
            }

            foreach (var Table in objTableList)
            {
                if (!string.IsNullOrEmpty(ParentChildTables))
                {
                    if (!ParentChildTables.ToUpper().Contains(Table.OriginalTableName.ToUpper()))
                    {
                        objHeirarchyTableList.Add(Table);
                    }
                    else
                    {
                        string[] PCArray = ParentChildTables.Split(';');
                        if (PCArray.Length > 0)
                        {
                            foreach (var PC in PCArray)
                            {
                                string[] PcChArray = PC.Split(':');
                                if (PcChArray.Length > 1)
                                {
                                    //-----------------
                                    if (objHeirarchyTableList.FindIndex(al => al.OriginalTableName == PcChArray[0]) < 0)
                                    {
                                        var ParentTable = objTableList.FirstOrDefault(td => td.OriginalTableName == PcChArray[0]);
                                        if (ParentTable != null)
                                        {
                                            ParentTable.IsParentTable = true;
                                            List<TableModel> ChildTableList = new List<TableModel>();

                                            string[] ChildTableArray = PcChArray[1].Split(',');
                                            if (ChildTableArray.Length > 0)
                                            {
                                                foreach (var ChildTa in ChildTableArray)
                                                {
                                                    var ChildTable = objTableList.FirstOrDefault(td => td.OriginalTableName == ChildTa);
                                                    if (ChildTable != null)
                                                    {
                                                        ChildTable.IsChildTable = true;
                                                        ChildTableList.Add(ChildTable);
                                                    }
                                                }
                                                ParentTable.ChildTableModelList = new List<TableModel>();
                                                ParentTable.ChildTableModelList.AddRange(ChildTableList);
                                            }
                                            objHeirarchyTableList.Add(ParentTable);
                                        }
                                        //----------------
                                    }
                                }
                            }
                        }
                    }

                }
            }
            return objHeirarchyTableList;
        }

        private void GenerateController(TableModel pTableModel)
        {
            try
            {
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(ControllerFolder + pTableModel.ControllerName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion



                    #region Write Namespaces
                    this.WriteControllerNamespaces(sw, pTableModel);
                    #endregion

                    #region Write Class Default Constructor
                    //this.WriteDefaultConstructor(sw, pTableModel.ControllerName);
                    #endregion




                    sb = new System.Text.StringBuilder("\t");
                    //===================================================
                    string dq = @"""";
                    sb.AppendLine("\t\t#region PROPERTIES");
                    sb.AppendLine(@"        //-----------------PROPERTIES-------------------------");
                    sb.AppendLine();
                    sb.AppendLine(@"        " + pTableModel.DotNetBLLName + " obj" + pTableModel.DotNetBLLName + "= new " + pTableModel.DotNetBLLName + "();");
                    sb.AppendLine();
                    sb.AppendLine("\t\t#endregion ");
                    sb.AppendLine();

                    sb.AppendLine("\t\t#region =================== CRUD Actions =========================");
                    sb.AppendLine();

                    sb.AppendLine(@"        // GET:/" + pTableModel.TableNameAsTitle + "/");
                    sb.AppendLine(@"        [HttpGet]");
                    sb.AppendLine(@"        public ActionResult GetAll()");
                    sb.AppendLine(@"            {");
                    sb.AppendLine(@"                IList<" + pTableModel.DotNetModelName + "> obj" + pTableModel.DotNetModelName + "List = obj" + pTableModel.DotNetBLLName + ".Get" + pTableModel.TableNameAsTitle + "ALL();");
                    sb.AppendLine(@"                //return new JsonResult { Data = list , JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ");
                    sb.AppendLine(@"                return View(obj" + pTableModel.DotNetModelName + "List); ");
                    sb.AppendLine(@"             } ");
                    sb.AppendLine(@" ");
                    sb.AppendLine();

                    sb.AppendLine(@"        [HttpGet] ");
                    sb.AppendLine(@"        public ActionResult Index(string filter = null, int page = 1, ");
                    sb.AppendLine(@"         int pageSize = 10, string sort = " + dq + pTableModel.PropetyList.FirstOrDefault(pr => pr.IsPrimayKey).SYSName + dq + @", string sortdir = ""DESC"") ");
                    sb.AppendLine(@"        { ");
                    sb.AppendLine(@"            IList<" + pTableModel.DotNetModelName + "> obj" + pTableModel.DotNetModelName + "List = null; ");
                    sb.AppendLine(@"            ViewBag.filter = filter; ");
                    sb.AppendLine(@"            if (string.IsNullOrEmpty(filter)) ");
                    sb.AppendLine(@"            { ");
                    sb.AppendLine(@"                obj" + pTableModel.DotNetModelName + "List = obj" + pTableModel.DotNetBLLName + ".Get" + pTableModel.TableNameAsTitle + "ALL(); ");
                    sb.AppendLine(@"            } ");
                    sb.AppendLine(@"            else ");
                    sb.AppendLine(@"            { ");
                    sb.AppendLine(@"                obj" + pTableModel.DotNetModelName + "List = obj" + pTableModel.DotNetBLLName + ".Get" + pTableModel.TableNameAsTitle + "BySearch(new " + pTableModel.DotNetModelName + "() {  }); ");
                    sb.AppendLine(@"            } ");
                    sb.AppendLine(@"");
                    sb.AppendLine(@"            //Order BY LOGIC GOES Here------------------------");
                    sb.AppendLine();

                    //sb.Append(@"            if (sortdir == ""DESC"")                                                                    ");
                    //sb.Append(@"            {                                                                                         ");
                    //sb.Append(@"                switch (sort.ToLower())                                                               ");
                    //sb.Append(@"                {                                                                                     ");
                    //sb.Append(@"                    case "notifyevent":                                                               ");
                    //sb.Append(@"                        list = list.OrderByDescending(f => f.NotifyEvent).ToList();                   ");
                    //sb.Append(@"                        break;                                                                        ");
                    //sb.Append(@"                    case "notifyeventsid":                                                            ");
                    //sb.Append(@"                        list = list.OrderByDescending(f => f.NotifyEventsId).ToList();                ");
                    //sb.Append(@"                        break;                                                                        ");
                    //sb.Append(@"                    case "templatename":                                                              ");
                    //sb.Append(@"                        list = list.OrderByDescending(f => f.TemplateName).ToList();                  ");
                    //sb.Append(@"                        break;                                                                        ");
                    //sb.Append(@"                    default:                                                                          ");
                    //sb.Append(@"                        break;                                                                        ");
                    //sb.Append(@"                }                                                                                     ");
                    //sb.Append(@"            }                                                                                         ");
                    //sb.Append(@"            else                                                                                      ");
                    //sb.Append(@"            {                                                                                         ");
                    //sb.Append(@"                switch (sort.ToLower())                                                               ");
                    //sb.Append(@"                {                                                                                     ");
                    //sb.Append(@"                    case "notifyevent":                                                               ");
                    //sb.Append(@"                        list = list.OrderBy(f => f.NotifyEvent).ToList();                             ");
                    //sb.Append(@"                        break;                                                                        ");
                    //sb.Append(@"                    case "notifyeventsid":                                                            ");
                    //sb.Append(@"                        list = list.OrderBy(f => f.NotifyEventsId).ToList();                          ");
                    //sb.Append(@"                        break;                                                                        ");
                    //sb.Append(@"                    case "templatename":                                                              ");
                    //sb.Append(@"                        list = list.OrderByDescending(f => f.TemplateName).ToList();                  ");
                    //sb.Append(@"                        break;                                                                        ");

                    //sb.Append(@"                    default:                                                                          ");
                    //sb.Append(@"                        break;                                                                        ");

                    //sb.Append(@"                }                                                                                     ");
                    //sb.Append(@"            }                                                                                         ");

                    sb.AppendLine(@"            var records = new PagedList<" + pTableModel.DotNetModelName + ">();                                    ");
                    sb.AppendLine(@"            /*records.Content = obj" + pTableModel.DotNetModelName + "List.Skip((page - 1) * pageSize)                                      ");
                    sb.AppendLine(@"                    .Take(pageSize)                                                                   ");
                    sb.AppendLine(@"                    .ToList(); ;*/                                                                    ");
                    sb.AppendLine(@"            records.Content = obj" + pTableModel.DotNetModelName + "List.ToList();                                                          ");
                    sb.AppendLine(@"            records.TotalRecords = obj" + pTableModel.DotNetModelName + "List.Count();                                                      ");
                    sb.AppendLine(@"            records.PageSize = pageSize;                                                              ");
                    sb.AppendLine(@"            records.CurrentPage = page;                                                               ");
                    sb.AppendLine(@"            return View(records);                                                                     ");
                    sb.AppendLine(@"        }                                                                                             ");
                    sb.AppendLine(@"                                                                                                      ");
                    sb.AppendLine();

                    sb.AppendLine(@"        [HttpGet]                                                                                     ");
                    sb.AppendLine(@"        [ActionName(""Create"")]                                                                        ");
                    sb.AppendLine(@"        public ActionResult Save" + pTableModel.TableNameAsTitle + "()                              ");
                    sb.AppendLine(@"        {                                                                                             ");
                    sb.AppendLine(@"                var obj" + pTableModel.DotNetModelName + " = new " + pTableModel.DotNetModelName + "();");
                    sb.AppendLine(@"                return View(obj" + pTableModel.DotNetModelName + ");                                                                       ");
                    sb.AppendLine(@"        }                                                                                             ");
                    sb.AppendLine();

                    sb.AppendLine(@"                                                                                                      ");
                    sb.AppendLine(@"        [HttpPost]                                                                                    ");
                    sb.AppendLine(@"        [ActionName(""Create"")]                                                                        ");
                    sb.AppendLine(@"        public ActionResult Save" + pTableModel.TableNameAsTitle + "(" + pTableModel.DotNetModelName + " p" + pTableModel.DotNetModelName + ")           ");
                    sb.AppendLine(@"        {                                                                                             ");
                    sb.AppendLine(@"            string msg = string.Empty;                                                                ");
                    sb.AppendLine(@"            if (ModelState.IsValid)                                                                   ");
                    sb.AppendLine(@"            {                                                                                         ");
                    sb.AppendLine(@"                p" + pTableModel.DotNetModelName + ".IsNew = true;                                                       ");
                    sb.AppendLine(@"                base.SetObjectStatus(p" + pTableModel.DotNetModelName + ");                                              ");
                    sb.AppendLine(@"                msg = obj" + pTableModel.DotNetBLLName + ".Save" + pTableModel.TableNameAsTitle + "(p" + pTableModel.DotNetModelName + ");                  ");
                    sb.AppendLine(@"                                                                                                      ");
                    sb.AppendLine(@"            }                                                                                         ");
                    sb.AppendLine(@"            else                                                                                      ");
                    sb.AppendLine(@"            {                                                                                         ");
                    sb.AppendLine(@"                msg = ""Please provide required fields value."";                                        ");
                    sb.AppendLine(@"            }                                                                                         ");
                    sb.AppendLine(@"            if (Request.IsAjaxRequest())                                                              ");
                    sb.AppendLine(@"            {                                                                                         ");
                    sb.AppendLine(@"                return new JsonResult { Data = msg, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ");
                    sb.AppendLine(@"            }                                                                                             ");
                    sb.AppendLine(@"            else                                                                                          ");
                    sb.AppendLine(@"            {                                                                                             ");
                    sb.AppendLine(@"                ViewBag.Message = msg;                                                                    ");
                    sb.AppendLine(@"                return View(p" + pTableModel.DotNetModelName + ");                                                           ");
                    sb.AppendLine(@"            }                                                                                             ");
                    sb.AppendLine(@"                                                                                                          ");
                    sb.AppendLine(@"        }                                                                                                 ");
                    sb.AppendLine();

                    string ActionSignatureForEdit = string.Empty;
                    string SignatureWithoutType = string.Empty;
                    string StringEmptyChecke = string.Empty;

                    var CompositePropsForEdit = pTableModel.PropetyList.FindAll(p => p.IsPrimayKey);
                    if (CompositePropsForEdit != null && CompositePropsForEdit.Count == 1)
                    {
                        ActionSignatureForEdit = CompositePropsForEdit[0].SYSType + " p" + CompositePropsForEdit[0].SYSName;
                        SignatureWithoutType = "\tp" + CompositePropsForEdit[0].SYSName;
                        if (CompositePropsForEdit[0].SYSType == "string")
                            StringEmptyChecke = "!String.IsNullOrEmpty(p" + CompositePropsForEdit[0].SYSName + ")";
                        else StringEmptyChecke = "";

                    }
                    else if (CompositePropsForEdit != null && CompositePropsForEdit.Count > 1)
                    {
                        foreach (var prop in CompositePropsForEdit)
                        {
                            ActionSignatureForEdit = ActionSignatureForEdit + prop.SYSType + " p" + prop.SYSName + ",";
                            SignatureWithoutType = SignatureWithoutType + "p" + prop.SYSName + ",";
                            if (prop.SYSType == "string")
                                StringEmptyChecke = StringEmptyChecke + "!String.IsNullOrEmpty(" + prop.SYSName + ") && ";

                        }
                        ActionSignatureForEdit = ActionSignatureForEdit.Substring(0, ActionSignatureForEdit.Length - 1);
                        SignatureWithoutType = SignatureWithoutType.Substring(0, SignatureWithoutType.Length - 1);
                        if (!string.IsNullOrEmpty(StringEmptyChecke) && StringEmptyChecke.Length > 3)
                            StringEmptyChecke = StringEmptyChecke.Substring(0, StringEmptyChecke.Length - 3);
                    }
                    else { }

                    sb.AppendLine(@"                                                                                                          ");
                    sb.AppendLine(@"        [HttpGet]                                                                                         ");
                    sb.AppendLine(@"        [ActionName(""Edit"")]                                                                              ");
                    sb.AppendLine(@"        public ActionResult Edit" + pTableModel.TableNameAsTitle + "(" + ActionSignatureForEdit + ")                                  ");
                    sb.AppendLine(@"        {                                                                                                 ");
                    if (!string.IsNullOrEmpty(StringEmptyChecke))
                    {
                        sb.AppendLine(@"            if (" + StringEmptyChecke + ")                                                    ");
                        sb.AppendLine(@"            {                                                                                             ");
                    }
                    sb.AppendLine(@"                var obj" + pTableModel.DotNetModelName + " = obj" + pTableModel.DotNetBLLName + ".Get" + pTableModel.TableNameAsTitle + "ById(" + SignatureWithoutType + ");                    ");
                    sb.AppendLine(@"                return View(obj" + pTableModel.DotNetModelName + ");                                                                           ");
                    if (!string.IsNullOrEmpty(StringEmptyChecke))
                    {
                        sb.AppendLine(@"            }                                                                                             ");
                    }
                    sb.AppendLine(@"            return View(new " + pTableModel.DotNetModelName + "());                                                                                ");
                    sb.AppendLine(@"        }                                                                                                 ");
                    sb.AppendLine(@"                                                                                                          ");
                    sb.AppendLine();

                    sb.AppendLine(@"        [HttpPost]                                                                                        ");
                    sb.AppendLine(@"        [ActionName(""Edit"")]                                                                              ");
                    sb.AppendLine(@"        public ActionResult Edit" + pTableModel.TableNameAsTitle + "(" + pTableModel.DotNetModelName + " p" + pTableModel.TableNameAsTitle + ")               ");
                    sb.AppendLine(@"        {                                                                                                 ");
                    sb.AppendLine(@"            string msg = string.Empty;                                                                    ");
                    sb.AppendLine(@"            if (ModelState.IsValid)                                                                       ");
                    sb.AppendLine(@"            {                                                                                             ");
                    sb.AppendLine(@"                p" + pTableModel.TableNameAsTitle + ".IsNew = true;                                                           ");
                    sb.AppendLine(@"                base.SetObjectStatus(p" + pTableModel.TableNameAsTitle + ");                                                  ");
                    sb.AppendLine(@"                msg = obj" + pTableModel.DotNetBLLName + ".Save" + pTableModel.TableNameAsTitle + "(p" + pTableModel.TableNameAsTitle + ");                      ");
                    sb.AppendLine(@"                                                                                                          ");
                    sb.AppendLine(@"            }                                                                                             ");
                    sb.AppendLine(@"            else                                                                                          ");
                    sb.AppendLine(@"            {                                                                                             ");
                    sb.AppendLine(@"                msg = ""Please provide required fields value."";                                            ");
                    sb.AppendLine(@"            }                                                                                             ");
                    sb.AppendLine(@"            if (Request.IsAjaxRequest())                                                                  ");
                    sb.AppendLine(@"            {                                                                                             ");
                    sb.AppendLine(@"                return new JsonResult { Data = msg, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ");
                    sb.AppendLine(@"            }                                                                                             ");
                    sb.AppendLine(@"            else                                                                                          ");
                    sb.AppendLine(@"            {                                                                                             ");
                    sb.AppendLine(@"                ViewBag.Message = msg;                                                                    ");
                    sb.AppendLine(@"                return View(p" + pTableModel.TableNameAsTitle + ");                                                           ");
                    sb.AppendLine(@"            }                                                                                             ");
                    sb.AppendLine(@"                                                                                                          ");
                    sb.AppendLine(@"        }                                                                                                 ");
                    sb.AppendLine();

                    sb.AppendLine(@"                                                                                                          ");
                    sb.AppendLine(@"        [HttpGet]                                                                                         ");
                    sb.AppendLine(@"        public ActionResult Details(string p" + ActionSignatureForEdit + ")                                                ");
                    sb.AppendLine(@"        {                                                                                                 ");
                    sb.AppendLine(@"            var obj" + pTableModel.DotNetModelName + " =  obj" + pTableModel.DotNetBLLName + ".Get" + pTableModel.TableNameAsTitle + "ById(" + SignatureWithoutType + ");                        ");
                    sb.AppendLine(@"            return View(obj" + pTableModel.DotNetModelName + ");                                                                               ");
                    sb.AppendLine(@"        }                                                                                                 ");
                    sb.AppendLine(@"                                                                                                          ");
                    sb.AppendLine();


                    string DCMethodSignature = string.Empty;
                    string ModelPropeties = string.Empty;
                    var CompositeProps = pTableModel.PropetyList.FindAll(p => p.IsPrimayKey);
                    if (CompositeProps != null && CompositeProps.Count == 1)
                    {
                        DCMethodSignature = CompositeProps[0].SYSType + " p" + CompositeProps[0].SYSName;
                        ModelPropeties = "\r\n\t\t\tobj" + pTableModel.DotNetModelName + "." + CompositeProps[0].SYSName + " = p" + CompositeProps[0].SYSName + ";";
                    }
                    else if (CompositeProps != null && CompositeProps.Count > 1)
                    {
                        foreach (var prop in CompositeProps)
                        {
                            DCMethodSignature = DCMethodSignature + prop.SYSType + " p" + prop.SYSName + ", ";
                            ModelPropeties = ModelPropeties + "\r\n\t\t\tobj" + pTableModel.DotNetModelName + "." + prop.SYSName + " = p" + prop.SYSName + ";";
                        }
                        //DCMethodSignature = DCMethodSignature.Substring(0, DCMethodSignature.Length - 2);
                    }
                    else { }



                    sb.AppendLine(@"        [HttpPost]                                                                                        ");
                    sb.AppendLine(@"        [ActionName(""Delete"")]                                                                            ");
                    sb.AppendLine(@"        public ActionResult Delete" + pTableModel.TableNameAsTitle + "(" + DCMethodSignature + ")                                ");
                    sb.AppendLine(@"        {                                                                                                 ");
                    sb.AppendLine(@"            string msg = string.Empty;                                                                              ");
                    sb.AppendLine("             " + pTableModel.DotNetModelName + " obj" + pTableModel.DotNetModelName + " = new " + pTableModel.DotNetModelName + "();");
                    //sb.AppendLine(@"            var obj" + pTableModel.DotNetModelName + " = obj" + pTableModel.DotNetBLLName + ".Get" + pTableModel.TableNameAsTitle + "ById(p" + pTableModel.PropetyList.FirstOrDefault(pr => pr.IsPrimayKey).SYSName + ");                        ");
                    sb.AppendLine(ModelPropeties);
                    sb.AppendLine(@"            obj" + pTableModel.DotNetModelName + ".IsNew = false;                                                                              ");
                    sb.AppendLine(@"            base.SetObjectStatus(obj" + pTableModel.DotNetModelName + ");                                                                      ");
                    sb.AppendLine(@"            msg = obj" + pTableModel.DotNetBLLName + ".Delete" + pTableModel.TableNameAsTitle + "(obj" + pTableModel.DotNetModelName + ");                                        ");
                    sb.AppendLine(@"            if (Request.IsAjaxRequest())                                                                  ");
                    sb.AppendLine(@"            {                                                                                             ");
                    sb.AppendLine(@"                return new JsonResult { Data = msg, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ");
                    sb.AppendLine(@"            }                                                                                             ");
                    sb.AppendLine(@"            else                                                                                          ");
                    sb.AppendLine(@"            {                                                                                             ");
                    sb.AppendLine(@"                ViewBag.Message = msg;                                                                    ");
                    sb.AppendLine(@"                return View();                                                                            ");
                    sb.AppendLine(@"            }                                                                                             ");
                    sb.AppendLine(@"        }                                                                                                 ");
                    sb.AppendLine();

                    sb.AppendLine("\r\n\t\t#endregion ");

                    sb.AppendLine(@"        }                                                                                                     ");

                    sb.AppendLine(@"                                                                                                              ");
                    sb.AppendLine(@"    } ");
                    //===================================================
                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InitializeConfiguration()
        {
            NameSpaceFirstPart = ConfigurationManager.AppSettings["NAMESPACEFIRSTPART"].ToString();
            ModuleName = ConfigurationManager.AppSettings["MODULENAME"].ToString();
            DBConnectionString = ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            ParentChildTables = ConfigurationManager.AppSettings["ParentChildTables"].ToString();
            IsModelInterfaceRequired = Convert.ToBoolean(ConfigurationManager.AppSettings["ISMODELINTERFACEREQUIRED"].ToString());
            IsBLLInterfaceRequired = Convert.ToBoolean(ConfigurationManager.AppSettings["ISBLLINTERFACEREQUIRED"].ToString());
            ColumnsToSkip = ConfigurationManager.AppSettings["COLUMNSTOSKIP"].ToString();
            DeleteColumn = ConfigurationManager.AppSettings["DELETECOLUMN"].ToString();
            DeleteSupportingColumns = ConfigurationManager.AppSettings["DELETESUPPORTINGCOLUMNS"].ToString();
            InsertSupportingColumns = ConfigurationManager.AppSettings["INSERTSUPPORTINGCOLUMNS"].ToString();
            UpdateSupportingColumns = ConfigurationManager.AppSettings["UPDATESUPPORTINGCOLUMNS"].ToString();
            RootFolderName = ConfigurationManager.AppSettings["ROOTFOLDERNAME"].ToString();

            SPFolderName = RootFolderName + ConfigurationManager.AppSettings["SPFOLDERNAME"].ToString() + @"\";
            CreateDirectory(SPFolderName);
            ModelInterfaceFolder = RootFolderName + ConfigurationManager.AppSettings["IMODEL"].ToString() + @"\";
            CreateDirectory(ModelInterfaceFolder);

            ModelFolder = RootFolderName + ConfigurationManager.AppSettings["MODEL"].ToString() + @"\";
            CreateDirectory(ModelFolder);

            BLLFolder = RootFolderName + ConfigurationManager.AppSettings["BLL"].ToString() + @"\";
            CreateDirectory(BLLFolder);

            IBLLFolder = RootFolderName + ConfigurationManager.AppSettings["IBLL"].ToString() + @"\";
            CreateDirectory(IBLLFolder);

            DataContextFolder = RootFolderName + ConfigurationManager.AppSettings["DATACONTEXT"].ToString() + @"\";
            CreateDirectory(DataContextFolder);

            ViewsFolder = RootFolderName + "Views" + @"\";
            CreateDirectory(ViewsFolder);

            ControllerFolder = RootFolderName + "Controller" + @"\";
            CreateDirectory(ControllerFolder);

        }

        private void CreateDirectory(string pDirectory)
        {
            if (!Directory.Exists(pDirectory))
                Directory.CreateDirectory(pDirectory);
        }
        private void btnConnectDisconnect_Click(object sender, EventArgs e)
        {
            try
            {

                if ((this.rboWindowsSecurity.Checked) && (this.txtServerName.Text != ""))
                {
                    this.lblConnectionStatus.Text = "Connected to " + this.txtServerName.Text; ;

                    #region Windows Security
                    if (ConnectToDatabase(CreateConnectionStringWithoutDatabase()))
                    {
                        Reset(false);
                        GetAllDatabaseNames();
                    }

                    #endregion
                }
                else if ((this.rboUsernamePasswordSecurity.Checked) && (this.txtServerName.Text != ""))
                {
                    this.lblConnectionStatus.Text = "Connected to " + this.txtServerName.Text; ;

                    #region User Name Password security
                    if ((this.txtUserName.Text != "") && (this.txtPassword.Text != ""))
                    {
                        if (ConnectToDatabase(CreateConnectionStringWithoutDatabase()))
                        {
                            Reset(false);
                            GetAllDatabaseNames();
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
                LogError(ex);
            }
        }
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
                Reset(true);
                if (connection != null)
                {
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
        private void chkCreateStoredProcedure_CheckedChanged(object sender, EventArgs e)
        {

        }
        #endregion

        #region Region Generate Class

        //.............................................. User Define Methods ..................................................


        //Generate Classes automatically for selected tables ............................

        private void GenerateClass(TableModel pTableModel, string NamespaceName, string ClassName)
        {
            try
            {
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;


                    string lstrTableName = strTable;  //table name

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(TitleCaseString(strTable) + "BLL");
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion

                    #region Get Table Name, Attributes Name and Attribute Types

                    #endregion

                    #region Write Namespaces
                    this.WriteNamespaces(sw, lstrTableName, NamespaceName, ClassName);
                    #endregion

                    #region Write Class Default Constructor
                    this.WriteDefaultConstructor(sw, ClassName);
                    #endregion

                    #region Write Private Variables
                    sb = new System.Text.StringBuilder("\r\n\t");
                    //sb.Append("#region Private Variables");
                    ////sb.Append("\r\n\tprivate int result;");
                    //for (int j = 0; j < AttributeNameArrayList.Count; j++)
                    //{
                    //    strAttributeName = AttributeNameArrayList[j].ToString();
                    //    strAttributeType_DotNet = AttributeTypeArrayList_DotNet[j].ToString();
                    //    this.WritePrivateVariables(sb, strAttributeType_DotNet, strAttributeName);
                    //}
                    //sb.Append("\r\n\t cls" + strTable + "  objcls" + strTable + ";");
                    //sb.Append("\r\n\t#endregion");
                    sw.WriteLine(sb.ToString());
                    #endregion

                    #region Write Public Properties
                    sb = new System.Text.StringBuilder("\r\n\t");
                    sb.Append("#region Public Properties");
                    foreach (var Column in pTableModel.PropetyList)
                    {
                        //strAttributeName = AttributeNameArrayList[j].ToString();
                        //strAttributeType_DotNet = AttributeTypeArrayList_DotNet[j].ToString();
                        this.WritePublicProperties(sb, Column.SYSType, Column.SYSName);
                    }
                    sb.Append("\r\n\t#endregion");
                    sw.WriteLine(sb.ToString());
                    #endregion

                    if (NamespaceName == "BusinessLayer")
                    {
                        //#region Write Public Methods for BLL
                        //sb = new System.Text.StringBuilder("\r\n\t");
                        //sb.Append("#region Public Methods");


                        ////...................................... Select Method ..........................................
                        //WriteSelectMethod_forBLL(sb, AttributeNameArrayList);

                        ////...................................... Insert Method ..........................................
                        //WriteInsertMethod_forBLL(sb, AttributeNameArrayList);

                        ////...................................... Update Method ..........................................
                        //WriteUpdateMethod_forBLL(sb, AttributeNameArrayList);

                        ////...................................... Delete Method ..........................................
                        //WriteDeleteMethod_forBLL(sb, AttributeNameArrayList);



                        //sb.Append("\r\n\t#endregion");
                        //sw.WriteLine(sb.ToString());

                        //#endregion
                    }
                    else
                        if (NamespaceName == "DatabaseLayer")
                    {
                        //#region Write Public Methods for DAL
                        //sb = new System.Text.StringBuilder("\r\n\t");
                        //sb.Append("#region Public Methods");


                        ////...................................... Select Method ..........................................
                        //strAttributeName = AttributeNameArrayList[0].ToString();
                        //strAttributeType_Sql = AttributeTypeArrayList_Sql[0].ToString();
                        //WriteSelectMethod_forDAL(sb, AttributeNameArrayList, AttributeTypeArrayList_Sql2);
                        //WriteSelectMethod_GK_forDAL(sb, AttributeNameArrayList, AttributeTypeArrayList_Sql2);
                        //WriteSelectMethod_GA_forDAL(sb, AttributeNameArrayList, AttributeTypeArrayList_Sql2);

                        ////...................................... Insert or Update Method ..........................................
                        //WriteInsertUpdateMethod_forDAL(sb, AttributeNameArrayList, AttributeTypeArrayList_Sql2);

                        ////...................................... Insert Method ..........................................
                        //WriteInsertMethod_forDAL(sb, AttributeNameArrayList);

                        ////...................................... Update Method ..........................................
                        //WriteUpdateMethod_forDAL(sb, AttributeNameArrayList);

                        ////...................................... Delete Method ..........................................
                        //strAttributeName = AttributeNameArrayList[0].ToString();
                        //strAttributeType_Sql = AttributeTypeArrayList_Sql[0].ToString();
                        //WriteDeleteMethod_forDAL(sb, strAttributeType_Sql, strAttributeName);



                        //sb.Append("\r\n\t#endregion");
                        //sb.Append("\r\n\t#region Private Helper Methods");
                        //WriteBuildModelMethod_forDAL(sb, AttributeNameArrayList, AttributeTypeArrayList_Sql2);
                        //sb.Append("\r\n\t#endregion");
                        //sw.WriteLine(sb.ToString());


                        //#endregion
                    }

                    #region Close file
                    if (sw != null)
                    {
                        sw.WriteLine("\r\n\t}\r\n}");
                        //dr.Close();
                        sw.Close();
                    }
                    #endregion


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenerateDataContextClass(TableModel pTableModel)
        {
            try
            {
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;

                    string dq = @"""";
                    string lstrTableName = strTable;  //table name

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(DataContextFolder + pTableModel.DotNetDataContextName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion

                    #region Get Table Name, Attributes Name and Attribute Types

                    #endregion

                    #region Write Namespaces
                    this.WriteDataContextNamespaces(sw, pTableModel);
                    #endregion

                    #region Write Class Default Constructor
                    this.WriteDefaultConstructor(sw, pTableModel.DotNetDataContextName);
                    #endregion

                    #region Write Private Variables
                    sb = new System.Text.StringBuilder("\r\n\t");

                    sw.WriteLine(sb.ToString());
                    #endregion

                    #region Write Public Methods for DAL
                    sb = new System.Text.StringBuilder("\r\n\t");

                    sb.AppendLine(@"private static string ConnectionString = " + dq + DBConnectionString + dq + ";");
                    sb.AppendLine(@"private static string vSAVESUCCESS = " + dq + "Data Saved Successfully." + dq + ";");
                    sb.AppendLine(@"private static string vSAVEFAIL = " + dq + "Data Not Saved." + dq + "; ");
                    sb.AppendLine(@"private static string vDELETESUCCESS = " + dq + "Data Deleted Successfully." + dq + "; ");
                    sb.AppendLine(@"private static string vDELETEFAIL = " + dq + "Data Not Deleted." + dq + "; ");

                    sb.AppendLine("#region Public Methods");


                    //...................................... Insert or Update Method ..........................................
                    DAL_InsertUpdateMethod(sb, pTableModel);

                    //...................................... Select Method ..........................................

                    //WriteSelectMethod_forDAL(sb, pTableModel);
                    DAL_GK(sb, pTableModel);
                    DAL_GA(sb, pTableModel);

                    DAL_BySearch(sb, pTableModel);


                    //...................................... Insert Method ..........................................
                    //WriteInsertMethod_forDAL(sb, AttributeNameArrayList);

                    //...................................... Update Method ..........................................
                    //WriteUpdateMethod_forDAL(sb, AttributeNameArrayList);

                    //...................................... Delete Method ..........................................

                    DAL_DeleteMethod(sb, pTableModel);



                    sb.Append("\r\n\t#endregion");
                    sb.Append("\r\n\t#region Private Helper Methods");
                    DAL_BuildModelMethod(sb, pTableModel);
                    sb.Append("\r\n\t#endregion");
                    sw.WriteLine(sb.ToString());


                    #endregion

                    #region Close file
                    if (sw != null)
                    {
                        sw.WriteLine("\r\n\t}\r\n}");
                        //dr.Close();
                        sw.Close();
                    }
                    #endregion


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenerateAllViewPages(TableModel pTableModel)
        {
            GenerateCreateViewPage(pTableModel);
            GenerateEditViewPage(pTableModel);
            GenerateIndexViewPage(pTableModel);
        }

        private void GenerateIndexViewPage(TableModel pTableModel)
        {
            try
            {
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;


                    string lstrTableName = strTable;  //table name

                    #region Create Empty cs file
                    string SpecificViewFolder = ViewsFolder + pTableModel.TableNameAsTitle + @"\";
                    CreateDirectory(SpecificViewFolder);
                    sb = new System.Text.StringBuilder(SpecificViewFolder + "Index");
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cshtml");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion

                    #region Get Table Name, Attributes Name and Attribute Types

                    #endregion

                    #region Write Namespaces
                    //this.WriteDataContextNamespaces(sw, pTableModel);
                    #endregion

                    #region Write Class Default Constructor
                    //this.WriteDefaultConstructor(sw, pTableModel.DotNetDataContextName);
                    #endregion

                    //#region Write Private Variables
                    sb = new System.Text.StringBuilder(" ");

                    sw.WriteLine(sb.ToString());
                    //#endregion

                    //#region Write Public Methods for DAL
                    // sb = new System.Text.StringBuilder("\r\n\t");
                    string dq = @"""";

                    sb.AppendLine(@"@model BTXCMS.Models.PagedList<" + NameSpaceFirstPart + "." + ModuleName + ".Model." + pTableModel.DotNetModelName + "> ");
                    sb.AppendLine(@" @{ ");
                    sb.AppendLine(@"                         ViewBag.Title = " + dq + pTableModel.DisplayName + " List" + dq + @" ; ");
                    sb.AppendLine(@"                     } ");
                    sb.AppendLine(@"  ");
                    sb.AppendLine(@" <div class=""container-fluid upass-main-btx""> ");
                    sb.AppendLine(@"     <div class=""row-fluid upass-tittle-btx""> ");
                    sb.AppendLine(@"         <div class=""upass-tittle-font-btx"">" + dq + pTableModel.DisplayName + " List" + dq + @"</div> ");
                    sb.AppendLine(@"     </div> ");
                    sb.AppendLine(@"     <div class=""well"" style=""margin-bottom: 0px !important; ""> ");
                    sb.AppendLine(@"         @using(Html.BeginForm(""Index"", " + dq + pTableModel.TableNameAsTitle + dq + @", FormMethod.Get, new { enctype = ""multipart/form-data"" })) ");
                    sb.AppendLine(@"         { ");
                    sb.AppendLine(@"             <div class=""row""> ");
                    sb.AppendLine(@"                 <div class=""col-md-8""> ");
                    sb.AppendLine(@"                     <div class=""input-group""> ");
                    sb.AppendLine(@"                         <input type = ""text"" ");
                    sb.AppendLine(@"                                name=""filter"" ");
                    sb.AppendLine(@"                                value=""@ViewBag.filter"" ");
                    sb.AppendLine(@"                                class=""form-control"" ");
                    sb.AppendLine(@"                                style=""display: inline; max-width:100 % !important; "" ");
                    sb.AppendLine(@"                                placeholder=""Search by name"" /> ");
                    sb.AppendLine(@"                         <span class=""input-group-btn""> ");
                    sb.AppendLine(@"                             <button class=""btn btn-success"" type=""submit"">Go</button> ");
                    sb.AppendLine(@"                         </span> ");
                    sb.AppendLine(@"  ");
                    sb.AppendLine(@"                     </div> ");
                    sb.AppendLine(@"                 </div> ");
                    sb.AppendLine(@"                 <div class=""pull-right col-md-1""> ");
                    sb.AppendLine(@"                     <a class=""btn btn-success"" data-modal="""" href='@Url.Action(""Create"", " + dq + pTableModel.TableNameAsTitle + dq + @")' id=""btnCreate""> ");
                    sb.AppendLine(@"                         <span class=""glyphicon glyphicon-plus""></span> ");
                    sb.AppendLine(@"                     </a> ");
                    sb.AppendLine(@"                 </div> ");
                    sb.AppendLine(@"             </div> ");
                    sb.AppendLine(@"             <div style = ""margin-top:17px; "" > ");
                    sb.AppendLine(@"             @{ ");
                    sb.AppendLine(@"                 var grid = new WebGrid(canPage: true, rowsPerPage: Model.PageSize, canSort: false, ajaxUpdateContainerId: ""grid""); ");
                    sb.AppendLine(@"  ");
                    sb.AppendLine(@"                 grid.Bind(Model.Content, rowCount: Model.TotalRecords, autoSortAndPage: false); ");
                    sb.AppendLine(@"                 grid.Pager(WebGridPagerModes.All); ");
                    sb.AppendLine(@"  ");
                    sb.AppendLine(@"                 @grid.GetHtml(htmlAttributes: new { id = ""grid"" },   // id for ajaxUpdateContainerId parameter ");
                    sb.AppendLine(@"                 fillEmptyRows: false, ");
                    sb.AppendLine(@"                 tableStyle: ""table  table-striped"", ");
                    sb.AppendLine(@"                 mode: WebGridPagerModes.All, ");
                    sb.AppendLine(@"                 columns: grid.Columns( ");

                    PrepareGridControls(sb, pTableModel);
                    sb.AppendLine(@"   ");
                    sb.AppendLine(@"                 )); ");
                    sb.AppendLine(@"             } ");
                    sb.AppendLine(@"             </div> ");
                    sb.AppendLine(@"         } ");
                    sb.AppendLine(@"     </div> ");
                    sb.AppendLine(@" </div> ");
                    sb.AppendLine(@"  ");
                    sb.AppendLine(@" @section Scripts ");
                    sb.AppendLine(@" { ");
                    sb.AppendLine(@"  ");
                    sb.AppendLine(@"     <script type=""text/javascript"" src=""~/Scripts/jquery.tablesorter.js""></script> ");
                    sb.AppendLine(@"     <script type=""text/javascript""> ");
                    sb.AppendLine(@"         $(document).ready(function () { ");
                    sb.AppendLine(@"             $(""#grid"").tablesorter(); ");
                    sb.AppendLine(@"     }); ");
                    sb.AppendLine(@"     </script> ");
                    sb.AppendLine(@" } ");


                    sw.WriteLine(sb.ToString());
                    #endregion
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        //dr.Close();
                        sw.Close();
                    }
                    #endregion


                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GenerateCreateViewPage(TableModel pTableModel)
        {
            try
            {
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;

                    string dq = @"""";
                    string lstrTableName = strTable;  //table name

                    #region Create Empty cs file
                    string SpecificViewFolder = ViewsFolder + pTableModel.TableNameAsTitle + @"\";
                    CreateDirectory(SpecificViewFolder);
                    sb = new System.Text.StringBuilder(SpecificViewFolder + "Create");
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cshtml");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion

                    #region Get Table Name, Attributes Name and Attribute Types

                    #endregion

                    #region Write Namespaces
                    //this.WriteDataContextNamespaces(sw, pTableModel);
                    #endregion

                    #region Write Class Default Constructor
                    //this.WriteDefaultConstructor(sw, pTableModel.DotNetDataContextName);
                    #endregion

                    //#region Write Private Variables
                    sb = new System.Text.StringBuilder("\t");

                    sw.WriteLine(sb.ToString());
                    //#endregion

                    //#region Write Public Methods for DAL
                    //sb = new System.Text.StringBuilder("\r\n\t");


                    sb.AppendLine(@"@model " + NameSpaceFirstPart + "." + ModuleName + ".Model." + pTableModel.DotNetModelName);
                    sb.AppendLine("");
                    sb.AppendLine(@"@{ ");
                    sb.AppendLine(@"     ViewBag.Title = ""Create""; ");
                    sb.AppendLine(@" }                                                                       ");
                    sb.AppendLine(@" @using(Html.BeginForm()) ");
                    sb.AppendLine(@"    {");
                    sb.AppendLine(@"       @Html.AntiForgeryToken()");
                    sb.AppendLine(@"       @Html.ValidationSummary(true) ");
                    sb.AppendLine(@"            <div class=""upass-panel-btx"">  ");
                    sb.AppendLine(@"                <div class=""panel-heading upass-panel-heading-btx"">    ");
                    sb.AppendLine(@"                    <div class=""upass-panel-tittle-font-btx"">" + pTableModel.DisplayName + " Create</div>   ");
                    sb.AppendLine(@"                </div>   ");
                    sb.AppendLine(@"                <div class=""well"" style=""margin-bottom: 0px !important;"">       ");
                    sb.AppendLine(@"                    <div class=""panel-body"">  ");
                    sb.AppendLine(@"      ");
                    sb.AppendLine(@"                        @Html.ValidationSummary(true, """", new { @class = ""text-danger"" }) ");
                    sb.AppendLine(@"                            <div class=""form-group row"">  ");
                    sb.AppendLine(@"                                <div class=""col-md-6"">    ");

                    sb.AppendLine(@" ");

                    PrepareViewControls(sb, pTableModel);

                    sb.AppendLine(@"                            </div > ");
                    sb.AppendLine(@"                       </div >");
                    //sb.AppendLine(@"                  </div >           ");
                    sb.AppendLine(@"               <div class=""form-group row"">");
                    sb.AppendLine(@" ");
                    sb.AppendLine(@"                   <div class=""col-md-2""></div>                                                                               ");
                    sb.AppendLine(@"                   <div class=""col-md-3"">@Html.ActionLink(""Back to List"", ""Index"", " + dq + pTableModel.TableNameAsTitle + dq + @", htmlAttributes: new { @class = ""btn btn-success"" })</div>");
                    sb.AppendLine(@"                   <div class=""col-md-7"" style=""text-align:right !important;"">  ");
                    sb.AppendLine(@"                        <input type = ""button"" value=""Create"" id=""btnSave"" class=""btn btn-success"" /> ");
                    sb.AppendLine(@"                   </div>  ");
                    sb.AppendLine(@"            </div>");
                    sb.AppendLine(@"         </div>   ");
                    sb.AppendLine(@"       </div>     ");
                    sb.AppendLine(@"      </div>      ");
                    sb.AppendLine(@"  }");
                    sb.AppendLine(@"   ");
                    sb.AppendLine(@"   ");
                    //sb.AppendLine("\r\n\t#region javascripts Methods");


                    sb.AppendLine(@"   @section Scripts {                                                                                                      ");
                    sb.AppendLine(@"                                                                                                                                            ");
                    sb.AppendLine(@"    <script type = ""text/javascript"" >                                                                                                     ");
                    sb.AppendLine(@"        $(document).ready(function() {                                                                                  ");
                    sb.AppendLine(@"            $(""#btnSave"").click(function() {                                                                                               ");
                    sb.AppendLine(@"                $(""#content"").html(""<b>Please Wait...</b>"");                                                                                ");
                    sb.AppendLine(@"                  var dataObject = {                                                                                          ");

                    string jQueryObject = "";
                    foreach (var prop in pTableModel.PropetyList)
                    {
                        if (!prop.IsSkippable)
                        {
                            jQueryObject = jQueryObject + "\r\n\t\t\t" + prop.SYSName + ": $(" + dq + "#" + prop.SYSName + dq + ").val(),";
                        }

                    }
                    if (!string.IsNullOrEmpty(jQueryObject))
                        jQueryObject = jQueryObject.Substring(0, jQueryObject.Length - 1);

                    sb.AppendLine(jQueryObject);

                    sb.AppendLine(@"                };                                                                                                                          ");
                    sb.AppendLine(@"                $.ajax({                                                                                                                    ");
                    sb.AppendLine(@"                    url: '@Url.Action(""Create"", " + dq + pTableModel.TableNameAsTitle + dq + ")', ");
                    sb.AppendLine(@"                    type: ""POST"",                                                                                                           ");
                    sb.AppendLine(@"                    data: dataObject,                                                                                                       ");
                    sb.AppendLine(@"                    dataType: ""json"",                                                                                                       ");
                    sb.AppendLine(@"                    success: function(data) {                                                                                               ");
                    sb.AppendLine(@"                                    upass_custom_alert(data, ""Success"");                                                                    ");
                    sb.AppendLine(@"                                },                                                                                                          ");
                    sb.AppendLine(@"                    error: function(xhr, httpStatus, msg) {                                                                                 ");
                    sb.AppendLine(@"                                    upass_custom_alert(""Error! Please try again"", ""Error"");                                                 ");
                    sb.AppendLine(@"                                }                                                                                                           ");
                    sb.AppendLine(@"                            });                                                                                                             ");
                    sb.AppendLine(@"                        });                                                                                                                 ");
                    sb.AppendLine(@"                                                                                                                                            ");
                    sb.AppendLine(@"                        function upass_custom_alert(output_msg, title_msg) {                                                                ");
                    sb.AppendLine(@"                        if (!title_msg)                                                                                                     ");
                    sb.AppendLine(@"                            title_msg = 'Alert';                                                                                            ");
                    sb.AppendLine(@"                                                                                                                                            ");
                    sb.AppendLine(@"                        if (!output_msg)                                                                                                    ");
                    sb.AppendLine(@"                            output_msg = 'No Message to Display.';                                                                          ");
                    sb.AppendLine(@"                                                                                                                                            ");
                    sb.AppendLine(@"                $(""<div></div>"").html(output_msg).dialog({                                                                                  ");
                    sb.AppendLine(@"                            title: title_msg,                                                                                               ");
                    sb.AppendLine(@"                                resizable: false,                                                                                           ");
                    sb.AppendLine(@"                                modal: true,                                                                                                ");
                    sb.AppendLine(@"                                buttons: {                                                                                                  ");
                    sb.AppendLine(@"                            ""Ok"": function() {                                                                                              ");
                    sb.AppendLine(@"                                window.location = '@Url.Action(""Index"", " + dq + pTableModel.TableNameAsTitle + dq + ")';               ");
                    sb.AppendLine(@"                            $(this).dialog(""close"");                                                                                        ");
                    sb.AppendLine(@"                            }                                                                                                               ");
                    sb.AppendLine(@"                        }                                                                                                                   ");
                    sb.AppendLine(@"                    }).addClass(""alert alert-success fade in"");                                                                             ");
                    sb.AppendLine(@"                }                                                                                                                           ");
                    sb.AppendLine(@"            })                                                                                                                              ");
                    sb.AppendLine(@"    </script >                                                                                                                             ");
                    sb.AppendLine(@"}                                                                                                                                           ");


                    //sb.Append("\r\n\t#endregion");
                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        //dr.Close();
                        sw.Close();
                    }
                    #endregion


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenerateEditViewPage(TableModel pTableModel)
        {
            try
            {
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;

                    string dq = @"""";

                    string lstrTableName = strTable;  //table name

                    #region Create Empty cs file
                    string SpecificViewFolder = ViewsFolder + pTableModel.TableNameAsTitle + @"\";
                    CreateDirectory(SpecificViewFolder);
                    sb = new System.Text.StringBuilder(SpecificViewFolder + "Edit");
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cshtml");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion

                    #region Get Table Name, Attributes Name and Attribute Types

                    #endregion

                    #region Write Namespaces
                    //this.WriteDataContextNamespaces(sw, pTableModel);
                    #endregion

                    #region Write Class Default Constructor
                    //this.WriteDefaultConstructor(sw, pTableModel.DotNetDataContextName);
                    #endregion

                    #region Write Private Variables
                    sb = new System.Text.StringBuilder("\t");

                    sw.WriteLine(sb.ToString());
                    #endregion

                    #region Write Public Methods for DAL
                    //sb = new System.Text.StringBuilder("\r\n\t");


                    sb.AppendLine(@"@model " + NameSpaceFirstPart + "." + ModuleName + ".Model." + pTableModel.DotNetModelName);
                    sb.AppendLine("");
                    sb.AppendLine(@"@{ ");
                    sb.AppendLine(@"     ViewBag.Title = ""Edit""; ");
                    sb.AppendLine(@" }                                                                       ");
                    sb.AppendLine(@" @using(Html.BeginForm()) ");
                    sb.AppendLine(@"    {");
                    sb.AppendLine(@"       @Html.AntiForgeryToken()");
                    sb.AppendLine(@"       @Html.ValidationSummary(true) ");
                    sb.AppendLine(@"            <div class=""upass-panel-btx"">  ");
                    sb.AppendLine(@"                <div class=""panel-heading upass-panel-heading-btx"">    ");
                    sb.AppendLine(@"                    <div class=""upass-panel-tittle-font-btx"">" + pTableModel.DisplayName + " Create</div>   ");
                    sb.AppendLine(@"                </div>   ");
                    sb.AppendLine(@"                <div class=""well"" style=""margin-bottom: 0px !important;"">       ");
                    sb.AppendLine(@"                    <div class=""panel-body"">  ");
                    sb.AppendLine(@"      ");
                    sb.AppendLine(@"                        @Html.ValidationSummary(true, """", new { @class = ""text-danger"" }) ");
                    sb.AppendLine(@"                            <div class=""form-group row"">  ");
                    sb.AppendLine(@"                                <div class=""col-md-6"">    ");

                    sb.AppendLine(@" ");

                    PrepareViewControls(sb, pTableModel);

                    sb.AppendLine(@"                            </div > ");
                    sb.AppendLine(@"                       </div >");
                    //sb.AppendLine(@"                  </div >           ");
                    sb.AppendLine(@"               <div class=""form-group row"">");
                    sb.AppendLine(@" ");
                    sb.AppendLine(@"                   <div class=""col-md-2""></div>                                                                               ");
                    sb.AppendLine(@"                   <div class=""col-md-3"">@Html.ActionLink(""Back to List"", ""Index"", " + dq + pTableModel.TableNameAsTitle + dq + @", htmlAttributes: new { @class = ""btn btn-success"" })</div>");
                    sb.AppendLine(@"                   <div class=""col-md-7"" style=""text-align:right !important;"">  ");
                    sb.AppendLine(@"                        <input type = ""button"" value=""Edit"" id=""AjaxPost"" class=""btn btn-success"" /> ");
                    sb.AppendLine(@"                   </div>  ");
                    sb.AppendLine(@"            </div>");
                    sb.AppendLine(@"         </div>   ");
                    sb.AppendLine(@"       </div>     ");
                    sb.AppendLine(@"      </div>      ");
                    sb.AppendLine(@"  }");
                    sb.AppendLine(@"   ");
                    sb.AppendLine(@"   ");
                    //sb.AppendLine("\r\n\t#region javascripts Methods");

                    sb.AppendLine(@"            @section Scripts {");



                    sb.AppendLine(@" <script type = ""text/javascript"" >                                                            ");
                    sb.AppendLine(@"        $(document).ready(function() {                                                             ");
                    sb.AppendLine(@"            $(""#AjaxPost"").click(function() {                                                      ");
                    sb.AppendLine(@"                $(""#content"").html(""<b>Please Wait...</b>"");                                       ");
                    sb.AppendLine(@"                                var dataObject = {                                                 ");

                    string jQueryObject = "";
                    foreach (var prop in pTableModel.PropetyList)
                    {
                        if (!prop.IsSkippable)
                        {
                            jQueryObject = jQueryObject + "\r\n\t\t\t" + prop.SYSName + ": $(" + dq + "#" + prop.SYSName + dq + ").val(),";
                        }

                    }
                    if (!string.IsNullOrEmpty(jQueryObject))
                        jQueryObject = jQueryObject.Substring(0, jQueryObject.Length - 1);
                    sb.AppendLine(jQueryObject);

                    sb.AppendLine(@"                };                                                                                 ");
                    sb.AppendLine(@"                $.ajax({                                                                           ");
                    sb.AppendLine(@"                   url: '@Url.Action(""Edit"", " + dq + pTableModel.TableNameAsTitle + dq + ")',                        ");
                    sb.AppendLine(@"                    type: ""POST"",                                                                  ");
                    sb.AppendLine(@"                    data: dataObject,                                                              ");
                    sb.AppendLine(@"                    dataType: ""json"",                                                              ");
                    sb.AppendLine(@"                    success: function(data) {                                                      ");
                    sb.AppendLine(@"                                    upass_custom_alert(data, ""Success"");                           ");
                    sb.AppendLine(@"                                },                                                                 ");
                    sb.AppendLine(@"                    error: function(xhr, httpStatus, msg) {                                        ");
                    sb.AppendLine(@"                                    upass_custom_alert(""Error! Please try again"", ""Error"");        ");
                    sb.AppendLine(@"                                }                                                                  ");
                    sb.AppendLine(@"                            });                                                                    ");
                    sb.AppendLine(@"                        });                                                                        ");
                    sb.AppendLine(@"                                                                                                   ");
                    sb.AppendLine(@"            $(""#btnDelete"").click(function() {                                                     ");
                    sb.AppendLine(@"                //e.preventDefault();                                                              ");
                    sb.AppendLine(@"                $(""#dlgConfirm"").dialog(""open"");                                                   ");
                    sb.AppendLine(@"                        });                                                                        ");
                    sb.AppendLine(@"                                                                                                   ");
                    sb.AppendLine(@"            $(""#dlgConfirm"").dialog({                                                              ");
                    sb.AppendLine(@"                            autoOpen: false,                                                       ");
                    sb.AppendLine(@"                modal: true,                                                                       ");
                    sb.AppendLine(@"                buttons:                                                                           ");
                    sb.AppendLine(@"                            {                                                                      ");
                    sb.AppendLine(@"                                ""Confirm"": function() {                                            ");
                    sb.AppendLine(@"                        $(this).dialog(""close"");                                                   ");
                    sb.AppendLine(@"                        $(""#content"").html(""<b>Please Wait...</b>"");                               ");
                    sb.AppendLine(@"                                    var dataObject = {                                             ");

                    string jQueryDeleteObject = "";
                    foreach (var prop in pTableModel.PropetyList)
                    {
                        if (prop.IsPrimayKey)
                        {
                            jQueryDeleteObject = jQueryDeleteObject + "\r\n\t\t\t" + prop.SYSName + ": $(" + dq + "#" + prop.SYSName + dq + ").val(),";
                        }

                    }
                    if (!string.IsNullOrEmpty(jQueryDeleteObject))
                        jQueryDeleteObject = jQueryDeleteObject.Substring(0, jQueryDeleteObject.Length - 1);

                    sb.AppendLine(jQueryDeleteObject);

                    //sb.AppendLine(@"                            id: $(""#Id"").val()                                                     ");

                    sb.AppendLine(@"                                                                                                   ");
                    sb.AppendLine(@"                        };                                                                         ");
                    sb.AppendLine(@"                        $.ajax({                                                                   ");
                    sb.AppendLine(@"                                    url: '@Url.Action(""Delete"", " + dq + pTableModel + dq + ")',                  ");
                    sb.AppendLine(@"                            type: ""POST"",                                                          ");
                    sb.AppendLine(@"                            data: dataObject,                                                      ");
                    sb.AppendLine(@"                            dataType: ""json"",                                                      ");
                    sb.AppendLine(@"                            success: function(data) {                                              ");
                    sb.AppendLine(@"                                        upass_custom_alert(data, ""Success"");                       ");
                    sb.AppendLine(@"                                    },                                                             ");
                    sb.AppendLine(@"                            error: function(xhr, httpStatus, msg) {                                ");
                    sb.AppendLine(@"                                        upass_custom_alert(""Error! Please try again"", ""Error"");    ");
                    sb.AppendLine(@"                                    }                                                              ");
                    sb.AppendLine(@"                                });                                                                ");
                    sb.AppendLine(@"                            },                                                                     ");
                    sb.AppendLine(@"                    ""Cancel"": function() {                                                         ");
                    sb.AppendLine(@"                        $(this).dialog(""close"");                                                   ");
                    sb.AppendLine(@"                            }                                                                      ");
                    sb.AppendLine(@"                        }                                                                          ");
                    sb.AppendLine(@"                    });                                                                            ");
                    sb.AppendLine(@"                                                                                                   ");
                    //sb.AppendLine(@"            $('#IsInternal').change(function() {                                                   ");
                    //sb.AppendLine(@"                        if ($(this).is(":checked")) {                                              ");
                    //sb.AppendLine(@"                            if ($("#EmailAddressTo").val() == '') {                                ");
                    //sb.AppendLine(@"                                alert("Please enter email address first for internal user");       ");
                    //sb.AppendLine(@"                                return;                                                            ");
                    //sb.AppendLine(@"                            }                                                                      ");
                    //sb.AppendLine(@"                    $.ajax({                                                                       ");
                    //sb.AppendLine(@"                                url: "@Url.Action("GetUserById", "NotifyEmails")",                 ");
                    //sb.AppendLine(@"                        type: "POST",                                                              ");
                    //sb.AppendLine(@"                        data: { EmailAddressTo: $("#EmailAddressTo").val() },                      ");
                    //sb.AppendLine(@"                        dataType: "json",                                                          ");
                    //sb.AppendLine(@"                        success: function(data) {                                                  ");
                    //sb.AppendLine(@"                            $('#FirstName').val(data.FirstName);                                   ");
                    //sb.AppendLine(@"                            $('#LastName').val(data.LastName);                                     ");
                    //sb.AppendLine(@"                                },                                                                 ");
                    //sb.AppendLine(@"                        error: function(xhr, httpStatus, msg) {                                    ");
                    //sb.AppendLine(@"                            $('#FirstName').val('');                                               ");
                    //sb.AppendLine(@"                            $('#LastName').val('');                                                ");
                    //sb.AppendLine(@"                                }                                                                  ");
                    //sb.AppendLine(@"                            });                                                                    ");
                    //sb.AppendLine(@"                        }                                                                          ");
                    //sb.AppendLine(@"                else {                                                                             ");
                    //sb.AppendLine(@"                    $('#FirstName').val('');                                                       ");
                    //sb.AppendLine(@"                    $('#LastName').val('');                                                        ");
                    //sb.AppendLine(@"                        }                                                                          ");
                    //sb.AppendLine(@"                                                                                                   ");
                    //sb.AppendLine(@"                    });                                                                            ");
                    //sb.AppendLine(@"                                                                                                   ");
                    //sb.AppendLine(@"            $('#EmailAddressTo').change(function() {                                               ");
                    //sb.AppendLine(@"                        if ($('#IsInternal').is(":checked")) {                                     ");
                    //sb.AppendLine(@"                            if ($("#EmailAddressTo").val() == '') {                                ");
                    //sb.AppendLine(@"                                alert("Please enter email address first for internal user");       ");
                    //sb.AppendLine(@"                                return;                                                            ");
                    //sb.AppendLine(@"                            }                                                                      ");
                    //sb.AppendLine(@"                    $.ajax({                                                                       ");
                    //sb.AppendLine(@"                                url: "@Url.Action("GetUserById", "NotifyEmails")",                 ");
                    //sb.AppendLine(@"                        type: "POST",                                                              ");
                    //sb.AppendLine(@"                        data: { EmailAddressTo: $("#EmailAddressTo").val() },                      ");
                    //sb.AppendLine(@"                        dataType: "json",                                                          ");
                    //sb.AppendLine(@"                        success: function(data) {                                                  ");
                    //sb.AppendLine(@"                            $('#FirstName').val(data.FirstName);                                   ");
                    //sb.AppendLine(@"                            $('#LastName').val(data.LastName);                                     ");
                    //sb.AppendLine(@"                                },                                                                 ");
                    //sb.AppendLine(@"                        error: function(xhr, httpStatus, msg) {                                    ");
                    //sb.AppendLine(@"                            $('#FirstName').val('');                                               ");
                    //sb.AppendLine(@"                            $('#LastName').val('');                                                ");
                    //sb.AppendLine(@"                                }                                                                  ");
                    //sb.AppendLine(@"                            });                                                                    ");
                    //sb.AppendLine(@"                        }                                                                          ");
                    //sb.AppendLine(@"                else {                                                                             ");
                    //sb.AppendLine(@"                    $('#FirstName').val('');                                                       ");
                    //sb.AppendLine(@"                    $('#LastName').val('');                                                        ");
                    //sb.AppendLine(@"                        }                                                                          ");
                    //sb.AppendLine(@"                                                                                                   ");
                    //sb.AppendLine(@"                    });                                                                            ");

                    sb.AppendLine(@"                                                                                                   ");
                    sb.AppendLine(@"                    function upass_custom_alert(output_msg, title_msg) {                           ");
                    sb.AppendLine(@"                    if (!title_msg)                                                                ");
                    sb.AppendLine(@"                        title_msg = 'Alert';                                                       ");
                    sb.AppendLine(@"                                                                                                   ");
                    sb.AppendLine(@"                    if (!output_msg)                                                               ");
                    sb.AppendLine(@"                        output_msg = 'No Message to Display.';                                     ");
                    sb.AppendLine(@"                                                                                                   ");
                    sb.AppendLine(@"                $(""<div></div>"").html(output_msg).dialog({                                         ");
                    sb.AppendLine(@"                        title: title_msg,                                                          ");
                    sb.AppendLine(@"                            resizable: false,                                                      ");
                    sb.AppendLine(@"                            modal: true,                                                           ");
                    sb.AppendLine(@"                            buttons: {                                                             ");
                    sb.AppendLine(@"                        ""Ok"": function() {                                                         ");
                    sb.AppendLine(@"                            if (output_msg.indexOf('successfully') >= 0)                           ");
                    sb.AppendLine(@"                            {                                                                      ");
                    sb.AppendLine(@"                                window.location = '@Url.Action(""Index"", " + dq + pTableModel.TableNameAsTitle + dq + ")';");
                    sb.AppendLine(@"                            }                                                                      ");
                    sb.AppendLine(@"                            $(this).dialog(""close"");                                               ");
                    sb.AppendLine(@"                        }                                                                          ");
                    sb.AppendLine(@"                    }                                                                              ");
                    sb.AppendLine(@"                }).addClass(""alert alert-success fade in"");                                        ");
                    sb.AppendLine(@"            }                                                                                      ");
                    sb.AppendLine(@"        })                                                                                         ");
                    sb.AppendLine(@"    </script>                                                                                      ");
                    sb.AppendLine(@"}                                                                                                  ");

                    //sb.Append("\r\n\t#endregion");
                    sw.WriteLine(sb.ToString());
                    #endregion
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        //dr.Close();
                        sw.Close();
                    }
                    #endregion


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //BLL-------------------------------------
        private void GenerateBLLClass(TableModel pTableModel)
        {
            try
            {
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(BLLFolder + pTableModel.DotNetBLLName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion



                    #region Write Namespaces
                    this.WriteBLLNamespaces(sw, pTableModel);
                    #endregion

                    #region Write Class Default Constructor
                    this.WriteDefaultConstructor(sw, pTableModel.DotNetBLLName);
                    #endregion




                    sb = new System.Text.StringBuilder("\r\n\t\t");
                    sb.Append("#region Public Methods");
                    #region Public Methods
                    sb.Append("\r\n\t\tpublic string Save[{MODEL}] (I[{MODEL}] p[{MODEL}])");
                    sb.Append("\r\n\t\t{");
                    sb.Append("\r\n\t\t// Business goes here........");
                    sb.Append("\r\n\t\t\ttry");
                    sb.Append("\r\n\t\t\t{");
                    sb.Append("\r\n\t\t\t\treturn [{MODEL}]DC.Save[{MODEL}](p[{MODEL}]);");
                    sb.Append("\r\n\t\t\t}");
                    sb.Append("\r\n\t\t\tcatch(Exception ex)");
                    sb.Append("\r\n\t\t\t{");
                    sb.Append("\r\n\t\t\t\tthrow new Exception(ex.Message);");
                    sb.Append("\r\n\t\t\t}");
                    sb.Append("\r\n\t\t}");

                    string BLLSignatureForEdit = string.Empty;
                    string SignatureWithoutType = string.Empty;
                    string StringEmptyChecke = string.Empty;

                    var CompositePropsForEdit = pTableModel.PropetyList.FindAll(p => p.IsPrimayKey);
                    if (CompositePropsForEdit != null && CompositePropsForEdit.Count == 1)
                    {
                        BLLSignatureForEdit = CompositePropsForEdit[0].SYSType + " p" + CompositePropsForEdit[0].SYSName;
                        SignatureWithoutType = "p" + CompositePropsForEdit[0].SYSName;
                        StringEmptyChecke = "!String.IsNullOrEmpty(" + CompositePropsForEdit[0].SYSName + ")";
                    }
                    else if (CompositePropsForEdit != null && CompositePropsForEdit.Count > 1)
                    {
                        foreach (var prop in CompositePropsForEdit)
                        {
                            BLLSignatureForEdit = BLLSignatureForEdit + prop.SYSType + " p" + prop.SYSName + ",";
                            SignatureWithoutType = SignatureWithoutType + "p" + prop.SYSName + ",";
                            StringEmptyChecke = StringEmptyChecke + "!String.IsNullOrEmpty(" + prop.SYSName + ") && ";

                        }
                        BLLSignatureForEdit = BLLSignatureForEdit.Substring(0, BLLSignatureForEdit.Length - 1);
                        SignatureWithoutType = SignatureWithoutType.Substring(0, SignatureWithoutType.Length - 1);
                        StringEmptyChecke = StringEmptyChecke.Substring(0, StringEmptyChecke.Length - 3);
                    }
                    else { }


                    sb.Append("\r\n\t\tpublic I[{MODEL}] Get[{MODEL}]ById(" + BLLSignatureForEdit + ")");
                    sb.Append("\r\n\t\t{                                                          ");
                    sb.Append("\r\n\t\t// Business goes here........                              ");
                    sb.Append("\r\n\t\t\ttry                                                       ");
                    sb.Append("\r\n\t\t\t{                                                         ");
                    sb.Append("\r\n\t\t\t\treturn [{MODEL}]DC.Get[{MODEL}]ById(" + SignatureWithoutType + ");               ");
                    sb.Append("\r\n\t\t\t}                                                         ");
                    sb.Append("\r\n\t\t\tcatch(Exception ex)                                       ");
                    sb.Append("\r\n\t\t\t{                                                         ");
                    sb.Append("\r\n\t\t\t\tthrow new Exception(ex.Message);                      ");
                    sb.Append("\r\n\t\t\t}                                                         ");
                    sb.Append("\r\n\t\t}                                                          ");
                    sb.Append("\r\n\t\tpublic IList<I[{MODEL}]> Get[{MODEL}]ALL()                         ");
                    sb.Append("\r\n\t\t{                                                          ");
                    sb.Append("\r\n\t\t// Business goes here........                              ");
                    sb.Append("\r\n\t\t\ttry                                                       ");
                    sb.Append("\r\n\t\t\t{                                                         ");
                    sb.Append("\r\n\t\t\t\treturn [{MODEL}]DC.Get[{MODEL}]ALL();                        ");
                    sb.Append("\r\n\t\t\t}                                                         ");
                    sb.Append("\r\n\t\t\tcatch(Exception ex)                                       ");
                    sb.Append("\r\n\t\t\t{                                                         ");
                    sb.Append("\r\n\t\t\t\tthrow new Exception(ex.Message);                      ");
                    sb.Append("\r\n\t\t\t}                                                         ");
                    sb.Append("\r\n\t\t}                                                          ");
                    sb.Append("\r\n\t\tpublic IList<I[{MODEL}]> Get[{MODEL}]BySearch(I[{MODEL}] p[{MODEL}])       ");
                    sb.Append("\r\n\t\t{                                                          ");
                    sb.Append("\r\n\t\t// Business goes here........                              ");
                    sb.Append("\r\n\t\t\ttry                                                       ");
                    sb.Append("\r\n\t\t\t{                                                         ");
                    sb.Append("\r\n\t\t\t\treturn [{MODEL}]DC.Get[{MODEL}]BySearch(p[{MODEL}]);             ");
                    sb.Append("\r\n\t\t\t}                                                         ");
                    sb.Append("\r\n\t\t\tcatch(Exception ex)                                       ");
                    sb.Append("\r\n\t\t\t{                                                         ");
                    sb.Append("\r\n\t\t\t\tthrow new Exception(ex.Message);                      ");
                    sb.Append("\r\n\t\t\t}                                                         ");
                    sb.Append("\r\n\t\t}                                                          ");
                    sb.Append("\r\n\t\tpublic string Delete[{MODEL}] (I[{MODEL}] p[{MODEL}])                  ");
                    sb.Append("\r\n\t\t{                                                          ");
                    sb.Append("\r\n\t\t// Business goes here........                              ");
                    sb.Append("\r\n\t\t\ttry                                                       ");
                    sb.Append("\r\n\t\t\t{                                                         ");
                    sb.Append("\r\n\t\t\t\treturn [{MODEL}]DC.Delete[{MODEL}](p[{MODEL}]);                  ");
                    sb.Append("\r\n\t\t\t}                                                         ");
                    sb.Append("\r\n\t\t\tcatch(Exception ex)                                       ");
                    sb.Append("\r\n\t\t\t{                                                         ");
                    sb.Append("\r\n\t\t\t\tthrow new Exception(ex.Message);                      ");
                    sb.Append("\r\n\t\t\t}                                                         ");
                    sb.Append("\r\n\t\t}                                                          ");
                    sb.Append("\r\n\t#endregion");
                    string bll = sb.ToString().Replace("[{MODEL}]", pTableModel.TableNameAsTitle);
                    if (!IsModelInterfaceRequired)
                    {
                        bll = bll.Replace("I" + pTableModel.TableNameAsTitle, pTableModel.DotNetModelName);
                    }
                    sw.WriteLine(bll);

                    if (pTableModel.ChildTableModelList != null && pTableModel.ChildTableModelList.Count > 0)
                    {
                        sb.Length = 0;
                        foreach (var pChild in pTableModel.ChildTableModelList)
                        {
                            sb.Append(GenerateChildBLL(pChild, BLLSignatureForEdit, SignatureWithoutType, pTableModel.TableNameAsTitle));
                        }
                    }
                    sw.WriteLine(sb.ToString());
                    #endregion

                    #region Close file
                    if (sw != null)
                    {
                        sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateChildBLL(TableModel pTableModel, string BllSignature, string DCSignature, string pMethodPartName)
        {
            StringBuilder sb = new StringBuilder();
            string BLLSignatureForEdit = string.Empty;
            string SignatureWithoutType = string.Empty;
            string StringEmptyChecke = string.Empty;

            var CompositePropsForEdit = pTableModel.PropetyList.FindAll(p => p.IsPrimayKey);
            if (CompositePropsForEdit != null && CompositePropsForEdit.Count == 1)
            {
                BLLSignatureForEdit = CompositePropsForEdit[0].SYSType + " p" + CompositePropsForEdit[0].SYSName;
                SignatureWithoutType = "p" + CompositePropsForEdit[0].SYSName;
                StringEmptyChecke = "!String.IsNullOrEmpty(" + CompositePropsForEdit[0].SYSName + ")";
            }
            else if (CompositePropsForEdit != null && CompositePropsForEdit.Count > 1)
            {
                foreach (var prop in CompositePropsForEdit)
                {
                    BLLSignatureForEdit = BLLSignatureForEdit + prop.SYSType + " p" + prop.SYSName + ",";
                    SignatureWithoutType = SignatureWithoutType + "p" + prop.SYSName + ",";
                    StringEmptyChecke = StringEmptyChecke + "!String.IsNullOrEmpty(" + prop.SYSName + ") && ";

                }
                BLLSignatureForEdit = BLLSignatureForEdit.Substring(0, BLLSignatureForEdit.Length - 1);
                SignatureWithoutType = SignatureWithoutType.Substring(0, SignatureWithoutType.Length - 1);
                StringEmptyChecke = StringEmptyChecke.Substring(0, StringEmptyChecke.Length - 3);
            }
            else { }


            sb.Append("\r\n\t\tpublic I[{MODEL}] Get[{MODEL}]ById(" + BLLSignatureForEdit + ")");
            sb.Append("\r\n\t\t{                                                          ");
            sb.Append("\r\n\t\t// Business goes here........                              ");
            sb.Append("\r\n\t\t\ttry                                                       ");
            sb.Append("\r\n\t\t\t{                                                         ");
            sb.Append("\r\n\t\t\t\treturn " + pMethodPartName + "DC.Get[{MODEL}]ById(" + SignatureWithoutType + ");               ");
            sb.Append("\r\n\t\t\t}                                                         ");
            sb.Append("\r\n\t\t\tcatch(Exception ex)                                       ");
            sb.Append("\r\n\t\t\t{                                                         ");
            sb.Append("\r\n\t\t\t\tthrow new Exception(ex.Message);                      ");
            sb.Append("\r\n\t\t\t}                                                         ");
            sb.Append("\r\n\t\t}                                                          ");

            sb.Append("\r\n\t\tpublic List<I[{MODEL}]> Get[{MODEL}]ByParentId(" + BllSignature + ")                         ");
            sb.Append("\r\n\t\t{                                                          ");
            sb.Append("\r\n\t\t// Business goes here........                              ");
            sb.Append("\r\n\t\t\ttry                                                       ");
            sb.Append("\r\n\t\t\t{                                                         ");
            sb.Append("\r\n\t\t\t\treturn " + pMethodPartName + "DC.Get[{MODEL}]ByParentId(" + DCSignature + ");                        ");
            sb.Append("\r\n\t\t\t}                                                         ");
            sb.Append("\r\n\t\t\tcatch(Exception ex)                                       ");
            sb.Append("\r\n\t\t\t{                                                         ");
            sb.Append("\r\n\t\t\t\tthrow new Exception(ex.Message);                      ");
            sb.Append("\r\n\t\t\t}                                                         ");
            sb.Append("\r\n\t\t}                                                          ");

            sb.Append("\r\n\t\tpublic IList<I[{MODEL}]> Get[{MODEL}]BySearch(I[{MODEL}] p[{MODEL}])       ");
            sb.Append("\r\n\t\t{                                                          ");
            sb.Append("\r\n\t\t// Business goes here........                              ");
            sb.Append("\r\n\t\t\ttry                                                       ");
            sb.Append("\r\n\t\t\t{                                                         ");
            sb.Append("\r\n\t\t\t\treturn " + pMethodPartName + "DC.Get[{MODEL}]BySearch(p[{MODEL}]);             ");
            sb.Append("\r\n\t\t\t}                                                         ");
            sb.Append("\r\n\t\t\tcatch(Exception ex)                                       ");
            sb.Append("\r\n\t\t\t{                                                         ");
            sb.Append("\r\n\t\t\t\tthrow new Exception(ex.Message);                      ");
            sb.Append("\r\n\t\t\t}                                                         ");
            sb.Append("\r\n\t\t}                                                          ");

            // Delete by PK
            sb.Append("\r\n\t\tpublic string Delete[{MODEL}] (I[{MODEL}] p[{MODEL}])                  ");
            sb.Append("\r\n\t\t{                                                          ");
            sb.Append("\r\n\t\t// Business goes here........                              ");
            sb.Append("\r\n\t\t\ttry                                                       ");
            sb.Append("\r\n\t\t\t{                                                         ");
            sb.Append("\r\n\t\t\t\treturn " + pMethodPartName + "DC.Delete[{MODEL}](p[{MODEL}]);                  ");
            sb.Append("\r\n\t\t\t}                                                         ");
            sb.Append("\r\n\t\t\tcatch(Exception ex)                                       ");
            sb.Append("\r\n\t\t\t{                                                         ");
            sb.Append("\r\n\t\t\t\tthrow new Exception(ex.Message);                      ");
            sb.Append("\r\n\t\t\t}                                                         ");
            sb.Append("\r\n\t\t}                                                          ");
            // Delete by parent id
            //sb.Append("\r\n\t\tpublic string Delete[{MODEL}] (I[{MODEL}] p[{MODEL}])                  ");
            //sb.Append("\r\n\t\t{                                                          ");
            //sb.Append("\r\n\t\t// Business goes here........                              ");
            //sb.Append("\r\n\t\t\ttry                                                       ");
            //sb.Append("\r\n\t\t\t{                                                         ");
            //sb.Append("\r\n\t\t\t\treturn [{MODEL}]DC.Delete[{MODEL}](p[{MODEL}]);                  ");
            //sb.Append("\r\n\t\t\t}                                                         ");
            //sb.Append("\r\n\t\t\tcatch(Exception ex)                                       ");
            //sb.Append("\r\n\t\t\t{                                                         ");
            //sb.Append("\r\n\t\t\t\tthrow new Exception(ex.Message);                      ");
            //sb.Append("\r\n\t\t\t}                                                         ");
            //sb.Append("\r\n\t\t}                                                          ");

            //sb.Append("\r\n\t#endregion");
            string bll = sb.ToString().Replace("[{MODEL}]", pTableModel.TableNameAsTitle);
            if (!IsModelInterfaceRequired)
            {
                bll = bll.Replace("I" + pTableModel.TableNameAsTitle, pTableModel.DotNetModelName);
            }
            return bll;
        }

        private void GenerateINTERFACEBLLClass(TableModel pTableModel)
        {
            try
            {
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(IBLLFolder + pTableModel.DotNetIBLLIntName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion



                    #region Write Namespaces
                    this.WriteIBLLNamespaces(sw, pTableModel);
                    #endregion

                    #region Write Class Default Constructor
                    //this.WriteDefaultConstructor(sw, pTableModel.DotNetIBLLIntName);
                    #endregion



                    #region Write Methods signature
                    sb = new System.Text.StringBuilder("\r\n\t\t");
                    sb.Append("\r\n\t\t#region Public Methods");
                    sb.Append("\r\n\t\tstring Save[{MODEL}](I[{MODEL}] p[{MODEL}]);");
                    sb.Append("\r\n\t\tI[{MODEL}] Get[{MODEL}]ById(Int16? p[{MODEL}]Id);");
                    sb.Append("\r\n\t\tIList<I[{MODEL}]> Get[{MODEL}]ALL();");
                    sb.Append("\r\n\t\tIList<I[{MODEL}]> Get[{MODEL}]BySearch(I[{MODEL}] p[{MODEL}]);");
                    sb.Append("\r\n\t\tstring Delete[{MODEL}](I[{MODEL}] p[{MODEL}]);");
                    sb.Append("\r\n\t\t#endregion");
                    string bll = sb.ToString().Replace("[{MODEL}]", pTableModel.TableNameAsTitle);
                    if (!IsModelInterfaceRequired)
                    {
                        bll = bll.Replace("I" + pTableModel.TableNameAsTitle, pTableModel.DotNetModelName);
                    }
                    sw.WriteLine(bll);
                    #endregion

                    #region Close file
                    if (sw != null)
                    {
                        sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void GenerateModelClass(TableModel pTableModel)
        {
            try
            {
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(ModelFolder + pTableModel.DotNetModelName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion

                    #region Get Table Name, Attributes Name and Attribute Types

                    #endregion

                    #region Write Namespaces
                    this.WriteModelNamespaces(sw, pTableModel);
                    #endregion

                    #region Write Class Default Constructor
                    //this.WriteDefaultConstructor(sw, ClassName);
                    #endregion



                    #region Write Public Properties
                    sb = new System.Text.StringBuilder("\r\n\t\t");
                    sb.AppendLine("#region Public Properties");
                    foreach (var column in pTableModel.PropetyList)
                    {
                        if (!column.IsSkippable)
                        {
                            this.WritePublicProperties(sb, column);
                        }
                    }

                    if (pTableModel.IsParentTable && pTableModel.ChildTableModelList != null && pTableModel.ChildTableModelList.Count > 0)
                    {
                        foreach (var ChildModel in pTableModel.ChildTableModelList)
                        {
                            sb.AppendFormat("\t\tpublic List<{0}> obj{1}List {2} get; set; {3}", ChildModel.DotNetModelName, ChildModel.DotNetModelName, "{", "}");
                            sb.AppendLine("\t\t");
                        }
                    }


                    sb.Append("\r\n\t\t#endregion");
                    sw.WriteLine(sb.ToString());
                    #endregion

                    #region Close file
                    if (sw != null)
                    {
                        sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                    if (pTableModel.IsParentTable && pTableModel.ChildTableModelList != null && pTableModel.ChildTableModelList.Count > 0)
                    {
                        foreach (var ChildModel in pTableModel.ChildTableModelList)
                        {
                            GenerateModelClass(ChildModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GenerateInterfaceClass(TableModel pTableModel)
        {
            try
            {
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(ModelInterfaceFolder + pTableModel.DotNetInterfaceName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion

                    #region Get Table Name, Attributes Name and Attribute Types

                    #endregion

                    #region Write Namespaces
                    this.WriteInterfaceNamespaces(sw, pTableModel);
                    #endregion

                    #region Write Class Default Constructor
                    //this.WriteDefaultConstructor(sw, ClassName);
                    #endregion



                    #region Write Public Properties
                    sb = new System.Text.StringBuilder("\r\n\t");
                    sb.Append("#region Public Properties");
                    foreach (var column in pTableModel.PropetyList)
                    {
                        if (!column.IsSkippable)
                        {
                            this.WriteInterfaceProperties(sb, column.SYSType, column.SYSName);
                        }
                    }
                    sb.Append("\r\n\t#endregion");
                    sw.WriteLine(sb.ToString());
                    #endregion

                    #region Close file
                    if (sw != null)
                    {
                        sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void WriteNamespaces(StreamWriter sw, string tstrClassName, string NamespaceName, string ClassName)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("using System;");
                sb.Append("\r\nusing System.Collections.Generic;");
                sb.Append("\r\nusing System.Text;");
                sb.Append("\r\nusing System.Data;");
                sb.Append("\r\nusing System.Data.Common;");
                sb.Append("\r\nusing Microsoft.Practices.EnterpriseLibrary.Data;");
                sb.Append("\r\nusing BLERP.Utility;");
                if (strTable.Contains("HRM"))
                {
                    sb.Append("\r\nusing BLERP.Model.HR;");
                    sb.Append("\r\n\r\nnamespace BLERP.BLL.HR");
                }
                else if (strTable.Contains("CMN"))
                {
                    sb.Append("\r\nusing BLERP.Model.Common;");
                    sb.Append("\r\n\r\nnamespace BLERP.BLL.Common");
                }
                sb.Append("\r\n{");
                sb.Append("\r\n\tpublic class ");
                sb.Append(TitleCaseString(strTable) + "BLL");
                sb.Append("\r\n\t{");
                sw.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void WriteDataContextNamespaces(StreamWriter sw, TableModel pTableModel)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("using System;");
                sb.Append("\r\nusing System;                         ");
                sb.Append("\r\nusing System.Collections.Generic;                    ");
                sb.Append("\r\nusing System.Text;                                   ");
                sb.Append("\r\nusing System.Data;                                   ");
                sb.Append("\r\nusing System.Data.Common;                            ");
                sb.Append("\r\nusing Microsoft.Practices.EnterpriseLibrary.Data;    ");
                sb.Append("\r\nusing " + NameSpaceFirstPart + ".Common.Interfaces;                       ");
                if (IsModelInterfaceRequired)
                    sb.Append("\r\nusing " + NameSpaceFirstPart + "." + ModuleName + ".Interfaces;                       ");
                sb.Append("\r\nusing " + NameSpaceFirstPart + "." + ModuleName + ".Model;                            ");
                if (strTable.Contains("HRM"))
                {
                    sb.Append("\r\nusing " + NameSpaceFirstPart + "." + ModuleName + ".Model;");
                    sb.Append("\r\n\r\nnamespace BLERP.BLL.HR");
                }
                else if (strTable.Contains("CMN"))
                {
                    sb.Append("\r\nusing " + NameSpaceFirstPart + "Common.Model;");
                    sb.Append("\r\n\r\nnamespace " + NameSpaceFirstPart + ".Common.BLL");
                }
                else
                {
                    sb.Append("\r\n namespace " + NameSpaceFirstPart + "." + ModuleName + ".DataContext");
                }
                sb.Append("\r\n{");
                sb.Append("\r\n\tpublic class ");
                sb.Append(pTableModel.DotNetDataContextName);
                sb.Append("\r\n\t{");
                sw.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void WriteControllerNamespaces(StreamWriter sw, TableModel pTableModel)
        {

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("using System;");

                sb.Append("\r\nusing System.Collections.Generic;");
                sb.Append("\r\nusing System.Linq;                              ");
                sb.Append("\r\nusing System.Web;                              ");
                sb.Append("\r\nusing System.Web.Mvc;                       ");
                sb.Append("\r\nusing " + NameSpaceFirstPart + "." + ModuleName + ".BLL;                       ");

                if (IsBLLInterfaceRequired)
                    sb.Append("\r\nusing " + NameSpaceFirstPart + "." + ModuleName + ".IBLL;");
                if (IsModelInterfaceRequired)
                {
                    sb.Append("\r\nusing " + NameSpaceFirstPart + ".Common.Interfaces;                  ");
                    sb.Append("\r\nusing " + NameSpaceFirstPart + "." + ModuleName + ".Interfaces;                  ");
                }
                else
                {
                    sb.Append("\r\nusing " + NameSpaceFirstPart + ".Common.Model;                  ");
                    sb.Append("\r\nusing " + NameSpaceFirstPart + "." + ModuleName + ".Model;                  ");
                }
                sb.Append("\r\n");
                sb.Append("\r\n");

                //sb.Append("\r\nnamespace " + NameSpaceFirstPart + "." + ModuleName + ".Controllers");
                sb.Append("\r\nnamespace ChamberAutomationProject.Controllers");

                //sb.Append("\r\n\r\nnamespace BLERP.BLL.Common");

                sb.Append("\r\n{");
                sb.Append("\r\n\t[Authorize]");
                sb.Append("\r\n\t[CheckUserPermissions]");
                sb.Append("\r\n\tpublic class ");
                sb.Append(pTableModel.ControllerName + " : " + "BaseController");
                sb.Append("\r\n\t{");
                sw.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void WriteBLLNamespaces(StreamWriter sw, TableModel pTableModel)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("using System;");

                sb.Append("\r\nusing System.Collections.Generic;");
                sb.Append("\r\nusing System.Text;                              ");
                sb.Append("\r\nusing System.Data;                              ");
                sb.Append("\r\nusing System.Data.Common;                       ");
                sb.Append("\r\nusing Microsoft.Practices.EnterpriseLibrary.Data;");
                sb.Append("\r\nusing " + NameSpaceFirstPart + "." + ModuleName + ".DataContext;");
                if (IsBLLInterfaceRequired)
                    sb.Append("\r\nusing " + NameSpaceFirstPart + "." + ModuleName + ".IBLL;");
                if (IsModelInterfaceRequired)
                {
                    sb.Append("\r\nusing " + NameSpaceFirstPart + ".Common.Interfaces;                  ");
                    sb.Append("\r\nusing " + NameSpaceFirstPart + "." + ModuleName + ".Interfaces;                  ");
                }
                else
                {
                    sb.Append("\r\nusing " + NameSpaceFirstPart + ".Common.Model;                  ");
                    sb.Append("\r\nusing " + NameSpaceFirstPart + "." + ModuleName + ".Model;                  ");
                }
                sb.Append("\r\n");
                sb.Append("\r\n");
                if (pTableModel.DotNetModelName.Contains("HRM"))
                {
                    sb.Append("\r\nnamespace " + NameSpaceFirstPart + "." + ModuleName + ".Model");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.HR");
                }
                else if (pTableModel.DotNetModelName.Contains("CMN"))
                {
                    sb.Append("\r\nnamespace " + NameSpaceFirstPart + ".Model.Common");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.Common");
                }
                else
                {
                    sb.Append("\r\nnamespace " + NameSpaceFirstPart + "." + ModuleName + ".BLL");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.Common");
                }
                sb.Append("\r\n{");
                sb.Append("\r\n\tpublic class ");
                sb.Append(pTableModel.DotNetBLLName + (!IsBLLInterfaceRequired ? " " : (" : " + pTableModel.DotNetIBLLIntName)));
                sb.Append("\r\n\t{");
                sw.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void WriteIBLLNamespaces(StreamWriter sw, TableModel pTableModel)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("using System;");

                sb.Append("\r\nusing System.Collections.Generic;");
                sb.Append("\r\nusing System.Text;                              ");
                sb.Append("\r\nusing System.Data;                              ");
                sb.Append("\r\nusing System.Data.Common;                       ");
                sb.Append("\r\nusing Microsoft.Practices.EnterpriseLibrary.Data;");

                if (IsModelInterfaceRequired)
                {
                    sb.Append("\r\nusing " + NameSpaceFirstPart + ".Common.Interfaces;                  ");
                    sb.Append("\r\nusing " + NameSpaceFirstPart + "." + ModuleName + ".Interfaces;                  ");
                }
                else
                {
                    sb.Append("\r\nusing " + NameSpaceFirstPart + "." + ModuleName + ".Model;                  ");
                    sb.Append("\r\nusing " + NameSpaceFirstPart + ".Common.Model;");
                }
                sb.Append("\r\n");
                sb.Append("\r\n");
                if (pTableModel.DotNetModelName.Contains("HRM"))
                {
                    sb.Append("\r\nnamespace " + NameSpaceFirstPart + "." + ModuleName + ".Model");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.HR");
                }
                else if (pTableModel.DotNetModelName.Contains("CMN"))
                {
                    sb.Append("\r\nnamespace " + NameSpaceFirstPart + ".Model.Common");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.Common");
                }
                else
                {
                    sb.Append("\r\nnamespace " + NameSpaceFirstPart + "." + ModuleName + ".IBLL");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.Common");
                }
                sb.Append("\r\n{");
                sb.Append("\r\n\tpublic interface ");
                sb.Append(pTableModel.DotNetIBLLIntName);
                sb.Append("\r\n\t{");
                sw.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void WriteModelNamespaces(StreamWriter sw, TableModel pTableModel)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("using System;");

                sb.Append("\r\nusing System.Collections.Generic; ");
                sb.Append("\r\nusing System.Text; ");
                sb.Append("\r\nusing System.Linq; ");
                sb.Append("\r\nusing " + NameSpaceFirstPart + ".Common.Interfaces; ");
                sb.Append("\r\nusing " + NameSpaceFirstPart + ".Common.Model; ");
                if (IsModelInterfaceRequired)
                {
                    sb.Append("\r\nusing " + NameSpaceFirstPart + "." + ModuleName + ".Interfaces; ");
                }
                sb.Append("\r\nusing System.ComponentModel.DataAnnotations;");

                sb.Append("\r\n");
                sb.Append("\r\n");
                if (pTableModel.DotNetModelName.Contains("HRM"))
                {
                    sb.Append("\r\nnamespace " + NameSpaceFirstPart + "." + ModuleName + ".Model");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.HR");
                }
                else if (pTableModel.DotNetModelName.Contains("CMN"))
                {
                    sb.Append("\r\nnamespace " + NameSpaceFirstPart + ".Model.Common");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.Common");
                }
                else
                {
                    sb.Append("\r\nnamespace " + NameSpaceFirstPart + "." + ModuleName + ".Model");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.Common");
                }
                sb.Append("\r\n{");
                sb.Append("\r\n\tpublic class ");
                sb.Append(pTableModel.DotNetModelName + ": BaseModel" + (IsModelInterfaceRequired ? (", " + pTableModel.DotNetInterfaceName) : ""));
                sb.Append("\r\n\t{");
                sw.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void WriteInterfaceNamespaces(StreamWriter sw, TableModel pTableModel)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("using System;");

                sb.Append("\r\nusing System.Collections.Generic;");
                sb.Append("\r\nusing System.Text;");
                sb.Append("\r\nusing System.Linq;");
                sb.Append("\r\nusing " + NameSpaceFirstPart + ".Common.Interfaces;");


                sb.Append("\r\n");
                sb.Append("\r\n");
                if (pTableModel.DotNetModelName.Contains("HRM"))
                {
                    sb.Append("\r\nnamespace " + NameSpaceFirstPart + "." + ModuleName + ".Model");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.HR");
                }
                else if (pTableModel.DotNetModelName.Contains("CMN"))
                {
                    sb.Append("\r\nnamespace " + NameSpaceFirstPart + ".Model.Common");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.Common");
                }
                else
                {
                    sb.Append("\r\nnamespace " + NameSpaceFirstPart + "." + ModuleName + ".Interfaces");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.Common");
                }
                sb.Append("\r\n{");
                sb.Append("\r\n\tpublic interface ");
                sb.Append(pTableModel.DotNetInterfaceName + " : IBase");
                sb.Append("\r\n\t{");
                sw.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Default Constructor
        private void WriteDefaultConstructor(StreamWriter sw, string ClassName)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\n\t\t#region Constructor");
                sb.Append("\r\n\t\tpublic ");
                sb.Append(ClassName);
                sb.Append("()\r\n\t\t{}");
                sb.Append("\r\n\t\t#endregion");
                sw.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //PrivateVariables
        private void WritePrivateVariables(StringBuilder sb, string tstrAttributeType_DotNet, string tstrAttributeName)
        {
            try
            {
                sb.Append("\r\n\t");
                sb.AppendFormat("private {0} _{1};", new object[] { tstrAttributeType_DotNet, tstrAttributeName, "{", "}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //PublicProperties
        private void WritePublicProperties(StringBuilder sb, string AttributeDotNetType, string AttributeDotNameName)
        {
            try
            {
                sb.Append("\r\n\t");
                //sb.AppendFormat("public {0} {1}\r\n\t{2} \r\n\t\tget {2} return _{1}; {3}\r\n\t\tset {2} _{1} = value; {3}\r\n\t{3}", new object[] { tstrAttributeType_DotNet, tstrAttributeName, "{", "}" });
                sb.AppendFormat("public {0} {1} {2} get; set; {3}", AttributeDotNetType, AttributeDotNameName, "{", "}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void WritePublicProperties(StringBuilder sb, ColumnModel pColumn)
        {
            const string dq = @"""";
            try
            {
                if (pColumn.IsPrimayKey)
                {
                    sb.AppendLine("\t\t[Key]");
                }
                if (!pColumn.IsNullable)
                {
                    sb.AppendLine("\t\t[Required]");
                }
                if (pColumn.SYSType.ToLower() == "string")
                {
                    sb.AppendLine("\t\t[StringLength(" + pColumn.DBLength + ", ErrorMessage = " + dq + "Maximum characters limit to " + pColumn.DBLength + dq + ")]");

                    //sb.AppendFormat("\r\n\t\t[Range(1,{0})]", pColumn.DBLength);
                }

                if (pColumn.SYSType.ToLower().Contains("int") && !pColumn.IsNullable)
                {
                    sb.AppendLine("\t\t[Range(1," + "9".PadLeft(Convert.ToInt32(pColumn.DBLength), '9') + ", ErrorMessage =" + dq + "Value must be less tha or equal to " + "9".PadLeft(Convert.ToInt32(pColumn.DBLength), '9') + dq + ")]");
                }

                if (pColumn.SYSType.ToLower().Contains("int") && pColumn.IsNullable)
                {
                    sb.AppendLine("\t\t[Range(0," + "9".PadLeft(Convert.ToInt32(pColumn.DBLength), '9') + ", ErrorMessage =" + dq + "Value must be less tha or equal to " + "9".PadLeft(Convert.ToInt32(pColumn.DBLength), '9') + dq + ")]");
                }

                sb.AppendLine("\t\t[Display(Name = " + dq + pColumn.DisplayName + dq + ")]");
                //sb.AppendFormat("public {0} {1}\r\n\t{2} \r\n\t\tget {2} return _{1}; {3}\r\n\t\tset {2} _{1} = value; {3}\r\n\t{3}", new object[] { tstrAttributeType_DotNet, tstrAttributeName, "{", "}" });
                sb.AppendFormat("\t\tpublic {0} {1} {2} get; set; {3}", pColumn.SYSType, pColumn.SYSName, "{", "}");
                sb.AppendLine("\t\t");
                sb.AppendLine();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void WriteInterfaceProperties(StringBuilder sb, string AttributeDotNetType, string AttributeDotNameName)
        {
            try
            {
                sb.Append("\r\n\t");
                //sb.AppendFormat("public {0} {1}\r\n\t{2} \r\n\t\tget {2} return _{1}; {3}\r\n\t\tset {2} _{1} = value; {3}\r\n\t{3}", new object[] { tstrAttributeType_DotNet, tstrAttributeName, "{", "}" });
                sb.AppendFormat(" {0} {1} {2} get; set; {3}", AttributeDotNetType, AttributeDotNameName, "{", "}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Methods for BusinessLogicLayer
        private void WriteSelectMethod_forBLL(StringBuilder sb, ArrayList AttributeNameArrayList)
        {
            try
            {

                string strParameter = string.Empty;
                string AttributeName = string.Empty;
                string objName = "objcls" + strTable;

                AttributeName = AttributeNameArrayList[0].ToString();
                strParameter = strParameter + "\r\n\t\t\t" + objName + "." + AttributeName + " = " + AttributeName + ";";

                sb.Append("\r\n\tpublic DataTable Select()");
                sb.Append("\r\n\t{");
                sb.Append("\r\n\t\tDataTable dt;");
                sb.Append("\r\n\t\ttry");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\t" + "objcls" + strTable + " = new " + "cls" + strTable + "();");
                sb.Append("\r\n\t\t\t" + strParameter);
                sb.Append("\r\n\t\t");
                sb.Append("\r\n\t\t\tdt = objcls" + strTable + ".Select();");
                sb.Append("\r\n\t\t\treturn dt;");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t\tcatch(Exception ex)");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\tthrow new Exception(ex.Message);");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t}");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void WriteInsertMethod_forBLL(StringBuilder sb, ArrayList AttributeNameArrayList)
        {
            try
            {

                string strParameter = string.Empty;
                string AttributeName = string.Empty;
                string objName = "objcls" + strTable;

                for (int i = 1; i < AttributeNameArrayList.Count; i++)
                {
                    AttributeName = AttributeNameArrayList[i].ToString();
                    strParameter = strParameter + "\r\n\t\t\t" + objName + "." + AttributeName + " = " + AttributeName + ";";
                }


                sb.Append("\r\n\tpublic bool Insert()");
                sb.Append("\r\n\t{");
                sb.Append("\r\n\t\ttry");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\t" + "objcls" + strTable + " = new " + "cls" + strTable + "();");
                sb.Append("\r\n\t\t\t" + strParameter);
                sb.Append("\r\n\t\t");
                sb.Append("\r\n\t\t\tif(objcls" + strTable + ".Insert())");
                sb.Append("\r\n\t\t\t{");
                sb.Append("\r\n\t\t\t\treturn true;");
                sb.Append("\r\n\t\t\t}");
                sb.Append("\r\n\t\t\treturn false;");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t\tcatch(Exception ex)");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\tthrow new Exception(ex.Message);");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void WriteUpdateMethod_forBLL(StringBuilder sb, ArrayList AttributeNameArrayList)
        {
            try
            {

                string AttributeName = string.Empty;
                string strParameter = string.Empty;
                string objName = "objcls" + strTable;


                for (int i = 0; i < AttributeNameArrayList.Count; i++)
                {
                    AttributeName = AttributeNameArrayList[i].ToString();
                    strParameter = strParameter + "\r\n\t\t\t" + objName + "." + AttributeName + " = " + AttributeName + ";";
                }

                sb.Append("\r\n\tpublic bool Update()");
                sb.Append("\r\n\t{");
                sb.Append("\r\n\t\ttry");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\t" + "objcls" + strTable + " = new " + "cls" + strTable + "();");
                sb.Append("\r\n\t\t\t" + strParameter);
                sb.Append("\r\n\t\t");
                sb.Append("\r\n\t\t\tif(objcls" + strTable + ".Update())");
                sb.Append("\r\n\t\t\t{");
                sb.Append("\r\n\t\t\t\treturn true;");
                sb.Append("\r\n\t\t\t}");
                sb.Append("\r\n\t\t\treturn false;");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t\tcatch(Exception ex)");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\tthrow new Exception(ex.Message);");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void WriteDeleteMethod_forBLL(StringBuilder sb, ArrayList AttributeNameArrayList)
        {
            try
            {
                string strParameter = string.Empty;
                string AttributeName = string.Empty;
                string objName = "objcls" + strTable;

                AttributeName = AttributeNameArrayList[0].ToString();
                strParameter = strParameter + "\r\n\t\t\t" + objName + "." + AttributeName + " = " + AttributeName + ";";

                sb.Append("\r\n\tpublic bool Delete()");
                sb.Append("\r\n\t{");
                sb.Append("\r\n\t\ttry");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\t" + "objcls" + strTable + " = new " + "cls" + strTable + "();");
                sb.Append("\r\n\t\t\t" + strParameter);
                sb.Append("\r\n\t\t");
                sb.Append("\r\n\t\t\tif(objcls" + strTable + ".Delete())");
                sb.Append("\r\n\t\t\t{");
                sb.Append("\r\n\t\t\t\treturn true;");
                sb.Append("\r\n\t\t\t}");
                sb.Append("\r\n\t\t\treturn false;");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t\tcatch(Exception ex)");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\tthrow new Exception(ex.Message);");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Methods for DataAccessLayer

        private void DAL_BuildModelMethod(StringBuilder sb, TableModel pTableModel)
        {
            try
            {

                string AttributeName = string.Empty;
                string AttributeTypeSQL = string.Empty;
                string strParameter = string.Empty;
                string strModelAttributes = string.Empty;
                string temp;
                string temp2 = Environment.NewLine;
                string strBlankSpace = string.Empty;
                const string consTemp = @"@P_";
                const string dq = @"""";
                //sp Name
                //string spName = "FSP_" + strTable + "_GK";
                //spName = dq + spName + dq;

                //Model build
                foreach (ColumnModel property in pTableModel.PropetyList)
                {
                    if (!property.IsSkippable)
                    {
                        strModelAttributes = strModelAttributes + "\r\n\t\t\t\tcase " + dq + property.SYSName + dq + " :";
                        //strModelAttributes = strModelAttributes + "\r\n\t\t\t\tif(!string.IsNullOrEmpty(pDataReader[" + dq + AttributeNameArrayList[i].ToString() + dq + "].ToString()))";
                        strModelAttributes = strModelAttributes + "\t\t\t\t\t" + PrepareModelAttributeFromDB(property, pTableModel.DotNetModelName);
                        strModelAttributes = strModelAttributes + "\r\n\t\t\t\tbreak;";
                    }
                }
                strModelAttributes = strModelAttributes + "\r\n\t\t\t\tdefault:";
                strModelAttributes = strModelAttributes + "\r\n\t\t\t\t\tbreak;";


                sb.Append("\r\n\tprivate static " + (IsModelInterfaceRequired ? pTableModel.DotNetInterfaceName : pTableModel.DotNetModelName) + " Build" + pTableModel.DotNetModelName + "(" + (IsModelInterfaceRequired ? pTableModel.DotNetInterfaceName : pTableModel.DotNetModelName) + " obj" + pTableModel.DotNetModelName + ",IDataReader dr)");
                sb.Append("\r\n\t{");
                sb.Append("\r\n\t\tDataTable dt = dr.GetSchemaTable();");
                sb.Append("\r\n\t\tforeach (DataRow dRow in dt.Rows)");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\tstring col = dRow.ItemArray[0].ToString();");
                sb.Append("\r\n\t\t\tswitch (col)");
                sb.Append("\r\n\t\t\t{");

                sb.Append("\r\n\t\t\t\t" + strModelAttributes);
                sb.Append("\r\n\t\t\t}");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\treturn obj" + pTableModel.DotNetModelName + ";");

                sb.Append("\r\n\t}");

                if (pTableModel.ChildTableModelList != null && pTableModel.ChildTableModelList.Count > 0)
                {
                    foreach (var pChild in pTableModel.ChildTableModelList)
                    {

                        DAL_BuildModelMethod(sb, pChild);

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DAL_GK(StringBuilder sb, TableModel pTableModel, string DCMethodSignature = "", string SPInSignature = "", Boolean pIsChild = false)
        {
            try
            {

                string AttributeName = string.Empty;
                string AttributeTypeSQL = string.Empty;
                string strParameter = string.Empty;
                string strModelAttributes = string.Empty;
                string temp;
                string temp2 = Environment.NewLine;
                string strBlankSpace = string.Empty;
                const string consTemp = @"@P_";
                const string dq = @"""";
                //sp Name
                string spName = "FSP_" + pTableModel.OriginalTableName + "_GK";
                spName = dq + spName + dq;
                //string DCMethodSignature = string.Empty;
                //string SPInSignature = string.Empty;
                string ChildMethodSignatureWitoutType = string.Empty;
                if (!pIsChild)
                {

                    var CompositeProps = pTableModel.PropetyList.FindAll(p => p.IsPrimayKey);
                    if (CompositeProps != null && CompositeProps.Count == 1)
                    {
                        DCMethodSignature = CompositeProps[0].SYSType + " p" + CompositeProps[0].SYSName;
                        SPInSignature = PrepareInParameter(CompositeProps[0], " obj" + pTableModel.DotNetModelName).Replace(("obj" + pTableModel.DotNetModelName + "."), "p");
                        ChildMethodSignatureWitoutType = " p" + CompositeProps[0].SYSName;
                    }
                    else if (CompositeProps != null && CompositeProps.Count > 1)
                    {
                        foreach (var prop in CompositeProps)
                        {
                            DCMethodSignature = DCMethodSignature + prop.SYSType + " p" + prop.SYSName + ", ";
                            SPInSignature = SPInSignature + PrepareInParameter(prop, " obj" + pTableModel.DotNetModelName).Replace(("obj" + pTableModel.DotNetModelName + "."), "p");
                            ChildMethodSignatureWitoutType = ChildMethodSignatureWitoutType + " p" + CompositeProps[0].SYSName + ", ";
                        }
                        DCMethodSignature = DCMethodSignature.Substring(0, DCMethodSignature.Length - 2);
                        ChildMethodSignatureWitoutType = ChildMethodSignatureWitoutType.Substring(0, ChildMethodSignatureWitoutType.Length - 2);
                    }
                    else { }

                }
                //Model build
                foreach (var property in pTableModel.PropetyList)
                {
                    if (!property.IsSkippable)
                    {
                        strModelAttributes += PrepareModelAttributeFromDB(property, pTableModel.TableNameAsTitle);
                    }
                }
                if (!pIsChild)
                    sb.Append("\r\n\tpublic static " + pTableModel.DotNetModelName + " Get" + pTableModel.TableNameAsTitle + "ById(" + DCMethodSignature + ")");
                else
                    sb.Append("\r\n\tpublic static " + pTableModel.DotNetModelName + " Get" + pTableModel.TableNameAsTitle + "ByParentId(" + DCMethodSignature + ")");
                sb.Append("\r\n\t{");
                sb.Append("\r\n\t\t" + pTableModel.DotNetModelName + " obj" + pTableModel.DotNetModelName + " = new " + pTableModel.DotNetModelName + "();");
                sb.Append("\r\n\t\tDatabase db = DatabaseFactory.CreateDatabase(ConnectionString);");
                sb.Append("\r\n\t\tstring sqlCommand = " + spName + ";");
                sb.Append("\r\n\t\tDbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);");
                sb.Append(SPInSignature);
                sb.Append("\r\n\t\ttry");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\tIDataReader dr = db.ExecuteReader(dbCommand);");
                sb.Append("\r\n\t\tif (dr.Read())");
                sb.Append("\r\n\t\t{");
                //sb.Append("\r\n\t\t\t" + strModelAttributes + "\r\n\t\t\t");

                sb.AppendLine("			 Build" + pTableModel.DotNetModelName + "(obj" + pTableModel.DotNetModelName + ", dr);                          ");

                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t\t");
                sb.AppendLine("\r\n\t\tdr.Close();");
                if (pTableModel.IsParentTable && pTableModel.ChildTableModelList != null && pTableModel.ChildTableModelList.Count > 0)
                {
                    foreach (var ChildModel in pTableModel.ChildTableModelList)
                    {
                        sb.AppendLine("			 obj" + pTableModel.DotNetModelName + ".obj" + ChildModel.DotNetModelName + "List = Get" + ChildModel.TableNameAsTitle + "ByParentId(" + ChildMethodSignatureWitoutType + ");");
                    }
                }
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t\tcatch(Exception ex)");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\tthrow new Exception(ex.Message);");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t\treturn obj" + pTableModel.DotNetModelName + ";");
                sb.Append("\r\n\t}");

                if (pTableModel.IsParentTable && pTableModel.ChildTableModelList != null && pTableModel.ChildTableModelList.Count > 0)
                {
                    foreach (var ChildModel in pTableModel.ChildTableModelList)
                    {

                        DAL_GParent(sb, ChildModel, DCMethodSignature, SPInSignature, true);
                        DAL_GK(sb, ChildModel);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DAL_BySearch(StringBuilder sb, TableModel pTableModel)
        {
            try
            {

                string AttributeName = string.Empty;
                string AttributeTypeSQL = string.Empty;
                string strParameter = string.Empty;
                string strModelAttributes = string.Empty;
                string temp;
                string temp2 = Environment.NewLine;
                string strBlankSpace = string.Empty;
                const string consTemp = @"@P_";
                const string dq = @"""";
                //sp Name
                string spName = "FSP_" + pTableModel.OriginalTableName + "_SEARCH";
                spName = dq + spName + dq;

                string strInParameterList = string.Empty;

                foreach (var column in pTableModel.PropetyList)
                {
                    if (!column.IsSkippable)
                    {
                        strInParameterList += PrepareInParameter(column, "obj" + pTableModel.TableNameAsTitle);
                    }
                }


                //Model build
                foreach (var property in pTableModel.PropetyList)
                {
                    if (!property.IsSkippable)
                    {
                        strModelAttributes += PrepareModelAttributeFromDB(property, "obj" + pTableModel.DotNetModelName);
                    }
                }

                sb.Append("\r\n\tpublic static List<" + pTableModel.DotNetModelName + "> Get" + pTableModel.TableNameAsTitle + "BySearch(" + pTableModel.DotNetModelName + " obj" + pTableModel.TableNameAsTitle + ")");
                sb.Append("\r\n\t{");
                sb.Append("\r\n\t\tList<" + pTableModel.DotNetModelName + "> obj" + pTableModel.TableNameAsTitle + "List = new List<" + pTableModel.DotNetModelName + ">();");
                sb.Append("\r\n\t\tDatabase db = DatabaseFactory.CreateDatabase(ConnectionString);");
                sb.Append("\r\n\t\tstring sqlCommand = " + spName + ";");
                sb.Append("\r\n\t\tDbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);");
                sb.Append("\r\n\t\t\t\t" + strInParameterList);
                sb.Append("\r\n\t\ttry");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\tIDataReader dr = db.ExecuteReader(dbCommand);");
                sb.Append("\r\n\t\twhile (dr.Read())");
                sb.Append("\r\n\t\t{");

                sb.Append("\r\n\t\t\t " + pTableModel.DotNetModelName + " obj" + pTableModel.DotNetModelName + " = new " + pTableModel.DotNetModelName + "();");
                sb.Append("\r\n\t\t\t Build" + pTableModel.DotNetModelName + "(obj" + pTableModel.DotNetModelName + ", dr);");
                sb.Append("\r\n\t\t\t obj" + pTableModel.TableNameAsTitle + "List.Add(obj" + pTableModel.DotNetModelName + ");");

                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t\tcatch(Exception ex)");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\tthrow new Exception(ex.Message);");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t\treturn obj" + pTableModel.TableNameAsTitle + "List;");
                sb.Append("\r\n\t}");

                if (pTableModel.IsParentTable && pTableModel.ChildTableModelList != null && pTableModel.ChildTableModelList.Count > 0)
                {
                    foreach (var ChildModel in pTableModel.ChildTableModelList)
                    {

                        DAL_BySearch(sb, ChildModel);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void DAL_GA(StringBuilder sb, TableModel pTableModel)
        {
            const string dq = @"""";

            string spName = "FSP_" + pTableModel.OriginalTableName + "_GA";
            spName = dq + spName + dq;
            try
            {

                sb.AppendLine("       public static List<I[{MODEL}]>  Get[{MODEL}]ALL() ");
                sb.AppendLine("{                                                                  ");
                sb.AppendLine(" List<I[{MODEL}]> obj[{MODEL}]List = new List<I[{MODEL}]>();               ");
                sb.AppendLine("Database db = DatabaseFactory.CreateDatabase(ConnectionString); ");
                sb.AppendLine("string sqlCommand = " + spName + ";                           ");
                sb.AppendLine("DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);      ");
                sb.AppendLine("try                                                             ");
                sb.AppendLine("{                                                               ");
                sb.AppendLine("using (IDataReader dr = db.ExecuteReader(dbCommand))            ");
                sb.AppendLine("	{                                                           ");
                sb.AppendLine("		 while (dr.Read())                                      ");
                sb.AppendLine("			{                                                   ");
                sb.AppendLine("			I[{MODEL}] obj[{MODEL}] = new [{MODEL}]Model();                 ");
                sb.AppendLine("			 Build" + pTableModel.DotNetModelName + "(obj[{MODEL}], dr);                          ");
                sb.AppendLine("			 obj[{MODEL}]List.Add(obj[{MODEL}]);                        ");
                sb.AppendLine("			 }                                                  ");
                sb.AppendLine("		 }                                                      ");
                sb.AppendLine("}                                                               ");
                sb.AppendLine("catch(Exception ex)                                             ");
                sb.AppendLine("{                                                               ");
                sb.AppendLine("	throw new Exception(ex.Message);                            ");
                sb.AppendLine("}                                                               ");
                sb.AppendLine("return obj[{MODEL}]List;                                            ");
                sb.AppendLine("}                                                                  ");

                sb.Replace("[{MODEL}]", pTableModel.TableNameAsTitle);
                if (!IsModelInterfaceRequired)
                {
                    sb.Replace("I" + pTableModel.TableNameAsTitle, pTableModel.DotNetModelName);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DAL_GParent(StringBuilder sb, TableModel pTableModel, string DCMethodSignature = "", string SPInSignature = "", Boolean pIsChild = false)
        {
            const string dq = @"""";

            string spName = "FSP_" + pTableModel.OriginalTableName + "_GParent";
            spName = dq + spName + dq;
            try
            {

                sb.AppendLine("       public static List<I[{MODEL}]>  Get[{MODEL}]ByParentId(" + DCMethodSignature + ") ");
                sb.AppendLine("{                                                                  ");
                sb.AppendLine(" List<I[{MODEL}]> obj[{MODEL}]List = new List<I[{MODEL}]>();               ");
                sb.AppendLine("Database db = DatabaseFactory.CreateDatabase(ConnectionString); ");
                sb.AppendLine("string sqlCommand = " + spName + ";                           ");
                sb.AppendLine("DbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);      ");
                sb.AppendLine(SPInSignature);
                sb.AppendLine("try                                                             ");
                sb.AppendLine("{                                                               ");
                sb.AppendLine("using (IDataReader dr = db.ExecuteReader(dbCommand))            ");
                sb.AppendLine("	{                                                           ");
                sb.AppendLine("		 while (dr.Read())                                      ");
                sb.AppendLine("			{                                                   ");
                sb.AppendLine("			I[{MODEL}] obj[{MODEL}] = new [{MODEL}]Model();                 ");
                sb.AppendLine("			 Build" + pTableModel.DotNetModelName + "(obj[{MODEL}], dr);                          ");
                sb.AppendLine("			 obj[{MODEL}]List.Add(obj[{MODEL}]);                        ");
                sb.AppendLine("			 }                                                  ");
                sb.AppendLine("		 }                                                      ");
                sb.AppendLine("}                                                               ");
                sb.AppendLine("catch(Exception ex)                                             ");
                sb.AppendLine("{                                                               ");
                sb.AppendLine("	throw new Exception(ex.Message);                            ");
                sb.AppendLine("}                                                               ");
                sb.AppendLine("return obj[{MODEL}]List;                                            ");
                sb.AppendLine("}                                                                  ");

                sb.Replace("[{MODEL}]", pTableModel.TableNameAsTitle);
                if (!IsModelInterfaceRequired)
                {
                    sb.Replace("I" + pTableModel.TableNameAsTitle, pTableModel.DotNetModelName);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string PrepareModelAttributeFromDB(ColumnModel column, string p)
        {
            string strModelAttributes = string.Empty;
            switch (column.SYSType.Trim())
            {
                case "DateTime":
                case "DateTime?":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;
                case "Boolean":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;
                case "Bool":
                case "bool":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? false : Convert.ToBoolean(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;
                case "Int16?":
                case "short?":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? (short)0 : Convert.ToInt16(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;

                case "Int32?":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;
                case "decimal?":
                case "decimal":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? 0 : Convert.ToDecimal(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;
                case "int?":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? 0 : Convert.ToInt32(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;
                case "Int64?":
                case "long?":
                    {
                        strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? 0 : Convert.ToInt64(dr[""" + column.DBName + @"""].ToString());";
                    }
                    break;
                default:
                    { strModelAttributes = "\r\n\t\t\t\tobj" + p + "." + column.SYSName + @"= dr[""" + column.DBName + @"""].Equals(DBNull.Value) ? string.Empty : dr[""" + column.DBName + @"""].ToString();"; }
                    break;
            }


            return strModelAttributes;
        }

        private string PrepareValueFromDB(string pType, string pValueName)
        {
            string strModelAttributes = string.Empty;
            switch (pType)
            {
                case "DateTime?":
                    {
                        strModelAttributes = "Convert.ToDateTime(" + pValueName + ");";
                    }
                    break;

                case "Int16?":
                case "short?":
                    {
                        strModelAttributes = " Convert.ToInt16(" + pValueName + ");";
                    }
                    break;
                case "int?":
                case "Int32?":
                    {
                        strModelAttributes = " Convert.ToInt32(" + pValueName + ");";
                    }
                    break;
                case "decimal?":
                case "decimal":
                    {
                        strModelAttributes = " Convert.ToDecimal(" + pValueName + ");";
                    }
                    break;
                case "Int64?":
                case "long?":
                    {
                        strModelAttributes = " Convert.ToInt64(" + pValueName + ");";
                    }
                    break;
                default:
                    { strModelAttributes = pValueName; }
                    break;
            }


            return strModelAttributes;
        }

        private void WriteSelectMethod_forDAL(StringBuilder sb, ArrayList AttributeNameArrayList, ArrayList AttributeTypeArrayList_Sql)
        {
            try
            {

                string AttributeName = string.Empty;
                string AttributeTypeSQL = string.Empty;
                string strParameter = string.Empty;
                string temp;
                string temp2 = Environment.NewLine;
                string strBlankSpace = string.Empty;
                const string consTemp = @"@";
                const string dq = @"""";


                //sp Name
                string spName = "SP_" + strTable + "_Select";
                spName = dq + spName + dq;


                for (int i = 0; i < AttributeTypeArrayList_Sql.Count; i++)
                {
                    AttributeTypeSQL = AttributeTypeArrayList_Sql[i].ToString();
                    temp = dq + consTemp + AttributeTypeSQL + dq;

                    strParameter = strParameter + "\r\n\t\t\t\tnew SqlParameter(" + dq + consTemp + AttributeNameArrayList[i] + dq + "," + "SqlDbType." + AttributeTypeArrayList_Sql[i] + "),";
                }
                strParameter = strParameter.Substring(0, strParameter.Length - 1);



                //conditions
                for (int i = 0; i < AttributeTypeArrayList_Sql.Count; i++)
                {
                    if (AttributeTypeArrayList_Sql[i].ToString().Contains("varchar"))
                    {
                        strBlankSpace = dq + "" + dq;
                        temp2 = temp2 + "\r\n\t\t\t\tif (" + AttributeNameArrayList[i] + " != " + strBlankSpace + " && " + AttributeNameArrayList[i] + " != null)";
                        temp2 = temp2 + "\r\n\t\t\t\t{\r\n\t\t\t\t\tParams[" + i + "].Value = " + AttributeNameArrayList[i].ToString() + ";\r\n\t\t\t\t}";
                        temp2 = temp2 + "\r\n\t\t\t\telse";
                        temp2 = temp2 + "\r\n\t\t\t\t{\r\n\t\t\t\t\tParams[" + i + "].Value = DBNull.Value;\r\n\t\t\t\t}\r\n";
                    }
                    else if (AttributeTypeArrayList_Sql[i].ToString().Contains("int"))
                    {
                        temp2 = temp2 + "\r\n\t\t\t\tif (" + AttributeNameArrayList[i] + " != 0)";
                        temp2 = temp2 + "\r\n\t\t\t\t{\r\n\t\t\t\t\tParams[" + i + "].Value = " + AttributeNameArrayList[i].ToString() + ";\r\n\t\t\t\t}";
                        temp2 = temp2 + "\r\n\t\t\t\telse";
                        temp2 = temp2 + "\r\n\t\t\t\t{\r\n\t\t\t\t\tParams[" + i + "].Value = DBNull.Value;\r\n\t\t\t\t}\r\n";
                    }
                    else
                    {
                        temp2 = temp2 + "\r\n\t\t\t\tif (" + AttributeNameArrayList[i] + " != null)";
                        temp2 = temp2 + "\r\n\t\t\t\t{\r\n\t\t\t\t\tParams[" + i + "].Value = " + AttributeNameArrayList[i].ToString() + ";\r\n\t\t\t\t}";
                        temp2 = temp2 + "\r\n\t\t\t\telse";
                        temp2 = temp2 + "\r\n\t\t\t\t{\r\n\t\t\t\t\tParams[" + i + "].Value = DBNull.Value;\r\n\t\t\t\t}\r\n";
                    }

                }


                sb.Append("\r\n\tpublic DataTable Select()");
                sb.Append("\r\n\t{");
                sb.Append("\r\n\t\tDataSet ds;");
                sb.Append("\r\n\t\ttry");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\tSqlParameter[] Params = \r\n\t\t\t{ " + strParameter + " \r\n\t\t\t};");
                sb.Append("\r\n\t\t\t" + temp2 + "\r\n\t\t\t");
                sb.Append("\r\n\t\t\tds = SqlHelper.ExecuteDataset(ConnectionString, CommandType.StoredProcedure," + spName + ",Params);");
                sb.Append("\r\n\t\t\treturn ds.Tables[0];");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t\tcatch(Exception ex)");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\tthrow new Exception(ex.Message);");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void WriteInsertMethod_forDAL(StringBuilder sb, ArrayList AttributeNameArrayList)
        {
            try
            {

                //Inverted commas
                const string consTemp = @"@";
                const string dq = @"""";
                string temp;

                //sp Name
                string spName = "SP_" + strTable + "_Insert";
                spName = dq + spName + dq;


                string strParameter = string.Empty;
                string AttributeName = string.Empty;

                for (int i = 1; i <= AttributeNameArrayList.Count - 1; i++)
                {
                    AttributeName = AttributeNameArrayList[i].ToString();
                    temp = dq + consTemp + AttributeName + dq;

                    strParameter = strParameter + "\r\n\t\t\t\tnew SqlParameter(" + temp + "," + AttributeName + "),";
                }
                strParameter = strParameter.Substring(0, strParameter.Length - 1);


                sb.Append("\r\n\tpublic bool Insert()");
                sb.Append("\r\n\t{");
                sb.Append("\r\n\t\ttry");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\tSqlParameter[] Params = \r\n\t\t\t{ " + strParameter + " \r\n\t\t\t};");
                sb.Append("\r\n\t\t\tint result = SqlHelper.ExecuteNonQuery(Transaction, CommandType.StoredProcedure," + spName + ",Params);");
                sb.Append("\r\n\t\t\tif (result > 0)");
                sb.Append("\r\n\t\t\t{");
                sb.Append("\r\n\t\t\t\treturn true;");
                sb.Append("\r\n\t\t\t}");
                sb.Append("\r\n\t\t\treturn false;");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t\tcatch(Exception ex)");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\tthrow new Exception(ex.Message);");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void WriteUpdateMethod_forDAL(StringBuilder sb, ArrayList AttributeNameArrayList)
        {
            try
            {

                //Inverted commas
                const string consTemp = @"@";
                const string dq = @"""";
                string temp;

                //sp Name
                string spName = "SP_" + strTable + "_Update";
                spName = dq + spName + dq;

                string AttributeName = string.Empty;
                string strParameter = string.Empty;


                for (int i = 0; i <= AttributeNameArrayList.Count - 1; i++)
                {
                    AttributeName = AttributeNameArrayList[i].ToString();
                    temp = dq + consTemp + AttributeName + dq;

                    strParameter = strParameter + "\r\n\t\t\t\tnew SqlParameter(" + temp + "," + AttributeName + "),";
                }
                strParameter = strParameter.Substring(0, strParameter.Length - 1);


                sb.Append("\r\n\tpublic bool Update()");
                sb.Append("\r\n\t{");
                sb.Append("\r\n\t\ttry");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\tSqlParameter[] Params = \r\n\t\t\t{ " + strParameter + " \r\n\t\t\t};");
                sb.Append("\r\n\t\t\tint result = SqlHelper.ExecuteNonQuery(Transaction, CommandType.StoredProcedure," + spName + ",Params);");
                sb.Append("\r\n\t\t\tif (result > 0)");
                sb.Append("\r\n\t\t\t{");
                sb.Append("\r\n\t\t\t\treturn true;");
                sb.Append("\r\n\t\t\t}");
                sb.Append("\r\n\t\t\treturn false;");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t\tcatch(Exception ex)");
                sb.Append("\r\n\t\t{");
                sb.Append("\r\n\t\t\tthrow new Exception(ex.Message);");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void DAL_InsertUpdateMethod(StringBuilder sb, TableModel pTableModel, Boolean IsChild = false)
        {
            try
            {

                //Inverted commas
                const string consTemp = @"@P_";
                const string dq = @"""";
                string objName = "obj" + pTableModel.TableNameAsTitle;
                //sp Name
                string spName = "FSP_" + pTableModel.OriginalTableName + "_INSERT_UPDATE";
                spName = dq + spName + dq;

                string AttributeName = string.Empty;
                string strParameter = string.Empty;

                foreach (var column in pTableModel.PropetyList)
                {
                    strParameter += PrepareInParameter(column, "obj" + pTableModel.TableNameAsTitle);
                }
                //strParameter = strParameter.Substring(0, strParameter.Length - 1);
                var primaryColumn = pTableModel.PropetyList.FirstOrDefault(pr => pr.IsPrimayKey);
                if (IsChild)
                {
                    sb.Append("\r\n\tpublic static string Save" + pTableModel.TableNameAsTitle + "(" + (IsModelInterfaceRequired ? pTableModel.DotNetInterfaceName : pTableModel.DotNetModelName) + " " + objName + ", Database db,DbConnection dbConnection,DbCommand dbCommand,DbTransaction dbTransaction)");
                    sb.Append("\r\n\t\t{");
                    //sb.Append("\r\n\t\t\tstring sMessage = string.Empty;");
                    //sb.Append("\r\n\t\t" + @"Database db = DatabaseFactory.CreateDatabase(ConnectionString);");
                    sb.Append("\r\n\t\tstring sqlCommand = " + spName + ";");
                    sb.Append("\r\n\t\tdbCommand = db.GetStoredProcCommand(sqlCommand);");

                    //sb.Append("\r\n\t\tDbTransaction dbTransaction = null;");
                    sb.Append("\r\n\t\tusing (dbConnection)");
                    sb.Append("\r\n\t\t{");
                    //sb.Append("\r\n\t\t    dbConnection.Open();");
                    //sb.Append("\r\n\t\t    dbTransaction = dbConnection.BeginTransaction(IsolationLevel.Serializable);");

                }
                else
                {
                    sb.Append("\r\n\tpublic static string Save" + pTableModel.TableNameAsTitle + "(" + (IsModelInterfaceRequired ? pTableModel.DotNetInterfaceName : pTableModel.DotNetModelName) + " " + objName + ")");
                    sb.Append("\r\n\t\t{");
                    //sb.Append("\r\n\t\t\tstring sMessage = string.Empty;");
                    sb.Append("\r\n\t\t\t" + @"Database db = DatabaseFactory.CreateDatabase(ConnectionString);");
                    sb.Append("\r\n\t\t\tstring sqlCommand = " + spName + ";");
                    sb.Append("\r\n\t\t\tDbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);");

                    sb.Append("\r\n\t\t\tDbTransaction dbTransaction = null;");
                    sb.Append("\r\n\t\t\tusing (DbConnection dbConnection = db.CreateConnection())");
                    sb.Append("\r\n\t\t\t\t{");
                    sb.Append("\r\n\t\t\t\t\tdbConnection.Open();");
                    sb.Append("\r\n\t\t\t\t\tdbTransaction = dbConnection.BeginTransaction(IsolationLevel.Serializable);");
                }

                sb.Append("\r\n\t\t\t\t\t\ttry");
                sb.Append("\r\n\t\t\t\t\t\t{");
                if (IsChild)
                {
                    sb.Append("\r\n\t\t\t\t\t\tdbCommand.Parameters.Clear();");
                }
                sb.Append("\r\n\t\t\t\t\t\tDbParameter output = db.DbProviderFactory.CreateParameter();");
                sb.Append("\r\n\t\t\t\t\t\toutput.ParameterName = " + dq + consTemp + primaryColumn.DBName + dq + ";");
                sb.Append("\r\n\t\t\t\t\t\toutput.Size = " + primaryColumn.DBLength + ";");
                sb.Append("\r\n\t\t\t\t\t\toutput.Value = " + objName + "." + primaryColumn.SYSName + ";");
                sb.Append("\r\n\t\t\t\t\t\toutput.DbType = DbType." + primaryColumn.SYSType.Replace("?", "") + ";");
                sb.Append("\r\n\t\t\t\t\t\toutput.Direction = ParameterDirection.InputOutput;");
                sb.Append("\r\n\t\t\t\t\t\t\t");
                sb.Append("\r\n\t\t\t\t\t\tdbCommand.Parameters.Add(output);");



                //sb.Append("\r\n\t\t");
                //sb.Append("\r\n\t\ttry");
                //sb.Append("\r\n\t\t{");

                sb.Append("\r\n\t\t\t\t\t\t\t\t " + strParameter);
                sb.Append("\r\n\t\t\t\t\t\t\t\t ");
                sb.Append("\r\n\t\t\t\t\t\t\t\tint result = db.ExecuteNonQuery(dbCommand);");
                sb.Append("\r\n\t\t\t\t\t\t\t\tif (result > 0)");
                sb.Append("\r\n\t\t\t\t\t\t\t\t{");

                sb.Append("\r\n\t\t\t\t\t\t\t\tstring v" + primaryColumn.SYSName + " = dbCommand.Parameters[" + dq + consTemp + primaryColumn.DBName + dq + "].Value.ToString();");

                if (pTableModel.IsParentTable && pTableModel.ChildTableModelList != null && pTableModel.ChildTableModelList.Count > 0)
                {
                    foreach (var ChildModel in pTableModel.ChildTableModelList)
                    {

                        sb.Append("\r\n\t\t\t\t\t\t\t\t\tif (" + objName + ".obj" + ChildModel.DotNetModelName + "List != null && " + objName + ".obj" + ChildModel.DotNetModelName + "List.Count > 0)              ");
                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t{                                                                     ");
                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\tforeach(var obj" + ChildModel.DotNetModelName + " in " + objName + ".obj" + ChildModel.DotNetModelName + "List)");
                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t\t{");

                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t\tobj" + ChildModel.DotNetModelName + "." + primaryColumn.SYSName + " = " + PrepareValueFromDB(primaryColumn.SYSType, "v" + primaryColumn.SYSName));
                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t\tSave" + ChildModel.TableNameAsTitle + "(obj" + ChildModel.DotNetModelName + ",  db,dbConnection, dbCommand, dbTransaction);");
                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t\t}");
                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t}                                                                 ");
                    }
                }
                sb.Append("\r\n\t\t\t\t\t\t\t\t\tif (result > 0)                                                       ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t{                                                                     ");
                if (!IsChild)
                    sb.AppendLine("\t\t\t\t\t\t\t\t\t\tdbTransaction.Commit();                                           ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t\treturn vSAVESUCCESS; ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t}                                                                     ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\telse                                                                  ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t{                                                                     ");
                if (!IsChild)
                    sb.AppendLine("\r\n\t\t\t\t\t\t\t\t\t\t\tdbTransaction.Rollback();                                         ");
                sb.AppendLine("\r\n\t\t\t\t\t\t\t\t\t\t\treturn vSAVEFAIL;                                          ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t}                                                                     ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t}                                                                         ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t}                                                                         ");
                sb.Append("\r\n\t\t\t\t\t\t\t\tcatch (Exception ex)                                                      ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t{                                                                         ");
                if (!IsChild)
                    sb.AppendLine("\r\n\t\t\t\t\t\t\t\t\t\tdbTransaction.Rollback();                                             ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t\tthrow new Exception(ex.Message);                                      ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t}                                                                         ");
                if (!IsChild)
                {
                    sb.Append("\r\n\t\t\t\t\t\t\t\t\tfinally                                                                   ");
                    sb.Append("\r\n\t\t\t\t\t\t\t\t\t{                                                                         ");
                    sb.Append("\r\n\t\t\t\t\t\t\t\t\t\tif (dbConnection.State == ConnectionState.Open)                       ");
                    sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t{                                                                     ");
                    sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t\tdbConnection.Close();                                             ");
                    sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t}                                                                     ");
                    sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t}                                                                         ");
                }
                sb.Append("\r\n\t\t\t\t\t\t\t\treturn vSAVESUCCESS;}                                                                             ");
                sb.Append("\r\n\t\t\t\t\t\t\t}");


                if (pTableModel.IsParentTable && pTableModel.ChildTableModelList != null && pTableModel.ChildTableModelList.Count > 0)
                {
                    foreach (var ChildModel in pTableModel.ChildTableModelList)
                    {

                        DAL_InsertUpdateMethod(sb, ChildModel, true);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PrepareGridControls(StringBuilder sb, TableModel pTableModel)
        {
            try
            {

                //Inverted commas
                const string consTemp = @"@P_";
                const string dq = @"""";

                string AttributeName = string.Empty;
                string strParameter = string.Empty;

                foreach (var column in pTableModel.PropetyList)
                {
                    if (!column.IsSkippable)
                        strParameter += PrepareWebGridColumns(column, pTableModel);
                }
                strParameter = strParameter.TrimEnd().Substring(0, strParameter.TrimEnd().Length - 1);

                sb.Append(strParameter);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PrepareViewControls(StringBuilder sb, TableModel pTableModel)
        {
            try
            {

                //Inverted commas
                const string consTemp = @"@P_";
                const string dq = @"""";
                string objName = "obj" + pTableModel.TableNameAsTitle;
                //sp Name
                string spName = "FSP_" + pTableModel.OriginalTableName + "_INSERT_UPDATE";
                spName = dq + spName + dq;

                string AttributeName = string.Empty;
                string strParameter = string.Empty;

                foreach (var column in pTableModel.PropetyList)
                {
                    if (!column.IsSkippable)
                        strParameter += PrepareRazorViewControl(column);
                }
                sb.Append(strParameter);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string PrepareRazorViewControl(ColumnModel column)
        {
            string strParameter = string.Empty;
            const string dq = @"""";
            const string consTemp = @"@P_";
            string temp = dq + consTemp + column.DBName + dq;
            StringBuilder sbControl = new StringBuilder();
            sbControl.AppendLine("\t\t\t\t\t " + @"<div class=""form-group row"">");
            sbControl.AppendLine("\t\t\t\t\t  " + @"  @Html.LabelFor(model => model." + column.SYSName + @", htmlAttributes: new { @class = ""control-label col-md-4"" }) ");
            sbControl.AppendLine("\t\t\t\t\t     " + @"   <div class=""col-md-8""> ");
            sbControl.AppendLine("\t\t\t\t\t        " + @"    @Html.EditorFor(model => model." + column.SYSName + @", new { htmlAttributes = new { @class = ""form-control"" } }) ");
            sbControl.AppendLine("\t\t\t\t\t       " + @"     @Html.ValidationMessageFor(model => model." + column.SYSName + @", """", new { @class = ""text-danger"" }) ");
            sbControl.AppendLine("\t\t\t\t\t        </div> ");
            sbControl.AppendLine("\t\t\t\t\t    </div> ");

            strParameter = sbControl.ToString();

            return strParameter;

        }
        private string PrepareWebGridColumns(ColumnModel column, TableModel pTableModel)
        {
            string strParameter = string.Empty;
            const string dq = @"""";
            const string consTemp = @"@P_";
            string temp = dq + consTemp + column.DBName + dq;
            StringBuilder sbControl = new StringBuilder();

            if (column.IsPrimayKey)
                sbControl.AppendLine("\t\t\t\t\t grid.Column(" + dq + column.SYSName + dq + @", " + dq + pTableModel.TableNameAsTitle + dq + @", format: @<text> @Html.ActionLink((string)(@item." + column.SYSName + @"), ""Edit"", " + dq + pTableModel.TableNameAsTitle + dq + @", new { id = (string)(@item." + column.SYSName + ") }, null) </text>),");
            else
            {
                switch (column.SYSType.Trim())
                {
                    case "String":
                        sbControl.AppendLine("\t\t\t\t\t grid.Column(" + dq + column.SYSName + dq + @", " + dq + column.SYSName + dq + @"), ");
                        break;
                    case "Int16?":
                    case "Int32?":
                    case "Int64?":
                    case "Int?":
                    case "Int16":
                    case "Int32":
                    case "Int64":
                    case "Int":
                        sbControl.AppendLine("\t\t\t\t\t grid.Column(" + dq + column.SYSName + dq + @", " + dq + column.SYSName + dq + @", style: "" upass-number-display-btx"", format: (item) => string.Format(""{0:#0.00}"", item." + column.SYSName + ")), ");
                        break;
                }
            }
            strParameter = sbControl.ToString();

            return strParameter;

        }

        private string PrepareInParameter(ColumnModel column, string pObjectName)
        {
            string strParameter = string.Empty;
            const string dq = @"""";
            const string consTemp = @"@P_";
            string temp = dq + consTemp + column.DBName + dq;
            switch (column.SYSType.Trim())
            {
                case "String":
                    {
                        strParameter = strParameter + "\r\n\t\t\t if(string.IsNullOrEmpty(" + pObjectName + "." + column.SYSName + "))";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ", DBNull.Value );";
                        strParameter = strParameter + "\r\n\t\t\telse";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + "," + pObjectName + "." + column.SYSName + ");";

                    }
                    break;
                case "DateTime":
                    {
                        strParameter = strParameter + "\r\n\t\t\t if(" + pObjectName + "." + column.SYSName + " != DateTime.MinValue)";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + "," + pObjectName + "." + column.SYSName + ");";
                        strParameter = strParameter + "\r\n\t\t\telse";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ", DBNull.Value );";

                    }
                    break;
                case "Int16?":
                case "Int32?":
                case "Int64?":
                case "Int?":
                case "DateTime?":
                    {
                        strParameter = strParameter + "\r\n\t\t\t if(" + pObjectName + "." + column.SYSName + ".HasValue)";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + "," + pObjectName + "." + column.SYSName + ");";
                        strParameter = strParameter + "\r\n\t\t\telse";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ", DBNull.Value );";
                        strParameter = strParameter.Replace('?', ' ');
                    }
                    break;
                case "Guid?":
                    {
                        strParameter = strParameter + "\r\n\t\t\t if(" + pObjectName + "." + column.SYSName + ".HasValue)";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ",new Guid(" + pObjectName + "." + column.SYSName + "));";
                        strParameter = strParameter + "\r\n\t\t\telse";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ", DBNull.Value );";
                        strParameter = strParameter.Replace('?', ' ');
                    }
                    break;

                case "Guid":
                    {
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + ",new Guid(" + pObjectName + "." + column.SYSName + "));";
                    }
                    break;

                case "decimal?":
                    {
                        strParameter = strParameter + "\r\n\t\t\t if(" + pObjectName + "." + column.SYSName + ".HasValue)";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType.Decimal," + pObjectName + "." + column.SYSName + ");";
                        strParameter = strParameter + "\r\n\t\t\telse";
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType.Decimal" + ", DBNull.Value );";

                    }
                    break;

                case "decimal":
                    {
                        strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType.Decimal" + ",new Guid(" + pObjectName + "." + column.SYSName + "));";
                    }
                    break;

                default:
                    { strParameter = strParameter + "\r\n\t\t\t\tdb.AddInParameter(dbCommand," + temp + ",DbType." + column.SYSType + "," + pObjectName + "." + column.SYSName + ");"; }
                    break;
            }
            return strParameter;

        }

        private void DAL_DeleteMethod(StringBuilder sb, TableModel pTableModel, string DCMethodSignature = "", string SPInSignature = "", Boolean IsChild = false, string pMethodPartName = "")
        {
            try
            {
                ColumnModel PrimaryColumn = pTableModel.PropetyList.Find(p => p.IsPrimayKey == true);
                //Inverted commas
                const string consTemp = @"@P_";
                string temp = consTemp + PrimaryColumn.SYSName;
                const string dq = @"""";
                temp = dq + temp + dq;

                //sp Name
                string spName = "";
                if (IsChild)
                    spName = "FSP_" + pTableModel.OriginalTableName + "_DELETEByParentId";
                else
                    spName = "FSP_" + pTableModel.OriginalTableName + "_DELETE";
                spName = dq + spName + dq;

                if (IsChild)
                {
                    sb.Append("\r\n\tpublic static string Delete" + pMethodPartName + "ByParentId(" + (IsModelInterfaceRequired ? pTableModel.DotNetInterfaceName : pTableModel.DotNetModelName) + " obj" + pTableModel.DotNetModelName + ", Database db,DbConnection dbConnection,DbCommand dbCommand,DbTransaction dbTransaction)");
                    sb.Append("\r\n\t{");
                    //sb.Append("\r\n\t\tstring sMessage = string.Empty;");
                    sb.Append("\r\n\t\tstring sqlCommand = " + spName + ";");
                    sb.Append("\r\n\t\tdbCommand = db.GetStoredProcCommand(sqlCommand);");

                    sb.Append("\r\n\t\tusing (dbConnection)");
                    sb.Append("\r\n\t\t{");
                }
                else
                {
                    var CompositeProps = pTableModel.PropetyList.FindAll(p => p.IsPrimayKey);
                    if (CompositeProps != null && CompositeProps.Count == 1)
                    {
                        DCMethodSignature = CompositeProps[0].SYSType + " p" + CompositeProps[0].SYSName;
                        SPInSignature = PrepareInParameter(CompositeProps[0], " obj" + pTableModel.DotNetModelName);
                    }
                    else if (CompositeProps != null && CompositeProps.Count > 1)
                    {
                        foreach (var prop in CompositeProps)
                        {
                            DCMethodSignature = DCMethodSignature + prop.SYSType + " p" + prop.SYSName + ", ";
                            SPInSignature = SPInSignature + PrepareInParameter(prop, " obj" + pTableModel.DotNetModelName);
                        }
                        DCMethodSignature = DCMethodSignature.Substring(0, DCMethodSignature.Length - 2);
                    }
                    else { }

                    sb.Append("\r\n\tpublic static string Delete" + pTableModel.TableNameAsTitle + "(" + (IsModelInterfaceRequired ? pTableModel.DotNetInterfaceName : pTableModel.DotNetModelName) + " obj" + pTableModel.DotNetModelName + ")");
                    sb.Append("\r\n\t{");
                    //sb.Append("\r\n\t\tstring sMessage = string.Empty;");
                    sb.Append("\r\n\t\t" + @"Database db = DatabaseFactory.CreateDatabase(ConnectionString);");
                    sb.Append("\r\n\t\tstring sqlCommand = " + spName + ";");
                    sb.Append("\r\n\t\tDbCommand dbCommand = db.GetStoredProcCommand(sqlCommand);");

                    sb.Append("\r\n\t\tDbTransaction dbTransaction = null;");
                    sb.Append("\r\n\t\tusing (DbConnection dbConnection = db.CreateConnection())");
                    sb.Append("\r\n\t\t{");
                    sb.Append("\r\n\t\t    dbConnection.Open();");
                    sb.Append("\r\n\t\t    dbTransaction = dbConnection.BeginTransaction(IsolationLevel.Serializable);");
                }

                if (IsChild)
                {
                    sb.Append("\r\n\t\t dbCommand.Parameters.Clear();");
                }

                sb.Append(SPInSignature);
                var UpdateDate = pTableModel.PropetyList.Find(pk => pk.SYSName == "UpdateDate");
                if (UpdateDate != null)
                    sb.Append(PrepareInParameter(UpdateDate, " obj" + pTableModel.DotNetModelName));
                var UpdateUser = pTableModel.PropetyList.Find(pk => pk.SYSName == "UpdateUser");
                if (UpdateUser != null)
                    sb.Append(PrepareInParameter(UpdateUser, " obj" + pTableModel.DotNetModelName));

                sb.AppendLine("\r\n\t\ttry");
                sb.AppendLine("\r\n\t\t{");
                sb.AppendLine("string vMsg = string.Empty;");

                Boolean HasChild = false;
                if (pTableModel.IsParentTable && pTableModel.ChildTableModelList != null && pTableModel.ChildTableModelList.Count > 0)
                {
                    HasChild = true;
                    foreach (var ChildModel in pTableModel.ChildTableModelList)
                    {
                        sb.AppendLine("// ============== Delete All Child " + ChildModel.TableNameAsTitle + " ===================");
                        sb.AppendLine("\t\t\t       vMsg =    Delete" + ChildModel.TableNameAsTitle + "ByParentId(obj" + pTableModel.DotNetModelName + ",  db,dbConnection, dbCommand, dbTransaction);");
                    }
                }
                sb.AppendLine("\t\t\t int result = 0;");
                if (HasChild)
                    sb.AppendLine("\t\t\t  if (vMsg.ToUpper().Contains(" + dq + "SUCCESSFULLY" + dq + "))                                                       ");
                sb.AppendLine("\t\t\t result = db.ExecuteNonQuery(dbCommand);");
                sb.AppendLine("\t\t\tif (result > 0)");

                sb.AppendLine("\t\t\t  {                                                                     ");
                if (!IsChild)
                    sb.AppendLine("\t\t\t      dbTransaction.Commit();                                           ");
                sb.AppendLine(@"       return vDELETESUCCESS;                                                  ");
                sb.Append("\t\t\t  }                                                                     ");
                sb.Append("\t\t\t  else                                                                  ");
                sb.Append("\t\t\t  {                                                                     ");
                if (!IsChild)
                    sb.AppendLine("\t\t\t      dbTransaction.Rollback();                                         ");
                sb.AppendLine(@"      return vDELETEFAIL;                                          ");
                sb.AppendLine("\t\t\t  }                                                                     ");
                sb.AppendLine("\t\t\t             }                                                                         ");

                sb.AppendLine("\t\t\t             catch (Exception ex)                                                      ");
                sb.AppendLine("\t\t\t             {                                                                         ");
                if (!IsChild)
                    sb.AppendLine("\t\t\t                 dbTransaction.Rollback();                                             ");
                sb.AppendLine("\t\t\t                 throw new Exception(ex.Message);                                      ");
                sb.AppendLine("\t\t\t             }                                                                         ");
                if (!IsChild)
                {
                    sb.AppendLine("\t\t\t             finally                                                                   ");
                    sb.AppendLine("\t\t\t             {                                                                         ");
                    sb.AppendLine("\t\t\t                 if (dbConnection.State == ConnectionState.Open)                       ");
                    sb.AppendLine("\t\t\t                 {                                                                     ");
                    sb.AppendLine("\t\t\t                     dbConnection.Close();                                             ");
                    sb.AppendLine("\t\t\t                 }                                                                     ");
                    sb.AppendLine("\t\t\t             }                                                                         ");
                }
                sb.AppendLine("\t\t\t return vDELETESUCCESS;        }                                                                             ");
                sb.AppendLine("\t\t\t     }");

                if (!IsChild)
                {
                    if (pTableModel.IsParentTable && pTableModel.ChildTableModelList != null && pTableModel.ChildTableModelList.Count > 0)
                    {
                        foreach (var ChildModel in pTableModel.ChildTableModelList)
                        {
                            TableModel vTableModel = new TableModel()
                            {
                                TableNameAsTitle = pTableModel.TableNameAsTitle,
                                DotNetModelName = pTableModel.DotNetModelName,
                                PropetyList = pTableModel.PropetyList,
                                OriginalTableName = ChildModel.OriginalTableName
                            };
                            //vTableModel = (TableModel)pTableModel;
                            //vTableModel.ChildTableModelList = new List<TableModel>();
                            string vMethodPartName = ChildModel.TableNameAsTitle;
                            DAL_DeleteMethod(sb, vTableModel, DCMethodSignature, SPInSignature, true, vMethodPartName);
                        }
                        foreach (var ChildModel in pTableModel.ChildTableModelList)
                        {
                            DAL_DeleteMethod(sb, ChildModel, DCMethodSignature = "", SPInSignature = "", false);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static String TitleCaseString(String s)
        {
            if (s == null) return s;

            String[] words = s.Split('_');
            //words.RemoveAt(0);
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length == 0) continue;

                Char firstChar = Char.ToUpper(words[i][0]);
                String rest = "";
                if (words[i].Length > 1)
                {
                    rest = words[i].Substring(1).ToLower();
                }
                words[i] = firstChar + rest;
            }
            return String.Join("", words, 1, words.Length - 1);
        }




        #region Region Connection string

        private void ClassGenerator_Load(object sender, EventArgs e)
        {
            try
            {
                this.btnLogout.Visible = false;
                this.txtUserName.Enabled = false;
                this.txtPassword.Enabled = false;
                this.gboConnectionStatus.Visible = false;
                this.gboConnectionString.Enabled = true;
                //this.gboConnectionStatus.Enabled = false;
                //this.gboCreateClass.Enabled = false;

                //Remove tab pages
                this.tabAmarCodeGenerator.TabPages.Remove(this.tbClassGenerator);
                this.tabAmarCodeGenerator.TabPages.Remove(this.tbConnectionString);
                this.tabAmarCodeGenerator.TabPages.Remove(this.tbModelFirst);

            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
        private void rboWindowsSecurity_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.rboWindowsSecurity.Checked)
                {
                    this.rboUsernamePasswordSecurity.Checked = false;
                    this.txtUserName.Enabled = false;
                    this.txtPassword.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
        private void rboUsernamePasswordSecurity_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.rboUsernamePasswordSecurity.Checked)
                {
                    this.rboWindowsSecurity.Checked = false;
                    this.txtUserName.Enabled = true;
                    this.txtPassword.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
        private void cboDatabaseNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cboDatabaseNames.SelectedIndex != -1)
                {
                    if (ConnectToDatabase(CreateConnectionStringWithDatabase()))
                    {
                        GetAllTableNames();
                    }
                }
                else
                {
                    MessageBox.Show("Please select Database");
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }




        #endregion

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserId.Text) && !string.IsNullOrEmpty(txtLoginPass.Text))
            {
                if (txtUserId.Text == txtLoginPass.Text)
                {
                    this.btnLogout.Visible = true;
                    this.gboConnectionStatus.Visible = true;
                    RemoveAllTabs();

                    this.tabAmarCodeGenerator.TabPages.Add(this.tbConnectionString);
                    this.tabAmarCodeGenerator.TabPages.Add(this.tbClassGenerator);
                    this.tabAmarCodeGenerator.TabPages.Add(this.tbModelFirst);

                }
                else
                    MessageBox.Show("Please enter valid user id or password");
            }
            else
                MessageBox.Show("Please enter user id or password");
        }

        private void RemoveAllTabs()
        {
            this.tabAmarCodeGenerator.TabPages.Remove(this.tbLogin);
            this.tabAmarCodeGenerator.TabPages.Remove(this.tbConnectionString);
            this.tabAmarCodeGenerator.TabPages.Remove(this.tbClassGenerator);
            this.tabAmarCodeGenerator.TabPages.Remove(this.tbModelFirst);
        }
        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.btnLogout.Visible = false;
            this.gboConnectionStatus.Visible = true;
            RemoveAllTabs();
            this.tabAmarCodeGenerator.TabPages.Add(this.tbLogin);



            //MessageBox.Show("Please enter valid user id or password");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<ColumnModel> PropertyList = new List<ColumnModel>();
            PropertyList = GetGridValues();
            ColumnModel Property = new ColumnModel();
            Property.DisplayName = txtDisplayName.Text;
            Property.DBName = txtDBName.Text;
            Property.SYSName = txtPropertyName.Text;
            Property.DBType = ddlDBType.Text;
            Property.SYSType = ddlSystemType.Text;
            Property.OriginalDBType = ddlOriDBType.Text;
            Property.UIControlName = ddlUIControl.Text;
            Property.DBLength = txtDBLength.Text;
            Property.IsPrimayKey = chkIsPK.Checked;
            Property.IsForeignKey = chkFK.Checked;
            Property.IsSkippable = chkSkip.Checked;
            Property.IsNullable = chkIsNull.Checked;

            if (ddlUIControl.SelectedValue.ToString().Trim() == "Picklist")
            {
                string strPigValues = txtPigValues.Text;
                if (!string.IsNullOrEmpty(strPigValues))
                {
                    Property.PigListValues = new Dictionary<string, string>();
                    string[] lines = strPigValues.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                    foreach (var line in lines)
                    {
                        Property.PigListValues.Add(line, line);
                    }
                }
            }
            else if (ddlUIControl.SelectedValue.ToString().Trim() == "LookupRelationship")
            {
                Property.LookUpDropdown = new LookUpDropdownModel();

                string selectedTable = chkForeignTables.SelectedValue.ToString();
                Property.LookUpDropdown.MappingTableName = selectedTable;
                Property.LookUpDropdown.DDLDataValue = ddlDDLDataValue.Text;
                Property.LookUpDropdown.DDLTextValue = ddlDDLTextValue.Text;
            }
            txtPigValues.Text = "";
            PropertyList.Add(Property);
            grdPropertyList.DataSource = PropertyList;
            grdPropertyList.Refresh();
        }

        private List<ColumnModel> GetGridValues()
        {
            List<ColumnModel> PropertyList = new List<ColumnModel>();
            for (int index = 0; index < grdPropertyList.Rows.Count; index++)
            {
                var selectedRow = grdPropertyList.Rows[index];
                var propp = (ColumnModel)selectedRow.DataBoundItem;

                PropertyList.Add(propp);
            }
            return PropertyList;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        /*
            1. Auto Number	A system-generated sequence number that uses a display format you define. The number is automatically incremented for each new record.
            2. Formula	A read-only field that derives its value from a formula expression you define. The formula field is updated when any of the source fields change.
            3.  Roll-Up Summary 
                    A read-only field that displays the sum, minimum, or maximum value of a field in a related list or the record count of all records listed in a related list.
 
            4.  Lookup Relationship	Creates a relationship that links this object to another object. The relationship field allows users to click on a lookup icon to select a value from a popup list. The other object is the source of the values in the list.
            5.  Master-Detail Relationship	Creates a special type of parent-child relationship between this object (the child, or "detail") and another object (the parent, or "master") where:
                    The relationship field is required on all detail records.
                    The ownership and sharing of a detail record are determined by the master record.
                    When a user deletes the master record, all detail records are deleted.
                    You can create rollup summary fields on the master record to summarize the detail records.
                    The relationship field allows users to click on a lookup icon to select a value from a popup list. The master object is the source of the values in the list.
            6.  External Lookup Relationship	Creates a relationship that links this object to an external object whose data is stored outside the Salesforce org.
 
            7.  Checkbox	Allows users to select a True (checked) or False (unchecked) value.
            8.  Currency	Allows users to enter a dollar or other currency amount and automatically formats the field as a currency amount. This can be useful if you export data to Excel or another spreadsheet.
            9.  Date	Allows users to enter a date or pick a date from a popup calendar.
            10. Date/Time	Allows users to enter a date and time, or pick a date from a popup calendar. When users click a date in the popup, that date and the current time are entered into the Date/Time field.
            11. Email	Allows users to enter an email address, which is validated to ensure proper format. If this field is specified for a contact or lead, users can choose the address when clicking Send an Email. Note that custom email addresses cannot be used for mass emails.
            12. Geolocation	Allows users to define locations. Includes latitude and longitude components, and can be used to calculate distance.
            13. Number	Allows users to enter any number. Leading zeros are removed.
            14. Percent	Allows users to enter a percentage number, for example, '10' and automatically adds the percent sign to the number.
            15. Phone	Allows users to enter any phone number. Automatically formats it as a phone number.
            16. Picklist	Allows users to select a value from a list you define.
            17. Picklist (Multi-Select)	Allows users to select multiple values from a list you define.
            18. Text	Allows users to enter any combination of letters and numbers.
            19. Text Area	Allows users to enter up to 255 characters on separate lines.
            20. Text Area (Long)	Allows users to enter up to 131,072 characters on separate lines.
            21. Text Area (Rich)	Allows users to enter formatted text, add images and links. Up to 131,072 characters on separate lines.
            22. Text (Encrypted) Allows users to enter any combination of letters and numbers and store them in encrypted form.
            23. URL	Allows users to enter any valid website address. When users click on the field, the URL will open in a separate browser window.

         */
        public enum UIControlType
        {

            AutoNumber = 1,
            Formula = 2,
            RollUpSummary = 3,
            LookupRelationship = 4,
            MasterDetailRelationship = 5,
            ExternalLookupRelationship = 6,
            Checkbox = 7,
            Currency = 8,
            Date = 9,
            DateTime = 10,
            Email = 11,
            Geolocation = 12,
            Number = 13,
            Percent = 14,
            Phone = 15,
            Picklist = 16,
            PicklistMultiSelect = 17,
            Text = 18,
            TextArea = 19,
            TextAreaLong = 20,
            TextAreaRich = 21,
            TextEncrypted = 22,
            URL = 23

        };

        private void tabAmarCodeGenerator_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage != null && e.TabPage.Name == "tbModelFirst")
            {
                ddlUIControl.DataSource = Enum.GetValues(typeof(UIControlType));
                ddlUIControl.SelectedItem = (UIControlType)18;

                List<TypeMapping> SystemTypes = GetTypeMappingList().OrderBy(dt => dt.SystemType).ToList();
                ddlSystemType.DataSource = SystemTypes;
                ddlSystemType.DisplayMember = "SystemType";
                ddlSystemType.ValueMember = "Id";
                ddlSystemType.SelectedValue = 17;

                ddlDBType.DataSource = GetTypeMappingList().OrderBy(dt => dt.DbType).ToList();
                ddlDBType.DisplayMember = "DbType";
                ddlDBType.ValueMember = "Id";
                ddlDBType.SelectedValue = 17;



                txtPigValues.Visible = false;
                chkForeignTables.Visible = false;
                ddlDDLDataValue.Visible = false;
                ddlDDLTextValue.Visible = false;

            }

        }





        private void txtDisplayName_Leave(object sender, EventArgs e)
        {
            string TempValue = RemoveSpacesBeforeUppercaseLetter(txtDisplayName.Text);
            txtPropertyName.Text = TempValue;
            txtDBName.Text = TempValue;
            //ddlUIControl.SelectedValue = "Text";
        }

        public List<TypeMapping> GetTypeMappingList()
        {
            List<TypeMapping> objList = new List<TypeMapping>();
            objList.Add(new TypeMapping() { Id = 1, DbType = "uniqueidentifier", SystemType = "Guid" });

            objList.Add(new TypeMapping()
            {
                Id = 2,
                DbType = "bigint"
                        ,
                SystemType = "BigInt"
            });
            objList.Add(new TypeMapping()
            {
                Id = 3,
                DbType = "smallint",
                SystemType = "SmallInt"
            });
            objList.Add(new TypeMapping()
            {
                Id = 4,
                DbType = "tinyint",
                SystemType = "TinyInt"
            });
            objList.Add(new TypeMapping()
            {
                Id = 5,
                DbType = "int",
                SystemType = "Int"
            });
            objList.Add(new TypeMapping()
            {
                Id = 6,
                DbType = "bit",
                SystemType = "Bit"
            });
            objList.Add(new TypeMapping()
            {
                Id = 7,
                DbType = "decimal",
                SystemType = "Decimal"
            });

            objList.Add(new TypeMapping()
            {
                Id = 8,
                DbType = "numeric",
                SystemType = "Decimal"
            });

            objList.Add(new TypeMapping()
            {
                Id = 9,
                DbType = "money",
                SystemType = "Money"
            });
            objList.Add(new TypeMapping()
            {
                Id = 10,
                DbType = "smallmoney",
                SystemType = "SmallMoney"
            });
            objList.Add(new TypeMapping()
            {
                Id = 11,
                DbType = "float",
                SystemType = "Float"
            });
            objList.Add(new TypeMapping()
            {
                Id = 12,
                DbType = "real",
                SystemType = "Float"
            });
            objList.Add(new TypeMapping()
            {
                Id = 13,
                DbType = "datetime",
                SystemType = "System.DateTime.DateTime"
            });
            objList.Add(new TypeMapping()
            {
                Id = 14,
                DbType = "smalldatetime",
                SystemType = "System.DateTime.SmallDateTime"
            });
            objList.Add(new TypeMapping()
            {
                Id = 15,
                DbType = "char",
                SystemType = "Char"
            });
            objList.Add(new TypeMapping()
            {
                Id = 16,
                DbType = "sql_variant",
                SystemType = "object"
            });
            objList.Add(new TypeMapping()
            {
                Id = 17,
                DbType = "nvarchar",
                SystemType = "NVarChar"
            });
            objList.Add(new TypeMapping()
            {
                Id = 18,
                DbType = "varchar",
                SystemType = "VarChar"
            });
            objList.Add(new TypeMapping()
            {
                Id = 19,
                DbType = "text",
                SystemType = "Text"
            });
            objList.Add(new TypeMapping()
            {
                Id = 20,
                DbType = "nchar",
                SystemType = "NChar"
            });
            objList.Add(new TypeMapping()
            {
                Id = 21,
                DbType = "binary",
                SystemType = "Binary"
            });
            objList.Add(new TypeMapping()
            {
                Id = 22,
                DbType = "varbinary",
                SystemType = "byte[]"
            });
            objList.Add(new TypeMapping()
            {
                Id = 23,
                DbType = "image",
                SystemType = "System.Drawing.Image"
            });
            objList.Add(new TypeMapping()
            {
                Id = 24,
                DbType = "unknown",
                SystemType = "unknown"
            });

            return objList;
        }

        private void ddlDBType_SelectedValueChanged(object sender, EventArgs e)
        {
            ddlSystemType.SelectedValue = ddlDBType.SelectedValue;
            ddlOriDBType.SelectedValue = ddlDBType.SelectedValue;
        }

        private void ddlUIControl_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ddlUIControl.SelectedValue.ToString().Trim() == "Picklist")
            {
                txtPigValues.Visible = true;
                chkForeignTables.Visible = false;
                ddlDDLDataValue.Visible = false;
                ddlDDLTextValue.Visible = false;
                lblDDLDataValue.Visible = false;
                lblDDLTextValue.Visible = false;
            }
            else if (ddlUIControl.SelectedValue.ToString().Trim() == "LookupRelationship")
            {
                txtPigValues.Visible = false;
                chkForeignTables.Visible = true;
                ddlDDLDataValue.Visible = true;
                ddlDDLTextValue.Visible = true;
                lblDDLDataValue.Visible = true;
                lblDDLTextValue.Visible = true;
                try
                {
                    if (ConnectToDatabase(CreateConnectionStringWithDatabase()))
                    {
                        GetReferencedTableNames();
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }
            }
            else
            {
                txtPigValues.Visible = false;
                ddlDDLDataValue.Visible = false;
                ddlDDLTextValue.Visible = false;
                lblDDLDataValue.Visible = false;
                lblDDLTextValue.Visible = false;
            }

        }

        private void GetReferencedTableNames()
        {
            try
            {
                string strSQLquery = "select table_name from Information_Schema.Tables where Table_Type='Base Table' order by table_name";
                Database db = new SqlDatabase(SQL_CONN_STRING);
                DbCommand cmd = db.GetSqlStringCommand(strSQLquery);
                using (DbConnection cn = db.CreateConnection())
                {
                    cn.Open();

                    DataSet ds = db.ExecuteDataSet(cmd);
                    if (ds.Tables.Count >= 1)
                    {
                        //Fill check list box with database tables
                        if (ds.Tables[0].Rows.Count >= 1)
                        {
                            chkForeignTables.DataSource = ds.Tables[0];
                            chkForeignTables.DisplayMember = "table_name";
                            chkForeignTables.ValueMember = "table_name";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void chkForeignTables_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < chkForeignTables.Items.Count; ++ix)
                if (ix != e.Index) chkForeignTables.SetItemChecked(ix, false);

            string selectedTable = chkForeignTables.SelectedValue.ToString();
            //var TableAttributes = GetTableAttributes(selectedTable);

            var ValueList = GetTableAttributes(selectedTable).PropetyList;
            ddlDDLDataValue.DataSource = ValueList;
            ddlDDLDataValue.DisplayMember = "DisplayName";
            ddlDDLDataValue.ValueMember = "DBName";

            var list = GetTableAttributes(selectedTable).PropetyList;
            ddlDDLTextValue.DataSource = list;
            ddlDDLTextValue.DisplayMember = "DisplayName";
            ddlDDLTextValue.ValueMember = "DBName";

            ddlDDLDataValue.Visible = true;
            ddlDDLTextValue.Visible = true;
            lblDDLDataValue.Visible = true;
            lblDDLTextValue.Visible = true;
        }

        private TableModel PrepareModelProperties(string DisplayName)
        {
            //SqlDataReader dr = null;
            string strTableName = RemoveSpacesBeforeUppercaseLetter(DisplayName);
            TableModel objTableModel = new TableModel();
            objTableModel.PropetyList = new List<ColumnModel>();
            objTableModel.OriginalTableName = "T_" + strTableName;
            objTableModel.TableNameAsTitle = strTableName;

            objTableModel.DisplayName = DisplayName;// AddSpacesBeforeUppercaseLetter(objTableModel.TableNameAsTitle);
            objTableModel.ControllerName = objTableModel.TableNameAsTitle + "Controller";
            objTableModel.IndexViewPageName = "Index";
            objTableModel.EditViewPageName = "Edit";
            objTableModel.CreateViewPageName = "Create";

            objTableModel.DotNetModelName = objTableModel.TableNameAsTitle + "Model";
            objTableModel.DotNetBLLName = objTableModel.TableNameAsTitle + "BLL";
            objTableModel.DotNetDataContextName = objTableModel.TableNameAsTitle + "DC";
            objTableModel.DotNetIBLLIntName = "I" + objTableModel.TableNameAsTitle + "BLL";
            objTableModel.DotNetInterfaceName = (IsModelInterfaceRequired ? "" : "I") + objTableModel.TableNameAsTitle;

            return objTableModel;
        }

        private void txtModelDisplayName_MouseLeave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtModelDisplayName.Text))
            {
                var objTable = PrepareModelProperties(txtModelDisplayName.Text);
                if (objTable != null)
                {
                    txtOriginalTableName.Text = objTable.OriginalTableName;
                    txtTableNameAsTitle.Text = objTable.TableNameAsTitle;
                    //.Text  = objTable.DisplayName;
                    txtControllerName.Text = objTable.ControllerName;
                    txtIndexViewPageName.Text = objTable.IndexViewPageName;
                    txtEditViewPageName.Text = objTable.EditViewPageName;
                    txtCreateViewPageName.Text = objTable.CreateViewPageName;
                    txtDotNetModelName.Text = objTable.DotNetModelName;
                    txtDotNetBLLName.Text = objTable.DotNetBLLName;
                    txtDotNetDataContextName.Text = objTable.DotNetDataContextName;
                    txtDotNetIBLLIntName.Text = objTable.DotNetIBLLIntName;
                    txtDotNetInterfaceName.Text = objTable.DotNetInterfaceName;
                }
            }
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //----------------Need to apply few validations

            // main model------------
            var objTable = new TableModel();
            objTable.OriginalTableName = txtOriginalTableName.Text;
            objTable.TableNameAsTitle = txtTableNameAsTitle.Text;
            objTable.DisplayName = txtModelDisplayName.Text;
            objTable.ControllerName = txtControllerName.Text;
            objTable.IndexViewPageName = txtIndexViewPageName.Text;
            objTable.EditViewPageName = txtEditViewPageName.Text;
            objTable.CreateViewPageName = txtCreateViewPageName.Text;
            objTable.DotNetModelName = txtDotNetModelName.Text;
            objTable.DotNetBLLName = txtDotNetBLLName.Text;
            objTable.DotNetDataContextName = txtDotNetDataContextName.Text;
            objTable.DotNetIBLLIntName = txtDotNetIBLLIntName.Text;
            objTable.DotNetInterfaceName = txtDotNetInterfaceName.Text;

            //--------- Properties-----------------
            List<ColumnModel> PropertyList = new List<ColumnModel>();
            PropertyList = GetGridValues();
            objTable.PropetyList = PropertyList;
            GenerateAllComponentsfromModel(objTable);
        }

        private void GenerateAllComponentsfromModel(TableModel objTable)
        {
            GenerateModelClass(objTable);
            GenerateDataContextClass(objTable);
        }

        private string RemoveSpacesBeforeUppercaseLetter(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0].ToString().ToUpper());
            for (int i = 1; i < text.Length; i++)
            {
                if (text[i] == ' ')
                    continue;

                else if (text[i - 1] == ' ' && char.IsLower(text[i]))
                    newText.Append((text[i]).ToString().ToUpper());
                else
                    newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }

    public class TypeMapping
    {
        public int Id { get; set; }
        public string DbType { get; set; }
        public string SystemType { get; set; }

    }
}