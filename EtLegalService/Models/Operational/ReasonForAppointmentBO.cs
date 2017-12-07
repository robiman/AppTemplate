using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EtLegalService.Models.Operational
{
    [Table("tblReasonForAppointment")]
    public class ReasonForAppointmentBO
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Reason For Appointment ID")]

        public int ReasonForAppointmentID { get; set; }

        [Required]
        [Display(Name = "ReasonForAppointment")]
        public string ReasonForAppointment { get; set; }

        [Display(Name = "Update Date")]
        public DateTime UpdateDate { get; set; }
    }
}