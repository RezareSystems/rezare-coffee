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
                    viewModel.UserList = dbS.GetAllUsers().ToList();
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
        public ActionResult Report(DateTime confirmDate)
        {
            List<ReportStruct> report = new List<ReportStruct>
            {
                new ReportStruct(){
                    DrinkType = new DrinkType()
                    {
                        Id = -1,
                        Name = "Coffee",
                    },
                    Users = new List<User>()
                    {
                        new User()
                        {
                            Id = -1,
                            Guid = new Guid(),
                            Drink = new DrinkType()
                            {
                                Id = -1,
                                Name = "Coffee"
                            },
                            LastName = "Lloyd",
                            FirstName = "Chris",
                            WillBeThere = true,
                            CoffeeOptions = new List<KeyValuePair<string, int>>()
                            {
                                new KeyValuePair<string, int>("Sugar", 2),

                            }
                        },
                        new User()
                        {
                            Id = -1,
                            Guid = new Guid(),
                            Drink = new DrinkType()
                            {
                                Id = -1,
                                Name = "Coffee"
                            },
                            LastName = "De Lange",
                            FirstName = "Andrew",
                            WillBeThere = true
                        },
                        new User()
                        {
                            Id = -1,
                            Guid = new Guid(),
                            Drink = new DrinkType()
                            {
                                Id = -1,
                                Name = "Coffee"
                            },
                            LastName = "Cooke",
                            FirstName = "Andrew",
                            WillBeThere = true
                        },
                    }
                },
                new ReportStruct(){
                    DrinkType = new DrinkType()
                    {
                        Id = -1,
                        Name = "Hot Chocolate",
                    },
                    Users = new List<User>()
                    {
                        new User()
                        {
                            Id = -1,
                            Guid = new Guid(),
                            Drink = new DrinkType()
                            {
                                Id = -1,
                                Name = "Hot Chocolate"
                            },
                            LastName = "Molchanov",
                            FirstName = "Sergey",
                            WillBeThere = true
                        },
                        new User()
                        {
                            Id = -1,
                            Guid = new Guid(),
                            Drink = new DrinkType()
                            {
                                Id = -1,
                                Name = "Hot Chocolate"
                            },
                            LastName = "DeLeon",
                            FirstName = "Gerard",
                            WillBeThere = true
                        },
                        new User()
                        {
                            Id = -1,
                            Guid = new Guid(),
                            Drink = new DrinkType()
                            {
                                Id = -1,
                                Name = "Hot Chocolate"
                            },
                            LastName = "Tisch",
                            FirstName = "Keenan",
                            WillBeThere = true
                        },
                    }
                }

            };
            return View(report);
        }
    }
}