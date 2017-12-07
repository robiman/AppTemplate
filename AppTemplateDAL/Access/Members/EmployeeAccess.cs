using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ECASDAL.Context;
using ECASDAL.Models.Members;
using System.Data.Entity;

namespace ECASDAL.Access.Members
{
    public class EmployeeAccess
    {
        private ECASContext db = new ECASContext();

        public object List()
        {
            return db.Employees.ToList();
        }

        public object Details(string id)
        {
            try
            {
                Employee emp = db.Employees.Find(id);

                if (emp == null)
                {
                    return null; // Not Found
                }
                return emp; // Success
            }
            catch (System.Exception e)
            {
                return null; // Exception
            }
        }

        public object Add(Employee emp)
        {
            try
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                return true; // Success
            }
            catch (System.Exception e)
            {
                return false; // Exception
            }
        }

        public object Revise(Employee document)
        {
            try
            {
                db.Entry(document).State = EntityState.Modified;
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
                Employee document = db.Employees.Find(id);
                db.Employees.Remove(document);
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
