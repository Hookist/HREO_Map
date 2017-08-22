using AddressesMap.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AddressesMap.Controllers
{
    public class UserController : Controller
    {
        AddressesMapModel db = new AddressesMapModel();

        // GET: User
        [Authorize(Roles = "Subdivision cooperator")]
        public ActionResult Index()
        {
            HttpCookie cookie = HttpContext.Request.Cookies.Get("SubdivisionId");

            ViewBag.SubdivisionId = cookie.Value;
            int subdivisionId = Convert.ToInt32(cookie.Value);
            ViewBag.SubdivisionName = db.Subdivisions.Where(s => s.SubdivisionId == subdivisionId).FirstOrDefault().SubdivisionName;

            return View();
        }
    }
}