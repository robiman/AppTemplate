using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using EtLegalService.Models.Master;

namespace EtLegalService.Models.Operational
{
    [Table("tblCaseFollowUp")]

    public class FollowUpBOInformation
    {
        public List<FollowUpBO> FollowUpBOIModel { get; set; }

        //public IEnumerable<SelectListItem> FileName { get; set; }
    }
    public class FollowUpBO
    {

        [Display(Name = "FollowUp ID")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FollowUpID { get; set; }
        
        [Display(Name = "Court Name")]
        public string CourtName { get; set; }
        
        [Display(Name = "File Number")]
        [ForeignKey("courtC")]
        public string DFileNumber { get; set; }
        public virtual CourtCaseBO courtC { get; set; }

        [Required]
        [Display(Name = "Case Status")]
        public string CaseStatus { get; set; }

        [Display(Name = "Next Appointment Date")]        
        public DateTime DateofNextAppointment { get; set; }
        [Display(Name = "Last Appointment Date")]
        public DateTime DateofLastAppointment { get; set; }

        [Display(Name = "Activity/task accomplished")]
        [DataType(DataType.MultilineText)]
        public string WorkDoneOnAppointment { get; set; }

        [Display(Name = "Activity/task to be done next")]
        [DataType(DataType.MultilineText)]
        public string WorkToBeDoneOnNextAppointment { get; set; }

        [Required]
        [Display(Name = "Decision Date")]
        public DateTime DecisionDate { get; set; }
        [Display(Name = "Level")]
        [ForeignKey("level")]
        public int CourtLevel { get; set; }
        public virtual CourtLevelBO level { get; set; }
        [Display(Name = "Outcome")]
        public string Outcome { get; set; }
        
        [ForeignKey("etlowyer1")]
        [Display(Name = "Assigned ET Attorney")]
        public string AssignedETCounsel { get; set; }
        public virtual EtLawyerBO etlowyer1 { get; set; }

        [Display(Name = "Assigned External Attorney")]
        [ForeignKey("etlowyer")]

        public string AssignedLocalCounsel { get; set; }
        public virtual EtLawyerBO etlowyer { get; set; }

        [Display(Name = "Reason For Appointment")]
        [ForeignKey("reasonApp")]

        public int AppReason { get; set; }
        public virtual ReasonForAppointmentBO reasonApp { get; set; }


        [Display(Name = "Case Created By")]
        public string CaseCreatedBy { get; set; }
        

        [Display(Name = "Date Updated")]
        public DateTime DateUpdated { get; set; }

    }
}