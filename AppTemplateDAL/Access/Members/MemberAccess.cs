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
    public class MemberAccess
    {
        private ECASContext db = new ECASContext();

        public object List()
        {
            return db.Members.OrderByDescending(p => p.memID).Take(50);
        }

        public Member Details(string employeeId)
        {
            var result = db.Members.Where(m => m.EmployeeID.Equals(employeeId));
            if (result.Count() > 0)
                return result.First();
            return null;
        }

        public List<Member> ListActiveMembers()
        {
            return db.Members.Where(m=>m.MemebrshipStatus.Equals("Active")).OrderByDescending(p => p.memID).ToList();
        }

        public object ListByEmployee(string id)
        {
            return db.Members.Where(d=>d.MemberID == id).ToList();
        }

        public object ListByEmployeeIdAndStatus(string id)
        {
            return db.Members.Where(d => d.MemberID == id && d.MemebrshipStatus == "Active").ToList();
        }

        public object CheckRejoin(string id)
        {
            return db.Members.Where(d => d.MemberID == id && d.MemebrshipStatus != "Active").ToList();
        }

        public object ListByEmployee(int id)
        {
            return db.Members.Where(d => d.memID == id).ToList();
        }

        public object Details(int id)
        {
            try
            {
                Member emp = db.Members.Find(id);

                if (emp == null)
                {
                    return false; // Not Found
                }
                return emp; // Success
            }
            catch (System.Exception e)
            {
                return false; // Exception
            }
        }

        public object Add(Member emp)
        {
            try
            {

                emp.StartDate = DateTime.Now;
                emp.EndDate = DateTime.Now.AddYears(100);
                emp.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                emp.CreationDate = DateTime.Now;
                

                db.Members.Add(emp);
                db.SaveChanges();
                return true; // Success
            }
            catch (System.Exception e)
            {
                return false; // Exception
            }
        }

        public object Revise(Member member)
        {
            try
            {

                member.RevisionDate = DateTime.Now;
                member.RevisedBy = HttpContext.Current.User.Identity.Name;

                var aExists = db.Members.Find(member.memID);
                if (aExists == null)
                {
                    db.Members.Add(member);
                }
                else
                {
                    db.Entry(aExists).State = EntityState.Detached;
                    db.Entry(member).State = EntityState.Modified;
                }

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
                Member member = db.Members.Find(id);
                db.Members.Remove(member);
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
