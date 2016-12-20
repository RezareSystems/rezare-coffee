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
                return JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(CoffeeOptionsJson);
            }
            set
            {
                CoffeeOptionsJson = JsonConvert.SerializeObject(value);
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
            WillBeThere = true;
            FirstName = user.FirstName;
        }

        /// <summary>
        /// A parameterless constructor
        /// </summary>
        public User()
        {

        }
    }
}