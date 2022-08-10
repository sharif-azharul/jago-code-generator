using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.Web.Http;

namespace MvcWithAngularJS.Controllers
{
    public class ProfileController : Controller
    {
        //
        // GET: /Profile/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetProfiles()
        {
            var profiles = GetProfileList();

            return new JsonResult { Data = profiles, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET api/profile
        //[HttpGet]
        //public IEnumerable<ProfileModel> GetProfiles()
        //{
        //    return GetProfileList();
        //}

        private List<ProfileModel> GetProfileList()
        {
            List<ProfileModel> profiles = (List<ProfileModel>)Session["PROFILES"];
            if (profiles == null)
            {
                profiles = new List<ProfileModel>() 
            { new ProfileModel(){Name="Azharul Sharif",EnrolledDate= Convert.ToDateTime("05-30-2012")}
                ,new ProfileModel() {Name="Sazzadul Sharif",EnrolledDate= Convert.ToDateTime("05-30-2011")
            }
            };
                Session["PROFILES"] = profiles;
            }
            return profiles;
        }
    }
    public class ProfileModel
    {

        public string Name { get; set; }
        public DateTime? EnrolledDate { get; set; }

        public string StrEnrolledDate { get { return this.EnrolledDate.Value.ToString("dd/MM/yyyy"); } }
    }
}
