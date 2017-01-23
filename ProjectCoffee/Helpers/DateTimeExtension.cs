using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCoffee.Helpers
{
    public static class DateTimeExtension
    {
        public static string GetReadable(this DateTime dt)
        {
                string suffix;

                if (new[] { 11, 12, 13 }.Contains(dt.Day))
                {
                    suffix = "th";
                }
                else if (dt.Day % 10 == 1)
                {
                    suffix = "st";
                }
                else if (dt.Day % 10 == 2)
                {
                    suffix = "nd";
                }
                else if (dt.Day % 10 == 3)
                {
                    suffix = "rd";
                }
                else
                {
                    suffix = "th";
                }

                return string.Format("{1}{2}", dt, dt.Day, suffix);
        }
    }    
}