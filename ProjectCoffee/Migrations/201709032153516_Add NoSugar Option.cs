namespace ProjectCoffee.Migrations
{
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class AddNoSugarOption : DbMigration
    {
        public override void Up()
        {
            //Add in no sugar option to list
            using (var db = new CoffeeContext())
            {
                // Get all the users
                foreach (var user in db.Users)
                {
                    // Transform JSON
                    List<KeyValuePair<string, KeyValuePair<string, string>>> options = JsonConvert.DeserializeObject<List<KeyValuePair<string, KeyValuePair<string, string>>>>(user.CoffeeOptionsJson);

                    // Add in No Sugar option
                    if (!options.Any(o => o.Key == "No Sugar")){
                        options.Add(new KeyValuePair<string, KeyValuePair<string, string>>(
                            key: "No Sugar",
                            value: new KeyValuePair<string, string>(
                                key: "bool",
                                value: "false"
                            )
                        ));
                    }

                    user.CoffeeOptionsJson = JsonConvert.SerializeObject(options);
                }

                db.SaveChanges();
            }
        }

        public override void Down()
        {
            //Removes no sugar option from list
            using (var db = new CoffeeContext())
            {
                // Get all the users
                foreach (var user in db.Users)
                {
                    // Transform JSON
                    List<KeyValuePair<string, KeyValuePair<string, string>>> options = JsonConvert.DeserializeObject<List<KeyValuePair<string, KeyValuePair<string, string>>>>(user.CoffeeOptionsJson);

                    // Add in No Sugar option
                    if (options.Any(o => o.Key == "No Sugar"))
                    {
                        options.Remove(options.First(o => o.Key == "No Sugar"));
                    }

                    user.CoffeeOptionsJson = JsonConvert.SerializeObject(options);
                }

                db.SaveChanges();
            }
        }
    }
}
