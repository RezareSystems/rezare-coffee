using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using ProjectCoffeAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCoffeAPI.Services
{
    public class CoffeeService : ICoffeeService
    {
        private readonly DynamoDBContext _context;

        public CoffeeService()
        {
            var credentials = new Amazon.Runtime.StoredProfileAWSCredentials("coffeesite");
            AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, RegionEndpoint.APSoutheast2);

            _context = new DynamoDBContext(client, new DynamoDBContextConfig { ConsistentRead = true, SkipVersionCheck = true });
        }

        public async Task<bool> AddUser(User addUser)
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

        public async Task<bool> UpdateUser(User existingUser)
        {
            //get user from key
            User updatedUser = await _context.LoadAsync<User>(existingUser.UserId);

            if (updatedUser != null)
            {
                updatedUser.DrinkCode = existingUser.DrinkCode;
                updatedUser.CupSizeCode = existingUser.CupSizeCode;
                updatedUser.Extras = existingUser.Extras;
                updatedUser.MilkTypeCode = existingUser.MilkTypeCode;

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
    }
}
