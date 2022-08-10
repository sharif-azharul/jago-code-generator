//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;

//namespace AmarCodeGenerator
//{
//    class UIAspxLayer
//    {
//        public void GenerateSparkListAspxFromTemplate(TableModel pTableModel)
//        {
//            try
//            {
//                CommonTask.CreateDirectory(SessionUtility.ViewsFolder);
//                if (pTableModel != null)
//                {
//                    StreamWriter sw = null;
//                    System.Text.StringBuilder sb = null;

//                    string dq = @"""";
//                    string lstrTableName = pTableModel.OriginalTableName;  //table name

//                    #region Create Empty cs file
//                    sb = new System.Text.StringBuilder(SessionUtility.ViewsFolder + pTableModel.IndexViewPageName);
//                    // sb = new System.Text.StringBuilder(lstrTableName);
//                    sb.Append(".aspx");
//                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
//                    sw = lobjFileInfo.CreateText();
//                    #endregion

//                    #region Get Table Name, Attributes Name and Attribute Types

//                    #endregion

//                    #region Write Namespaces
//                    //this.WriteDataContextNamespaces(sw, pTableModel);
//                    #endregion

//                    #region Write Class Default Constructor
//                    //CommonTask.WriteDefaultConstructor(sw, pTableModel.DotNetDataContextName);
//                    #endregion

//                    #region Write Private Variables
//                    sb = new System.Text.StringBuilder();

//                    sw.WriteLine(sb.ToString());
//                    #endregion

//                    #region Write Public Methods for DAL
//                    sb = new System.Text.StringBuilder();
//                    sb.Append(CommonTask.PrepareMailContent(pTableModel, "SparkListUI_ASPX.html"));
//                    sw.WriteLine(sb.ToString().Replace("[[P]]", "@").Replace("FSP_", "").Replace("?", "").Replace("[[WHAT]]", "?").Replace("DbType.string", "DbType.String"));


//                    #endregion

//                    #region Close file
//                    if (sw != null)
//                    {
//                        //dr.Close();
//                        sw.Close();
//                    }
//                    #endregion


//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void GenerateListAspxPage(TableModel pTableModel)
//        {
//            try
//            {
//                if (pTableModel != null)
//                {
//                    StreamWriter sw = null;
//                    System.Text.StringBuilder sb = null;

//                    string dq = @"""";
//                    //string lstrTableName = strTable;  //table name

//                    #region Create Empty cs file
//                    string SpecificViewFolder = SessionUtility.ViewsFolder + pTableModel.TableNameAsTitle + @"\";
//                    CommonTask.CreateDirectory(SpecificViewFolder);
//                    sb = new System.Text.StringBuilder(SpecificViewFolder + "Create");
//                    // sb = new System.Text.StringBuilder(lstrTableName);
//                    sb.Append(".aspx");
//                    FileInfo lobjFileInfo = new FileInfo(sb.ToString());
//                    sw = lobjFileInfo.CreateText();
//                    #endregion

//                    #region Get Table Name, Attributes Name and Attribute Types

//                    #endregion

//                    #region Write Namespaces
//                    //this.WriteDataContextNamespaces(sw, pTableModel);
//                    #endregion

//                    #region Write Class Default Constructor
//                    //this.WriteDefaultConstructor(sw, pTableModel.DotNetDataContextName);
//                    #endregion

//                    //#region Write Private Variables
//                    sb = new System.Text.StringBuilder("\t");

//                    sw.WriteLine(sb.ToString());
//                    //#endregion

//                    //#region Write Public Methods for DAL
//                    //sb = new System.Text.StringBuilder("\r\n\t");

//                    //---------------------------------------------------------------------------------

//                    <%@ Page Language = "C#" MasterPageFile = "Spark.Master" AutoEventWireup = "true" CodeBehind = "JobCircularList.aspx.cs" Inherits = "Aplectrum.Spark.JobCircularList" %>
        

//        <%@ Register Src = "Modules/AdminMainHeader.ascx" TagName = "AdminMainHeader" TagPrefix = "uc1" %>
//            <%@ Register Assembly = "AjaxControlToolkit" Namespace = "AjaxControlToolkit" TagPrefix = "asp" %>
                

//                < asp:Content ID = "Content1" ContentPlaceHolderID = "mainPlaceHolder" runat = "server" >
                     
//                         < uc1:AdminMainHeader ID = "AdminMainHeader2" runat = "server" />
                        
//                            < div class="clearfix"></div>

//    <div class="container-fluid">
//        <form id = "form1" runat="server">
//            <div class="Head heading text-center">
//                <asp:Literal runat = "server" Text="Job Circular List" />
//            </div>
//            <asp:ScriptManager ID = "ScriptManager1" runat="server">
//            </asp:ScriptManager>

//            <div class="search-block-table collapseable-table">
//                <table class="marginauto">
//                    <tr>
//                        <td>
//                            <asp:Localize ID = "Company" runat="server" Text="<%$ Resources:LocalizedText, Company%>" />
//                        </td>
                        
//                    </tr>
//                    <tr>
//                        <td>
//                            <asp:DropDownList ID = "BranchList" DataTextField="CompanyName"
//                                DataValueField="CompanyId" runat="server" ></asp:DropDownList></td>
                        
//                        <td>
//                                <a href = 'JobCircularSave.aspx?id=0' class="btn btn-purple round"><span class="pull-left"><i class="fa fa-plus-circle"></i></span> <span>Add New Circular</span></a>

//                            </td>
//                    </tr>

                    
//                </table>


//            </div>
//            <div class="grid-table">
//                <asp:GridView ID = "JobCircularGrid"
//                    HeaderStyle-CssClass="GrayBackWhiteFont"
//                    HeaderStyle-BackColor="#f3f3f3"
//                    CssClass="table big-table"
//                    FooterStyle-CssClass="GrayBackWhiteFont"
//                    ItemStyle-CssClass="NormalSmallBangla"
//                    AlternatingItemStyle-BackColor="#FFFFFF"
//                    runat="server" CellSpacing="1" CellPadding="5"
//                    AllowPaging="true" PageSize="5" PagerStyle-CssClass="CommandButton"
//                    AutoGenerateColumns="false" DataKeyNames="CircularID" GridLines="None"
//                    BorderWidth="0px" ShowFooter="true" Height="0px"
//                    OnRowEditing="JobCircularGrid_RowEditing"
//                    OnRowUpdating="JobCircularGrid_RowUpdating"
//                    OnRowCancelingEdit="JobCircularGrid_RowCancelingEdit"
//                    OnRowCommand="JobCircularGrid_RowCommand"
//                    OnRowDeleting="JobCircularGrid_RowDeleting"
//                    OnPageIndexChanging="JobCircularGrid_PageIndexChanging">
//                    <Columns>
                        
//                        <asp:TemplateField HeaderText = "Title" FooterStyle-CssClass="red" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="true">
//                            <ItemTemplate>
//                                <%#DataBinder.Eval(Container.DataItem, "JobTitle")%>
//                            </ItemTemplate>
//                        </asp:TemplateField>

                        

//                        <asp:TemplateField HeaderText = "Action" FooterStyle-CssClass="red" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="60px" FooterStyle-Width="60px" ItemStyle-Width="60px">
//                            <ItemTemplate>
//                                <%# string.Format("<a href='JobCircularSave.aspx?id={0}' class='edit'><i class='fa fa-pencil fa-lg'  title='Edit'></i></a>", Eval("CircularID")) %>&nbsp;&nbsp;&nbsp;
//                                <asp:LinkButton CommandName = "DeleteNotifyEvent" CausesValidation="false"  CssClass="delete" ID="btnDelete" 
//                                    runat="server" OnClientClick="return confirm('Are you sure you want to delete this record?');"><span class="alert-danger"><i class='fa fa-trash fa-lg' title='Delete'></i></span></asp:LinkButton>
//                            </ItemTemplate>
//                        </asp:TemplateField>

                        
//                    </Columns>
//                    <PagerSettings Mode = "NumericFirstLast" PageButtonCount="4"
//                                    FirstPageText="First" LastPageText="Last"></PagerSettings>
//                                <PagerStyle HorizontalAlign = "Center" CssClass="pagination-aplec"></PagerStyle>
//                </asp:GridView>
//            </div>
//        </form>
//    </div>
//</asp:Content>
//                    //*********************************************************************************
                    


//                    //sb.Append("\r\n\t#endregion");
//                    sw.WriteLine(sb.ToString());
//                    #region Close file
//                    if (sw != null)
//                    {
//                        //sw.WriteLine("\r\n\t}\r\n}");
//                        //dr.Close();
//                        sw.Close();
//                    }
//                    #endregion


//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//    }
//}
