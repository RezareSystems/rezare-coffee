using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCoffee.Models
{
    /// <summary>
    /// The Type of drink a user can order
    /// </summary>
    public class DrinkType
    {
        /// <summary>
        /// The Id of the drink type in the database
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the type of drink
        /// </summary>
        public string Name { get; set; }
    }
}