using System.Collections.Generic;
using ProjectCoffee.Models;
using ProjectCoffee.Models.DatabaseModels;

namespace ProjectCoffee.Services
{
    public interface IDatabaseService
    {
        User GetUser(string guid);
        IEnumerable<User> GetAllUsers();
        IEnumerable<DrinkType> GetAllDrinkTypes();
        DrinkType GetDrinkType(int id);
        void UpdateUser(User user);
    }
}