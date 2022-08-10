using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TelerikMVCUI.Models;

namespace TelerikMVCUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly List<ProfileModel> clients = new List<ProfileModel>
    {
        new ProfileModel { ProfileId = 1, FirstName = "Julio",LastName= "Avellaneda", Email = "julito_gtu@hotmail.com" ,DateOfBirth=Convert.ToDateTime("11-23-1983")},
        new ProfileModel { ProfileId = 2, FirstName = "Juan",LastName= "Torres", Email = "jtorres@hotmail.com" ,DateOfBirth=Convert.ToDateTime("11-23-1983")},
        new ProfileModel { ProfileId = 3, FirstName = "Oscar",LastName= "Camacho", Email = "oscar@hotmail.com" ,DateOfBirth=Convert.ToDateTime("11-23-1983")},
        new ProfileModel { ProfileId = 4, FirstName = "Gina",LastName= "Urrego", Email = "ginna@hotmail.com" ,DateOfBirth=Convert.ToDateTime("11-23-1983")},
        new ProfileModel { ProfileId = 5, FirstName = "Nathalia",LastName= "Ramirez", Email = "natha@hotmail.com" ,DateOfBirth=Convert.ToDateTime("11-23-1983")},
        new ProfileModel { ProfileId = 6, FirstName = "Raul",LastName= "Rodriguez", Email = "rodriguez.raul@hotmail.com" ,DateOfBirth=Convert.ToDateTime("11-23-1983")},
        new ProfileModel { ProfileId = 7, FirstName = "Johana",LastName= "Espitia", Email = "johana_espitia@hotmail.com" ,DateOfBirth=Convert.ToDateTime("11-23-1983")},
        new ProfileModel { ProfileId = 8, FirstName = "Azharul",LastName= "Sharif", Email = "sharif.azharul@gmail.com" ,DateOfBirth=Convert.ToDateTime("11-23-1983")}
    };
        // GET: Profile
        public ActionResult Index()
        {
            return View(clients);
        }

        // GET: Profile/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Profile/Create
        public ActionResult Create()
        {
            var model = new ProfileModel
            {
                ProfileId = 8,
                FirstName = "Azharul",
                LastName = "Sharif",
                Email = "sharif.azharul@gmail.com",
                DateOfBirth = Convert.ToDateTime("11-23-1983"),
                IsActive = true
            };
            return View(model);
        }

        // POST: Profile/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Profile/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profile/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Profile/Delete/5
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
