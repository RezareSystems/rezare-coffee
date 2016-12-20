using System;
using System.Collections.Generic;
using System.Linq;
using ProjectCoffee.Models;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ProjectCoffee.Models.DatabaseModels;

namespace ProjectCoffee.Services
{
    public class DatabaseService:IDatabaseService
    {
        public User GetUser(string guid)
        {
            using (var coffeeContext= new CoffeeContext())
            {
                 var userGuid = new Guid(guid);
                 return coffeeContext.Users.Include(m=>m.Drink).FirstOrDefault(i => i.Guid == userGuid);
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var coffeeContext = new CoffeeContext())
            {
                 return coffeeContext.Users.Include(m => m.Drink).ToList();
            }
        }

        public IEnumerable<DrinkType> GetAllDrinkTypes()
        {
            using (var coffeeContext= new CoffeeContext())
            {
             return coffeeContext.DrinkTypes.ToList();
            }
        }

        public DrinkType GetDrinkType(int id)
        {
            using (var coffeeContext= new CoffeeContext())
            {
                return coffeeContext.DrinkTypes.FirstOrDefault(i => i.Id == id);
            }
        }

        public void UpdateUser(User user)
        {
            using (var coffeeContext= new CoffeeContext())
            {
                coffeeContext.Users.AddOrUpdate(user);
                coffeeContext.SaveChanges();
            }
        }
    }
}