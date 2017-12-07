using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtLegalService.Models
{
    [Table("Currency")]
    public class CurrencyBO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string CurrencyName { get; set; }
        public string description { get; set; }
    }
}
