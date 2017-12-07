using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EtLegalService.Models.Master
{
    [Table("CaseTypeCategory")]
    public class CaseTypeCategoryBO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "CaseType Catagory ID")]
        public int CaseTypeCatagoryID { get; set; }
        [ForeignKey("casetype")]
        [Required]
        [Display(Name = "Case Type ID")]
        public int CaseTypeID { get; set; }
        public virtual CaseTypeBO casetype { get; set; }

        [Required]
        [Display(Name = "Case Type Catagory")]
        public string CaseTypeCatagory { get; set; }        
        
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

      
    }
}