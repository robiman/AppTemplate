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
    public class StationsBOesController : Controller
    {
        private IdealogContext db = new IdealogContext();

        // GET: StationsBOes
        public ActionResult Index()
        {
            var stationsBO = db.StationsBO.Include(s => s.citycountry);
            return View(stationsBO.ToList());
        }

        // GET: StationsBOes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StationsBO stationsBO = db.StationsBO.Find(id);
            if (stationsBO == null)
            {
                return HttpNotFound();
            }
            return View(stationsBO);
        }

        // GET: StationsBOes/Create
        public ActionResult Create()
        {
            ViewBag.CityID = new SelectList(db.CityCountry, "CityId", "cityName");
            return View();
        }

        // POST: StationsBOes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StationID,StationName,CityID,StartDate,EndDate,CreationDate,CreatedBy,RevisionDate,RevisedBy")] StationsBO stationsBO)
        {
            if (ModelState.IsValid)
            {
                stationsBO.EndDate = DateTime.Now;
                stationsBO.StartDate = DateTime.Now;
                stationsBO.RevisionDate = DateTime.Now;
                stationsBO.CreationDate = DateTime.Now;
                stationsBO.CreatedBy = HttpContext.User.Identity.Name;
                db.StationsBO.Add(stationsBO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(db.CityCountry, "CityId", "cityName", stationsBO.CityID);
            return View(stationsBO);
        }

        // GET: StationsBOes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StationsBO stationsBO = db.StationsBO.Find(id);
            if (stationsBO == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(db.CityCountry, "CityId", "cityName", stationsBO.CityID);
            return View(stationsBO);
        }

        // POST: StationsBOes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StationID,StationName,CityID,StartDate,EndDate,CreationDate,CreatedBy,RevisionDate,RevisedBy")] StationsBO stationsBO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stationsBO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.CityCountry, "CityId", "cityName", stationsBO.CityID);
            return View(stationsBO);
        }

        // GET: StationsBOes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StationsBO stationsBO = db.StationsBO.Find(id);
            if (stationsBO == null)
            {
                return HttpNotFound();
            }
            return View(stationsBO);
        }

        // POST: StationsBOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StationsBO stationsBO = db.StationsBO.Find(id);
            db.StationsBO.Remove(stationsBO);
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
