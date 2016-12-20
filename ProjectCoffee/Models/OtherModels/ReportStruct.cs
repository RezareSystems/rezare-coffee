using ProjectCoffee.Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCoffee.Models.OtherModels
{
    /// <summary>
    /// The structure of the Coffee Report
    /// </summary>
    public class ReportStruct
    {
        /// <summary>
        /// The type of drink these users are ordering
        /// </summary>
        public DrinkType DrinkType { get; set; }
        /// <summary>
        /// The users ordering that type of drink
        /// </summary>
        public List<User> Users { get; set; }
    }
}