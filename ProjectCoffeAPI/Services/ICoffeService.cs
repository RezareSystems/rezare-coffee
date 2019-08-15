using System.Collections.Generic;

namespace ProjectCoffeAPI.Services
{
    public interface ICoffeService
    {
        Dictionary<string, string> GetUserList();
        void AddUserToUserList(Models.UserModel user);
    }
}