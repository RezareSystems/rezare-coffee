using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace ProjectCoffeAPI.Functions
{
    public class DynamoDBQueryExample
    {
        private readonly DynamoDBContext _context;

        public DynamoDBQueryExample()
        {
            var credentials = new Amazon.Runtime.StoredProfileAWSCredentials("coffeesite");
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, RegionEndpoint.APSoutheast2);

            _context = new DynamoDBContext(client, new DynamoDBContextConfig { ConsistentRead = true, SkipVersionCheck = true });
        }

        public void InsertTestUser()
        {
            User newUser = new User
            {
                UserId = "test1",
                Drink = "Latte",
                Name = "Test"
            };
            _context.SaveAsync(newUser);
        }

        public async Task<User> GetUser()
        {
            return await _context.LoadAsync<User>("vivekS");
        }

        //public async Task<List<User>> GetAllUsersAsync()
        //{

        //}

        public async Task<bool> UpdateUser(string key) 
        {
            //get user from key
            User updatedUser = await _context.LoadAsync<User>(key);

            if (updatedUser != null)
            {
                updatedUser.Drink = "Updated Drink";
                updatedUser.Name = "Updated Name";

                await _context.SaveAsync(updatedUser);

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteUser(string key)
        {
            //get user from key
            User deleteUser = await _context.LoadAsync<User>(key, new DynamoDBContextConfig
            {
                ConsistentRead = true
            });

            if (deleteUser != null)
            {
                await _context.DeleteAsync<User>(deleteUser);

                return true;
            }

            return false;
        }
    }

    [DynamoDBTable("Users")]
    public class User
    {
        [DynamoDBHashKey]
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Drink { get; set; }
    }
}
