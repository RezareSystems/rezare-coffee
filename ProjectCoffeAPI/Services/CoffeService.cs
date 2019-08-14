using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCoffeAPI.Services
{
    public class CoffeService : ICoffeService
    {
        private Dictionary<string, string> _CoffeListDictrionary = new Dictionary<string, string>();
        public Dictionary<string, string> GetUserList()
        {
            return _CoffeListDictrionary;
        }
    }
}
