using System.Collections.Generic;
using ECI.Test.Shared.Models;

namespace ECI.Test.DA.Repositories.Interfaces
{
    public interface IWalkRepository
    {
        IEnumerable<Walk> GetAll();
        Walk GetById(int id);
        void Add(Walk walk);
        void Update(Walk walk);
        void Delete(int id);
        IEnumerable<Walk> GetByClientIdDogId(int clientId, int dogId);
    }
}
