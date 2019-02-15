using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SopiiTaiEi1.ViewModel;

namespace SopiiTaiEi1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Calendar()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult Calendar(MeetingViewModel model )
        {
            if (ModelState.IsValid)
            {
                ViewBag.EndingDateErrorMessage = "";
                ViewBag.StartingDateErrorMessage = "";
                if (model.StartingDateValidation(model.StartingDate))
                {
                    if (!(model.EndingDate < model.StartingDate))
                    {
                        var interval = (model.EndingDate - model.StartingDate).TotalDays;
                        ViewBag.interval = interval;
                        ViewBag.startingDate = model.StartingDate.Date;
                        ViewBag.endingdate = model.EndingDate.Date;
                        return View();
                    }
                    else
                    {
                        ViewBag.EndingDateErrorMessage = ("You can't choose a date earlier than" + model.StartingDate.ToShortDateString());
                    }
                }
                else
                {
                    ViewBag.StartingDateErrorMessage = "You can't choose old date ";
                }
            }
            return View("Index" , model);
        }
        

        public ActionResult Contact()
        {
            return View();
        }
    }
}
