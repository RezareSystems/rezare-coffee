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
        /// The id of the type of drink the people are ordering
        /// </summary>
        public int DrinkTypeId { get; set; }

        /// <summary>
        /// The name of the drink these people are ordering
        /// </summary>
        public string DrinkName { get; set; }

        /// <summary>
        /// The users ordering that type of drink
        /// </summary>
        public List<UserEntry> Users { get; set; }
    }

    /// <summary>
    /// The structure of the Users who ordered a particular drink
    /// </summary>
    public class UserEntry
    {
        /// <summary>
        /// The ID of the user that this entry is for
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The First Name of the person the coffee is for
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The Last Name of the person the coffee is for
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The options the user had for the report
        /// </summary>
        public List<KeyValuePair<string, int>> Options { get; set; }

        public string GetStringCoffeeOptions
        {
            get
            {
                var listOfString = new List<string>();
                foreach (var cf in Options)
                {
                    if (cf.Value > 0)
                    {
                        listOfString.Add($"{cf.Key} - {cf.Value}");
                    }
                }

                return string.Join(", ", listOfString);
            }
        }
    }
}