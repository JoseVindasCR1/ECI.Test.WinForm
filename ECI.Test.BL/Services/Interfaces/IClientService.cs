using System.Collections.Generic;
using ECI.Test.Shared.Models;

namespace ECI.Test.BL.Services.Interfaces
{
    public interface IClientService
    {
        IEnumerable<Client> GetAll();
        void Add(Client client);
        void Update(Client client);
        void Delete(int id);
    }
}
