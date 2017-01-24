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
using System.Web.WebPages;

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
                ViewBag.Title = "Choose your Coffee";

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
                    ViewBag.Title = "Choose your Coffee | Coffee Admin";

                    viewModel.IsAdmin = true;
                    var usersList = dbS.GetAllUsers().ToList();
                    foreach (var item in usersList)
                    {
                        var lastDate = adS.FindLastLogonTime(item.Guid);
                        item.WillBeThere = lastDate >= DateTime.Today || item.WillBeThere;
                    }
                    viewModel.UserList = usersList;
                    viewModel.UserList.Sort(new Comparison<User>((user1, user2) =>
                    {
                        if (user1.FirstName != user2.FirstName)
                            return user1.FirstName.CompareTo(user2.FirstName);
                        else
                        {
                            return user1.LastName.CompareTo(user2.LastName);
                        }
                    }));


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
            ViewBag.ReadableNextMeeting = currentMeeting.AddDays(14).GetReadable();
        }

        public ActionResult Credits()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            new DatabaseService().GetActiveDirectoryChanges();
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

            ViewBag.OrderDate = confirmDate.ToString("MMMM") + " " + confirmDate.GetReadable() + ", " + confirmDate.ToString("yyyy");
            ViewBag.Title = "Coffee Order for " + ViewBag.OrderDate;
            ViewBag.Shownav = false;

            if (Session["Guid"] == null)
            {
                ViewBag.Error = "Must be logged in to generate report";
                ViewBag.Shownav = true;
                return View(reports);
            }

            if (!adS.IsAdmin((Guid)Session["Guid"]))
            {
                ViewBag.Error = "Must be an admin to generate reports. Sneaky sneaky.";
                ViewBag.Shownav = true;
                return View(reports);
            }

            if (confirmDate.Date != dbS.GetMeeting().Date)
            {
                ViewBag.Error = "Cannot Re-generate Report";
                ViewBag.Shownav = true;
                return View(reports);
            }

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
            dbS.ClearWillBeThere();

            return View(reports);
        }

        public ActionResult GetAdminDate(DateTime date)
        {
            return Content(date.ToString("MMMM") + " " +  date.GetReadable());
        }
    }
}