﻿using System.Web.Mvc;

namespace AppTemplate.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult TestPage()
        {
            return View();
        }
    }
}