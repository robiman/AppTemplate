using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppTemplateDAL.Models.Others
{
    public class UserMenu
    {
        public UserMenu()
        {
        }
        public int Id { get; set; }
        public string LinkText { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Roles { get; set; }
    }
}