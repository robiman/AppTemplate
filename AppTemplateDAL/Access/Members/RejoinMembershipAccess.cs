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
    public class RejoinMembershipAccess
    {
        private ECASContext db = new ECASContext();

        public object List()
        {
            return db.RejoinMemberships.OrderByDescending(p => p.RejoinMembershipId).Take(50).ToList();
        }

        public object ListByEmployee(string id)
        {
            return db.RejoinMemberships.Where(d => d.Member.MemberID == id).ToList();
        }


        public object Details(int id)
        {
            try
            {
                RejoinMembership rejoin = db.RejoinMemberships.Find(id);

                if (rejoin == null)
                {
                    return false; // Not Found
                }
                return rejoin; // Success
            }
            catch (System.Exception e)
            {
                return false; // Exception
            }
        }

        public object Add(RejoinMembership rejoin)
        {
            try
            {

                rejoin.StartDate = DateTime.Now;
                rejoin.EndDate = DateTime.Now.AddYears(100);
                rejoin.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                rejoin.CreationDate = DateTime.Now;
                

                db.RejoinMemberships.Add(rejoin);
                db.SaveChanges();
                return true; // Success
            }
            catch (System.Exception e)
            {
                return false; // Exception
            }
        }

        public object Revise(RejoinMembership rejoin)
        {
            try
            {

                rejoin.RevisionDate = DateTime.Now;
                rejoin.RevisedBy = HttpContext.Current.User.Identity.Name;

                db.Entry(rejoin).State = EntityState.Modified;
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
                RejoinMembership rejoin = db.RejoinMemberships.Find(id);
                db.RejoinMemberships.Remove(rejoin);
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
