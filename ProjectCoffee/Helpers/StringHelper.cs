using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectCoffee.Helpers
{
    public static class StringHelper
    {
        /// <summary>
        /// Gets a string representing the coffee options
        /// </summary>
        /// <param name="options">The options to parse into a string</param>
        /// <returns>A comma separated string of the options for the coffee (human readable)</returns>
        public static string GetCoffeeOptions(List<KeyValuePair<string, KeyValuePair<string,string>>> options)
        {
            var listOfString = new List<string>();
            foreach (var cf in options)
            {
                switch (cf.Value.Key)
                {
                    case "int":
                        var intValue = Convert.ToInt32(cf.Value.Value);
                        if (intValue > 0)
                        {
                            listOfString.Add($"{cf.Key} - {intValue}");
                        }
                        break;

                    case "bool":
                        var boolValue = Convert.ToBoolean(cf.Value.Value);
                        if (boolValue)
                        {
                            listOfString.Add($"{cf.Key}");
                        }
                        break;
                }
            }

            return string.Join(", ", listOfString);
        }
    }
}