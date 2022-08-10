using MVC.GRID.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.GRID.Controllers
{
    public class ClientController : Controller
    {
        private readonly List<ClientModel> clients = new List<ClientModel>
    {
        new ClientModel { Id = 1, FirstName = "Julio",LastName= "Avellaneda", Email = "julito_gtu@hotmail.com" },
        new ClientModel { Id = 2, FirstName = "Juan",LastName= "Torres", Email = "jtorres@hotmail.com" },
        new ClientModel { Id = 3, FirstName = "Oscar",LastName= "Camacho", Email = "oscar@hotmail.com" },
        new ClientModel { Id = 4, FirstName = "Gina",LastName= "Urrego", Email = "ginna@hotmail.com" },
        new ClientModel { Id = 5, FirstName = "Nathalia",LastName= "Ramirez", Email = "natha@hotmail.com" },
        new ClientModel { Id = 6, FirstName = "Raul",LastName= "Rodriguez", Email = "rodriguez.raul@hotmail.com" },
        new ClientModel { Id = 7, FirstName = "Johana",LastName= "Espitia", Email = "johana_espitia@hotmail.com" }
    };
        // GET: Client
        public ActionResult Index()
        {
            return View(clients);
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
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

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Client/Edit/5
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

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Client/Delete/5
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
