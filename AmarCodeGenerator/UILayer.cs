using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AmarCodeGenerator
{
   public class UILayer
    {
        public void GenerateAllViewPages(TableModel pTableModel)
        {
            CommonTask.CreateDirectory(SessionUtility.ViewsFolder);

            GenerateCreateViewPage(pTableModel);
            GenerateEditViewPage(pTableModel);
            GenerateIndexViewPage(pTableModel);
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
                    //string lstrTableName = strTable;  //table name

                    #region Create Empty cs file
                    string SpecificViewFolder = SessionUtility.ViewsFolder + pTableModel.TableNameAsTitle + @"\";
                    CommonTask.CreateDirectory(SpecificViewFolder);
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


                    sb.AppendLine(@"@model " + SessionUtility.NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".Model." + pTableModel.DotNetModelName);
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

                   // string lstrTableName = strTable;  //table name

                    #region Create Empty cs file
                    string SpecificViewFolder =SessionUtility. ViewsFolder + pTableModel.TableNameAsTitle + @"\";
                   CommonTask. CreateDirectory(SpecificViewFolder);
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


                    sb.AppendLine(@"@model " + SessionUtility.NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".Model." + pTableModel.DotNetModelName);
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

        private void GenerateIndexViewPage(TableModel pTableModel)
        {
            try
            {
                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;


                    //string lstrTableName = strTable;  //table name

                    #region Create Empty cs file
                    string SpecificViewFolder = SessionUtility.ViewsFolder + pTableModel.TableNameAsTitle + @"\";
                   CommonTask. CreateDirectory(SpecificViewFolder);
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

                    sb.AppendLine(@"@model BTXCMS.Models.PagedList<" +SessionUtility. NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".Model." + pTableModel.DotNetModelName + "> ");
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
//#endregion
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
    }
}
