namespace ProjectCoffee.Migrations
{
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    public partial class AddedBooleanOptionsToCoffeeOptions : DbMigration
    {
        public override void Up()
        {
            // Change JSON for coffee options from KeyValuePair<string, int> to KeyValuePair<string, KeyValuePair<string, string>>
            using(var db = new CoffeeContext())
            {
                // Get all the users
                foreach(var user in db.Users)
                {
                    // Transform JSON
                    List<KeyValuePair<string, int>> old = JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(user.CoffeeOptionsJson);
                    List<KeyValuePair<string, KeyValuePair<string,string>>> newOptions = new List<KeyValuePair<string, KeyValuePair<string, string>>>();

                    foreach(KeyValuePair<string, int> kvp in old)
                    {
                        newOptions.Add(
                            new KeyValuePair<string, KeyValuePair<string, string>>(
                                kvp.Key,
                                new KeyValuePair<string, string>(
                                    "int",
                                    kvp.Value.ToString())
                                )
                        );
                    }

                    user.CoffeeOptionsJson = JsonConvert.SerializeObject(newOptions);
                }

                db.SaveChanges();
            }
        }
        
        public override void Down()
        {
            // Change JSON for coffee options from KeyValuePair<string, KeyValuePair<string, string>> to KeyValuePair<string, int
            // THIS RESULTS IN DATA LOSS (of non-integer values)
            using (var db = new CoffeeContext())
            {
                // Get all the users
                foreach (var user in db.Users)
                {
                    // Transform JSON
                    List<KeyValuePair<string, KeyValuePair<string, string>>> old = JsonConvert.DeserializeObject<List<KeyValuePair<string, KeyValuePair<string, string>>>>(user.CoffeeOptionsJson);
                    List<KeyValuePair<string, int>> newOptions = new List<KeyValuePair<string, int>>();

                    foreach (KeyValuePair<string, KeyValuePair<string, string>> kvp in old)
                    {
                        if (kvp.Value.Key == "int")
                        {
                            newOptions.Add(
                                new KeyValuePair<string, int>(
                                    kvp.Key,
                                    Int32.Parse(kvp.Value.Value)
                                )
                            );
                        }
                    }

                    user.CoffeeOptionsJson = JsonConvert.SerializeObject(newOptions);
                }

                db.SaveChanges();
            }
        }
    }
}
