using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;
using System.Data.Entity;
using System.Linq;

namespace AppTemplate.Models
{

    /// <summary>
    /// Privilege base model
    /// </summary>
    public class ApplicationPrivilege
    {
        public string Id { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Role privilege base model
    /// </summary>
    public class ApplicationRolePrivilege
    {
        public string RoleId { get; set; }
        public string PrivilegeId { get; set; }

        public ApplicationPrivilege Privilage { get; set; }
    }

    /// <summary>
    /// Role base model (inherits Identity Role model
    /// </summary>
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string name, string description)
            : base(name)
        {
            this.Description = description;
        }

        public virtual string Description { get; set; }

        public ICollection<ApplicationRolePrivilege> RolePrivileges { get; set; }
    }

    /// <summary>
    /// User role base model (inherits Identity User Role model)
    /// </summary>
    public class ApplicationUserRole : IdentityUserRole
    {
        public ApplicationUserRole()
            : base()
        { }

        public ApplicationRole Role { get; set; }
    }

    /// <summary>
    /// User base model (inherits Identity User model)
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            FirstLogin = true;
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public bool FirstLogin { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }

    /// <summary>
    /// Identity database context containing all identity methods
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("name=IdealogContext", throwIfV1Schema: false)
        {
            //Database.SetInitializer<ApplicationDbContext>(new CreateIfNotExistsInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        /// <summary>
        /// Overridden On Model Creating method
        /// </summary>
        /// <param name="modelBuilder">Database Model Builder</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("ModelBuilder is NULL");
            }

            base.OnModelCreating(modelBuilder);

            //Rename the default table names
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            //Name the new tables; define keys and relations
            modelBuilder.Entity<ApplicationRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users").HasMany<ApplicationUserRole>((ApplicationUser u) => u.UserRoles);
            modelBuilder.Entity<ApplicationUserRole>().HasKey(r => new { UserId = r.UserId, RoleId = r.RoleId });
            modelBuilder.Entity<ApplicationPrivilege>().ToTable("Privileges").HasKey<string>(p => p.Id);
            modelBuilder.Entity<ApplicationRole>().HasMany<ApplicationRolePrivilege>((ApplicationRole r) => r.RolePrivileges);
            modelBuilder.Entity<ApplicationRolePrivilege>().ToTable("RolePrivileges").HasKey(p => new { RoleId = p.RoleId, PrivilegeId = p.PrivilegeId });
        }

        /// <summary>
        /// Seed method for initial system run
        /// </summary>
        /// <param name="context"> Identity database context</param>
        /// <returns></returns>
        public bool Seed(ApplicationDbContext context)
        {
#if DEBUG
            // Create my debug (testing) objects here

            bool success = false;

            ApplicationDbContext _db = new ApplicationDbContext();

            List<ApplicationPrivilege> privileges = new List<ApplicationPrivilege>()
            {
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Roles-Create", Description="Create Roles"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Roles-Edit", Description="Edit Roles"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Roles-Index", Description="List Roles"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Roles-Delete", Description="Delete Roles"},

                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Account-Index", Description="List Accounts"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Account-Index", Description="List Accounts"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Account-Update", Description="Update Account"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Account-Update", Description="Update Account"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Account-Register", Description="Create Account"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Account-Register", Description="Create Account"},

                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Privileges-Create", Description="Create privileges"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Privileges-Create", Description="Create privileges"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Privileges-Edit", Description="Edit privileges"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Privileges-Edit", Description="Edit privileges"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Privileges-Delete", Description="Delete privileges"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Privileges-Index", Description="List privileges"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Privileges-Delete", Description="Delete privileges"},
                new ApplicationPrivilege(){Id = Guid.NewGuid().ToString(), Action="Privileges-Index", Description="List privileges"},
                
            };

            _db.ApplicationPrivileges.AddRange(privileges);

            ApplicationRoleManager _roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));

            success = this.CreateRole(_roleManager, "Admin", "Global access");
            if (!success == true) return success;

            foreach (ApplicationPrivilege ap in privileges)
            {
                _db.ApplicationRolePrivileges.Add(
                    new ApplicationRolePrivilege
                    {
                        PrivilegeId = ap.Id,
                        RoleId = _db.Roles.First(r => r.Name == "Admin").Id
                    });
            }

            _db.SaveChanges();

            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            ApplicationUser user = new ApplicationUser();

            user.UserName = "admin@AppTemplate.com";
            user.Email = "admin@AppTemplate.com";

            IdentityResult result = userManager.Create(user, "Abcd@1234");

            success = this.AddUserToRole(userManager, user.Id, "Admin");
            if (!success) return success;

            return success;
#endif

        }

        /// <summary>
        /// Checks whether a given role exists
        /// </summary>
        /// <param name="roleManager">Role manager in which to look for the role</param>
        /// <param name="name">Role name</param>
        /// <returns></returns>

        public bool RoleExists(ApplicationRoleManager roleManager, string name)
        {
            return roleManager.RoleExists(name);
        }

        /// <summary>
        /// Creates a new role
        /// </summary>
        /// <param name="_roleManager">Role manager in which to create the role</param>
        /// <param name="name">Role name</param>
        /// <param name="description">Role description</param>
        /// <returns></returns>
        public bool CreateRole(ApplicationRoleManager _roleManager, string name, string description = "")
        {
            var idResult = _roleManager.Create<ApplicationRole, string>(new ApplicationRole(name, description));
            return idResult.Succeeded;
        }

        /// <summary>
        /// Adds a user to a given role
        /// </summary>
        /// <param name="_userManager">User manager in which to add user to role</param>
        /// <param name="userId">User id to be added to a role</param>
        /// <param name="roleName">Role to which to add a role</param>
        /// <returns></returns>
        public bool AddUserToRole(ApplicationUserManager _userManager, string userId, string roleName)
        {
            var idResult = _userManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }

        /// <summary>
        /// Clears role of a given user
        /// </summary>
        /// <param name="userManager">User manager in which to clear user to role</param>
        /// <param name="userId">User id to be cleared</param>
        public void ClearUserRoles(ApplicationUserManager userManager, string userId)
        {
            var user = userManager.FindById(userId);
            var currentRoles = new List<IdentityUserRole>();

            currentRoles.AddRange(user.Roles);

            ApplicationDbContext _db = new ApplicationDbContext();

            foreach (var role in currentRoles)
            {
                string name = _db.Roles.FirstOrDefault(x => x.Id == role.RoleId).Name;
                userManager.RemoveFromRole(userId, name);
            }
        }

        /// <summary>
        /// Removes a given user from a given role
        /// </summary>
        /// <param name="userManager">User manager in which to remove user to role</param>
        /// <param name="userId">User id to be removed</param>
        /// <param name="roleName">Role name from which to be removed</param>
        public void RemoveFromRole(ApplicationUserManager userManager, string userId, string roleName)
        {
            userManager.RemoveFromRole(userId, roleName);
        }

        /// <summary>
        /// Deletes a given role
        /// </summary>
        /// <param name="context">Identity database context</param>
        /// <param name="userManager">User manager in which to delete a role</param>
        /// <param name="roleId">Role id to be removed</param>

        public void DeleteRole(ApplicationDbContext context, ApplicationUserManager userManager, string roleId)
        {
            var roleUsers = context.Users.Where(u => u.UserRoles.Any(r => r.RoleId == roleId));
            var role = context.Roles.Find(roleId);

            foreach (var user in roleUsers)
            {
                this.RemoveFromRole(userManager, user.Id, role.Name);
            }
            context.Roles.Remove(role);
            context.SaveChanges();
        }

        /// <summary>
        /// Context Initializer for the initial run
        /// </summary>
        public class CreateIfNotExistsInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

        public DbSet<ApplicationPrivilege> ApplicationPrivileges { get; set; }
        //public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationRolePrivilege> ApplicationRolePrivileges { get; set; }
    }
}