using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using EtLegalService.Models.Operational;

namespace EtLegalService.Models.Master
{
    [Table("tblEtLawyer")]
    public class EtLawyerBO
    {
        [Display(Name = "First Name")]
      
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Midle Name")]
        public string MidleName { get; set; }

      
        [Key]
        [Display(Name = "IDNo")]
        public string IDNo { get; set; } 
    
        [Required]
        [Display(Name = "OutLook Email")]
        [DataType(DataType.EmailAddress)]
        public string OutLookEmail { get; set; }
      
        [Display(Name = "Other Email")]
        [DataType(DataType.EmailAddress)]
        public string OtherEmail { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Mobile Phone No")]

        public string MobilePhoneNo { get; set; }      
        [Required]
        [Display(Name = "Lawyer Type")]

        public string LawyerType { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        public ICollection<ClaimBO> claimbo { get; set; }
    }
}