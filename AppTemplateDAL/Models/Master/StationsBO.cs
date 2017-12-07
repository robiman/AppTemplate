using AppTemplateDAL.Models.Master;
using AppTemplateDAL.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppTemplateDAL.Models.Master
{
    [Table(" Stations")]
    public class StationsBO : AuditAttribute
    {
        [Key]
        public int StationID { get; set; }
        public string StationName { get; set; }
        [ForeignKey("citycountry")]
        public int CityID { get; set; }
       
        public virtual CityCountry citycountry { get; set; }
    }
}