namespace ProjectCoffee.Migrations
{
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjectCoffee.Models.CoffeeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ProjectCoffee.Models.CoffeeContext context)
        {
            // Populates users from Active Directory


            // Admin User names - overridden by web.config
            List<string> adminUsers = new List<string> {
                "SaundersK",
                "SomanV",
            };

            // Find settings on web.config
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["adminUsernames"]))
            {
                adminUsers = ConfigurationManager.AppSettings["adminUsernames"].Split(',').ToList();
            }

            // Add any new users in Active Directory / Remove old users
            var users = new ActiveDirectoryService().GetUsers();
            var dbUsers = context.Users.ToList();
            var notInDb = users.Where(u => !dbUsers.Any(dbu => dbu.Guid == u.Guid)).ToList();
            var deleteMe = dbUsers.Where(u => !users.Any(adu => adu.Guid == u.Guid));

            context.Users.AddRange(notInDb.Select(u => new User(u, adminUsers.Any(v => v == u.Username))));
            context.Users.RemoveRange(deleteMe);

            context.SaveChanges();
            dbUsers = context.Users.ToList();

            // Update the rest of the users details
            foreach(var adUser in users)
            {
                var dbuser = dbUsers.First(u => u.Guid == adUser.Guid);
                dbuser.Name = adUser.Name;
                dbuser.NickName = adUser.NickName;
                dbuser.IsAdmin = adminUsers.Any(v => v == adUser.Username);
                context.Users.AddOrUpdate(dbuser);
            }

        }
    }
}
