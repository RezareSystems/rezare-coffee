using Newtonsoft.Json;
using ProjectCoffee.Helpers;
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
        public List<KeyValuePair<string, KeyValuePair<string,string>>> CoffeeOptions {
            get
            {
                if (CoffeeOptionsJson == null) return new List<KeyValuePair<string, KeyValuePair<string,string>>>();
                return JsonConvert.DeserializeObject<List<KeyValuePair<string, KeyValuePair<string, string>>>>(CoffeeOptionsJson);
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
                return StringHelper.GetCoffeeOptions(CoffeeOptions);
            }
        }

        /// <summary>
        /// This says whether the user has made any modifications to the coffee
        /// </summary>
        public bool HasModifications()
        {
            // Are any shots / int values > 0?

            // Are any booleans true?

            return true;
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
            DrinkId = 1;
            CoffeeOptionsJson = "[{\"Key\":\"SoyMilk\",\"Value\":{\"Key\":\"int\",\"Value\":\"0\"}},{\"Key\":\"ExtraShot\",\"Value\":{\"Key\":\"int\",\"Value\":\"0\"}},{\"Key\":\"Sugar\",\"Value\":{\"Key\":\"int\",\"Value\":\"0\"}},{\"Key\":\"Vanilla\",\"Value\":{\"Key\":\"int\",\"Value\":\"0\"}},{\"Key\":\"Hazelnut\",\"Value\":{\"Key\":\"int\",\"Value\":\"0\"}},{\"Key\":\"Caramel\",\"Value\":{\"Key\":\"int\",\"Value\":\"0\"}},{\"Key\":\"No Sugar\",\"Value\":{\"Key\":\"bool\",\"Value\":\"false\"}},{\"Key\":\"Trim\",\"Value\":{\"Key\":\"bool\",\"Value\":\"false\"}},{\"Key\":\"Marshmallows\",\"Value\":{\"Key\":\"bool\",\"Value\":\"false\"}}]";
        }

        /// <summary>
        /// A parameterless constructor
        /// </summary>
        public User()
        {

        }
    }
}