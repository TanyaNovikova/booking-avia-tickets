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
    public class PlaceController : Controller
    {
        //
        // GET: /Place/

        public ActionResult Index()
        {
            ViewData.Model = (new DataPlaceDataContext()).Place.ToList();
            return View();
        }

        //
        // GET: /Place/Details/5

        public ActionResult Details(int id)
        {
            var db = new DataPlaceDataContext();
            var place = db.Place.SingleOrDefault(p => p.IpPlace == id);

            ViewData.Model = place;
            return View();
        }

        //
        // GET: /Place/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Place/Create

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

        //
        // GET: /Place/Edit/5

        public ActionResult Edit(int id)
        {
            var db = new DataPlaceDataContext();
            var place = db.Place.SingleOrDefault(p => p.IpPlace == id);

            ViewData.Model = place;
            return View();
        }

        //
        // POST: /Place/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var db = new DataPlaceDataContext();
                var place = db.Place.SingleOrDefault(a => a.Id == id);

                if ((collection["Id"] == "") || (collection["Number"] == "") || (collection["Class"] == "") || (collection["Flight"] == "") || (collection["FreePlace"] == ""))
                {
                    return RedirectToAction("Index");
                }
                place.Id = Convert.ToInt32(collection["Id"]);
                place.Number = collection["Number"];
                place.Class = Convert.ToInt32(collection["Class"]);
                place.Flight = Convert.ToInt32(collection["Flight"]);
                place.FreePlace = Convert.ToBoolean(collection["FreePlace"]);

                db.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Place/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Place/Delete/5

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
