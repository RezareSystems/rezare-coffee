using ProjectCoffee.Models;
using ProjectCoffee.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectCoffee.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            using (var context = new CoffeeContext())
            {
                var drinks = from d in context.DrinkTypes
                             select d;

                var drinkList = drinks.ToList();

                var currentUser = from u in context.Users
                                  where u.Guid == new Guid("33B4BFC1-7A0A-40F7-90A3-D84B29564C2F")
                                  select u;

                var viewModel = new UserViewModel();
                viewModel.User = currentUser.FirstOrDefault();
                viewModel.CoffeeList = drinkList;

                viewModel.Date = DateTime.Now;


                return View(viewModel);
            }
        }
    }

    public class UserViewModel
    {
        public User User { get; set; }

        public List<DrinkType> CoffeeList { get; set; }

        public DateTime Date { get; set; }
    }
}