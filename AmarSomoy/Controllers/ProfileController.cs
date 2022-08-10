using AmarSomoy.Models;
using AmarSomoy.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telerik.Web.Mvc;

namespace AmarSomoy.Controllers
{
    public class ProfileController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Project
        public ActionResult Index()
        {
            return View();
        }

        // GET: Project/Details/5
        public ActionResult Details(string pCode)
        {
            var com = db.Profiles.FirstOrDefault(co => co.ProfileCode == pCode);
            return View(com);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            ProfileModel profile = new ProfileModel();
            var UserId = SessionUtility.SessionContainer.USER_ID;
            if (!string.IsNullOrEmpty(UserId))
            {
                profile = db.Profiles.FirstOrDefault(pr => pr.UserId == UserId);
                if (profile == null)
                {
                    profile = new ProfileModel();
                    profile.UserId = SessionUtility.SessionContainer.USER_ID;
                    PrepareViewBag(profile.CompanyCode);
                    return View(profile);
                }
                else
                {
                    PrepareViewBag(profile.CompanyCode);
                    return View("Edit", profile);
                }
            }
            PrepareViewBag("");
            return View();
        }

        private void PrepareViewBag(string pCompanyCode)
        {
            var CompList = db.Companies.Where(com => com.IsDeleted == false);

            ViewBag.CompanyList = new SelectList(CompList.OrderBy(k => k.CompanyName), "CompanyCode", "CompanyName", pCompanyCode);

            //var CompList = db.Companies.Where(com => com.IsDeleted == false);

            //ViewBag.CompanyList = new SelectList(CompList.OrderBy(k => k.CompanyName), "CompanyCode", "CompanyName", pCompanyCode);
        }

        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(ProfileModel pProfile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pProfile.ProfileCode = Guid.NewGuid().ToString();
                    pProfile.IsNew = true;
                    base.SetObjectStatus(pProfile);
                    db.Profiles.Add(pProfile);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(pProfile);
                }
            }
            return View(pProfile);

        }
        // GET: Project/Edit/5
        public ActionResult Edit(string pCode)
        {
            var com = db.Profiles.FirstOrDefault(co => co.ProfileCode == pCode);
            PrepareViewBag(com.CompanyCode);
            return View(com);
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit(string pCode, ProfileModel pProfile)
        {
            try
            {
                var profile = db.Profiles.FirstOrDefault(co => co.ProfileCode == pCode);
                profile.FirstName = pProfile.FirstName;
                profile.LastName = pProfile.LastName;
                profile.MobileNo = pProfile.MobileNo;
                profile.EmailId = pProfile.EmailId;
                profile.Address = pProfile.Address;
                profile.DateOfBirth = pProfile.DateOfBirth;
                profile.IsNew = false;
                base.SetObjectStatus(profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Project/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [GridAction]
        public virtual ActionResult ListProfileAjax(SearchProfileModel search)
        {
            var gridModel = new GridModel();
            List<ProfileModel> profiles = new List<Models.ProfileModel>();
            if (search != null)
            {
                 profiles = db.Profiles.ToList();
            }
            gridModel.Data = profiles;
            return View(gridModel);
        }
    }
   
}
