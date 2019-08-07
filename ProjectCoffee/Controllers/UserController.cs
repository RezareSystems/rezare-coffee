using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectCoffee.Helpers;
using ProjectCoffee.Models;
using ProjectCoffee.Models.DatabaseModels;
using ProjectCoffee.Services;
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

        [HttpPost]
        public void SaveChanges(string data)
        {
            dynamic d = JObject.Parse(data);

            var drinkID = d.DrinkID;
            var IsHere = d.IsHere;

            var newOptions = new List<KeyValuePair<string, KeyValuePair<string, string>>>();

            foreach (var i in d.Options)
            {
                newOptions.Add(new KeyValuePair<string, KeyValuePair<string,string>>(i.Key.ToString(), new KeyValuePair<string, string>(i.Value.Key.ToString(), i.Value.Value.ToString())));
            }

            var databaseService = new DatabaseService();
            var user = databaseService.GetUser(Session["Guid"].ToString());

            user.CoffeeOptionsJson = JsonConvert.SerializeObject(newOptions);
            user.DrinkId = drinkID;
            user.WillBeThere = IsHere;

            databaseService.UpdateUser(user);
        }
    }

    public class UserViewModel
    {
        public User User { get; set; }

        public List<DrinkType> CoffeeList { get; set; }

        public DateTime Date { get; set; }

        public bool IsAdmin { get; set; }

        public List<User> UserList { get; set; } 
    }
}