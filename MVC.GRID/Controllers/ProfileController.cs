using Grid.Mvc.Ajax.GridExtensions;
using GridMVCAjaxDemo.Helpers;
using MVC.GRID.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.GRID.Controllers
{
    public class ProfileController : Controller
    {

        private const string GRID_PARTIAL_PATH = "~/Views/Profile/Partial/IndexGrid.cshtml";

        private IGridMvcHelper gridMvcHelper;
        
            

            // GET: Profile
            public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult GetGrid()
        {
            var vm = new List<ProfileModel>()
            {
                new ProfileModel() { FirstName = "John", LastName = "Doe" }
            }
           .AsQueryable();
            var ajaxGridFactory = new Grid.Mvc.Ajax.GridExtensions.AjaxGridFactory();
            var grid = ajaxGridFactory.CreateAjaxGrid(vm, 1, false);

            return Json(new { Html = grid.ToJson(GRID_PARTIAL_PATH, this), grid.HasItems }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Profiles()
        {
            var vm = new List<ProfileModel>()
            {
                new ProfileModel() { FirstName = "John", LastName = "Doe" }
            }
            .AsQueryable();
            var ajaxGridFactory = new Grid.Mvc.Ajax.GridExtensions.AjaxGridFactory();
            var grid = ajaxGridFactory.CreateAjaxGrid(vm, 1, false);

            return Json(new { Html = grid.ToJson("~/Views/Profile/Partial/IndexGrid.cshtml", this), grid.HasItems }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProfilesPaged(int page)
        {
            var vm = new List<ProfileModel>()
            {
                new ProfileModel() { FirstName = "John", LastName = "Doe" }
            }
            .AsQueryable();
            var ajaxGridFactory = new Grid.Mvc.Ajax.GridExtensions.AjaxGridFactory();
            var grid = ajaxGridFactory.CreateAjaxGrid(vm, page, true);
            return Json(new { Html = grid.ToJson("~/Views/Profile/Partial/IndexGrid.cshtml", this), grid.HasItems }, JsonRequestBehavior.AllowGet);
        }
    }
}