using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using AppTemplate.Models;
using System.Net;
using AppTemplate;

namespace AppTemplate.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _db = new ApplicationDbContext();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            //_db.Seed(_db);
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //Append missed ZEROS
            string userName = model.Username.Trim();

            string appendableDigit = "";
            for (int i = 0; i < (8 - userName.Length); i++)
                appendableDigit += "0";

            model.Username = appendableDigit + model.Username.Trim();
            //
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    var user = _db.Users.First(u => u.UserName == model.Username);
                    Session["Name"] = user.UserName;
                    if (user.FirstLogin)
                        return RedirectToAction("IdeaHomePage", "idealogs");
                    //return RedirectToAction("ChangePassword");
                    else
                        return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

     
        public ActionResult ChangePass_Admin(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ChangePasswordAdmin model = new ChangePasswordAdmin();
            model.Username = username;
            return View(model);
        }


        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass_Admin(ChangePasswordAdmin model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }           

            var user =  UserManager.FindByName(model.Username);

            if (user != null)
            {
                user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.NewPassword);
                var result = UserManager.Update(user);
                if (result.Succeeded)
                {
                    TempData["Passchagemsg"] = "Password changed.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Passchagemsg"] = result.Errors;
                }
            }

            return View(model);

        }

        //
        // GET: /Account/Index
        [HttpGet]
        [AppTemplateAuthorizeAttribute]
        public ActionResult Index()
        {
            var accounts = new List<RegisterViewModel>();
            foreach (var acct in _db.Users)
            {
                var account = new RegisterViewModel();
                account.Username = acct.UserName;
                accounts.Add(account);
            }

            if (TempData["AccountMessage"] != null)
            {
                ViewBag.AccountMessage = TempData["AccountMessage"];
                TempData["AccountMessage"] = null;
            }
            return View(accounts);
        }

        //
        // GET: /Account/Unauthorized
        [HttpGet]
        public ActionResult Unauthorized()
        {
            //Session.Abandon();
            return View();
        }

        //
        // GET: /Account/Update
        [HttpGet]
        [AppTemplateAuthorizeAttribute]
        public ActionResult Update(string userName)
        {
            var user = _db.Users.FirstOrDefault(x => x.UserName == userName);
            string selected = "";
            foreach (var item in user.Roles)
                selected += item.RoleId + ",";
            ViewBag.Selected = selected;
            ViewBag.Roles = _db.Roles.ToList();

            RegisterViewModel model = new RegisterViewModel();
            model.Username = user.UserName;
            return View(model);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AppTemplateAuthorizeAttribute]
        [ValidateAntiForgeryToken]
        public ActionResult Update(RegisterViewModel model)
        {
            string role = Request.Form["role"];
            if (role != null)
            {
                //Append missed ZEROS
                string userName = model.Username.Trim();

                string appendableDigit = "";
                for (int i = 0; i < (8 - userName.Length); i++)
                    appendableDigit += "0";

                model.Username = appendableDigit + model.Username.Trim();
                //
                var user = _db.Users.FirstOrDefault(x => x.UserName == model.Username);
                if (user != null)
                {
                    _db.ClearUserRoles(UserManager, user.Id);
                    string[] roles = role.Split(',');
                    foreach (var item in roles)
                    {
                        string name = _db.Roles.FirstOrDefault(x => x.Id == item).Name;
                        _db.AddUserToRole(UserManager, user.Id, name);
                    }
                    TempData["AccountMessage"] = "Account updated successfully.";
                    return RedirectToAction("Index", "Account");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }


        // GET: /Account/Register
        [HttpGet]
        [AppTemplateAuthorizeAttribute]
        public ActionResult Register()
        {
            ViewBag.Roles = new SelectList(_db.Roles.Select(x => x.Name).Distinct());
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AppTemplateAuthorizeAttribute]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model, FormCollection formCollection)
        {
            if (ModelState.IsValid)
            {
                //Append missed ZEROS
                string userName = model.Username.Trim();
                string xx = Request.Form["Role"];
                string appendableDigit = "";
                for (int i = 0; i < (8 - userName.Length); i++)
                    appendableDigit += "0";

                model.Username = appendableDigit + model.Username.Trim();
                //
                var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    _db.AddUserToRole(UserManager, user.Id, model.Role);

                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    TempData["AccountMessage"] = "User successfully registered.";
                    return RedirectToAction("Index", "Account");
                }
                //AddErrors(result);
                TempData["AccountMessage"] = GetErrors(result);
                return RedirectToAction("Index", "Account");
            }
            // If we got this far, something failed, redisplay form
            ViewBag.Roles = new SelectList(_db.Roles.Select(x => x.Name).Distinct());
            return View(model);
        }


        //
        // GET: /Account/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    user.FirstLogin = false;
                    await UserManager.UpdateAsync(user);
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                TempData["AccountMessage"] = "Password has changed successfully.";
                return RedirectToAction("Welcome", "Home");
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Account/ForgotPassword
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Append missed ZEROS
                string userName = model.Username.Trim();

                string appendableDigit = "";
                for (int i = 0; i < (8 - userName.Length); i++)
                    appendableDigit += "0";

                model.Username = appendableDigit + model.Username.Trim();
                //
                var user = await UserManager.FindByNameAsync(model.Username);
                if (user == null)// || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "<p>Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a></p>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //Append missed ZEROS
            string userName = model.Username.Trim();

            string appendableDigit = "";
            for (int i = 0; i < (8 - userName.Length); i++)
                appendableDigit += "0";

            model.Username = appendableDigit + model.Username.Trim();
            //
            var user = await UserManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/LogOff
        [HttpGet]
        [Authorize]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session["Name"] = null;
            return RedirectToAction("Login", "Account");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private string GetErrors(IdentityResult result)
        {
            string message = "";
            foreach (var error in result.Errors)
            {
                message = message + error + " ";
            }
            return message;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Welcome", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        public JsonResult GetMenuList()
        {
            List<string> ControllerActionList = new List<string>();

            //var Userrole = _db.ApplicationRolePrivileges.First(r => r.Users == HttpContext.User.Identity.Name);
            var role = _db.Roles.First(r => r.Name == HttpContext.User.Identity.Name);

            List<ApplicationRolePrivilege> rolePrivilege = _db.ApplicationRolePrivileges.Where(r => r.RoleId == role.Id).ToList();
            foreach (var privilege in rolePrivilege)
            {
                ControllerActionList.Add(privilege.Privilage.Action);
            }
            return Json(new
            {
                resultData = ControllerActionList,
                hasList = ControllerActionList.Count() > 0
            }, JsonRequestBehavior.AllowGet);
        }
    }
}