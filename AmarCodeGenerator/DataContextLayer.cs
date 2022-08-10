using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AmarCodeGenerator
{
    class DataContextLayer
    {
        public void GenerateDataContextClass(TableModel pTableModel)
        {
            try
            {
                CommonTask.CreateDirectory(SessionUtility.DataContextFolder);
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;

                    string dq = @"""";
                    string lstrTableName = pTableModel.OriginalTableName;  //table name

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.DataContextFolder + pTableModel.DotNetDataContextName);
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
                    CommonTask.WriteDefaultConstructor(sw, pTableModel.DotNetDataContextName);
                    #endregion

                    #region Write Private Variables
                    sb = new System.Text.StringBuilder("\r\n\t");

                    sw.WriteLine(sb.ToString());
                    #endregion

                    #region Write Public Methods for DAL
                    sb = new System.Text.StringBuilder("\r\n\t");

                    //sb.AppendLine(@"private static string ConnectionString = " + dq + SessionUtility.DBConnectionString + dq + ";");
                    sb.AppendLine(@"private static string vSAVESUCCESS = " + dq + "Data Saved Successfully." + dq + ";");
                    sb.AppendLine(@"private static string vSAVEFAIL = " + dq + "Data Not Saved." + dq + "; ");
                    sb.AppendLine(@"private static string vDELETESUCCESS = " + dq + "Data Deleted Successfully." + dq + "; ");
                    sb.AppendLine(@"private static string vDELETEFAIL = " + dq + "Data Not Deleted." + dq + "; ");

                    sb.AppendLine("#region Public Methods");


                    //...................................... Insert or Update Method ..........................................
                    DAL_InsertUpdateMethodWithAmarHelper(sb, pTableModel);

                    //...................................... Select Method ..........................................

                    //WriteSelectMethod_forDAL(sb, pTableModel);
                    DAL_GKForAmarHelper(sb, pTableModel);
                    DAL_GAForAmarHelper(sb, pTableModel);

                    //DAL_BySearch(sb, pTableModel);


                    //...................................... Insert Method ..........................................
                    //WriteInsertMethod_forDAL(sb, AttributeNameArrayList);

                    //...................................... Update Method ..........................................
                    //WriteUpdateMethod_forDAL(sb, AttributeNameArrayList);

                    //...................................... Delete Method ..........................................

                    //DAL_DeleteMethod(sb, pTableModel);



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

        public void GenerateDataContextClassFromTemplate(TableModel pTableModel)
        {
            try
            {
                CommonTask.CreateDirectory(SessionUtility.DataContextFolder);
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;

                    string dq = @"""";
                    string lstrTableName = pTableModel.OriginalTableName;  //table name

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.DataContextFolder + pTableModel.DotNetDataContextName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion

                    #region Get Table Name, Attributes Name and Attribute Types

                    #endregion

                    #region Write Namespaces
                    //this.WriteDataContextNamespaces(sw, pTableModel);
                    #endregion

                    #region Write Class Default Constructor
                    //CommonTask.WriteDefaultConstructor(sw, pTableModel.DotNetDataContextName);
                    #endregion

                    #region Write Private Variables
                    sb = new System.Text.StringBuilder();

                    sw.WriteLine(sb.ToString());
                    #endregion

                    #region Write Public Methods for DAL
                    sb = new System.Text.StringBuilder();
                    sb.Append(CommonTask.PrepareMailContent(pTableModel, "DataContextTemplate.html"));
                    sw.WriteLine(sb.ToString().Replace("[[P]]", "@P_"));


                    #endregion

                    #region Close file
                    if (sw != null)
                    {
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

        public void GenerateSparkDCFromTemplate(TableModel pTableModel)
        {
            try
            {
                CommonTask.CreateDirectory(SessionUtility.DataContextFolder);
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;

                    string dq = @"""";
                    string lstrTableName = pTableModel.OriginalTableName;  //table name

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.DataContextFolder + pTableModel.DotNetDataContextName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion

                    #region Get Table Name, Attributes Name and Attribute Types

                    #endregion

                    #region Write Namespaces
                    //this.WriteDataContextNamespaces(sw, pTableModel);
                    #endregion

                    #region Write Class Default Constructor
                    //CommonTask.WriteDefaultConstructor(sw, pTableModel.DotNetDataContextName);
                    #endregion

                    #region Write Private Variables
                    sb = new System.Text.StringBuilder();

                    sw.WriteLine(sb.ToString());
                    #endregion

                    #region Write Public Methods for DAL
                    sb = new System.Text.StringBuilder();
                    sb.Append(CommonTask.PrepareMailContent(pTableModel, "DCSparkTemplate.html"));
                    sw.WriteLine(sb.ToString().Replace("[[P]]", "@").Replace("FSP_","").Replace("?","").Replace("[[WHAT]]", "?").Replace("DbType.string", "DbType.String"));


                    #endregion

                    #region Close file
                    if (sw != null)
                    {
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
        private void WriteDataContextNamespaces(StreamWriter sw, TableModel pTableModel)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("using System;");
                sb.Append("\r\nusing System;                         ");
                sb.Append("\r\nusing System.Collections.Generic;                    ");
                //sb.Append("\r\nusing System.Text;                                   ");
                sb.Append("\r\nusing System.Data;                                   ");
                //sb.Append("\r\nusing System.Data.Common;                            ");
                //sb.Append("\r\nusing Microsoft.Practices.EnterpriseLibrary.Data;    ");
                //sb.Append("\r\nusing " + SessionUtility.NameSpaceFirstPart + ".Common.Interfaces;                       ");
                sb.Append("\r\nusing JAGO.Beats.Models;");
                sb.Append("\r\nusing AmarDBHelper;");

                if (SessionUtility.IsModelInterfaceRequired)
                    sb.Append("\r\nusing " + SessionUtility.NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".Interfaces;                       ");
                sb.Append("\r\nusing " + SessionUtility.NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".Model;                            ");
                //if (strTable.Contains("HRM"))
                //{
                //    sb.Append("\r\nusing " + SessionUtility.NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".Model;");
                //    sb.Append("\r\n\r\nnamespace BLERP.BLL.HR");
                //}
                //else if (strTable.Contains("CMN"))
                //{
                //    sb.Append("\r\nusing " + SessionUtility.NameSpaceFirstPart + "Common.Model;");
                //    sb.Append("\r\n\r\nnamespace " + SessionUtility.NameSpaceFirstPart + ".Common.BLL");
                //}
                //else
                //{
                //    sb.Append("\r\n namespace " + SessionUtility.NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".DataContext");
                //}

                sb.Append("\r\nnamespace JAGO.Beats.DC");
                sb.Append("\r\n{");
                sb.Append("\r\n\tpublic class ");
                sb.Append(pTableModel.DotNetDataContextName + "  : BaseDC");
                sb.Append("\r\n\t{");
                sw.WriteLine(sb.ToString());
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
                    strParameter += CommonTask.PrepareInParameter(column, "obj" + pTableModel.TableNameAsTitle);
                }
                //strParameter = strParameter.Substring(0, strParameter.Length - 1);
                var primaryColumn = pTableModel.PropetyList.FirstOrDefault(pr => pr.IsPrimayKey);
                if (IsChild)
                {
                    sb.Append("\r\n\tpublic static string Save" + pTableModel.TableNameAsTitle + "(" + (SessionUtility.IsModelInterfaceRequired ? pTableModel.DotNetInterfaceName : pTableModel.DotNetModelName) + " " + objName + ", Database db,DbConnection dbConnection,DbCommand dbCommand,DbTransaction dbTransaction)");
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
                    sb.Append("\r\n\tpublic static string Save" + pTableModel.TableNameAsTitle + "(" + (SessionUtility.IsModelInterfaceRequired ? pTableModel.DotNetInterfaceName : pTableModel.DotNetModelName) + " " + objName + ")");
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

                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t\tobj" + ChildModel.DotNetModelName + "." + primaryColumn.SYSName + " = " + CommonTask.PrepareValueFromDB(primaryColumn.SYSType, "v" + primaryColumn.SYSName));
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

        private void DAL_InsertUpdateMethodWithAmarHelper(StringBuilder sb, TableModel pTableModel, Boolean IsChild = false)
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
                int i = 0;
                foreach (var column in pTableModel.PropetyList)
                {
                    strParameter += CommonTask.PrepareInParameterForAmarHelper(i, column, "obj" + pTableModel.TableNameAsTitle);
                    i++;
                }
                var primaryColumn = pTableModel.PropetyList.FirstOrDefault(pr => pr.IsPrimayKey);
                if (IsChild)
                {
                    sb.Append("\r\n\tpublic string Save" + pTableModel.TableNameAsTitle + "(" + (SessionUtility.IsModelInterfaceRequired ? pTableModel.DotNetInterfaceName : pTableModel.DotNetModelName) + " " + objName + ", Database db,DbConnection dbConnection,DbCommand dbCommand,DbTransaction dbTransaction)");
                    sb.Append("\r\n\t\t{");
                    sb.Append("\r\n\t\tstring sqlCommand = " + spName + ";");
                    sb.Append("\r\n\t\tusing (AdoHelper db = new AdoHelper(base.ConnectionString))");
                    sb.Append("\r\n\t\t{");
                }
                else
                {
                    sb.Append("\r\n\tpublic  string Save" + pTableModel.TableNameAsTitle + "(" + (SessionUtility.IsModelInterfaceRequired ? pTableModel.DotNetInterfaceName : pTableModel.DotNetModelName) + " " + objName + ")");
                    sb.Append("\r\n\t\t{");

                    sb.Append("\r\n\t\t\tstring sqlCommand = " + spName + ";");

                    sb.Append("\r\n\t\t\tusing (AdoHelper db = new AdoHelper(base.ConnectionString))");
                    sb.Append("\r\n\t\t\t\t{");
                    sb.Append("\r\n\t\t\t\t\t\ttry");
                    sb.Append("\r\n\t\t\t\t\t\t{");
                    sb.Append("\r\n\t\t\t\t\tstring[] Params = new string[" + i + "];");
                    sb.Append("\r\n\t\t\t\t\tdb.BeginTransaction();");
                }


                if (IsChild)
                {
                    sb.Append("\r\n\t\t\t\t\t\tdbCommand.Parameters.Clear();");
                }
                //sb.Append("\r\n\t\t\t\t\t\tDbParameter output = db.DbProviderFactory.CreateParameter();");
                //sb.Append("\r\n\t\t\t\t\t\toutput.ParameterName = " + dq + consTemp + primaryColumn.DBName + dq + ";");
                //sb.Append("\r\n\t\t\t\t\t\toutput.Size = " + primaryColumn.DBLength + ";");
                //sb.Append("\r\n\t\t\t\t\t\toutput.Value = " + objName + "." + primaryColumn.SYSName + ";");
                //sb.Append("\r\n\t\t\t\t\t\toutput.DbType = DbType." + primaryColumn.SYSType.Replace("?", "") + ";");
                //sb.Append("\r\n\t\t\t\t\t\toutput.Direction = ParameterDirection.InputOutput;");
                //sb.Append("\r\n\t\t\t\t\t\t\t");
                //sb.Append("\r\n\t\t\t\t\t\tdbCommand.Parameters.Add(output);");



                //sb.Append("\r\n\t\t");
                //sb.Append("\r\n\t\ttry");
                //sb.Append("\r\n\t\t{");

                sb.Append("\r\n\t\t\t\t\t\t\t\t " + strParameter);
                sb.Append("\r\n\t\t\t\t\t\t\t\t ");
                sb.Append("\r\n\t\t\t\t\t\t\t\tint result = db.PrepSPToNonQuery(sqlCommand,Params);");
                sb.Append("\r\n\t\t\t\t\t\t\t\tdb.CloseTransaction();");
                sb.Append("\r\n\t\t\t\t\t\t\t\tif (result > 0)");
                sb.Append("\r\n\t\t\t\t\t\t\t\t{");

                //sb.Append("\r\n\t\t\t\t\t\t\t\tstring v" + primaryColumn.SYSName + " = dbCommand.Parameters[" + dq + consTemp + primaryColumn.DBName + dq + "].Value.ToString();");

                if (pTableModel.IsParentTable && pTableModel.ChildTableModelList != null && pTableModel.ChildTableModelList.Count > 0)
                {
                    foreach (var ChildModel in pTableModel.ChildTableModelList)
                    {

                        sb.Append("\r\n\t\t\t\t\t\t\t\t\tif (" + objName + ".obj" + ChildModel.DotNetModelName + "List != null && " + objName + ".obj" + ChildModel.DotNetModelName + "List.Count > 0)              ");
                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t{                                                                     ");
                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\tforeach(var obj" + ChildModel.DotNetModelName + " in " + objName + ".obj" + ChildModel.DotNetModelName + "List)");
                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t\t{");

                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t\tobj" + ChildModel.DotNetModelName + "." + primaryColumn.SYSName + " = " + CommonTask.PrepareValueFromDB(primaryColumn.SYSType, "v" + primaryColumn.SYSName));
                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t\tSave" + ChildModel.TableNameAsTitle + "(obj" + ChildModel.DotNetModelName + ",  db,dbConnection, dbCommand, dbTransaction);");
                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t\t}");
                        sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t}                                                                 ");
                    }
                }
                sb.Append("\r\n\t\t\t\t\t\t\t\t\tif (result > 0)                                                       ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t{                                                                     ");
                //if (!IsChild)
                //    sb.AppendLine("\t\t\t\t\t\t\t\t\t\tdbTransaction.Commit();                                           ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t\treturn vSAVESUCCESS; ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t}                                                                     ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\telse                                                                  ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t{                                                                     ");
                if (!IsChild)
                    sb.AppendLine("\r\n\t\t\t\t\t\t\t\t\t\t\tdb.Rollback();                                         ");
                sb.AppendLine("\r\n\t\t\t\t\t\t\t\t\t\t\treturn vSAVEFAIL;                                          ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t}                                                                     ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t}                                                                         ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t}                                                                         ");
                sb.Append("\r\n\t\t\t\t\t\t\t\tcatch (Exception ex)                                                      ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t{                                                                         ");
                if (!IsChild)
                    sb.AppendLine("\r\n\t\t\t\t\t\t\t\t\t\tdb.Rollback();                                             ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t\tthrow new Exception(ex.Message);                                      ");
                sb.Append("\r\n\t\t\t\t\t\t\t\t\t}                                                                         ");
                if (!IsChild)
                {
                    //sb.Append("\r\n\t\t\t\t\t\t\t\t\tfinally                                                                   ");
                    //sb.Append("\r\n\t\t\t\t\t\t\t\t\t{                                                                         ");
                    //sb.Append("\r\n\t\t\t\t\t\t\t\t\t\tif (dbConnection.State == ConnectionState.Open)                       ");
                    //sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t{                                                                     ");
                    //sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t\tdbConnection.Close();                                             ");
                    //sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t\t}                                                                     ");
                    //sb.Append("\r\n\t\t\t\t\t\t\t\t\t\t}                                                                         ");
                }
                sb.Append("\r\n\t\t\t\t\t\t\t\treturn vSAVESUCCESS;}                                                                             ");
                sb.Append("\r\n\t\t\t\t\t\t\t}");


                if (pTableModel.IsParentTable && pTableModel.ChildTableModelList != null && pTableModel.ChildTableModelList.Count > 0)
                {
                    foreach (var ChildModel in pTableModel.ChildTableModelList)
                    {

                        DAL_InsertUpdateMethodWithAmarHelper(sb, ChildModel, true);
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
                        SPInSignature = CommonTask.PrepareInParameter(CompositeProps[0], " obj" + pTableModel.DotNetModelName).Replace(("obj" + pTableModel.DotNetModelName + "."), "p");
                        ChildMethodSignatureWitoutType = " p" + CompositeProps[0].SYSName;
                    }
                    else if (CompositeProps != null && CompositeProps.Count > 1)
                    {
                        foreach (var prop in CompositeProps)
                        {
                            DCMethodSignature = DCMethodSignature + prop.SYSType + " p" + prop.SYSName + ", ";
                            SPInSignature = SPInSignature + CommonTask.PrepareInParameter(prop, " obj" + pTableModel.DotNetModelName).Replace(("obj" + pTableModel.DotNetModelName + "."), "p");
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
                        strModelAttributes += CommonTask.PrepareModelAttributeFromDB(property, pTableModel.TableNameAsTitle);
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

        private void DAL_GKForAmarHelper(StringBuilder sb, TableModel pTableModel, string DCMethodSignature = "", string SPInSignature = "", Boolean pIsChild = false)
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
                int i = 0;

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
                        SPInSignature = CommonTask.PrepareInParameterForAmarHelper(i, CompositeProps[0], " obj" + pTableModel.DotNetModelName).Replace(("obj" + pTableModel.DotNetModelName + "."), "p");
                        ChildMethodSignatureWitoutType = " p" + CompositeProps[0].SYSName;
                        i++;
                    }
                    else if (CompositeProps != null && CompositeProps.Count > 1)
                    {

                        foreach (var prop in CompositeProps)
                        {
                            DCMethodSignature = DCMethodSignature + prop.SYSType + " p" + prop.SYSName + ", ";
                            SPInSignature = SPInSignature + CommonTask.PrepareInParameterForAmarHelper(i, prop, " obj" + pTableModel.DotNetModelName).Replace(("obj" + pTableModel.DotNetModelName + "."), "p");
                            i++;
                            ChildMethodSignatureWitoutType = ChildMethodSignatureWitoutType + " p" + CompositeProps[0].SYSName + ", ";
                        }
                        DCMethodSignature = DCMethodSignature.Substring(0, DCMethodSignature.Length - 2);
                        ChildMethodSignatureWitoutType = ChildMethodSignatureWitoutType.Substring(0, ChildMethodSignatureWitoutType.Length - 2);
                    }
                    else { }

                }
                //Model build
                //foreach (var property in pTableModel.PropetyList)
                //{
                //    if (!property.IsSkippable)
                //    {
                //        strModelAttributes += CommonTask.PrepareModelAttributeFromDB(property, pTableModel.TableNameAsTitle);
                //    }
                //}
                if (!pIsChild)
                    sb.Append("\r\n\tpublic  " + pTableModel.DotNetModelName + " Get" + pTableModel.TableNameAsTitle + "ById(" + DCMethodSignature + ")");
                else
                    sb.Append("\r\n\tpublic  " + pTableModel.DotNetModelName + " Get" + pTableModel.TableNameAsTitle + "ByParentId(" + DCMethodSignature + ")");
                sb.Append("\r\n\t{");
                sb.Append("\r\n\t\t" + pTableModel.DotNetModelName + " obj" + pTableModel.DotNetModelName + " = new " + pTableModel.DotNetModelName + "();");
                sb.Append("\r\n\t\tusing(AdoHelper db = new AdoHelper(base.ConnectionString))");
                sb.Append("\r\n\t\t{");

                sb.Append("\r\n\t\tstring sqlCommand = " + spName + ";");
                sb.Append("\r\n\t\t\t\t\tstring[] Params = new string[" + i + "];");

                sb.Append(SPInSignature);
                sb.Append("\r\n\t\ttry");
                sb.Append("\r\n\t\t{");
                sb.Append(string.Format("\r\n\t obj{0} = db.PrepSPToModel<{1}>(sqlCommand,Params);", pTableModel.DotNetModelName, pTableModel.DotNetModelName));

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
                if (!SessionUtility.IsModelInterfaceRequired)
                {
                    sb.Replace("I" + pTableModel.TableNameAsTitle, pTableModel.DotNetModelName);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void DAL_GAForAmarHelper(StringBuilder sb, TableModel pTableModel)
        {
            const string dq = @"""";

            string spName = "FSP_" + pTableModel.OriginalTableName + "_GA";
            spName = dq + spName + dq;
            try
            {

                sb.AppendLine("       public  List<I[{MODEL}]>  Get[{MODEL}]ALL() ");
                sb.AppendLine("{                                                                  ");
                sb.AppendLine(" List<I[{MODEL}]> obj[{MODEL}]List = new List<I[{MODEL}]>();               ");
                sb.Append("\r\n\t\tusing(AdoHelper db = new AdoHelper(base.ConnectionString))");
                sb.AppendLine("{                                                               ");
                sb.AppendLine("string sqlCommand = " + spName + ";                           ");
                sb.AppendLine("try                                                             ");
                sb.AppendLine("{                                                               ");
                sb.Append(string.Format("\r\n\t obj{0}List = db.SqlToList<{1}>(sqlCommand);", pTableModel.TableNameAsTitle, pTableModel.DotNetModelName));
                sb.AppendLine("}                                                               ");
                sb.AppendLine("catch(Exception ex)                                             ");
                sb.AppendLine("{                                                               ");
                sb.AppendLine("	throw new Exception(ex.Message);                            ");
                sb.AppendLine("}                                                               ");
                sb.AppendLine("return obj[{MODEL}]List;                                            ");
                sb.AppendLine("}                                                                  ");
                sb.AppendLine("}                                                                  ");

                sb.Replace("[{MODEL}]", pTableModel.TableNameAsTitle);
                if (!SessionUtility.IsModelInterfaceRequired)
                {
                    sb.Replace("I" + pTableModel.TableNameAsTitle, pTableModel.DotNetModelName);

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
                if (!SessionUtility.IsModelInterfaceRequired)
                {
                    sb.Replace("I" + pTableModel.TableNameAsTitle, pTableModel.DotNetModelName);

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
                        strInParameterList += CommonTask.PrepareInParameter(column, "obj" + pTableModel.TableNameAsTitle);
                    }
                }


                //Model build
                foreach (var property in pTableModel.PropetyList)
                {
                    if (!property.IsSkippable)
                    {
                        strModelAttributes += CommonTask.PrepareModelAttributeFromDB(property, "obj" + pTableModel.DotNetModelName);
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
                    sb.Append("\r\n\tpublic static string Delete" + pMethodPartName + "ByParentId(" + (SessionUtility.IsModelInterfaceRequired ? pTableModel.DotNetInterfaceName : pTableModel.DotNetModelName) + " obj" + pTableModel.DotNetModelName + ", Database db,DbConnection dbConnection,DbCommand dbCommand,DbTransaction dbTransaction)");
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
                        SPInSignature = CommonTask.PrepareInParameter(CompositeProps[0], " obj" + pTableModel.DotNetModelName);
                    }
                    else if (CompositeProps != null && CompositeProps.Count > 1)
                    {
                        foreach (var prop in CompositeProps)
                        {
                            DCMethodSignature = DCMethodSignature + prop.SYSType + " p" + prop.SYSName + ", ";
                            SPInSignature = SPInSignature + CommonTask.PrepareInParameter(prop, " obj" + pTableModel.DotNetModelName);
                        }
                        DCMethodSignature = DCMethodSignature.Substring(0, DCMethodSignature.Length - 2);
                    }
                    else { }

                    sb.Append("\r\n\tpublic static string Delete" + pTableModel.TableNameAsTitle + "(" + (SessionUtility.IsModelInterfaceRequired ? pTableModel.DotNetInterfaceName : pTableModel.DotNetModelName) + " obj" + pTableModel.DotNetModelName + ")");
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
                    sb.Append(CommonTask.PrepareInParameter(UpdateDate, " obj" + pTableModel.DotNetModelName));
                var UpdateUser = pTableModel.PropetyList.Find(pk => pk.SYSName == "UpdateUser");
                if (UpdateUser != null)
                    sb.Append(CommonTask.PrepareInParameter(UpdateUser, " obj" + pTableModel.DotNetModelName));

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
                        strModelAttributes = strModelAttributes + "\t\t\t\t\t" + CommonTask.PrepareModelAttributeFromDB(property, pTableModel.DotNetModelName);
                        strModelAttributes = strModelAttributes + "\r\n\t\t\t\tbreak;";
                    }
                }
                strModelAttributes = strModelAttributes + "\r\n\t\t\t\tdefault:";
                strModelAttributes = strModelAttributes + "\r\n\t\t\t\t\tbreak;";


                sb.Append("\r\n\tprivate static " + (SessionUtility.IsModelInterfaceRequired ? pTableModel.DotNetInterfaceName : pTableModel.DotNetModelName) + " Build" + pTableModel.DotNetModelName + "(" + (SessionUtility.IsModelInterfaceRequired ? pTableModel.DotNetInterfaceName : pTableModel.DotNetModelName) + " obj" + pTableModel.DotNetModelName + ",IDataReader dr)");
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
    }
}
