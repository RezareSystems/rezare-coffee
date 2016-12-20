using Newtonsoft.Json;
using ProjectCoffee.Models.OtherModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProjectCoffee.Models.DatabaseModels
{
    /// <summary>
    /// A user of the coffee system
    /// </summary>
    public class User
    {
        /// <summary>
        /// The ID of the record in our database
        /// </summary>
        public int Id { get; set; }

        [ForeignKey("Drink")]
        public int? DrinkId { get; set; }

        /// <summary>
        /// The ID of the record in Active Directory
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// The Last Name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The Users First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The drink the user wants ordered for them
        /// </summary>
        public virtual DrinkType Drink { get; set; }


        public int Drink_Id { get; }

        /// <summary>
        /// The user will be at the next meeting
        /// </summary>
        public bool WillBeThere { get; set; }

        /// <summary>
        /// The user is an administrator
        /// </summary>
        [Obsolete("Do not use this, use the ActiveDirectoryService.IsAdmin() function instead", true)]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// The JSON String that holds the coffee options
        /// </summary>
        public string CoffeeOptionsJson { get; set; }

        /// <summary>
        /// The list of Coffee Options for the user
        /// </summary>
        [NotMapped]
        public List<KeyValuePair<string, int>> CoffeeOptions {
            get
            {
                if (CoffeeOptionsJson == null) return new List<KeyValuePair<string, int>>();
                return JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(CoffeeOptionsJson);
            }
            set
            {
                CoffeeOptionsJson = JsonConvert.SerializeObject(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string GetStringCoffeeOptions
        {
            get
            {
                var listOfString = new List<string>();
                foreach(var cf in CoffeeOptions)
                {
                    if (cf.Value > 0)
                    {
                        listOfString.Add($"{cf.Key} - {cf.Value}");
                    }
                }

                return string.Join(", ", listOfString);
            }
        }


        /// <summary>
        /// Creates a new Database user from an Active Directory User (basic)
        /// </summary>
        /// <param name="user">Some user Information from Active Directory</param>
        /// <param name="isAdmin">The user is an administrator</param>
        public User(AdUser user, bool isAdmin = false)
        {
            Guid = user.Guid;
            LastName = user.LastName;
            WillBeThere = false;
            FirstName = user.FirstName;
            CoffeeOptionsJson = "[{\"Key\":\"SoyMilk\",\"Value\":0},{\"Key\":\"ExtraShot\",\"Value\":0},{\"Key\":\"Sugar\",\"Value\":0},{\"Key\":\"Vanilla\",\"Value\":0},{\"Key\":\"Hazelnut\",\"Value\":0},{\"Key\":\"Caramel\",\"Value\":0}]";
        }

        /// <summary>
        /// A parameterless constructor
        /// </summary>
        public User()
        {

        }
    }
}