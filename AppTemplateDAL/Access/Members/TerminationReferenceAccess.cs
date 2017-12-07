using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ECASDAL.Context;
using ECASDAL.Models.Members;
using System.Data.Entity;
using System.Web;

namespace ECASDAL.Access.Members
{
    public class TerminationReferenceAccess
    {
        private ECASContext db = new ECASContext();

        public object List()
        {
            return db.TerminationReferences.OrderByDescending(p => p.TerminationReferenceId).Take(50).ToList();
        }

        public object ListByEmployee(string id)
        {
            return db.TerminationReferences.Where(d => d.Member.MemberID == id).ToList();
        }

        public int GetCountByEmployee(int id)
        {
            return db.TerminationReferences.Where(d => d.memID == id).ToList().Count()+1;
        }

        public object GetCountByEmployeeRejoin(int id)
        {
            return db.TerminationReferences.Where(d => d.memID == id).OrderByDescending(d=>d.Sequence).ToList();
        }


        public object Details(int id)
        {
            try
            {
                TerminationReference leave = db.TerminationReferences.Find(id);

                if (leave == null)
                {
                    return false; // Not Found
                }
                return leave; // Success
            }
            catch (System.Exception e)
            {
                return false; // Exception
            }
        }

        public object Add(TerminationReference leave)
        {
            try
            {

                leave.StartDate = DateTime.Now;
                leave.EndDate = DateTime.Now.AddYears(100);
                leave.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                leave.CreationDate = DateTime.Now;                

                db.TerminationReferences.Add(leave);
                db.SaveChanges();
                return true; // Success
            }
            catch (System.Exception e)
            {
                return false; // Exception
            }
        }

        public object Revise(TerminationReference leave)
        {
            try
            {

                leave.RevisionDate = DateTime.Now;
                leave.RevisedBy = HttpContext.Current.User.Identity.Name;

                db.Entry(leave).State = EntityState.Modified;
                db.SaveChanges();
                return true;// Success
            }
            catch (System.Exception e)
            {
                return false; // Exception
            }
        }

        public object Delete(int id)
        {
            try
            {
                TerminationReference leave = db.TerminationReferences.Find(id);
                db.TerminationReferences.Remove(leave);
                db.SaveChanges();
                return true;// Success
            }
            catch (System.Exception e)
            {
                return false; // Exception
            }
        }
    }
}
