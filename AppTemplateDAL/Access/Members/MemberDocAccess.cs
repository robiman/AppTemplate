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
    public class MemberDocAccess
    {

        private ECASContext db = new ECASContext();

        public object List()
        {  
            return db.MemberDocs.ToList();
        }

        public object List(int memID)
        {
            return db.MemberDocs.Where(a => a.memID == memID).ToList();
        }

        public object Details(int id)
        {
            try
            {
                MemberDoc emp = db.MemberDocs.Find(id);

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

        public object Add(MemberDoc doc)
        {
            try
            {

                doc.StartDate = DateTime.Now;
                doc.EndDate = DateTime.Now.AddYears(100);
                doc.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                doc.CreationDate = DateTime.Now;


                db.MemberDocs.Add(doc);
                db.SaveChanges();
                return true; // Success
            }
            catch (System.Exception e)
            {
                return false; // Exception
            }
        }

        public object Revise(MemberDoc doc)
        {
            try
            {

                doc.RevisionDate = DateTime.Now;
                doc.RevisedBy = HttpContext.Current.User.Identity.Name;

                db.Entry(doc).State = EntityState.Modified;
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
                MemberDoc doc = db.MemberDocs.Find(id);
                db.MemberDocs.Remove(doc);
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
