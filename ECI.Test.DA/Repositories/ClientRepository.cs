using System.Collections.Generic;
using System.Linq;
using ECI.Test.DA.Repositories.Interfaces;
using ECI.Test.Shared.Models;

namespace ECI.Test.DA.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly TestDbContext _context;

        public ClientRepository(TestDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Client> GetAll()
        {
            return _context.Clients.ToList();
        }

        public Client GetById(int id)
        {
            return _context.Clients.Find(id);
        }

        public void Add(Client client)
        {
            _context.Clients.Add(client);
            _context.SaveChanges();
        }

        public void Update(Client client)
        {
            var existing = _context.Clients.Find(client.Id);
            if (existing != null)
            {
                existing.Name = client.Name;
                existing.Phone = client.Phone;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var client = _context.Clients.Find(id);
            if (client != null)
            {
                var clientDogs = _context.ClientDogs.Where(cd => cd.ClientId == id).ToList();
                foreach (var cd in clientDogs)
                {
                    _context.ClientDogs.Remove(cd);
                }

                var walks = _context.Walks.Where(w => w.ClientId == id).ToList();
                foreach (var walk in walks)
                {
                    _context.Walks.Remove(walk);
                }

                _context.Clients.Remove(client);
                _context.SaveChanges();
            }
        }
    }
}
