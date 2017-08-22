using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AddressesMap.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Map()
        {
            bool result = User.IsInRole("Admin");

            if(result)
                return RedirectToAction("Index", "Admin");

            result = User.IsInRole("Subdivision cooperator");

            if (result)
                return RedirectToAction("Index", "User");

            return RedirectToAction("Login", "Acount");
        }
    }
}