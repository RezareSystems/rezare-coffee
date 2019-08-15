using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
//using ProjectCoffeAPI.Services;

namespace ProjectCoffeeLambdas
{
    public class API
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        //public async Task<List<User>> GetAllUsers(object input, ILambdaContext context)
        //{
        //    //create coffee service
        //    CoffeeService _coffeeService = new CoffeeService();
        //    List<User> allUsers = await _coffeeService.GetAllUsersAsync();

        //    return allUsers;
        //}

        public string GetAllUsers(object input, ILambdaContext context)
        { 
            return "WHAT!!!!";
        }
    }
}
