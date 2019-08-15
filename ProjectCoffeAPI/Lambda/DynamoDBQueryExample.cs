using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace ProjectCoffeAPI.Functions
{
    public class DynamoDBQueryExample
    {
        private readonly IDynamoDBContext _dbContext;

        public DynamoDBQueryExample()
        {
        }

        public DynamoDBQueryExample(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
            //var context = new DynamoDBContext(client);
        }

        public void InsertUserData()
        {
            Table table = InitializeClientAndTable();

            var user = new Document();
            user["UserId"] = "bloemj";
            user["Name"] = "Janine";
            user["Drink"] = "Coffee, no sugar";
            table.PutItemAsync(user);

            user["UserId"] = "danielB";
            user["Name"] = "Daniel";
            user["Drink"] = "Coffee, no milk";
            table.PutItemAsync(user);
        }

        public async Task<User> GetUser()
        {
            Table table = InitializeClientAndTable();
            GetItemOperationConfig config = new GetItemOperationConfig
            {
                AttributesToGet = new List<string> { "UserId", "Name", "Drink" },
                ConsistentRead = true
            };

            var item = await table.GetItemAsync("bloemj", config);
            var jsonItem = item.ToJson();

            return new User();

        }

        private Table InitializeClientAndTable()
        {
            var credentials = new Amazon.Runtime.StoredProfileAWSCredentials("coffeesite");

            var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.APSoutheast2);

            Table table = Table.LoadTable(client, "Users");

            return table;
        }
    }

    [Serializable]
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Drink { get; set; }
    }
}
