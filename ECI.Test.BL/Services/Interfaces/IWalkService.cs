using System.Collections.Generic;
using ECI.Test.Shared.Models;

namespace ECI.Test.BL.Services.Interfaces
{
    public interface IWalkService
    {
        void Add(Walk walk);
        void Update(Walk walk);
        void Delete(int id);
        IEnumerable<Walk> GetByClientIdDogId(int clientId, int dogId);
    }
}
