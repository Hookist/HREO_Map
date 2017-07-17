using AddressesMap.Extensions;
using AddressesMap.Models.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AddressesMap.Controllers
{
    public class HomeController : Controller
    {
        AddressesMapModel db = new AddressesMapModel();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult EditAddress(int id)
        {
            Address adr = db.Addresses.Find(id);
            if (adr != null)
            {
                ViewBag.Streets = db.Streets.ToList().ToSelectList(str => str.StreetName,
                                                          str => str.StreetId.ToString(),
                                                          "-1",
                                                          "-- None -- ",
                                                          true);
                ViewBag.Subdivisions = db.Subdivisions.ToList().ToSelectList(str => str.SubdivisionName,
                                                                             str => str.SubdivisionId.ToString(),
                                                                             "-1",
                                                                             "-- None -- ",
                                                                             true);
                return PartialView("EditAddress", adr);
            }
            return View("Index");


        }
    }
}