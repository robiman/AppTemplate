using System.Web.Mvc;

namespace AppTemplate.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Welcome()
        {
            return View();
        }
    }
}