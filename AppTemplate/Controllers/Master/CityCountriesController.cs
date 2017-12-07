using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppTemplateDAL.Context;
using AppTemplateDAL.Models.Master;

namespace PIMoni.Controllers.Master
{
    public class CityCountriesController : Controller
    {
        private IdealogContext db = new IdealogContext();

        // GET: CityCountries
        public ActionResult Index()
        {
            return View(db.CityCountry.ToList());
        }

        // GET: CityCountries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityCountry cityCountry = db.CityCountry.Find(id);
            if (cityCountry == null)
            {
                return HttpNotFound();
            }
            return View(cityCountry);
        }

        // GET: CityCountries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CityCountries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CityId,cityName,country,StartDate,EndDate,CreationDate,CreatedBy,RevisionDate,RevisedBy")] CityCountry cityCountry)
        {
            if (ModelState.IsValid)
            {
                cityCountry.EndDate = DateTime.Now;
                cityCountry.StartDate = DateTime.Now;
                cityCountry.RevisionDate = DateTime.Now;
                cityCountry.CreationDate = DateTime.Now;
                cityCountry.CreatedBy = HttpContext.User.Identity.Name;
                db.CityCountry.Add(cityCountry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cityCountry);
        }

        // GET: CityCountries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityCountry cityCountry = db.CityCountry.Find(id);
            if (cityCountry == null)
            {
                return HttpNotFound();
            }
            return View(cityCountry);
        }

        // POST: CityCountries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CityId,cityName,country,StartDate,EndDate,CreationDate,CreatedBy,RevisionDate,RevisedBy")] CityCountry cityCountry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cityCountry).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cityCountry);
        }

        // GET: CityCountries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CityCountry cityCountry = db.CityCountry.Find(id);
            if (cityCountry == null)
            {
                return HttpNotFound();
            }
            return View(cityCountry);
        }

        // POST: CityCountries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CityCountry cityCountry = db.CityCountry.Find(id);
            db.CityCountry.Remove(cityCountry);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
