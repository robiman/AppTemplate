﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EtLegalService.Models.Master
{
    [Table("ClaimType")]
    public class ClaimTypeBO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Claim Type ID: ")]

        public int ClaimTypeID { get; set; }

        [Required]
        [Display(Name = "Claim Type ")]
        public string ClaimType { get; set; }

        [Display(Name = "Operated By")]
        public string OperatedBy { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}