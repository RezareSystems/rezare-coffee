using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectCoffeAPI.Models;

namespace ProjectCoffeAPI.Services
{
    public class CoffeService : ICoffeService
    {
        private Dictionary<string, string> _CoffeListDictrionary = new Dictionary<string, string>();

        public void AddUserToUserList(UserModel user)
        {
            _CoffeListDictrionary.Add(user.Name, user.CoffeType);
        }

        public Dictionary<string, string> GetUserList()
        {
            return _CoffeListDictrionary;
        }
    }
}
