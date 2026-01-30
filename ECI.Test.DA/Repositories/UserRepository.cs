using System.Collections.Generic;
using System.Linq;
using ECI.Test.DA.Repositories.Interfaces;
using ECI.Test.Shared.Models;

namespace ECI.Test.DA.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TestDbContext _context;

        public UserRepository(TestDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public User GetByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            // Check if the entity is already being tracked
            var existingEntry = _context.Entry(user);
            
            if (existingEntry.State == System.Data.Entity.EntityState.Detached)
            {
                // Find if there's already a tracked entity with the same ID
                var trackedEntity = _context.Users.Local.FirstOrDefault(u => u.Id == user.Id);
                
                if (trackedEntity != null)
                {
                    // Update the tracked entity's properties
                    _context.Entry(trackedEntity).CurrentValues.SetValues(user);
                }
                else
                {
                    // Attach the entity and mark it as modified
                    _context.Users.Attach(user);
                    _context.Entry(user).State = System.Data.Entity.EntityState.Modified;
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
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public bool UserExists(string userName)
        {
            return _context.Users.Any(u => u.UserName == userName);
        }
    }
}