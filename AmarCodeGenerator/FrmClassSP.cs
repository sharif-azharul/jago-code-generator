using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmarCodeGenerator
{
    public partial class FrmClassSP : Form
    {
        private static string strTable = string.Empty;

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

        public FrmClassSP()
        {
            InitializeComponent();
        }

        //............ Get Database , Tables and Attributes Names ................
        public void GetAllDatabaseNames()
        {
            try
            {
                //Get all database names from selected computer
                string strSQLquery = "select name as DatabaseName from master.dbo.sysdatabases";

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(strSQLquery, SessionUtility.SQL_CONN_STRING))
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
                FrmConnection frm = new FrmConnection();
                if (CommonTask.ConnectToDatabase(frm.CreateConnectionStringWithDatabase(this.cboDatabaseNames.SelectedValue.ToString())))
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
                Database db = new SqlDatabase(SessionUtility.SQL_CONN_STRING);
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

        private void btnCreateClassAndSP_Click(object sender, EventArgs e)
        {
            Model objModel = new Model();
            BusinessLayer objBusinessLayer = new BusinessLayer();
            DataContextLayer objDataContextLayer = new DataContextLayer();
            StoreProcedureLayer objStoreProcedureLayer = new StoreProcedureLayer();
            ControllerLayer objControllerLayer = new AmarCodeGenerator.ControllerLayer();
            Repository objRepository = new AmarCodeGenerator.Repository();
            UILayer objUILayer = new UILayer();
            XHRCommand oXHRCommand = new XHRCommand();
            XHRAngular oXHRAngular = new XHRAngular();
            //UIAspxLayer oUIAspxLayer = new UIAspxLayer();

            try
            {
                bool isCreated = false;

                if ((this.chkDataAccessLayerClasses.Checked) || (this.chkBusinessLogicLayerClasses.Checked) || (this.chkSelectbyKeySP.Checked) || (this.chkDeleteSP.Checked) || (this.chkInsertSP.Checked))
                {
                    //InitializeConfiguration();
                    if (this.chkDataAccessLayerClasses.Checked)
                    {
                        var objTableModelList = DefineParentChildTables();
                        oXHRCommand.GeneratePermissionProviderXHR(objTableModelList);
                        #region Create DataAccessLayer Class

                        foreach (var pTable in objTableModelList)
                        {
                            //objModel.GenerateModelFromTemplateDataAnotation(pTable);
                            objModel.GenerateModelFromTemplateXHR(pTable);
                            oXHRCommand.GenerateCommandFromTemplateXHR(pTable);
                            oXHRCommand.GenerateCommandVMFromTemplateXHR(pTable);
                            oXHRCommand.GenerateCreateCommandFromTemplateXHR(pTable);

                            oXHRCommand.GenerateUpdateCommandFromTemplateXHR(pTable);
                            oXHRCommand.GenerateMarkAsDeleteCommandHandlerFromTemplateXHR(pTable);

                            // queries 
                            oXHRCommand.GenerateQueriesModelXHR(pTable);
                            oXHRCommand.GenerateQueriesXHR(pTable);
                            oXHRCommand.GenerateQueryHandlerXHR(pTable);
                            oXHRCommand.GenerateListQueryHandlerXHR(pTable);
                            //controller
                            oXHRCommand.GenerateControllerXHR(pTable);
                            //Configuration
                            oXHRCommand.GenerateConfigurationXHR(pTable);
                            //
                            oXHRCommand.GenerateGetAllFunctionXHR(pTable);
                            //Angular
                            oXHRCommand.GenerateAngularModelXHR(pTable);
                            oXHRCommand.GenerateAngularServiceXHR(pTable);

                            oXHRAngular.CreateComponent(pTable);
                            oXHRAngular.CreateComponentCSS(pTable);
                            oXHRAngular.CreateComponentHTML(pTable);
                            oXHRAngular.CreateComponentSpec(pTable);
                            oXHRAngular.ListComponent(pTable);
                            oXHRAngular.ListComponentCSS(pTable);
                            oXHRAngular.ListComponentHTML(pTable);
                            oXHRAngular.ListComponentSpec(pTable);

                            //objBusinessLayer.GenerateBLLClass(pTable);
                            ////objDataContextLayer.GenerateDataContextClass(pTable);
                            //objStoreProcedureLayer.GenerateStoredProcedures(pTable);
                            //objControllerLayer.GenerateController(pTable);
                            ////objUILayer.GenerateAllViewPages(pTable);


                            ////
                            //objRepository.GenerateRepository(pTable);
                            //objRepository.GenerateRepositoryInterface(pTable);

                            ////Spark pages
                            //objDataContextLayer.GenerateSparkDCFromTemplate(pTable);
                            ////oUIAspxLayer.GenerateSparkListAspxFromTemplate(pTable);

                        }

                        #endregion

                        isCreated = true;
                    }

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
                CommonTask.LogError(ex);
            }
        }

        //==================================Helper Method========================================

        //private void InitializeConfiguration()
        //{
        //    //NameSpaceFirstPart = ConfigurationManager.AppSettings["NAMESPACEFIRSTPART"].ToString();
        //    //ModuleName = ConfigurationManager.AppSettings["MODULENAME"].ToString();
        //    //DBConnectionString = ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        //    //ParentChildTables = ConfigurationManager.AppSettings["ParentChildTables"].ToString();
        //    //IsModelInterfaceRequired = Convert.ToBoolean(ConfigurationManager.AppSettings["ISMODELINTERFACEREQUIRED"].ToString());
        //    //IsBLLInterfaceRequired = Convert.ToBoolean(ConfigurationManager.AppSettings["ISBLLINTERFACEREQUIRED"].ToString());
        //    //ColumnsToSkip = ConfigurationManager.AppSettings["COLUMNSTOSKIP"].ToString();
        //    //DeleteColumn = ConfigurationManager.AppSettings["DELETECOLUMN"].ToString();
        //    //DeleteSupportingColumns = ConfigurationManager.AppSettings["DELETESUPPORTINGCOLUMNS"].ToString();
        //    //InsertSupportingColumns = ConfigurationManager.AppSettings["INSERTSUPPORTINGCOLUMNS"].ToString();
        //    //UpdateSupportingColumns = ConfigurationManager.AppSettings["UPDATESUPPORTINGCOLUMNS"].ToString();
        //    //RootFolderName = ConfigurationManager.AppSettings["ROOTFOLDERNAME"].ToString();

        //    //SPFolderName = RootFolderName + ConfigurationManager.AppSettings["SPFOLDERNAME"].ToString() + @"\";
        //    //CreateDirectory(SPFolderName);
        //    //ModelInterfaceFolder = RootFolderName + ConfigurationManager.AppSettings["IMODEL"].ToString() + @"\";
        //    //CreateDirectory(ModelInterfaceFolder);

        //    //ModelFolder = RootFolderName + ConfigurationManager.AppSettings["MODEL"].ToString() + @"\";
        //    //CreateDirectory(ModelFolder);

        //    //BLLFolder = RootFolderName + ConfigurationManager.AppSettings["BLL"].ToString() + @"\";
        //    //CreateDirectory(BLLFolder);

        //    //IBLLFolder = RootFolderName + ConfigurationManager.AppSettings["IBLL"].ToString() + @"\";
        //    //CreateDirectory(IBLLFolder);

        //    //DataContextFolder = RootFolderName + ConfigurationManager.AppSettings["DATACONTEXT"].ToString() + @"\";
        //    //CreateDirectory(DataContextFolder);

        //    //ViewsFolder = RootFolderName + "Views" + @"\";
        //    //CreateDirectory(ViewsFolder);

        //    //ControllerFolder = RootFolderName + "Controller" + @"\";
        //    //CreateDirectory(ControllerFolder);

        //}
        private void CreateDirectory(string pDirectory)
        {
            if (!Directory.Exists(pDirectory))
                Directory.CreateDirectory(pDirectory);
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
                else
                {
                    return objTableList;
                }
            }
            return objHeirarchyTableList;
        }
        private TableModel GetTableAttributes(string strTableName)
        {
            string TableSchemaName = "dbo";
            try
            {
                Database db = new SqlDatabase(SessionUtility.SQL_CONN_STRING);
                DbCommand cmd = db.GetSqlStringCommand(string.Format("select * from Information_Schema.Tables where Table_Type='Base Table' AND TABLE_NAME= '{0}' ", strTableName));
                using (DbConnection cn = db.CreateConnection())
                {
                    cn.Open();

                    using (IDataReader dr = db.ExecuteReader(cmd))
                    {
                        if (dr.Read())
                        {
                            TableSchemaName = dr["TABLE_SCHEMA"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //SqlDataReader dr = null;
            TableModel objTableModel = new TableModel();
            objTableModel.PropetyList = new List<ColumnModel>();
            objTableModel.OriginalTableName = strTableName;
            objTableModel.TableNameAsTitle = strTableName.Substring(SessionUtility.SkippingTableName, strTableName.Length - SessionUtility.SkippingTableName);//GenerateModelName(strTableName);
            if (SessionUtility.IsTableHasUnderline)
            {
                objTableModel.TableNameAsTitle = CommonTask.RemoveUnderscoreAndTitleString(objTableModel.TableNameAsTitle);
            }

            objTableModel.ObjectName = "obj" + objTableModel.TableNameAsTitle;
            objTableModel.MethodSaveName = "Save" + objTableModel.TableNameAsTitle;
            objTableModel.MethodGetByKeyName = "Get" + objTableModel.TableNameAsTitle + "ByKey";
            objTableModel.MethodGetByAllName = "Get" + objTableModel.TableNameAsTitle + "ByAll";
            objTableModel.MethodGetBySearchName = "Get" + objTableModel.TableNameAsTitle + "BySearch";
            objTableModel.MethodDeleteName = "Delete" + objTableModel.TableNameAsTitle;
            objTableModel.TableSchemaName = TableSchemaName;

            objTableModel.UpdateConstructorName = "Update" + objTableModel.OriginalTableName;
            objTableModel.CommandVMName = objTableModel.OriginalTableName + "CommandVM";
            objTableModel.QueriesVMName = objTableModel.OriginalTableName + "VM";
            objTableModel.GetListMethodName = "Get" + objTableModel.OriginalTableName + "List";
            objTableModel.GetMethodName = "Get" + objTableModel.OriginalTableName;

            objTableModel.GetListQueryHandlerName = "Get" + objTableModel.OriginalTableName + "ListQueryHandler";
            objTableModel.GetQueryHandlerName = "Get" + objTableModel.OriginalTableName + "QueryHandler";
            objTableModel.GetListDBFunctionName = "Get" + objTableModel.OriginalTableName + "List";
            objTableModel.GetDBFunctionName = "Get" + objTableModel.OriginalTableName + "ById";
            //Command name
            objTableModel.CreateCommandName = "Create" + objTableModel.OriginalTableName;
            objTableModel.UpdateCommandName = "Update" + objTableModel.OriginalTableName;
            objTableModel.DeleteCommandName = "MarkAsDelete" + objTableModel.OriginalTableName;

            objTableModel.SPSaveName = "FSP_" + objTableModel.OriginalTableName + "_INSERT_UPDATE";
            objTableModel.SPGetByKeyName = "FSP_" + objTableModel.OriginalTableName + "_GK";
            objTableModel.SPGetByAllName = "FSP_" + objTableModel.OriginalTableName + "_GA";
            objTableModel.SPGetBySearchName = "FSP_" + objTableModel.OriginalTableName + "_SEARCH";
            objTableModel.SPDeleteName = "FSP_" + objTableModel.OriginalTableName + "_DELETE";
            objTableModel.ControllerName = objTableModel.OriginalTableName + "Controller";

            objTableModel.DisplayName = objTableModel.TableNameAsTitle.Contains("_") ? CommonTask.RemoveUnderscoreAddSpaceAndUpperFirst(objTableModel.TableNameAsTitle)
                : AddSpacesBeforeUppercaseLetter(objTableModel.TableNameAsTitle);
            //objTableModel.ControllerName = objTableModel.TableNameAsTitle + "Controller";
            objTableModel.IndexViewPageName = "Index";
            objTableModel.EditViewPageName = "Edit";
            objTableModel.CreateViewPageName = "Create";

            objTableModel.DotNetModelName = objTableModel.TableNameAsTitle + "Model";
            objTableModel.DotNetBLLName = objTableModel.TableNameAsTitle + "BLL";
            objTableModel.DotNetDataContextName = objTableModel.TableNameAsTitle + "DC";
            objTableModel.DotNetIBLLIntName = "I" + objTableModel.TableNameAsTitle + "BLL";
            objTableModel.DotNetInterfaceName = (IsModelInterfaceRequired ? "" : "I") + objTableModel.TableNameAsTitle;

            objTableModel.RepositoryName = string.Format("{0}Repository", objTableModel.TableNameAsTitle);
            objTableModel.RepositoryInterfaceName = string.Format("I{0}Repository", objTableModel.TableNameAsTitle);

            //===================================================================================================================change below
            //objTableModel.DotNetModelName = objTableModel.TableNameAsTitle;

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
                sbSQL.AppendLine(" c.object_id = OBJECT_ID('" + TableSchemaName + "." + strTableName + "') ");

                Database db = new SqlDatabase(SessionUtility.SQL_CONN_STRING);
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
            else if (objTableModel.PropetyList.Count > 0 && objColumnList.FindAll(c => c.IsPrimayKey == true).Count < 1)
            {
                objTableModel.PropetyList[0].IsPrimayKey = true;
            }
            else
                objTableModel.HasCompositeKey = false;
            return objTableModel;
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
            if (SessionUtility.ColumnsToSkip.ToUpper().Contains(p.ToUpper()))
                IsSkip = true;
            return IsSkip;
        }

        private bool IsDeleteColumn(string p)
        {
            Boolean IsDelete = false;
            if (SessionUtility.DeleteColumn.ToUpper().Contains(p.ToUpper()))
                IsDelete = true;
            return IsDelete;
        }

        private bool IsDeleteSupportColumn(string p)
        {
            Boolean IsDeleteSupport = false;
            if (SessionUtility.DeleteSupportingColumns.ToUpper().Contains(p.ToUpper()))
                IsDeleteSupport = true;
            return IsDeleteSupport;
        }
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
                            _Type = "string";
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
                        //remove underscore
                        objColumnModel.SYSName = objColumnModel.DBName.Contains("_") ? CommonTask.RemoveUnderscoreAndTitleString(objColumnModel.DBName) : objColumnModel.DBName;
                        // SYS NAME remain as DB name
                        objColumnModel.SYSName = objColumnModel.DBName;
                        objColumnModel.ParameterFormatName = objColumnModel.SYSName.Substring(0, 1).ToLower() + objColumnModel.SYSName.Substring(1);
                        objColumnModel.IsSkippable = IsSkippable(objColumnModel.DBName);
                        objColumnModel.IsDeleteColumn = IsDeleteColumn(objColumnModel.DBName);
                        objColumnModel.IsDeleteSupportColumn = IsDeleteSupportColumn(objColumnModel.DBName);
                        break;
                    case "Data_Type":
                        {
                            objColumnModel.OriginalDBType = dr["Data_Type"].Equals(DBNull.Value) ? "" : dr["Data_Type"].ToString();
                            objColumnModel.DBType = SqlDatabseType(dr["Data_Type"].Equals(DBNull.Value) ? "" : dr["Data_Type"].ToString());
                            objColumnModel.SYSType = GetSystemType(dr["Data_Type"].Equals(DBNull.Value) ? "" : dr["Data_Type"].ToString());
                            //Type type = Type.GetType("System." + objColumnModel.SYSType);
                            //objColumnModel.DBType = TypeConvertor.ToDbType(type).ToString();
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
            objColumnModel.DisplayName = objColumnModel.DBName.Contains("_") ? CommonTask.RemoveUnderscoreAddSpaceAndUpperFirst(objColumnModel.DBName)
               : AddSpacesBeforeUppercaseLetter(objColumnModel.DBName);

            return objColumnModel;
        }

        private void cboDatabaseNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.cboDatabaseNames.SelectedIndex != -1)
                {
                    FrmConnection frmCon = new FrmConnection();

                    if (CommonTask.ConnectToDatabase(frmCon.CreateConnectionStringWithDatabase(cboDatabaseNames.SelectedValue.ToString())))
                    {
                        GetAllTableNames();
                        frmCon.Hide();
                    }
                }
                else
                {
                    MessageBox.Show("Please select Database");
                }
            }
            catch (Exception ex)
            {
                CommonTask.LogError(ex);
            }

        }

        private void btnAspnetZeroGenerate_Click(object sender, EventArgs e)
        {

        }

        //public  string CreateConnectionStringWithDatabase()
        //{
        //    try
        //    {
        //        string strDataSource = this.txtServerName.Text;
        //        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //        if (this.rboUsernamePasswordSecurity.Checked)
        //        {
        //            //........................... user name password security mode............................
        //            string strUserName = this.txtUserName.Text;
        //            string strPassword = this.txtPassword.Text;
        //            sb.AppendFormat("Data Source=" + strDataSource + ";Persist Security Info=True;User ID=" + strUserName + ";Password=" + strPassword + ";Initial Catalog=" + this.cboDatabaseNames.SelectedValue);
        //        }
        //        else if (this.rboWindowsSecurity.Checked)
        //        {
        //            //......................... windows authentication security mode..........................
        //            sb.AppendFormat("Data Source=" + strDataSource + ";Integrated Security=SSPI;Persist Security Info=False" + ";Initial Catalog=" + this.cboDatabaseNames.SelectedValue);
        //        }

        //        SQL_CONN_STRING = sb.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return SQL_CONN_STRING;
        //}
    }
}
