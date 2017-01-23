using ProjectCoffee.Helpers;
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
                var dbS = new DatabaseService();
                var viewModel =  new UserViewModel
                {
                    CoffeeList = new DatabaseService().GetAllDrinkTypes().ToList(),
                    Date = dbS.GetMeeting(),
                    User = user,
                };

                var isAdminFlag = adS.IsAdmin(user);
                if (isAdminFlag)
                {
                    viewModel.IsAdmin = true;
                    var usersList = dbS.GetAllUsers().ToList();
                    foreach (var item in usersList)
                    {
                        var lastDate = adS.FindLastLogonTime(item.Guid);
                        item.WillBeThere = lastDate >= DateTime.Today || item.WillBeThere;
                    }
                    viewModel.UserList = usersList;
                    SetupAdmin(dbS.GetMeeting());
                }

                ViewBag.ReadableDate = viewModel.Date.GetReadable();

                return View("UserPage", viewModel);
            }

            ViewBag.Title = "Project:Coffee";
            ViewBag.Shownav = false;
            return View("LoginPage");
        }

        private void SetupAdmin(DateTime currentMeeting)
        {
            ViewBag.Title = "Project Coffee";
            ViewBag.Shownav = false;
            ViewBag.CurrentMeeting = currentMeeting;
            ViewBag.NextMeeting = currentMeeting.AddDays(14);
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
        public ActionResult Report(DateTime confirmDate, DateTime nextMeeting)
        {
            var dbS = new DatabaseService();
            var adS = new ActiveDirectoryService();
            var usersList = dbS.GetAllUsers().Where(p => p.Drink != null).ToList();
            var reports = new List<ReportStruct>();

            if (confirmDate.Date != dbS.GetMeeting().Date) return View(reports);

            // Filter for active users
            foreach (var item in usersList)
            {
                var lastDate = adS.FindLastLogonTime(item.Guid);
                item.WillBeThere = lastDate >= DateTime.Today || item.WillBeThere;
            }

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

            // Update the next meeting date
            dbS.SetMeeting(nextMeeting);

            return View(reports);
        }
    }
}