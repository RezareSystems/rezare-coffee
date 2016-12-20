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


        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            var userService = new ActiveDirectoryService();
            var success = userService.Authenticate(user.UserName, user.Password);
                if (success)
                {
                    Session["Guid"] = userService.GetGuid(user.UserName);
                    return RedirectToAction("LoggedIn");
                }
                else
                {
                    ModelState.AddModelError("", "UserName or Password is wrong");
                }
            
            return View("LoginPage");
        }

        public ActionResult LoggedIn()
        {
            if (Session["Guid"] != null)
            {
                return View("LoginPage");

            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        
    }
}