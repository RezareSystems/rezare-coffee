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

        public User GetTestUser()
        {
            User newUser = new User
            {
                UserId = "janineB",
                UserName = "Janine",
                DrinkCode = "HC",
                CupSizeCode = "R",
                MilkTypeCode = "Trim",
                Extras = new List<DrinkExtra>()
                 {new DrinkExtra { ExtraCode = "CA", ExtraCount = 2 } }
            };

            return newUser;
        }

        public List<DrinkExtra> Extras { get; set; }

        public async Task<bool> InsertUser(User addUser)
        {
            User user = await _context.LoadAsync<User>(addUser.UserId);

            if (user != null)
            {
                return false;
            }

            await _context.SaveAsync(addUser);

            return true;
        }

        public async Task<User> GetUser(string key)
        {
            return await _context.LoadAsync<User>(key);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var conditions = new List<ScanCondition>();
            // you can add scan conditions, or leave empty
            return await _context.ScanAsync<User>(conditions).GetRemainingAsync();
        }

        public async Task<bool> UpdateUser(string key)
        {
            //get user from key
            User updatedUser = await _context.LoadAsync<User>(key);

            if (updatedUser != null)
            {
                updatedUser.DrinkCode = "Updated Drink";
                updatedUser.UserName = "Updated Name";

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
        [DynamoDBProperty]
        public string UserName { get; set; }
        [DynamoDBProperty]
        public string DrinkCode { get; set; }
        [DynamoDBProperty]
        public string CupSizeCode { get; set; }

        [DynamoDBProperty]
        public string MilkTypeCode { get; set; }

        [DynamoDBProperty]
        public List<DrinkExtra> Extras { get; set; }
    }

    public class DrinkExtra
    {
        public string ExtraCode { get; set; }
        public int ExtraCount { get; set; }
    }
}
