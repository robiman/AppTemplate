using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EtLegalService.Models.Master
{
    [Table("tblCourtName")]
    public class CourtNameBO
    {
        [Key]
        [Display(Name = "Court Name Code")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourtNameID { get; set; }

        [Required]
        [Display(Name = "Court Name")]
        public string CourtName { get; set; }

        [Required]
        [Display(Name = "Court Location")]
        public string CourtLocation { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime DateUpdated { get; set; }
        
    }
}