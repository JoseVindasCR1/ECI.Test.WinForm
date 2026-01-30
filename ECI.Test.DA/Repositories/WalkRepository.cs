using System.Collections.Generic;
using System.Linq;
using ECI.Test.DA.Repositories.Interfaces;
using ECI.Test.Shared.Models;

namespace ECI.Test.DA.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly TestDbContext _context;

        public WalkRepository(TestDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Walk> GetAll()
        {
            return _context.Walks.ToList();
        }

        public Walk GetById(int id)
        {
            return _context.Walks.Find(id);
        }

        public void Add(Walk walk)
        {
            _context.Walks.Add(walk);
            _context.SaveChanges();
        }

        public void Update(Walk walk)
        {
            // Check if the entity is already being tracked
            var existingEntry = _context.Entry(walk);
            
            if (existingEntry.State == System.Data.Entity.EntityState.Detached)
            {
                // Find if there's already a tracked entity with the same ID
                var trackedEntity = _context.Walks.Local.FirstOrDefault(w => w.Id == walk.Id);
                
                if (trackedEntity != null)
                {
                    // Update the tracked entity's properties
                    _context.Entry(trackedEntity).CurrentValues.SetValues(walk);
                }
                else
                {
                    // Attach the entity and mark it as modified
                    _context.Walks.Attach(walk);
                    _context.Entry(walk).State = System.Data.Entity.EntityState.Modified;
                }
            }
            else
            {
                // Entity is already tracked, just mark it as modified
                existingEntry.State = System.Data.Entity.EntityState.Modified;
            }
            
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var walk = _context.Walks.Find(id);
            if (walk != null)
            {
                _context.Walks.Remove(walk);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Walk> GetByClientIdDogId(int clientId, int dogId)
        {
            return _context.Walks.Where(w => w.ClientId == clientId && w.DogId == dogId).ToList();
        }
    }
}
