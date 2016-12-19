using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCoffee.Models
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

        /// <summary>
        /// The ID of the record in Active Directory
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// The Name of the user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Users First Name
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// The drink the user wants ordered for them
        /// </summary>
        public DrinkType Drink { get; set; }

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
        /// Creates a new Database user from an Active Directory User (basic)
        /// </summary>
        /// <param name="user">Some user Information from Active Directory</param>
        /// <param name="isAdmin">The user is an administrator</param>
        public User(AdUser user, bool isAdmin = false)
        {
            Guid = user.Guid;
            Name = user.Name;
            WillBeThere = true;
            NickName = user.NickName;
        }

        /// <summary>
        /// A parameterless constructor
        /// </summary>
        public User()
        {

        }
    }
}