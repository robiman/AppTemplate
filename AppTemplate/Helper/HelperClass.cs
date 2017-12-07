using AppTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppTemplate.Helper
{
    public class HelperClass
    {
        public static List<UserMenuModel> GetMenuList()
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    List<UserMenuModel> userMenuList = new List<UserMenuModel>();
                    ApplicationDbContext db = new ApplicationDbContext();
                    List<ApplicationUserRole> Roles = new List<ApplicationUserRole>();

                    ApplicationUser user = db.Users.Where(u => u.UserName == HttpContext.Current.User.Identity.Name).FirstOrDefault();
                    //Get User role list. Enrollment, curicullum or scheduler ,instructor trainee, dispacher
                    if (user != null)
                    {
                        foreach (var userRole in user.Roles)
                        {
                            Roles.Add(new ApplicationUserRole { RoleId = userRole.RoleId });
                        }
                    }

                    //For every role of that user, get privileges (ControllerName - ActionName)
                    foreach (ApplicationUserRole userRole in Roles)
                    {

                        string roleId = userRole.RoleId;
                        var role = db.Roles.Where(r => r.Id == roleId).ToList().FirstOrDefault();

                        List<ApplicationRolePrivilege> rolePrivilegeList = db.ApplicationRolePrivileges.Include("Privilage").Where(r => r.RoleId == roleId).ToList();
                        foreach (var rolePrivilege in rolePrivilegeList)
                        {
                            int i = 1;
                            var privilege = db.ApplicationPrivileges.Where(p => rolePrivilege.PrivilegeId == p.Id).ToList().FirstOrDefault();

                            if (privilege != null)
                            {
                                if (!String.IsNullOrEmpty(privilege.Action))
                                {
                                    string[] controllerActionName = privilege.Action.Split('-');
                                    userMenuList.Add(new UserMenuModel
                                    {
                                        Id = i++,//privilege.Id != null ? Convert.ToInt16(privilege.Id) : 0,
                                        LinkText = controllerActionName[0],
                                        ControllerName = controllerActionName[0],
                                        ActionName = controllerActionName[1],
                                        Roles = role.Name
                                    });
                                }
                            }
                        }
                    }
                    return userMenuList;
                }

                return new List<UserMenuModel>();
            }
            catch (Exception ex)
            {
                return new List<UserMenuModel>();
            }

        }
    }
}