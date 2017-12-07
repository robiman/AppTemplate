using AppTemplateDAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppTemplateDAL.Models.Master
{
    [Table("CityCountry")]
    public class CityCountry : AuditAttribute
    {
        [Key]
        public int CityId { get; set; }
        public string cityName { get; set; }
        public string country { get; set; }
    }
}