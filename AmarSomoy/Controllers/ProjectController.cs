using AmarSomoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmarSomoy.Controllers
{
    public class ProjectController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Project
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Project/Details/5
        public ActionResult Details(string pCode)
        {
            var com = db.Projects.FirstOrDefault(co => co.ProjectCode == pCode);
            return View(com);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            PrepareViewBag("");
            return View();
        }

        private void PrepareViewBag(string pCompanyCode)
        {
            var CompList = db.Companies.Where(com => com.IsDeleted == false);

            ViewBag.CompanyList = new SelectList(CompList.OrderBy(k => k.CompanyName), "CompanyCode", "CompanyName", pCompanyCode);
        }
        // POST: Project/Create
        [HttpPost]
        public ActionResult Create(ProjectModel pProject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pProject.ProjectCode = Guid.NewGuid().ToString();
                    pProject.IsNew = true;
                    base.SetObjectStatus(pProject);
                    db.Projects.Add(pProject);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    PrepareViewBag(pProject.CompanyCode);
                    return View(pProject);
                }
            }
            PrepareViewBag(pProject.CompanyCode);
            return View(pProject);

        }
        // GET: Project/Edit/5
        public ActionResult Edit(string pCode)
        {
            var com = db.Projects.FirstOrDefault(co => co.ProjectCode == pCode);
            PrepareViewBag(com.CompanyCode);
            return View(com);
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit(string pCode, ProjectModel pProject)
        {
            try
            {
                var project = db.Projects.FirstOrDefault(co => co.ProjectCode == pCode);
                project.ProjectDescription = pProject.ProjectDescription;
                project.IsNew = false;
                base.SetObjectStatus(project);
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
    }
}
