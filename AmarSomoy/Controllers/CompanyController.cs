using AmarSomoy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AmarSomoy.Controllers
{
    public class CompanyController : BaseController
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Company
        public ActionResult Index()
        {

            return View(db.Companies.ToList());
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create(CompanyModel pCompany)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    pCompany.IsNew = true;
                    base.SetObjectStatus(pCompany);
                    db.Companies.Add(pCompany);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    return View(pCompany);
                }
            }
            return View(pCompany);

        }

        // GET: Company/Edit/5
        public ActionResult Edit(string pCode)
        {
            var com = db.Companies.FirstOrDefault(co => co.CompanyCode == pCode);
            return View(com);
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit(string pCode, CompanyModel pCompany)
        {
            try
            {
                var company = db.Companies.FirstOrDefault(co => co.CompanyCode == pCode);
                company.CompanyName = pCompany.CompanyName;
                company.CompanyAddress = pCompany.CompanyAddress;
                company.IsNew = false;
                base.SetObjectStatus(company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Company/Delete/5
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
