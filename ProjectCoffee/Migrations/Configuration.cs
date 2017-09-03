namespace ProjectCoffee.Migrations
{
    using Models;
    using Models.DatabaseModels;
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
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ProjectCoffee.Models.CoffeeContext context)
        {
            // Populates users from Active Directory

            new DatabaseService().GetActiveDirectoryChanges();

        }
    }
}
