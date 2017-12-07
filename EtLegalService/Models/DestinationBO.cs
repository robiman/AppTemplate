using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EtLegalService.Models.Master
{
    [Table("Destination")]
    public class DestinationBO
    {
        
        [Key]
        [Display(Name = "DestinationCode ")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DestinationCode { get; set; }

        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Is domestic")]
        public bool IsDomestic { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }


       
    }
}