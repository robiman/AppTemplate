using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EtLegalService.Models.Master
{
    [Table("ClaimCategory")]
    public class ClaimCategoryBO
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Claim Category ID: ")]
        public int ClaimCategoryID { get; set; }

        [Required]
        [Display(Name = "Claim Category ")]
        public string ClaimCategory { get; set; }

        [Display(Name = "Operated BY")]
        public string OperatedBy { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}