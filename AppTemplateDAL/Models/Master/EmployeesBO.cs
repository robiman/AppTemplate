using AppTemplateDAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppTemplateDAL.Models.Master
{
    [Table("EmployeesBO")]
    public class EmployeesBO : AuditAttribute
    {
        [Key]
        public string EmployeeId { get; set; }
        
        public string FirstName { get; set; }
      
        public string MiddleName { get; set; }
       
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        [ForeignKey("StationBO")]
        public int StationID { get; set; }
       public string Section { get; set; }
       public string position { get; set; }
        
        public string PhoneNumber { get; set; }
      public virtual StationsBO StationBO { get; set; }
    }
}