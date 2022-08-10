using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AmarCodeGenerator
{
    public class StoreProcedureLayer
    {
        public void GenerateStoredProcedures(TableModel pTableModel)
        {
            //Generate StoredProcedures automatically for selected tables ............................
            try
            {
                CommonTask.CreateDirectory(SessionUtility.SPFolderName);

                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    string strSPname = string.Empty;

                    #region Create Empty txt file
                    sb = new System.Text.StringBuilder(SessionUtility.SPFolderName + pTableModel.OriginalTableName);
                    sb.Append(".sql");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion

                    //if (this.chkInsertSP.Checked)
                    {
                        #region Write Insert SP Script
                        strSPname = "FSP_" + pTableModel.OriginalTableName + "_INSERT_UPDATE".ToUpper();
                        SP_Heading(sw, strSPname);
                        Search_SP_in_DB(sw, strSPname);
                        Set_QuotedIdentifierAndANSInull(sw);
                        SP_InsertUpdate(sw, strSPname, pTableModel);
                        Set_QuotedIdentifierAndANSInull(sw);
                        sb = new StringBuilder();
                        sb.Append("\r\n");
                        sb.Append("\r\n");
                        sb.Append("\r\n");
                        sw.WriteLine(sb.ToString());
                        #endregion
                    }

                   // if (this.chkSelectbyKeySP.Checked)
                    {
                        #region Write Select by key SP Script
                        strSPname = "FSP_" + pTableModel.OriginalTableName + "_GK";
                        SP_Heading(sw, strSPname);
                        Search_SP_in_DB(sw, strSPname);
                        Set_QuotedIdentifierAndANSInull(sw);
                        SP_GK(sw, strSPname, pTableModel);
                        Set_QuotedIdentifierAndANSInull(sw);
                        sb = new StringBuilder();
                        sb.Append("\r\n");
                        sb.Append("\r\n");
                        sb.Append("\r\n");
                        sw.WriteLine(sb.ToString());
                        #endregion

                        #region Write Select by ALL SP Script
                        strSPname = "FSP_" + pTableModel.OriginalTableName + "_GA";
                        SP_Heading(sw, strSPname);
                        Search_SP_in_DB(sw, strSPname);
                        Set_QuotedIdentifierAndANSInull(sw);
                        SP_GetALL(sw, strSPname, pTableModel);
                        Set_QuotedIdentifierAndANSInull(sw);
                        sb = new StringBuilder();
                        sb.Append("\r\n");
                        sb.Append("\r\n");
                        sb.Append("\r\n");
                        sw.WriteLine(sb.ToString());
                        #endregion

                        #region Write Select by ALL SP Script
                        strSPname = "FSP_" + pTableModel.OriginalTableName + "_SEARCH";
                        SP_Heading(sw, strSPname);
                        Search_SP_in_DB(sw, strSPname);
                        Set_QuotedIdentifierAndANSInull(sw);
                        SP_GetBySearch(sw, strSPname, pTableModel);
                        Set_QuotedIdentifierAndANSInull(sw);
                        sb = new StringBuilder();
                        sb.Append("\r\n");
                        sb.Append("\r\n");
                        sb.Append("\r\n");
                        sw.WriteLine(sb.ToString());
                        #endregion
                    }
                    //if (this.chkDeleteSP.Checked)
                    {
                        #region Write Delete SP Script
                        strSPname = "FSP_" + pTableModel.OriginalTableName + "_DELETE";
                        SP_Heading(sw, strSPname);
                        Search_SP_in_DB(sw, strSPname);
                        Set_QuotedIdentifierAndANSInull(sw);
                        SP_Delete_Soft(sw, strSPname, pTableModel);
                        Set_QuotedIdentifierAndANSInull(sw);
                        sb = new StringBuilder();
                        sb.Append("\r\n");
                        sb.Append("\r\n");
                        sb.Append("\r\n");
                        sw.WriteLine(sb.ToString());
                        #endregion
                    }

                    #region Close file
                    if (sw != null)
                    {
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
        private void SP_Heading(StreamWriter sw, string strSPname)
        {
            try
            {
                //write SP name
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\n--------- " + strSPname);
                sb.Append("\r\n");
                sw.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void Search_SP_in_DB(StreamWriter sw, string strSPname)
        {
            try
            {
                //search SP in database if SP exist then delete it
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nif exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[" + strSPname + "]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)");
                sb.Append("\r\ndrop procedure [dbo].[" + strSPname + "]");
                sb.Append("\r\nGo");
                sw.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                CommonTask.LogError(ex);
            }
        }
        private void Set_QuotedIdentifierAndANSInull(StreamWriter sw)
        {
            try
            {
                //set quote identifier and ANSI null
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nSET QUOTED_IDENTIFIER OFF ");
                sb.Append("\r\nGo");
                sb.Append("\r\nSET ANSI_NULLS OFF");
                sb.Append("\r\nGo");
                sw.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                CommonTask.LogError(ex);
            }
        }
        private void SP_InsertUpdate(StreamWriter sw, string strSPname, TableModel pTableModel)
        {
            try
            {
                var PrimaryColumns = pTableModel.PropetyList.FindAll(rr => rr.IsPrimayKey == true);
                const string consTemp = @"@";
                string temp = string.Empty;
                string strColumns = string.Empty;
                string strParametersWithDataType = string.Empty;
                string strParametersWithoutDataType = string.Empty;
                string UpdateWhereConditions = string.Empty;

                foreach (var prCol in PrimaryColumns)
                {
                    UpdateWhereConditions = UpdateWhereConditions + prCol.DBName + "= " + consTemp + "P_" + prCol.SYSName + " AND ";
                }
                if(!string.IsNullOrEmpty(UpdateWhereConditions))
                UpdateWhereConditions = UpdateWhereConditions.Substring(0, UpdateWhereConditions.Length - 4);
                //Parameters with datatype
                foreach (var property in pTableModel.PropetyList)
                {

                    if (property.DBType.ToLower().Contains("varchar"))
                    {
                        strParametersWithDataType = strParametersWithDataType + "\r\n\t" + consTemp + "P_" + property.SYSName + " " + property.OriginalDBType + "(" + property.DBLength + ")" + (property.IsNullable ? " = NULL ," : " ,");
                    }
                    else
                    {
                        strParametersWithDataType = strParametersWithDataType + "\r\n\t" + consTemp + "P_" + property.SYSName + " " + property.OriginalDBType + (property.IsNullable ? " = NULL ," : " ,");
                    }
                }
                //remove "," from string
                strParametersWithDataType = strParametersWithDataType.Substring(0, strParametersWithDataType.Length - 1);


                //Parameters without datatype for INSERT
                foreach (var property in pTableModel.PropetyList)
                {
                    if (!SessionUtility.UpdateSupportingColumns.ToUpper().Contains(property.DBName.ToUpper()))
                        strParametersWithoutDataType = strParametersWithoutDataType + "\r\n\t" + consTemp + "P_" + property.SYSName + ",";

                }
                strParametersWithoutDataType = strParametersWithoutDataType.Substring(0, strParametersWithoutDataType.Length - 1);
                foreach (var property in pTableModel.PropetyList)
                {
                    if (!SessionUtility.UpdateSupportingColumns.ToUpper().Contains(property.DBName.ToUpper()))
                        strColumns = strColumns + "\r\n\t" + property.DBName + ",";
                }
                //remove "," from string
                strColumns = strColumns.Substring(0, strColumns.Length - 1);

                // update--------
                string strUpdateColumnList = string.Empty;
                foreach (var property in pTableModel.PropetyList)
                {
                    if (!property.IsPrimayKey && !SessionUtility.InsertSupportingColumns.ToUpper().Contains(property.DBName.ToUpper()))
                        strUpdateColumnList = strUpdateColumnList + "\r\n\t" + property.DBName + " = " + consTemp + "P_" + property.SYSName + ",";
                }

                //remove "," from string
                strUpdateColumnList = strUpdateColumnList.Substring(0, strUpdateColumnList.Length - 1);

                #region Insert SP
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE [dbo].[" + strSPname+"]");
                sb.Append("\r\n(" + strParametersWithDataType);
                sb.Append("\r\n)");
                sb.Append("\r\nAS");
                sb.Append("\r\n");
                sb.Append("\r\n IF EXISTS (SELECT " + PrimaryColumns[0].DBName + " FROM " + pTableModel.OriginalTableName + " WHERE "
                    + UpdateWhereConditions + " )");
                sb.Append("\r\n");
                sb.Append("\r\nUPDATE " + pTableModel.OriginalTableName);
                sb.Append("\r\nSET");
                sb.Append(strUpdateColumnList);
                sb.Append("\r\nWHERE " + UpdateWhereConditions);
                sb.Append("\r\n  ELSE");
                sb.Append("\r\nINSERT INTO " + pTableModel.OriginalTableName);
                sb.Append("\r\n(");
                sb.Append(strColumns);
                sb.Append("\r\n)");
                sb.Append("\r\nVALUES");
                sb.Append("\r\n(");
                sb.Append(strParametersWithoutDataType);
                sb.Append("\r\n)");
                sb.Append("\r\nSELECT CASE WHEN " + consTemp + "P_" + PrimaryColumns[0].SYSName + " IS NULL THEN @@IDENTITY ELSE " + consTemp + "P_" + PrimaryColumns[0].SYSName +
                    " END as P_" + PrimaryColumns[0].SYSName);
                sb.Append("\r\nGo");
                sb.Append("\r\n");
                sw.WriteLine(sb.ToString());
                #endregion

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void SP_GK(StreamWriter sw, string strSPname, TableModel pTableModel)
        {
            try
            {
                var PrimaryColumns = pTableModel.PropetyList.FindAll(PR => PR.IsPrimayKey);

                const string consTemp = @"@";
                // string temp = consTemp + PrimaryColumn.SYSName;
                string strColumns = string.Empty;
                string strParametersWithDataType = string.Empty;
                string strWhereConditions = string.Empty;

                foreach (var property in pTableModel.PropetyList)
                {
                    strColumns = strColumns + "\r\n\t" + property.DBName + ",";
                }
                strColumns = strColumns.Substring(0, strColumns.Length - 1);

                //Parameter with datatype
                foreach (var PrimaryColumn in PrimaryColumns)
                {
                    if (PrimaryColumn.DBType.ToLower().Contains("varchar"))
                    {
                        strParametersWithDataType = strParametersWithDataType + "\r\n" + consTemp + "P_" + PrimaryColumn.SYSName + " " + PrimaryColumn.OriginalDBType + "(" + PrimaryColumn.DBLength + "),";
                    }
                    else
                    {
                        strParametersWithDataType = strParametersWithDataType + "\r\n" + consTemp + "P_" + PrimaryColumn.SYSName + " " + PrimaryColumn.OriginalDBType + ",";
                    }

                    //where conditions
                    strWhereConditions = strWhereConditions + "\r\n" + PrimaryColumn.DBName + " = " + consTemp + "P_" + PrimaryColumn.SYSName + " AND ";
                }
                strParametersWithDataType = strParametersWithDataType.Substring(0, strParametersWithDataType.Length - 1);

                strWhereConditions = strWhereConditions.Substring(0, strWhereConditions.Length - 4);
                #region Select SP
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE  [dbo].[" + strSPname + "]");
                sb.Append("\r\n" + strParametersWithDataType);
                sb.Append("\r\nAS");
                sb.Append("\r\nSELECT ");
                sb.Append(strColumns);
                sb.Append("\r\nFROM " + pTableModel.OriginalTableName);
                sb.Append("\r\nWHERE " + strWhereConditions);
                sb.Append("\r\n");
                sb.Append("\r\nGo");
                sb.Append("\r\n");
                sw.WriteLine(sb.ToString());
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SP_GetALL(StreamWriter sw, string strSPname, TableModel pTableModel)
        {
            try
            {

                const string consTemp = @"@";
                string strColumns = string.Empty;
                string strParametersWithDataType = string.Empty;
                string strWhereConditions = string.Empty;

                foreach (var property in pTableModel.PropetyList)
                {
                    strColumns = strColumns + "\r\n\t" + property.DBName + ",";
                }
                strColumns = strColumns.Substring(0, strColumns.Length - 1);

                //Parameter with datatype



                #region Select SP
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE  [dbo].[" + strSPname + "]");
                sb.Append("\r\nAS");
                sb.Append("\r\n");
                sb.Append("\r\nSELECT ");
                sb.Append(strColumns);
                sb.Append("\r\nFROM " + pTableModel.OriginalTableName);
                sb.Append("\r\nWHERE  " + SessionUtility.DeleteColumn + " = 1");
                sb.Append("\r\n");
                sb.Append("\r\nGo");
                sb.Append("\r\n");
                sw.WriteLine(sb.ToString());
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SP_GetBySearch(StreamWriter sw, string strSPname, TableModel pTableModel)
        {
            try
            {
                var PrimaryColumn = pTableModel.PropetyList.Find(PK => PK.IsPrimayKey);

                const string consTemp = @"@";
                string temp = consTemp + PrimaryColumn.SYSName;
                string strColumns = string.Empty;
                string strParametersWithDataType = string.Empty;
                string strWhereConditions = string.Empty;

                foreach (var property in pTableModel.PropetyList)
                {
                    strColumns = strColumns + "\r\n\t" + property.DBName + ",";
                }
                strColumns = strColumns.Substring(0, strColumns.Length - 1);

                //Parameter with datatype
                foreach (var property in pTableModel.PropetyList)
                {
                    if (property.DBType.ToLower().Contains("varchar"))
                    {
                        strParametersWithDataType = strParametersWithDataType + "\r\n\t" + consTemp + "P_" + property.SYSName + " " + property.OriginalDBType + "(" + property.DBLength + ")" + (property.IsNullable ? " = NULL ," : " ,");
                    }
                    else
                    {
                        strParametersWithDataType = strParametersWithDataType + "\r\n\t" + consTemp + "P_" + property.SYSName + " " + property.OriginalDBType + (property.IsNullable ? " = NULL ," : " ,");
                    }
                }
                //remove "," from string
                strParametersWithDataType = strParametersWithDataType.Substring(0, strParametersWithDataType.Length - 1);

                //where conditions
                foreach (var property in pTableModel.PropetyList)
                {
                    if (property.DBType.ToLower().Contains("varchar"))
                        strWhereConditions = strWhereConditions + "\r\n\t( " + consTemp + "P_" + property.SYSName + " is null OR  " + property.DBName + " LIKE '%' + " + consTemp + "P_" + property.SYSName + " + '%' ) AND";
                    else
                        strWhereConditions = strWhereConditions + "\r\n\t( " + consTemp + "P_" + property.SYSName + " IS NULL or " + property.DBName + " = " + consTemp + "P_" + property.SYSName + " ) AND";
                }

                strWhereConditions = strWhereConditions.Substring(0, strWhereConditions.Length - 4);
                #region Select SP
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE  [dbo].[" + strSPname + "]");
                sb.Append("\r\n(");
                sb.Append(strParametersWithDataType);
                sb.Append("\r\n)");
                sb.Append("\r\nAS");
                sb.Append("\r\nSELECT ");
                sb.Append(strColumns);
                sb.Append("\r\nFROM " + pTableModel.OriginalTableName);
                sb.Append("\r\nWHERE " + strWhereConditions);
                sb.Append("\r\n");
                sb.Append("\r\nGo");
                sb.Append("\r\n");
                sw.WriteLine(sb.ToString());
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SP_Delete_Soft(StreamWriter sw, string strSPname, TableModel pTableModel)
        {
            try
            {
                var PrimaryColumns = pTableModel.PropetyList.FindAll(pr => pr.IsPrimayKey);

                const string consTemp = @"@";
                //string temp = consTemp + strAttributeName;
                string strColumns = string.Empty;

                string strParametersWithDataType = string.Empty;
                string DeleteWhereCondition = string.Empty;
                foreach (var prCol in PrimaryColumns)
                {
                    DeleteWhereCondition = DeleteWhereCondition + prCol.DBName + " = " + consTemp + "P_" + prCol.SYSName + " AND ";
                }
                DeleteWhereCondition = DeleteWhereCondition.Substring(0, DeleteWhereCondition.Length - 4);
                //Parameters with datatype
                foreach (var property in pTableModel.PropetyList)
                {
                    if (property.DBName == SessionUtility.DeleteColumn || SessionUtility.DeleteSupportingColumns.Contains(property.DBName) || property.IsPrimayKey)
                    {
                        if (property.DBType.ToLower().Contains("varchar"))
                        {
                            strParametersWithDataType = strParametersWithDataType + "\r\n\t" + consTemp + "P_" + property.SYSName + " " + property.OriginalDBType + "(" + property.DBLength + ")" + ",";
                        }
                        else
                        {
                            strParametersWithDataType = strParametersWithDataType + "\r\n\t" + consTemp + "P_" + property.SYSName + " " + property.OriginalDBType + ",";
                        }
                    }
                }
                //remove "," from string
                strParametersWithDataType = strParametersWithDataType.Substring(0, strParametersWithDataType.Length - 1);
                // Delete--------
                string strDeleteColumnList = string.Empty;
                foreach (var property in pTableModel.PropetyList)
                {
                    if (property.DBName == SessionUtility.DeleteColumn || SessionUtility.DeleteSupportingColumns.Contains(property.DBName))
                    {
                        strDeleteColumnList = strDeleteColumnList + "\r\n\t" + property.DBName + " = " + consTemp + "P_" + property.SYSName + ",";
                    }
                }

                //remove "," from string
                if(!string.IsNullOrEmpty(strDeleteColumnList))
                strDeleteColumnList = strDeleteColumnList.Substring(0, strDeleteColumnList.Length - 1);

                #region Delete SP
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE  [dbo].[" + strSPname + "]");
                sb.Append("\r\n(");
                sb.Append(strParametersWithDataType);
                sb.Append("\r\n)");
                sb.Append("\r\nAS");
                sb.Append("\r\n");
                sb.Append("\r\nUPDATE " + pTableModel.OriginalTableName + " SET ");
                sb.Append(strDeleteColumnList);
                sb.Append("\r\nWHERE " + DeleteWhereCondition);
                sb.Append("\r\n");
                sb.Append("\r\nGo");
                sb.Append("\r\n");
                sw.WriteLine(sb.ToString());
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
