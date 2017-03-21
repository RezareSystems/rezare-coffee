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

        public CoffeeReport GetLastReport()
        {
            using (var coffeeContext = new CoffeeContext())
            {
                return coffeeContext.CoffeeReports.ToList().LastOrDefault();
            }
        }

        public DateTime GetMeeting()
        {
            using(var coffeeContext = new CoffeeContext())
            {
                var settings = coffeeContext.GlobalInformation.FirstOrDefault();
                return settings == null ? DateTime.Now : settings.MeetingDate;
            }
        }

        public void SetMeeting(DateTime time)
        {
            using(var coffeeContext = new CoffeeContext())
            {
                var settings = coffeeContext.GlobalInformation.FirstOrDefault();
                settings = settings ?? new GlobalInformation();
                settings.MeetingDate = time;
                coffeeContext.GlobalInformation.AddOrUpdate(settings);
                coffeeContext.SaveChanges();
            }
        }

        public void ClearWillBeThere()
        {
            using (var coffeeContext = new CoffeeContext())
            {
                foreach(var user in coffeeContext.Users)
                {
                    user.WillBeThere = false;
                }

                coffeeContext.SaveChanges();
            }
        }

        public void GetActiveDirectoryChanges(CoffeeContext context = null)
        {
            using (var coffeeContext = context ?? new CoffeeContext())
            {
                // Add any new users in Active Directory / Remove old users
                var users = new ActiveDirectoryService().GetUsers();
                var dbUsers = coffeeContext.Users.ToList();
                var notInDb = users.Where(u => !dbUsers.Any(dbu => dbu.Guid == u.Guid)).ToList();
                var deleteMe = dbUsers.Where(u => !users.Any(adu => adu.Guid == u.Guid)).ToList();

                coffeeContext.Users.AddRange(notInDb.Select(u => new User(u)));
                coffeeContext.Users.RemoveRange(deleteMe);

                coffeeContext.SaveChanges();
                dbUsers = coffeeContext.Users.ToList();

                // Update the rest of the users details
                foreach (var adUser in users)
                {
                    var dbuser = dbUsers.First(u => u.Guid == adUser.Guid);
                    dbuser.LastName = adUser.LastName;
                    dbuser.FirstName = adUser.FirstName;
                    coffeeContext.Users.AddOrUpdate(dbuser);
                }

                coffeeContext.SaveChanges();
            }
        }

        public void SaveReport(CoffeeReport coffeereport)
        {
            using (var coffeeContext = new CoffeeContext())
            {
                coffeeContext.CoffeeReports.Add(coffeereport);

                coffeeContext.SaveChanges();
            }
        }

        public CoffeeReport GetReport(int id)
        {
            using (var coffeeContext = new CoffeeContext())
            {
                return coffeeContext.CoffeeReports.Include(r => r.GeneratedBy).FirstOrDefault(r => r.Id == id);
            }
        }

        public CoffeeReport GetReport(DateTime generatedTime)
        {
            using (var coffeeContext = new CoffeeContext())
            {
                return coffeeContext.CoffeeReports.SingleOrDefault(r => r.GeneratedOn == generatedTime);
            }
        }

        public Reminder GetReminder(Guid reminderId)
        {
            using (var coffeeContext = new CoffeeContext())
            {
                return coffeeContext.Reminders.Include(r => r.User).Include(r => r.User.Drink).SingleOrDefault(r => r.Id == reminderId);
            }
        }

        public void SetWillBeThere(User forUser, bool value = true)
        {
            using (var coffeeContext = new CoffeeContext())
            {
                var user = coffeeContext.Users.SingleOrDefault(u => u.Id == forUser.Id);
                user.WillBeThere = value;
                coffeeContext.SaveChanges();
            }
        }

        public void SetWillBeThere(User forUser, Reminder reminder)
        {
            SetWillBeThere(forUser);
            using (var coffeeContext = new CoffeeContext())
            {
                var dbReminder = coffeeContext.Reminders.Single(r => r.Id == reminder.Id);
                coffeeContext.Reminders.Remove(dbReminder);
                coffeeContext.SaveChanges();
            }
        }
        
        public void ClearOldReminders(DateTime oldestAcceptableDate)
        {
            using (var coffeeContext = new CoffeeContext())
            {
                var removethese = coffeeContext.Reminders.Where(r => r.CreatedOn < oldestAcceptableDate);
                coffeeContext.Reminders.RemoveRange(removethese);
                coffeeContext.SaveChanges();
            }
        }
    }
}