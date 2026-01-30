using System.Collections.Generic;
using ECI.Test.Shared.Models;

namespace ECI.Test.DA.Repositories.Interfaces
{
    public interface IDogRepository
    {
        IEnumerable<Dog> GetAll();
        Dog GetById(int id);
        void Add(Dog dog);
        void Update(Dog dog);
        void Delete(int id);
    }
}
