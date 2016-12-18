using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ProjectCoffee.Models
{
    [DirectoryRdnPrefix("CN")]
    [DirectoryObjectClass("user")]
    public class UserPrincipalExtended : UserPrincipal
    {
        public UserPrincipalExtended(PrincipalContext context) : base(context) { }

        public UserPrincipalExtended(PrincipalContext
            context,
            string samAccountName,
            string password,
            bool enabled)
            : base(context, samAccountName, password, enabled) { }

        public static new UserPrincipalExtended FindByIdentity(PrincipalContext context,
                                                       string identityValue)
        {
            return (UserPrincipalExtended)FindByIdentityWithType(context,
                                                         typeof(UserPrincipalExtended),
                                                         identityValue);
        }

        public static new UserPrincipalExtended FindByIdentity(PrincipalContext context,
                                                       IdentityType identityType,
                                                       string identityValue)
        {
            return (UserPrincipalExtended)FindByIdentityWithType(context,
                                                         typeof(UserPrincipalExtended),
                                                         identityType,
                                                         identityValue);
        }

        #region custom attributes
        [DirectoryProperty("RealLastLogon")]
        public DateTime? RealLastLogon
        {
            get
            {
                if (ExtensionGet("LastLogon").Length > 0)
                {
                    var lastLogonDate = ExtensionGet("LastLogon")[0];
                    var lastLogonDateType = lastLogonDate.GetType();

                    var highPart = (Int32)lastLogonDateType.InvokeMember("HighPart",
                        BindingFlags.GetProperty, null, lastLogonDate, null);
                    var lowPart = (Int32)lastLogonDateType.InvokeMember("LowPart",
                        BindingFlags.GetProperty | BindingFlags.Public, null, lastLogonDate, null);

                    var longDate = ((Int64)highPart << 32 | (UInt32)lowPart);

                    return longDate > 0 ? (DateTime?)DateTime.FromFileTime(longDate) : null;
                }

                return null;
            }
        }
        #endregion
    }
}