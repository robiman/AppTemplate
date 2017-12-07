using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using EtLegalService.Models.Master;

namespace EtLegalService.Models.Operational
{
    [Table("tblCase")]
    public class CourtCaseBOInformation
    {
        public List<CourtCaseBO> CourtCaseBOModel { get; set; }

        //public IEnumerable<SelectListItem> FileName { get; set; }
    }
    public class CourtCaseBO
    {
        [Display(Name = "File Number")]
        [Key]
        public string FileNumber { get; set; }

        [Required]
        [Display(Name = "Other Party Name")]
        public string Name { get; set; }

        [Display(Name = "Location")]
        [ForeignKey("destination")]
        public int Location { get; set; }
        public virtual DestinationBO destination { get; set; }

        [Display(Name = "Date Filed")]
        public DateTime DateFiled { get; set; }
        
        [Display(Name = "Case Nature")]
        public string CaseNature { get; set; }
        [Display(Name = "Court File Number")]
        public string CourtFileNumber { get; set; }

        [ForeignKey("etlowyer1")]
        [Display(Name = "Assigned ET Attorney")]
        public string AssignedETCounsel { get; set; }
        public virtual EtLawyerBO etlowyer1 { get; set; }

        [Display(Name = "Assigned Local Attorney")]
        [ForeignKey("etlowyer")]

        public string AssignedLocalCounsel { get; set; }
        public virtual EtLawyerBO etlowyer { get; set; }

        [Display(Name = "Case Status")]
        public string CaseStatus { get; set; }


        [Display(Name = "Amount")]
        [DataType(DataType.Currency)]
        public double Budget { get; set; }


        [Display(Name = "Amount In ETB")]
        [DataType(DataType.Currency)]
        public double BudgetInETB { get; set; }


        [Display(Name = "Currency Name")]
        [DataType(DataType.Currency)]
        [ForeignKey("currency")]
        public int CurrencyName { get; set; }
        public virtual CurrencyBO currency{get;set ;}

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Remark")]
        [DataType(DataType.MultilineText)]
        public string Remark { get; set; }

       
        [Display(Name = "Case Type Category")]
        [ForeignKey("CaseTypeCatagoryID")]
        public virtual CaseTypeCategoryBO caseTyCat { get; set; }
       
        public int CaseTypeCatagoryID { get; set; }

        /////////////////

        [Display(Name = "Party")]
        public string Party { get; set; }



        [Display(Name = "Level")]
        [ForeignKey("level")]
        public int CourtLevel { get; set; }
        public virtual CourtLevelBO level { get; set; }
        /////////////////
        [Display(Name = "Court Name")]
        public string CourtName { get; set; }
        [Display(Name = "Case Created By")]
        public string CaseCreatedBy { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime DateUpdated { get; set; }
        [ScaffoldColumn(false)]
        public int idSquence { get; set; }


    }

}