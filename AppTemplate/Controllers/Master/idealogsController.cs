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
    public class idealogsController : Controller
    {
        private IdealogContext db = new IdealogContext();

        // GET: idealogs
        public ActionResult Index()
        {
            var idealog = db.idealog.Include(i => i.EmployeesBO);
            return View(idealog.ToList());
        }

        // GET: idealogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            idealog idealog = db.idealog.Find(id);
            if (idealog == null)
            {
                return HttpNotFound();
            }
            return View(idealog);
        }

        // GET: idealogs/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.EmployeesBO, "EmployeeId", "FirstName");
            return View();
        }
        // GET: idealogs/Create
    
        

      
        public ActionResult IdeaHomePage(idealog idea)
        {
            //ViewBag.EmployeeId = new SelectList(db.EmployeesBO, "EmployeeId", "FirstName");
          
            return View();
        }

        // POST: idealogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LogId,EmployeeId,idea,StartDate,EndDate,CreationDate,CreatedBy,RevisionDate,RevisedBy,Title,FocusDivisions,ProjectFinance,StakeHolder,EmployeesBO")] idealog idealog)
        {
            if (ModelState.IsValid)
            {
                EmployeesBO em = idealog.EmployeesBO;
                try
                {
                   em.EmployeeId = idealog.EmployeeId;
                    // em.EmployeeId = HttpContext.User.Identity.Name;
                    em.EndDate = DateTime.Now;
                    em.StartDate = DateTime.Now;
                    em.RevisionDate = DateTime.Now;
                    em.CreationDate = DateTime.Now;
                    em.CreatedBy = idealog.EmployeeId;
                    em.StationID = 1;
                    EmployeesBO findEM = db.EmployeesBO.Find(em.EmployeeId);
                    if (findEM == null)
                    {
                        db.EmployeesBO.Add(em);
                        db.SaveChanges();
                    }
                }
                catch (Exception)
                {
                }

                int count = db.idealog.Where(i => i.EmployeeId == em.EmployeeId).Count();
                try
                {
                    idealog.EmployeesBO = null;
                    idealog.FollowupKey = "myidea" + idealog.EmployeeId + (count + 1).ToString();
                    idealog.status = "Pending";

                    idealog.EndDate = DateTime.Now;
                    idealog.StartDate = DateTime.Now;
                    idealog.RevisionDate = DateTime.Now;
                    idealog.CreationDate = DateTime.Now;
                    idealog.CreatedBy = HttpContext.User.Identity.Name;

                    db.idealog.Add(idealog);
                    db.SaveChanges();
                }
                catch (Exception ee)
                {
                    
                }
                TempData["Message"]= "Your Idea is Successfully inserted Your followup key : " + idealog.FollowupKey;
                return RedirectToAction("IdeaHomePage");
            }

            ViewBag.EmployeeId = new SelectList(db.EmployeesBO, "EmployeeId", "FirstName", idealog.EmployeeId);
            return View(idealog);
        }

        // GET: idealogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            idealog idealog = db.idealog.Find(id);
            if (idealog == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.EmployeesBO, "EmployeeId", "FirstName", idealog.EmployeeId);
            return View(idealog);
        }

        // POST: idealogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LogId,EmployeeId,idea,StartDate,EndDate,CreationDate,CreatedBy,RevisionDate,RevisedBy")] idealog idealog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(idealog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.EmployeesBO, "EmployeeId", "FirstName", idealog.EmployeeId);
            return View(idealog);
        }

        // GET: idealogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            idealog idealog = db.idealog.Find(id);
            if (idealog == null)
            {
                return HttpNotFound();
            }
            return View(idealog);
        }

        // POST: idealogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            idealog idealog = db.idealog.Find(id);
            db.idealog.Remove(idealog);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
        public ActionResult AcceptStatus(int id)
        {
            idealog idea = new idealog();
            idea = db.idealog.Find(id);
            idea.status = "Accepted";
            idea.RevisedBy = HttpContext.User.Identity.Name;
            idea.RevisionDate = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult RejectStatus(int id)
        {
            idealog idea = new idealog();
            idea = db.idealog.Find(id);
            idea.status = "Rejected";
            idea.RevisedBy = HttpContext.User.Identity.Name;
            idea.RevisionDate = DateTime.Now;
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
