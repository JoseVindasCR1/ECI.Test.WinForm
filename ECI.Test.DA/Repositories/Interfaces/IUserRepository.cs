using System.Collections.Generic;
using ECI.Test.Shared.Models;

namespace ECI.Test.DA.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User GetByUserName(string userName);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
        bool UserExists(string userName);
    }
}