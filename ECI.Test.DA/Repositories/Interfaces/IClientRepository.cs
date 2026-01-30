using System.Collections.Generic;
using ECI.Test.Shared.Models;

namespace ECI.Test.DA.Repositories.Interfaces
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAll();
        Client GetById(int id);
        void Add(Client client);
        void Update(Client client);
        void Delete(int id);
    }
}
