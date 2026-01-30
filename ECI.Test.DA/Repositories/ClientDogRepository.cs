using System.Collections.Generic;
using System.Linq;
using ECI.Test.DA.Repositories.Interfaces;
using ECI.Test.Shared.Models;

namespace ECI.Test.DA.Repositories
{
    public class ClientDogRepository : IClientDogRepository
    {
        private readonly TestDbContext _context;

        public ClientDogRepository(TestDbContext context)
        {
            _context = context;
        }

        public void AssignDogToClient(int clientId, int dogId)
        {
            var existing = _context.ClientDogs.Find(clientId, dogId);
            if (existing == null)
            {
                _context.ClientDogs.Add(new ClientDog { ClientId = clientId, DogId = dogId });
                _context.SaveChanges();
            }
        }

        public IEnumerable<Dog> GetDogsByClientId(int clientId)
        {
            var dogIds = _context.ClientDogs
                .Where(cd => cd.ClientId == clientId)
                .Select(cd => cd.DogId)
                .ToList();

            return _context.Dogs.Where(d => dogIds.Contains(d.Id)).ToList();
        }
    }
}
