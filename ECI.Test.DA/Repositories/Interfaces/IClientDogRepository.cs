using System.Collections.Generic;
using ECI.Test.Shared.Models;

namespace ECI.Test.DA.Repositories.Interfaces
{
    public interface IClientDogRepository
    {
        void AssignDogToClient(int clientId, int dogId);
        IEnumerable<Dog> GetDogsByClientId(int clientId);
    }
}
