using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EtLegalService.Models.Master
{
    [Table("tblCaseType")]
    public class CaseTypeBO
    {          
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Case Type ID")]

        public int CaseTypeID { get; set; }

        [Required]
        [Display(Name = "CaseType")]
        public string CaseType { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}