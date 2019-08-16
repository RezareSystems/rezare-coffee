using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using ProjectCoffeAPI.Models;
using ProjectCoffeeLambdas.Models;

namespace ProjectCoffeeLambdas
{
    public class API
    {   
        private DynamoDBContext _context;

        public async Task<List<User>> GetAllUsers(object input, ILambdaContext context)
        { 
            context.Logger.Log("GET ALL USERs: " + input );

            InitializeDB();
            
            var conditions = new List<ScanCondition>();
            // you can add scan conditions, or leave empty
            return await _context.ScanAsync<User>(conditions).GetRemainingAsync();
        }

        public async Task<User> GetUser(MyIds user, ILambdaContext context)
        {
            context.Logger.Log("GET USER: " + user.id);
            InitializeDB();
            
            return await _context.LoadAsync<User>(user.id);
        }

        public async Task<bool> DeleteUser(object key, ILambdaContext context)
        {
            context.Logger.Log("DELETE USER: " + key);
            InitializeDB();

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

        public async Task<bool> AddUser(User addUser, ILambdaContext context)
        {
            context.Logger.Log("ADD USER: " + addUser);
            InitializeDB();

            User user = await _context.LoadAsync<User>(addUser.UserId);

            if (user != null)
            {
                return false;
            }

            await _context.SaveAsync(addUser);

            return true;
        }
        

        public async Task<bool> UpdateUser(User existingUser, ILambdaContext context)
        {
            context.Logger.Log("UPDATE USER: " + existingUser);
            InitializeDB();

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

        private void InitializeDB()
        {
            //var credentials = new Amazon.Runtime.StoredProfileAWSCredentials("coffeesite");
            //AmazonDynamoDBClient client = new AmazonDynamoDBClient(credentials, RegionEndpoint.APSoutheast2);
            AmazonDynamoDBClient client = new AmazonDynamoDBClient();

            this._context = new DynamoDBContext(client, new DynamoDBContextConfig { ConsistentRead = true, SkipVersionCheck = true });
        }
    }
}
