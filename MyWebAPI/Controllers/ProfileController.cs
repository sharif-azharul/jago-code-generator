using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Providers.Entities;

namespace MyWebAPI.Controllers
{
    public class ProfileController : ApiController
    {
        // GET api/profile
        public IEnumerable<ProfileModel> Get()
        {
            return GetProfileList();
        }

        // GET api/profile/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/profile
        public void Post([FromBody]ProfileModel profile)
        {
            List<ProfileModel> profiles = (List<ProfileModel>)HttpContext.Current.Session["PROFILES"];
            if (profiles == null)
                profiles = new List<ProfileModel>();
            profiles.Add(profile);

            HttpContext.Current.Session["PROFILES"] = profiles;

        }

        // PUT api/profile/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/profile/5
        public void Delete(int id)
        {
        }

        private List<ProfileModel> GetProfileList()
        {
             List<ProfileModel> profiles = (List<ProfileModel>)HttpContext.Current.Session["PROFILES"];
             if (profiles == null)
             {
                 profiles = new List<ProfileModel>() 
            { new ProfileModel(){Name="Azharul Sharif",EnrolledDate= Convert.ToDateTime("05-30-2012")}
                ,new ProfileModel() {Name="Sazzadul Sharif",EnrolledDate= Convert.ToDateTime("05-30-2011")
            }
            };
                 HttpContext.Current.Session["PROFILES"] = profiles;
             }
            return profiles;
        }
    }
    public class ProfileModel
    {

        public string Name { get; set; }
        public DateTime? EnrolledDate { get; set; }
    }
}
