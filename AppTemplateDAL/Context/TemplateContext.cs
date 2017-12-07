using System.Data.Entity;
using AppTemplateDAL.Models;
using AppTemplateDAL.Models.Master;
//using ECASDAL.Models.Members;


namespace AppTemplateDAL.Context
{
    public class IdealogContext : DbContext
    {
        public IdealogContext()
            : base("name=IdealogContext")
        {
        }
        public static IdealogContext Create()
        {
            return new IdealogContext();
        }

        // public DbSet<Employee> Employees { get; set; }

   public DbSet<EmployeesBO> EmployeesBO { get; set; }
        public DbSet<idealog> idealog { get; set; }
        public DbSet<CityCountry> CityCountry { get; set; }
        public DbSet<StationsBO> StationsBO { get; set; }
    }
}