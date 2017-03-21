using ProjectCoffee.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectCoffee.Models
{
    /// <summary>
    /// The Database Context
    /// </summary>
    public class CoffeeContext : DbContext
    {
        /// <summary>
        /// All users in the database (not necessarly everyone in AD)
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// The Global Information
        /// </summary>
        public DbSet<GlobalInformation> GlobalInformation { get; set; }
        
        /// <summary>
        /// The types of drink available for order
        /// </summary>
        public DbSet <DrinkType> DrinkTypes { get; set; }

        /// <summary>
        /// Previous Coffee Reports
        /// </summary>
        public DbSet <CoffeeReport> CoffeeReports { get; set; }

        /// <summary>
        /// Reminder codes that have been emailed to users
        /// </summary>
        public DbSet <Reminder> Reminders { get; set; }
    }
}