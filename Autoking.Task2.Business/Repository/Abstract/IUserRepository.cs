using Autoking.Task2.Core.Models;
using System.Collections.Generic;

namespace JwtBasicTwo.Core.Repositories
{
    public interface IUserRepository 
    {
        User Login(string userName, string password);
        User RefleshTokenGet(string refleshToken);
        string InsertUser(string name, string lastName, string password, string userName);
        User UpdateUser(int id, string firstName, string lastName);
        string DeleteUser(string userName);
        IEnumerable<User> GetAll();
    }
}
