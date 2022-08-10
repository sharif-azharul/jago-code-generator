using AmarSomoy.Models;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.Mvc;

namespace AmarSomoy.Controllers
{
    public class BaseController : Controller
    {

        protected ActionResult RedirectToAction<TController>(
            Expression<Action<TController>> action)
           where TController : Controller
        {
            return ControllerExtensions.RedirectToAction(this, action);
        }

        protected BaseModel SetObjectStatus(BaseModel pBaseModel)
        {

            if (pBaseModel.IsNew)
            {
                pBaseModel.CreateUser = SessionUtility.SessionContainer.USER_ID;
                pBaseModel.CreateDate = DateTime.Now;
            }
            else
            {
                pBaseModel.UpdateUser = SessionUtility.SessionContainer.USER_ID;
                pBaseModel.UpdateDate = DateTime.Now;
            }

            return pBaseModel;
        }

        public string PrepareMailContent(dynamic master, string pTemplateName)
        {
            StringBuilder sbContent = new StringBuilder();
            string TemplatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", pTemplateName);
            var template = System.IO.File.ReadAllText(TemplatePath);
            try
            {
                //sbContent.Append(RazorEngine.Razor.Parse(template, master));
                var templateService = new TemplateService();
                sbContent.Append(templateService.Parse(template, master, null, null));
            }
            catch (Exception ex) { }
            return sbContent.ToString();
        }
    }
}