using System.Collections.Generic;
using System.Linq;
using ECI.Test.DA.Repositories.Interfaces;
using ECI.Test.Shared.Models;

namespace ECI.Test.DA.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly TestDbContext _context;

        public DogRepository(TestDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Dog> GetAll()
        {
            return _context.Dogs.ToList();
        }

        public Dog GetById(int id)
        {
            return _context.Dogs.Find(id);
        }

        public void Add(Dog dog)
        {
            _context.Dogs.Add(dog);
            _context.SaveChanges();
        }

        public void Update(Dog dog)
        {
            var existing = _context.Dogs.Find(dog.Id);
            if (existing != null)
            {
                existing.Name = dog.Name;
                existing.Breed = dog.Breed;
                existing.Age = dog.Age;
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var dog = _context.Dogs.Find(id);
            if (dog != null)
            {
                var clientDogs = _context.ClientDogs.Where(cd => cd.DogId == id).ToList();
                foreach (var cd in clientDogs)
                {
                    _context.ClientDogs.Remove(cd);
                }

                var walks = _context.Walks.Where(w => w.DogId == id).ToList();
                foreach (var walk in walks)
                {
                    _context.Walks.Remove(walk);
                }

                _context.Dogs.Remove(dog);
                _context.SaveChanges();
            }
        }
    }
}
