using ProjectCoffee.Models;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectCoffee.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult Testing()
        {
            ViewBag.Users = new List<string>();
            ViewBag.GroupUsers = new List<string>();

            using(PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, "REZDC4"))
            {
                //PrincipalSearchResult<UserPrincipal> users = UserPrincipal.FindByLogonTime(principalContext, new DateTime(2016, 01, 01), MatchType.GreaterThan);
                //foreach(var user in users)
                //{
                //    ViewBag.Users.Add(user.SamAccountName + " - " + user.LastLogon.Value.ToString("yyyy-MM-dd hh:mm:ss"));
                //}

                GroupPrincipal group = GroupPrincipal.FindByIdentity(principalContext, "GitUsers");
                PrincipalSearchResult<Principal> groupusers = group.GetMembers();

                IEnumerable<UserPrincipal> groupusersagain = groupusers.Select(g => g as UserPrincipal);

                foreach(var user in groupusersagain)
                { 
                    ViewBag.GroupUsers.Add(user.SamAccountName + " - " + findlastlogon(user.SamAccountName).ToString("yyyy-MM-dd hh:mm:ss"));
                }
            }


            return View(); ;
        }

        // Taken from http://stackoverflow.com/questions/19454162/getting-last-logon-time-on-computers-in-active-directory
        public DateTime findlastlogon(string userName)

        {
            DirectoryContext context = new DirectoryContext(DirectoryContextType.Domain, "rezare.co.nz");
            DateTime latestLogon = DateTime.MinValue;
            DomainControllerCollection dcc = DomainController.FindAll(context);
            Parallel.ForEach(dcc.Cast<object>(), dc1 =>
            {


                DirectorySearcher ds;
                DomainController dc = (DomainController)dc1;
                using (ds = dc.GetDirectorySearcher())
                {
                    try
                    {
                        ds.Filter = String.Format(
                          "(sAMAccountName={0})",
                          userName
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
                                //servername = dc1.Name;
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            });
            return latestLogon;
        }
    }
}