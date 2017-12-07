using AppTemplateDAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppTemplateDAL.Models.Master
{
    [Table("idealog")]
    public class idealog: AuditAttribute
    {
        [Key]
        public int LogId { get; set; }

        public string FollowupKey { get; set; }
        [ForeignKey("EmployeesBO")]
        [Required]
        public string EmployeeId { get; set; }
      
       [Required]
        public string Title { get; set; }
        [Display(Name = "Division")]
        [Required]
        public string FocusDivisions { get; set; }
       [ Display(Name ="Project Finance")]
        public string ProjectFinance { get; set; }
        
       
        public string StakeHolder { get; set; }
       [Required]
        public string idea { get; set; }
        public string status { get; set; }


        public virtual EmployeesBO EmployeesBO { get; set; }


    }
}