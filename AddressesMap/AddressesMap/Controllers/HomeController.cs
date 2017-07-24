using AddressesMap.Models;
using AddressesMap.Models.DBModels;
using AddressesMap.Models.VerifyModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            //Guid g = Guid.NewGuid();
            //vm.AspNetRoles.Add(new AspNetRole() { Id = g.ToString(), Name = "Subdivision cooperator" });
            //vm.SaveChanges();
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

    }
}