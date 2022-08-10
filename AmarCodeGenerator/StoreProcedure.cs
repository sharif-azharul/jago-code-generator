using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AmarCodeGenerator
{
    public partial class ClassGenerator : Form
    {
        #region Region Generate StoredProcedure

        //............................................. User Define Methods ...............................................

        private void GenerateStoredProcedures(TableModel pTableModel)
        {
            //Generate StoredProcedures automatically for selected tables ............................
            try
            {
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    string strSPname = string.Empty;

                    #region Create Empty txt file
                    sb = new System.Text.StringBuilder(SPFolderName + pTableModel.OriginalTableName);
                    sb.Append(".sql");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion

                    if (this.chkInsertSP.Checked)
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

                    if (this.chkSelectbyKeySP.Checked)
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
                    if (this.chkDeleteSP.Checked)
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
                LogError(ex);
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
                LogError(ex);
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
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE " + strSPname);
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
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE " + strSPname);
                sb.Append("\r\nAS");
                sb.Append("\r\n");
                sb.Append("\r\nSELECT ");
                sb.Append(strColumns);
                sb.Append("\r\nFROM " + pTableModel.OriginalTableName);
                sb.Append("\r\nWHERE  " + DeleteColumn + " = 0");
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
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE " + strSPname);
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
        private void Select_StoredProcedure(StreamWriter sw, string strSPname, ArrayList AttributeNameArrayList, ArrayList AttributeTypeArrayList_Sql)
        {
            try
            {
                string strAttributeName = AttributeNameArrayList[0].ToString();
                string strAttributeType = AttributeTypeArrayList_Sql[0].ToString();

                const string consTemp = @"@";
                string temp = consTemp + strAttributeName;
                string strColumns = string.Empty;
                string strParametersWithDataType = string.Empty;
                string strWhereConditions = string.Empty;

                for (int i = 0; i < AttributeNameArrayList.Count; i++)
                {
                    strColumns = strColumns + "\r\n" + AttributeNameArrayList[i].ToString() + ",";
                }
                strColumns = strColumns.Substring(0, strColumns.Length - 1);

                //Parameter with datatype
                for (int i = 0; i < AttributeNameArrayList.Count; i++)
                {
                    if (AttributeTypeArrayList_Sql[i].ToString().Contains("varchar"))
                    {
                        strParametersWithDataType = strParametersWithDataType + "\r\n" + consTemp + AttributeNameArrayList[i].ToString() + " " + AttributeTypeArrayList_Sql[i].ToString() + "(50)" + ",";
                    }
                    else
                    {
                        strParametersWithDataType = strParametersWithDataType + "\r\n" + consTemp + AttributeNameArrayList[i].ToString() + " " + AttributeTypeArrayList_Sql[i].ToString() + ",";
                    }
                }
                //remove "," from string
                strParametersWithDataType = strParametersWithDataType.Substring(0, strParametersWithDataType.Length - 1);

                //where conditions
                for (int i = 0; i < AttributeNameArrayList.Count - 1; i++)
                {
                    strWhereConditions = strWhereConditions + "\r\n( " + consTemp + AttributeNameArrayList[i].ToString() + " is null or " + consTemp + AttributeNameArrayList[i].ToString() + " = " + AttributeNameArrayList[i].ToString() + " ) and";
                }


                #region Select SP
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE " + strSPname);
                sb.Append("\r\n");
                sb.Append("\r\n" + strParametersWithDataType);
                sb.Append("\r\n");
                sb.Append("\r\nAS");
                sb.Append("\r\n");
                sb.Append("\r\nSelect ");
                sb.Append("\r\n");
                sb.Append(strColumns);
                sb.Append("\r\n");
                sb.Append("\r\nfrom " + strTable);
                sb.Append("\r\n");
                sb.Append("\r\nwhere " + strWhereConditions + "\r\n @IsActive = IsActive");
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
                    if (!UpdateSupportingColumns.ToUpper().Contains(property.DBName.ToUpper()))
                        strParametersWithoutDataType = strParametersWithoutDataType + "\r\n\t" + consTemp + "P_" + property.SYSName + ",";

                }
                strParametersWithoutDataType = strParametersWithoutDataType.Substring(0, strParametersWithoutDataType.Length - 1);
                foreach (var property in pTableModel.PropetyList)
                {
                    if (!UpdateSupportingColumns.ToUpper().Contains(property.DBName.ToUpper()))
                        strColumns = strColumns + "\r\n\t" + property.DBName + ",";
                }
                //remove "," from string
                strColumns = strColumns.Substring(0, strColumns.Length - 1);

                // update--------
                string strUpdateColumnList = string.Empty;
                foreach (var property in pTableModel.PropetyList)
                {
                    if (!property.IsPrimayKey && !InsertSupportingColumns.ToUpper().Contains(property.DBName.ToUpper()))
                        strUpdateColumnList = strUpdateColumnList + "\r\n\t" + property.DBName + " = " + consTemp + "P_" + property.SYSName + ",";
                }

                //remove "," from string
                strUpdateColumnList = strUpdateColumnList.Substring(0, strUpdateColumnList.Length - 1);

                #region Insert SP
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE " + strSPname);
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
        private void Insert_StoredProcedure(StreamWriter sw, string strSPname, ArrayList AttributeNameArrayList, ArrayList AttributeTypeArrayList_Sql)
        {
            try
            {
                const string consTemp = @"@";
                string temp = string.Empty;
                string strColumns = string.Empty;
                string strParametersWithDataType = string.Empty;
                string strParametersWithoutDataType = string.Empty;

                //Parameters with datatype
                for (int i = 1; i < AttributeNameArrayList.Count - 1; i++)
                {
                    if (AttributeTypeArrayList_Sql[i].ToString().Contains("varchar"))
                    {
                        strParametersWithDataType = strParametersWithDataType + "\r\n" + consTemp + AttributeNameArrayList[i].ToString() + " " + AttributeTypeArrayList_Sql[i].ToString() + "(50)" + ",";
                    }
                    else
                    {
                        strParametersWithDataType = strParametersWithDataType + "\r\n" + consTemp + AttributeNameArrayList[i].ToString() + " " + AttributeTypeArrayList_Sql[i].ToString() + ",";
                    }
                }
                //remove "," from string
                strParametersWithDataType = strParametersWithDataType.Substring(0, strParametersWithDataType.Length - 1);


                //Parameters without datatype
                for (int i = 1; i < AttributeNameArrayList.Count - 1; i++)
                {
                    if (AttributeTypeArrayList_Sql[i].ToString().Contains("varchar"))
                    {
                        strParametersWithoutDataType = strParametersWithoutDataType + "\r\n" + consTemp + AttributeNameArrayList[i].ToString() + ",";
                    }
                    else
                    {
                        strParametersWithoutDataType = strParametersWithoutDataType + "\r\n" + consTemp + AttributeNameArrayList[i].ToString() + ",";
                    }
                }
                //concatenate 1 for IsActive 
                strParametersWithoutDataType = strParametersWithoutDataType + "\r\n1";


                for (int i = 1; i < AttributeNameArrayList.Count; i++)
                {
                    strColumns = strColumns + "\r\n" + AttributeNameArrayList[i].ToString() + ",";
                }
                //remove "," from string
                strColumns = strColumns.Substring(0, strColumns.Length - 1);

                #region Insert SP
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE " + strSPname);
                sb.Append("\r\n");
                sb.Append("\r\n" + strParametersWithDataType);
                sb.Append("\r\n");
                sb.Append("\r\nAS");
                sb.Append("\r\n");
                sb.Append("\r\nInsert into " + strTable);
                sb.Append("\r\n(");
                sb.Append(strColumns);
                sb.Append("\r\n)");
                sb.Append("\r\nvalues");
                sb.Append("\r\n(");
                sb.Append(strParametersWithoutDataType);
                sb.Append("\r\n)");
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
        private void Update_StoredProcedure(StreamWriter sw, string strSPname, ArrayList AttributeNameArrayList, ArrayList AttributeTypeArrayList_Sql)
        {
            try
            {
                const string consTemp = @"@";
                string strTemp = string.Empty;
                string strColumns = string.Empty;
                string strParameters = string.Empty;

                for (int i = 0; i < AttributeNameArrayList.Count - 1; i++)
                {
                    if (AttributeTypeArrayList_Sql[i].ToString().Contains("varchar"))
                    {
                        strParameters = strParameters + "\r\n" + consTemp + AttributeNameArrayList[i].ToString() + "    " + AttributeTypeArrayList_Sql[i].ToString() + "(50)" + ",";
                    }
                    else
                    {
                        strParameters = strParameters + "\r\n" + consTemp + AttributeNameArrayList[i].ToString() + "    " + AttributeTypeArrayList_Sql[i].ToString() + ",";
                    }
                }
                strParameters = strParameters.Substring(0, strParameters.Length - 1);

                for (int i = 1; i < AttributeNameArrayList.Count - 1; i++)
                {
                    strTemp = strTemp + "\r\n\t" + AttributeNameArrayList[i].ToString() + " = " + consTemp + AttributeNameArrayList[i].ToString() + ",";
                }
                //strTemp = strTemp.Substring(0, strTemp.Length - 1);
                strTemp = strTemp + "\r\n\t IsActive = 1";

                #region Update SP
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE " + strSPname);
                sb.Append("\r\n");
                sb.Append("\r\n" + strParameters);
                sb.Append("\r\n");
                sb.Append("\r\nAS");
                sb.Append("\r\n");
                sb.Append("\r\nUpdate " + strTable);
                sb.Append("\r\n");
                sb.Append("\r\nSet");
                sb.Append("\r\n\t" + strTemp);
                sb.Append("\r\nWhere");
                sb.Append("\r\n");
                sb.Append("\r\n" + AttributeNameArrayList[0].ToString() + " = " + consTemp + AttributeNameArrayList[0].ToString());
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
                    if (property.DBName == DeleteColumn || DeleteSupportingColumns.Contains(property.DBName) || property.IsPrimayKey)
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
                    if (property.DBName == DeleteColumn || DeleteSupportingColumns.Contains(property.DBName))
                    {
                        strDeleteColumnList = strDeleteColumnList + "\r\n\t" + property.DBName + " = " + consTemp + "P_" + property.SYSName + ",";
                    }
                }

                //remove "," from string
                strDeleteColumnList = strDeleteColumnList.Substring(0, strDeleteColumnList.Length - 1);

                #region Delete SP
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE " + strSPname);
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
        private void SoftDelete_StoredProcedure(StreamWriter sw, string strSPname, ArrayList AttributeNameArrayList, ArrayList AttributeTypeArrayList_Sql)
        {
            try
            {
                string strAttributeName = AttributeNameArrayList[0].ToString();
                string strAttributeType = AttributeTypeArrayList_Sql[0].ToString();

                const string consTemp = @"@";
                string temp = consTemp + strAttributeName;
                string strColumns = string.Empty;

                #region Delete SP
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE " + strSPname);
                sb.Append("\r\n");
                sb.Append("\r\n" + temp + " " + strAttributeType);
                sb.Append("\r\n");
                sb.Append("\r\nAS");
                sb.Append("\r\n");
                sb.Append("\r\nUpdate ");
                sb.Append("\r\n" + strTable + "\r\n");
                sb.Append("\r\nset IsActive = 0");
                sb.Append("\r\n");
                sb.Append("\r\nwhere " + strAttributeName + "=" + consTemp + strAttributeName);
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
        private void PermanentlyDelete_StoredProcedure(StreamWriter sw, string strSPname, ArrayList AttributeNameArrayList, ArrayList AttributeTypeArrayList_Sql)
        {
            try
            {
                string strAttributeName = AttributeNameArrayList[0].ToString();
                string strAttributeType = AttributeTypeArrayList_Sql[0].ToString();

                const string consTemp = @"@";
                string temp = consTemp + strAttributeName;
                string strColumns = string.Empty;

                #region Delete SP
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\nCREATE PROCEDURE " + strSPname);
                sb.Append("\r\n");
                sb.Append("\r\n" + temp + " " + strAttributeType);
                sb.Append("\r\n");
                sb.Append("\r\nAS");
                sb.Append("\r\n");
                sb.Append("\r\nDelete ");
                sb.Append("\r\n");
                sb.Append("\r\nfrom " + strTable);
                sb.Append("\r\n");
                sb.Append("\r\nwhere " + strAttributeName + "=" + temp);
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

        #endregion
    }
}
