using Microsoft.AspNetCore.Mvc;
using ProjectCoffeAPI.Models;
using ProjectCoffeAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectCoffeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeController : ControllerBase
    {
        private readonly ICoffeeService _coffeService;
        
        public CoffeeController(ICoffeeService coffeService)
        {
            _coffeService = coffeService;
        }

        // GET: api/Coffee/GetUser/janineB
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            User existingUser = await _coffeService.GetUser(id);
           
            return Ok(existingUser);
        }

        // PUT: api/Coffee/updateUser/janineB
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User existingUser)
        {
            existingUser.UserId = id;
            var updateResult = await _coffeService.UpdateUser(existingUser);
             
            return Ok(updateResult);
        }

        // DELETE: api/coffee/DeleteUser/janineB
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var updateResult = await _coffeService.DeleteUser(id);

            return Ok(updateResult);
        }

        // POST: api/Coffee/AddUser
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User newUser)
        {
            var addResult = await _coffeService.AddUser(newUser);
            return Ok(addResult);
        }

        // GET: api/Coffee/GetUsers
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            List<User> allUsers = await _coffeService.GetAllUsersAsync();
            return Ok(allUsers);
        }
        
    }
}
