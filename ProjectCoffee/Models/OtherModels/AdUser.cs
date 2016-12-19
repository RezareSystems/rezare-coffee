using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;

namespace ProjectCoffee.Models.OtherModels
{
    /// <summary>
    /// A basic Active Directory User
    /// </summary>
    public class AdUser
    {
        /// <summary>
        /// The GUID in Active Directory for the User
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// The Username in Active Directory
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The users last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The users first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The actual active directory user
        /// </summary>
        private UserPrincipal _user { get; set; }

        /// <summary>
        /// Setup a new Active Directory user in memory
        /// </summary>
        /// <param name="user">The user pulled back from active directory</param>
        public AdUser(UserPrincipal user)
        {
            Guid = user.Guid.Value;
            Username = user.SamAccountName;
            LastName = user.Surname;
            FirstName = user.GivenName;
            _user = user;
        }
    }
}