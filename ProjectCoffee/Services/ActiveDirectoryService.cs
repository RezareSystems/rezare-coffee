using ProjectCoffee.Models;
using ProjectCoffee.Models.DatabaseModels;
using ProjectCoffee.Models.OtherModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ProjectCoffee.Services
{
    /// <summary>
    /// Handles All Calls to Active Directory
    /// </summary>
    public class ActiveDirectoryService
    {
        /// <summary>
        /// The name of the Active Directory group that contains those who you wish to order coffee for
        /// </summary>
        public string ACTIVE_DIRECTORY_GROUP = "Rezare Coffee";

        /// <summary>
        /// The name of the Active Directory domain
        /// </summary>
        public string ACTIVE_DIRECTORY_DOMAIN = "rezare.co.nz";

        public string ACTIVE_DIRECTORY_ADMIN_GROUP = "Rezare Coffee Admins";


        public ActiveDirectoryService()
        {
            // Get settings from web.config
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["activeDirectoryDomain"])) ACTIVE_DIRECTORY_DOMAIN = ConfigurationManager.AppSettings["activeDirectoryDomain"];
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["activeDirectoryGroup"])) ACTIVE_DIRECTORY_GROUP = ConfigurationManager.AppSettings["activeDirectoryGroup"];
            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["activeDirectoryAdmins"])) ACTIVE_DIRECTORY_ADMIN_GROUP = ConfigurationManager.AppSettings["activeDirectoryAdmins"];
        }

        /// <summary>
        /// Returns whether the user is an admin user or not
        /// </summary>
        /// <param name="user">Determine if this user is admin</param>
        /// <returns>The user is an admin user</returns>
        public bool IsAdmin(User user)
        {
            return IsAdmin(user.Guid);
        }

        /// <summary>
        /// Returns whether the user is an admin user or not
        /// </summary>
        /// <param name="user">Determine if the user with this GUID is admin</param>
        /// <returns>The user is an admin user</returns>
        public bool IsAdmin(Guid guid)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, ACTIVE_DIRECTORY_DOMAIN))
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(pc,IdentityType.Guid, guid.ToString());
                IEnumerable<GroupPrincipal> groups = user.GetGroups().Select(u => u as GroupPrincipal);

                if (groups.Any(u => u.SamAccountName == ACTIVE_DIRECTORY_ADMIN_GROUP)) return true;

                return false;
            }
        }

        /// <summary>
        /// Returns whether the user is an admin user or not
        /// </summary>
        /// <param name="username">Determine if the user with this username is admin</param>
        /// <returns>The user is an admin user</returns>
        public bool IsAdmin(string username)
        {
            using(PrincipalContext pc = new PrincipalContext(ContextType.Domain, ACTIVE_DIRECTORY_DOMAIN))
            {
                UserPrincipal user = UserPrincipal.FindByIdentity(pc, username);
                IEnumerable<GroupPrincipal> groups = user.GetGroups().Select(u => u as GroupPrincipal);

                if (groups.Any(u => u.SamAccountName == ACTIVE_DIRECTORY_ADMIN_GROUP)) return true;

                return false;
            }
        }
        
        /// <summary>
        /// Validates the users credentials
        /// </summary>
        /// <param name="username">The Username</param>
        /// <param name="password">The Password for the user</param>
        /// <returns>The Credentials provided were valid</returns>
        public bool Authenticate(string username, string password)
        {
            using(PrincipalContext pc = new PrincipalContext(ContextType.Domain, ACTIVE_DIRECTORY_DOMAIN))
            {
                return pc.ValidateCredentials(username, password);
            }
        }

        /// <summary>
        /// Gets the Guid for user from Active Directory
        /// </summary>
        /// <param name="username">The user's username</param>
        /// <returns>The GUID from Active Directory</returns>
        public Guid GetGuid(string username)
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, ACTIVE_DIRECTORY_DOMAIN))
            {
                GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, ACTIVE_DIRECTORY_GROUP);
                PrincipalSearchResult<Principal> groupusers = group.GetMembers();
                return groupusers.First(u => (u as UserPrincipal).SamAccountName.ToLower() == username.ToLower()).Guid.Value;
            }
        }

        /// <summary>
        /// Gets the Username from the Active Directory Guid
        /// </summary>
        /// <param name="guid">User Guid</param>
        /// <returns>The Users username</returns>
        public string GetUsername(Guid guid)
        {
            using(var context = new PrincipalContext(ContextType.Domain, ACTIVE_DIRECTORY_DOMAIN))
            {
                return UserPrincipal.FindByIdentity(context, IdentityType.Guid, guid.ToString()).SamAccountName;
            }
        }

        /// <summary>
        /// Gets all the users in the ACTIVE_DIRECTORY_GROUP
        /// </summary>
        /// <returns>A list of users (Domain GUID and name)</returns>
        public List<AdUser> GetUsers()
        {
            // Create the List
            List<AdUser> users = new List<AdUser>();

            // Get connection to AD
            using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, ACTIVE_DIRECTORY_DOMAIN))
            {
                GroupPrincipal group = GroupPrincipal.FindByIdentity(principalContext, ACTIVE_DIRECTORY_GROUP);
                PrincipalSearchResult<Principal> groupusers = group.GetMembers();

                IEnumerable<UserPrincipal> groupusersagain = groupusers.Select(g => g as UserPrincipal);

                // Add users to list
                foreach (var user in groupusersagain)
                {
                    users.Add(new AdUser(user));
                }
            }
            return users;
        }

        /// <summary>
        /// Finds the users last logon time over the whole domain
        /// </summary>
        /// <param name="userId">The Guid of the user</param>
        /// <returns>The time the user last logged on / authenticated with AD</returns>
        /// <remarks>Taken from http://stackoverflow.com/questions/19454162/getting-last-logon-time-on-computers-in-active-directory </remarks>
        public DateTime FindLastLogonTime(Guid userId)
        {
            DirectoryContext context = new DirectoryContext(DirectoryContextType.Domain, ACTIVE_DIRECTORY_DOMAIN);
            DateTime latestLogon = DateTime.MinValue;
            DomainControllerCollection dcc = DomainController.FindAll(context);
            var username = GetUsername(userId);
            foreach (DomainController dc in dcc.Cast<object>()){ 
            
                DirectorySearcher ds;
                using (ds = dc.GetDirectorySearcher())
                {
                    try
                    {
                        ds.Filter = String.Format(
                          "(sAMAccountName={0})",
                          username
                          );
                        ds.PropertiesToLoad.Add("lastLogon");
                        ds.SizeLimit = 1;
                        SearchResult sr = ds.FindOne();

                        if (sr != null)
                        {
                            DateTime lastLogon = DateTime.MinValue;
                            if (sr.Properties.Contains("lastLogon"))
                            {
                                lastLogon = DateTime.FromFileTime(
                                  (long)sr.Properties["lastLogon"][0]
                                  );
                            }

                            if (DateTime.Compare(lastLogon, latestLogon) > 0)
                            {
                                latestLogon = lastLogon;
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            return latestLogon;
        }

    }
}