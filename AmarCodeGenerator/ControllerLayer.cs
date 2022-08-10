using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AmarCodeGenerator
{
    public class ControllerLayer
    {
        public void GenerateController(TableModel pTableModel)
        {
            try
            {
                CommonTask.CreateDirectory(SessionUtility.ControllerFolder);

                if (pTableModel != null)
                {
                    StreamWriter sw = null;
                    System.Text.StringBuilder sb = null;
                    //Stream myStream = null;

                    #region Create Empty cs file
                    sb = new System.Text.StringBuilder(SessionUtility. ControllerFolder + pTableModel.ControllerName);
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
        private void WriteControllerNamespaces(StreamWriter sw, TableModel pTableModel)
        {

            try
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder("using System;");

                sb.Append("\r\nusing System.Collections.Generic;");
                sb.Append("\r\nusing System.Linq;                              ");
                sb.Append("\r\nusing System.Web;                              ");
                sb.Append("\r\nusing System.Web.Mvc;                       ");
                sb.Append("\r\nusing " +SessionUtility. NameSpaceFirstPart + "." + SessionUtility.ModuleName + ".BLL;                       ");

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
    }
}
