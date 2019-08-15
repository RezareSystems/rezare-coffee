using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectCoffeAPI.Models;

namespace ProjectCoffeAPI.Services
{
    public interface ICoffeeService
    {
        Task<bool> AddUser(User addUser);

        Task<User> GetUser(string key);

        Task<bool> UpdateUser(User existingUser);

        Task<bool> DeleteUser(string key);

        Task<List<User>> GetAllUsersAsync();
    }
}