using ProjectCoffee.Models;
using ProjectCoffee.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectCoffee.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// The main page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // TODO: If the user is logged in, go main page

            ViewBag.Title = "Project:Coffee";
            ViewBag.Shownav = false;
            return View("LoginPage");
        }

        public ActionResult Test()
        {
            
            using (var context = new CoffeeContext())
            {
                var user = context.Users.First();
            
                ViewBag.Title = "Project Coffee";
                ViewBag.Shownav = false;
                ViewBag.CurrentMeeting = DateTime.Now;
                ViewBag.NextMeeting = DateTime.Now.AddDays(14);
                return View("AdminPage", user);
            }
        }

        public ActionResult Credits()
        {
            return View();
        }
    }
}