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
    public class EmployeesBOesController : Controller
    {
        private IdealogContext db = new IdealogContext();

        // GET: EmployeesBOes
        public ActionResult Index()
        {
            var employeesBO = db.EmployeesBO.Include(e => e.StationBO);
            return View(employeesBO.ToList());
        }

        // GET: EmployeesBOes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeesBO employeesBO = db.EmployeesBO.Find(id);
            if (employeesBO == null)
            {
                return HttpNotFound();
            }
            return View(employeesBO);
        }

        // GET: EmployeesBOes/Create
        public ActionResult Create()
        {
            ViewBag.StationID = new SelectList(db.StationsBO, "StationID", "StationName");
            return View();
        }

        // POST: EmployeesBOes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,FirstName,MiddleName,LastName,StationID,Section,position,PhoneNumber,StartDate,EndDate,CreationDate,CreatedBy,RevisionDate,RevisedBy")] EmployeesBO employeesBO)
        {
            if (ModelState.IsValid)
            {
                employeesBO.EndDate = DateTime.Now;
                employeesBO.StartDate = DateTime.Now;
                employeesBO.RevisionDate = DateTime.Now;
                employeesBO.CreationDate = DateTime.Now;
                employeesBO.CreatedBy = HttpContext.User.Identity.Name;
                db.EmployeesBO.Add(employeesBO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StationID = new SelectList(db.StationsBO, "StationID", "StationName", employeesBO.StationID);
            return View(employeesBO);
        }

        // GET: EmployeesBOes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeesBO employeesBO = db.EmployeesBO.Find(id);
            if (employeesBO == null)
            {
                return HttpNotFound();
            }
            ViewBag.StationID = new SelectList(db.StationsBO, "StationID", "StationName", employeesBO.StationID);
            return View(employeesBO);
        }

        // POST: EmployeesBOes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,FirstName,MiddleName,LastName,StationID,Section,position,PhoneNumber,StartDate,EndDate,CreationDate,CreatedBy,RevisionDate,RevisedBy")] EmployeesBO employeesBO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeesBO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StationID = new SelectList(db.StationsBO, "StationID", "StationName", employeesBO.StationID);
            return View(employeesBO);
        }

        // GET: EmployeesBOes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeesBO employeesBO = db.EmployeesBO.Find(id);
            if (employeesBO == null)
            {
                return HttpNotFound();
            }
            return View(employeesBO);
        }

        // POST: EmployeesBOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            EmployeesBO employeesBO = db.EmployeesBO.Find(id);
            db.EmployeesBO.Remove(employeesBO);
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
