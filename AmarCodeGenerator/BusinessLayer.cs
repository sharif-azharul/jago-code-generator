using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AmarCodeGenerator
{
    public class BusinessLayer
    {
        //BLL-------------------------------------
        public void GenerateBLLClass(TableModel pTableModel)
        {
            try
            {
                CommonTask.CreateDirectory(SessionUtility.BLLFolder);
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility.BLLFolder + pTableModel.DotNetBLLName);
                    // sb = new System.Text.StringBuilder(lstrTableName);
                    sb.Append(".cs");
                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
                    sw = lobjFileInfo.CreateText();
                    #endregion



                    #region Write Namespaces
                    this.WriteBLLNamespaces(sw, pTableModel);
                    #endregion

                    #region Write Class Default Constructor
                    WriteDefaultConstructor(sw, pTableModel);
                    #endregion




                    sb = new System.Text.StringBuilder("\r\n\t\t");
                    sb.Append("#region Public Methods");
                    #region Public Methods
                    sb.Append("\r\n\t\tpublic string Save[{MODEL}] (I[{MODEL}] p[{MODEL}])");
                    sb.Append("\r\n\t\t{");
                    sb.Append("\r\n\t\t// Business goes here........");
                    sb.Append("\r\n\t\t\ttry");
                    sb.Append("\r\n\t\t\t{");
                    sb.Append("\r\n\t\t\t\treturn o[{MODEL}]DC.Save[{MODEL}](p[{MODEL}]);");
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
                    sb.Append("\r\n\t\t\t\treturn o[{MODEL}]DC.Get[{MODEL}]ById(" + SignatureWithoutType + ");               ");
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
                    sb.Append("\r\n\t\t\t\treturn o[{MODEL}]DC.Get[{MODEL}]ALL();                        ");
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
                    sb.Append("\r\n\t\t\t\treturn o[{MODEL}]DC.Get[{MODEL}]BySearch(p[{MODEL}]);             ");
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
                    sb.Append("\r\n\t\t\t\treturn o[{MODEL}]DC.Delete[{MODEL}](p[{MODEL}]);                  ");
                    sb.Append("\r\n\t\t\t}                                                         ");
                    sb.Append("\r\n\t\t\tcatch(Exception ex)                                       ");
                    sb.Append("\r\n\t\t\t{                                                         ");
                    sb.Append("\r\n\t\t\t\tthrow new Exception(ex.Message);                      ");
                    sb.Append("\r\n\t\t\t}                                                         ");
                    sb.Append("\r\n\t\t}                                                          ");
                    sb.Append("\r\n\t#endregion");
                    string bll = sb.ToString().Replace("[{MODEL}]", pTableModel.TableNameAsTitle);
                    if (!SessionUtility.IsModelInterfaceRequired)
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
                        sw.WriteLine(sb.ToString());
                    }

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

        private void WriteDefaultConstructor(StreamWriter sw, TableModel pTableModel)
        {
            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("\r\n\t\t#region Constructor");
                sb.Append("\r\n\t\tpublic ");
                sb.Append(pTableModel.DotNetBLLName);
                sb.Append("()\r\n\t\t{");
                sb.Append("\r\n\t\t" + pTableModel.DotNetDataContextName + " o" + pTableModel.DotNetDataContextName + " = new " + pTableModel.DotNetDataContextName + "();");
                sb.Append("\r\n\t\t}");
                sb.Append("\r\n\t\t#endregion");
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
                //sb.Append("\r\nusing System.Text;                              ");
                //sb.Append("\r\nusing System.Data;                              ");
                //sb.Append("\r\nusing System.Data.Common;                       ");
                //sb.Append("\r\nusing Microsoft.Practices.EnterpriseLibrary.Data;");

                //sb.Append("\r\nusing " + SessionUtility.NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".DataContext;");

                sb.Append("\r\nusing JAGO.Beats.DC;");
                sb.Append("\r\nusing JAGO.Beats.Models;");
                if (SessionUtility.IsBLLInterfaceRequired)
                    sb.Append("\r\nusing " + SessionUtility.NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".IBLL;");
                if (SessionUtility.IsModelInterfaceRequired)
                {
                    sb.Append("\r\nusing " + SessionUtility.NameSpaceFirstPart + ".Common.Interfaces;                  ");
                    sb.Append("\r\nusing " + SessionUtility.NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".Interfaces;                  ");
                }
                else
                {
                    sb.Append("\r\nusing " + SessionUtility.NameSpaceFirstPart + ".Common.Model;                  ");
                    sb.Append("\r\nusing " + SessionUtility.NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".Model;                  ");
                }
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
                    sb.Append("\r\nnamespace JAGO.Beats.BLL");
                    //sb.Append("\r\n\r\nnamespace BLERP.BLL.Common");
                }
                sb.Append("\r\n{");
                sb.Append("\r\n\tpublic class ");
                sb.Append(pTableModel.DotNetBLLName + (!SessionUtility.IsBLLInterfaceRequired ? " " : (" : " + pTableModel.DotNetIBLLIntName)));
                sb.Append("\r\n\t{");
                sw.WriteLine(sb.ToString());
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
            if (!SessionUtility.IsModelInterfaceRequired)
            {
                bll = bll.Replace("I" + pTableModel.TableNameAsTitle, pTableModel.DotNetModelName);
            }
            return bll;
        }

    }
}
