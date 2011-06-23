﻿using System;
using System.Web.Mvc;
using GiveCampLondon.Website.Models;

namespace GiveCampLondon.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Schedule()
        {
            return View();
        }

        public ActionResult Location()
        {
            return View();
        }
    
        [ActionName("Contact-Us")]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Contact-Us")]
        public ActionResult ContactUs(ContactUsViewModel vm)
        {
            ContactUsViewModel model;
            try
            {
                ViewBag.RequestSent = true;
                model = new ContactUsViewModel();
            }
            catch (Exception)
            {
                model = vm;
            }

            return View(model);
        }
    }
}
