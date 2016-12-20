using ProjectCoffee.Models;
using ProjectCoffee.Models.DatabaseModels;
using ProjectCoffee.Models.OtherModels;
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
            if (Session["Guid"] != null)
            {
                var user = new DatabaseService().GetUser(Session["Guid"].ToString());
                var adS = new ActiveDirectoryService();
                var viewModel =  new UserViewModel
                {
                    CoffeeList = new DatabaseService().GetAllDrinkTypes().ToList(),
                    Date = DateTime.Now,
                    User = user,
                };

                var isAdminFlag = adS.IsAdmin(user);
                if (isAdminFlag)
                {
                    var dbS = new DatabaseService();
                    viewModel.IsAdmin = true;
                    var usersList = dbS.GetAllUsers().ToList();
                    foreach (var item in usersList)
                    {
                        var lastDate = adS.FindLastLogonTime(item.Guid);
                        item.WillBeThere = lastDate >= DateTime.Today || item.WillBeThere;
                    }
                    viewModel.UserList = usersList;
                    SetupAdmin();
                }

                return View("UserPage", viewModel);
            }

            ViewBag.Title = "Project:Coffee";
            ViewBag.Shownav = false;
            return View("LoginPage");
        }

        private void SetupAdmin()
        {
            ViewBag.Title = "Project Coffee";
            ViewBag.Shownav = false;
            ViewBag.CurrentMeeting = DateTime.Now;
            ViewBag.NextMeeting = DateTime.Now.AddDays(14);
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
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "UserName or Password is wrong");
                }
            
            return RedirectToAction("Index");
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

        public ActionResult LogOut()
        {
            Session.Abandon();

            return RedirectToAction("Index");
        }
        
        /// <summary>
        /// Returns a report of who wants what coffee
        /// </summary>
        /// <param name="confirmDate">The date for the report. This MUST match the date in the GlobalSettings in order for the report to generate.</param>
        /// <returns>A view with the report</returns>
        public ActionResult Report(string confirmDate)
        {
            var dbS = new DatabaseService();
            var usersList = dbS.GetAllUsers().Where(p => p.WillBeThere == true && p.Drink != null).ToList();
            var reports = new List<ReportStruct>();
            
            foreach (var user in usersList)
            {
                int count = usersList.Count(u => u.FirstName == user.FirstName);
                if (count > 1)
                {
                    user.FirstName = $"{user.FirstName} {user.LastName.Substring(0, 1)}.";
                }

                var report = reports.FirstOrDefault(p => p.DrinkType.Name == user.Drink?.Name);
                if (report != null)
                {
                    report.Users.Add(user);
                }
                else
                {
                    reports.Add(new ReportStruct
                    {
                        DrinkType = user.Drink,
                        Users = new List<User>{ user}
                    });           
                }
            }
            return View(reports);
        }
    }
}