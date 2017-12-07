using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using EtLegalService.Models.Master;

namespace EtLegalService.Models.Operational
{
    public class ClaimFollowUpBO
    {
        [Required]
        [Display(Name = "Claim Follow-Up ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClaimFollowUpID { get; set; }

      
        [Display(Name = "Claim ID")]
        [ForeignKey("ClaimID")]
        public virtual ClaimBO claim { get; set; }
        public string ClaimID { get; set; }

        [ForeignKey("etlowyer1")]
        [Display(Name = "Assigned ET Counsel")]
        public string AssignedETCounsel { get; set; }
        public virtual EtLawyerBO etlowyer1 { get; set; }

        [Display(Name = "Assigned Local Counsel")]
        [ForeignKey("etlowyer")]

        public string AssignedLocalCounsel { get; set; }
        public virtual EtLawyerBO etlowyer { get; set; }

        [Required]
        [Display(Name = "Date Of Transaction From")]
        public DateTime DateOfTransactionFrom { get; set; }

        [Required]
        [Display(Name = "Date Of Transaction To")]
        public DateTime DateOfTransactionTo { get; set; }

        [Required]
        [Display(Name = "Date Of Claim")]
        public DateTime DateOfClaim { get; set; }
        /*TypeOfClaim, CaseDescription */
      

        [Display(Name = "Case Description")]
        [DataType(DataType.MultilineText)]
        public string CaseDescription { get; set; }

        [Required]
        [Display(Name = "Sectors Affected By The Claim")]
        [DataType(DataType.MultilineText)]
        public string SectorsAffectedByTheClaim { get; set; }


        [Display(Name = "Claim Amount")]
        [DataType(DataType.Currency)]
        public double ClaimAmount { get; set; }

        [Display(Name = "Currency Name")]
        public string CurrencyName { get; set; }

   



        [Display(Name = "Claim Processes")]
        public string ClaimProcesses { get; set; }

        [Display(Name = "Claim Processes Description")]
        [DataType(DataType.MultilineText)]
        public string ClaimProcessesDescription { get; set; }

     

        [Display(Name = "Date Of Resolution")]
        public DateTime DateOfResolution { get; set; }

        [Display(Name = "Claim Status")]
        public string ClaimStatus { get; set; }

        [Display(Name = "Claim Created By")]
        public string ClaimCreatedBy { get; set; }

        [Display(Name = "Start Date: ")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date: ")]
        public DateTime EndDate { get; set; }

        //[Display(Name = "File Name: ")]
        //public string FileName { get; set; }

        //public IEnumerable<SelectListItem> DropDownFileNames { get; set; }

        /* (ClaimID,NameOfClaimant,AssignedETCounsel,AssignedLocalCounsel,DateOfTransaction,DateOfClaim,LocationOfClaim,SectorsAffectedByTheClaim ,FlightNumber, 
        ClaimAmount,CurrencyName,ClaimCategory,ClaimProcesses,DateOfResolution,ClaimStatus,ClaimCreatedBy,FileName,Startdate,EndDate)  */



    }
}