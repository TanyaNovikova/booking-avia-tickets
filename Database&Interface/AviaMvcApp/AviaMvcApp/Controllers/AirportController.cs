using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AviaMvcApp;
using AviaMvcApp.Models;

namespace AviaMvcApp.Controllers
{
    public class AirportController : Controller
    {
        //
        // GET: /Airport/

        public ActionResult Index()
        {
            ViewData.Model = (new DataAirportDataContext()).Airport.ToList();
            return View();
        }

        //
        // GET: /Airport/Details/5

        public ActionResult Details(int id)
        {
            var db = new DataAirportDataContext();
            var airport = db.Airport.SingleOrDefault(a => a.Id == id);

            ViewData.Model = airport;
            
            return View();
        }

        //
        // GET: /Airport/Create

        public ActionResult Create()
        {

            return View();
        }

        //
        // POST: /Airport/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var db = new DataAirportDataContext();
                Airport airport = new Airport();


                /*var airport = db.Airport.SingleOrDefault(a => a.Id == collection["Id"]);*/
                airport.Id = db.Airport.Count() + 1;
                airport.Name = collection["Name"];
                airport.Adress = collection["Adress"];

                db.Airport.InsertOnSubmit(airport);
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Airport/Edit/5

        public ActionResult Edit(int id)
        {
            var db = new DataAirportDataContext();
            var airport = db.Airport.SingleOrDefault(a => a.Id == id);

            ViewData.Model = airport;

            return View();
        }

        //
        // POST: /Airport/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                //DataContext db = new DataContext(@"D:\KPI\Технології створення програмних продуктів\AviaMvcApp\DB\AviaDB.mdf");
                var db = new DataAirportDataContext();
                var airport = db.Airport.SingleOrDefault(a => a.Id == id);

                if (collection["Name"] == "")
                {
                    return RedirectToAction("Index");
                }
                airport.Name = collection["Name"];
                airport.Adress=collection["Adress"];

                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Airport/Delete/5

        public ActionResult Delete(int id)
        {
            var db = new DataAirportDataContext();
            var airport = db.Airport.SingleOrDefault(a => a.Id == id);

            ViewData.Model = airport;

            return View();
        }

        //
        // POST: /Airport/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                var db = new DataAirportDataContext();
                var airport = db.Airport.SingleOrDefault(a => a.Id == id);
        
                airport.Name = collection["Name"];
                airport.Adress = collection["Adress"];
                
                db.Airport.DeleteOnSubmit(airport);
                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
