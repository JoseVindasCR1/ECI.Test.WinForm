using System.Collections.Generic;
using ECI.Test.Shared.Models;

namespace ECI.Test.BL.Services.Interfaces
{
    public interface IDogService
    {
        IEnumerable<Dog> GetByClientId(int clientId);
        void Add(Dog dog);
        void Update(Dog dog);
        void Delete(int id);
        void AssignToClient(int clientId, int dogId);
    }
}
