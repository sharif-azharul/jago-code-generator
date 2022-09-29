using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace AmarCodeGenerator
{
    public class Model
    {


        public void GenerateModelClass(TableModel pTableModel)
        {
            try
            {

                CommonTask.CreateDirectory(SessionUtility.ModelFolder);

                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.ModelFolder + pTableModel.DotNetModelName);
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
        public void GenerateModelFromTemplate(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.ModelFolder + pTable.DotNetModelName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "ModelTemplate.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void GenerateModelFromTemplateXHR(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.ModelFolder + pTable.OriginalTableName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "XHRModelTemplate.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #region ---------------- ASP NET ZERO
        public void GenerateCreateInputDtoFromTemplateAspNetZero(TableModel pTable) {
            if (pTable != null) {
                try {
                    CommonTask.CreateDirectory(SessionUtility.ModelFolder + pTable.ZeroFolderName + @"\Dto\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.ModelFolder + pTable.ZeroFolderName+@"\Dto\" + pTable.ZeroCreateInputDtoName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "ZeroModelCreateInputTemplate.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null) {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                } catch (Exception ex) {
                    throw ex;
                }
            }
        }

        public void GenerateCreateFilterDtoFromTemplateAspNetZero(TableModel pTable) {
            if (pTable != null) {
                try {
                    CommonTask.CreateDirectory(SessionUtility.ModelFolder + pTable.ZeroFolderName + @"\Dto\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.ModelFolder + pTable.ZeroFolderName + @"\Dto\" + pTable.ZeroFilterInputDtoName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "ZeroModelFilterInputTemplate.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null) {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                } catch (Exception ex) {
                    throw ex;
                }
            }
        }

        public void GenerateOutputDtoFromTemplateAspNetZero(TableModel pTable) {
            if (pTable != null) {
                try {
                    CommonTask.CreateDirectory(SessionUtility.ModelFolder + pTable.ZeroFolderName + @"\Dto\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.ModelFolder + pTable.ZeroFolderName + @"\Dto\" + pTable.ZeroOutputDtoName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "ZeroModelOutputTemplate.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null) {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                } catch (Exception ex) {
                    throw ex;
                }
            }
        }

        public void GenerateUpdateDtoFromTemplateAspNetZero(TableModel pTable) {
            if (pTable != null) {
                try {
                    CommonTask.CreateDirectory(SessionUtility.ModelFolder + pTable.ZeroFolderName + @"\Dto\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.ModelFolder + pTable.ZeroFolderName + @"\Dto\" + pTable.ZeroUpdateInputDtoName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "ZeroModelUpdateInputTemplate.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null) {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                } catch (Exception ex) {
                    throw ex;
                }
            }
        }

        public void GenerateServiceFromTemplateAspNetZero(TableModel pTable) {
            if (pTable != null) {
                try {

                    var folderPath = SessionUtility.RootFolderName  + @"\Service\";
                    CommonTask.CreateDirectory(folderPath + pTable.ZeroFolderName + @"\");
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(folderPath + pTable.ZeroFolderName + @"\" + pTable.ZeroServiceName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "ZeroServiceTemplate.html"));


                    sw.WriteLine(sb.ToString().Replace("--",""));
                    #region Close file
                    if (sw != null) {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                } catch (Exception ex) {
                    throw ex;
                }
            }
        }

        #endregion
        public void GenerateModelClassWithoutAnnotation(TableModel pTableModel)
        {
            try
            {

                CommonTask.CreateDirectory(SessionUtility.ModelFolder);

                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.ModelFolder + pTableModel.DotNetModelName);
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
                            this.WritePublicPropertiesWithoutAnnotation(sb, column);
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

        private void WritePublicPropertiesWithoutAnnotation(StringBuilder sb, ColumnModel pColumn)
        {
            const string dq = @"""";
            try
            {
                //if (pColumn.IsPrimayKey)
                //{
                //    sb.AppendLine("\t\t[Key]");
                //}
                //if (!pColumn.IsNullable)
                //{
                //    sb.AppendLine("\t\t[Required]");
                //}
                //if (pColumn.SYSType.ToLower() == "string")
                //{
                //    sb.AppendLine("\t\t[StringLength(" + pColumn.DBLength + ", ErrorMessage = " + dq + "Maximum characters limit to " + pColumn.DBLength + dq + ")]");

                //    //sb.AppendFormat("\r\n\t\t[Range(1,{0})]", pColumn.DBLength);
                //}

                //if (pColumn.SYSType.ToLower().Contains("int") && !pColumn.IsNullable)
                //{
                //    sb.AppendLine("\t\t[Range(1," + "9".PadLeft(Convert.ToInt32(pColumn.DBLength), '9') + ", ErrorMessage =" + dq + "Value must be less tha or equal to " + "9".PadLeft(Convert.ToInt32(pColumn.DBLength), '9') + dq + ")]");
                //}

                //if (pColumn.SYSType.ToLower().Contains("int") && pColumn.IsNullable)
                //{
                //    sb.AppendLine("\t\t[Range(0," + "9".PadLeft(Convert.ToInt32(pColumn.DBLength), '9') + ", ErrorMessage =" + dq + "Value must be less tha or equal to " + "9".PadLeft(Convert.ToInt32(pColumn.DBLength), '9') + dq + ")]");
                //}

                //sb.AppendLine("\t\t[Display(Name = " + dq + pColumn.DisplayName + dq + ")]");
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

        public void GenerateModelFromTemplateDataAnotation(TableModel pTable)
        {
            if (pTable != null)
            {
                try
                {
                    CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.ModelFolder + pTable.DotNetModelName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion
                    sb = new System.Text.StringBuilder();

                    //CommonTask.CreateDirectory(SessionUtility.ModelFolder);
                    sb.Append(CommonTask.PrepareMailContent(pTable, "ModelTemplateDataAnnotation.html"));


                    sw.WriteLine(sb.ToString());
                    #region Close file
                    if (sw != null)
                    {
                        //sw.WriteLine("\r\n\t}\r\n}");
                        sw.Close();
                    }
                    #endregion

                }
                catch (Exception ex)
                {
                    throw ex;
                }
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
                sb.Append("\r\nusing " + SessionUtility.NameSpaceFirstPart + ".Common.Interfaces; ");
                sb.Append("\r\nusing " + SessionUtility.NameSpaceFirstPart + ".Common.Model; ");
                if (SessionUtility.IsModelInterfaceRequired)
                {
                    sb.Append("\r\nusing " + SessionUtility.NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".Interfaces; ");
                }
                sb.Append("\r\nusing System.ComponentModel.DataAnnotations;");

                sb.Append("\r\n");
                sb.Append("\r\n");
                if (pTableModel.DotNetModelName.Contains("HRM"))
                {
                    sb.Append("\r\nnamespace " + SessionUtility.NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".Model");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.HR");
                }
                else if (pTableModel.DotNetModelName.Contains("CMN"))
                {
                    sb.Append("\r\nnamespace " + SessionUtility.NameSpaceFirstPart + ".Model.Common");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.Common");
                }
                else
                {
                    sb.Append("\r\nnamespace " + SessionUtility.NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".Model");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.Common");
                }
                sb.Append("\r\n{");
                sb.Append("\r\n\tpublic class ");
                sb.Append(pTableModel.DotNetModelName + ": BaseModel" + (SessionUtility.IsModelInterfaceRequired ? (", " + pTableModel.DotNetInterfaceName) : ""));
                sb.Append("\r\n\t{");
                sw.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
