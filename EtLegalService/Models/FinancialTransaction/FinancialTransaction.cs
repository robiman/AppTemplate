using ECASDAL.Models.Master;
using ECASDAL.Models.Members;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECASDAL.Models.FinancialTransaction
{
    [Table("FinancialTransaction")]
    public class FinancialTransaction : Generic.AuditAttribute
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FinancialTransactionId { get; set; }

        //refer to memebr id
        [Display(Name = "Membership ID")]
        public int memID { get; set; }

        [ForeignKey("memID")]
        public virtual Member Member { get; set; }

        [Required(ErrorMessage = "Transaction No is required.")]
        [Display(Name = "Transaction No")]
        [Index("UK_TransactionNo", IsUnique = true, Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionNo { get; set; }

        //refer to master data combo
        [Required(ErrorMessage = "Transaction Type is required.")]
        [Display(Name = "Transaction Type")]
        public int FinancialTransactionTypeId { get; set; }
        [ForeignKey("comboid")]
        public virtual Combo cbo { get; set; }   
                
        [Display(Name = "Description")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Transaction date is required.")]
        [Display(Name = "Transaction Date")]
        public DateTime TransactionDate { get; set; }
        

    }
}
