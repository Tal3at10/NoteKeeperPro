using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteKeeperPro.Domain.Entities.Users;
using NoteKeeperPro.Infrastructure.Presistance.Data;

namespace NoteKeeperPro.Infrastructure.Presistance.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        // DataBase ==> Repositories ==> Services ==> Controllers

        private readonly ApplicationDbContext _dbContext;
        public UserRepository(ApplicationDbContext dbContext) // Ask Clr To Create Instnce (Dependeny Injection)
        {
            _dbContext = dbContext;
        }
        public int AddUser(User user)
        {
            _dbContext.Users.Add(user); // Saved Locally
            return _dbContext.SaveChanges(); // Apply Remotly
        }

        public int DeleteUser(User user)
        {
            _dbContext.Users.Remove(user);
            return _dbContext.SaveChanges(); // Apply Remotly
        }

        public IEnumerable<User> GetAll(bool asNoTracking = true)
        {
            if (asNoTracking)
            {
                // Detached
                return _dbContext.Users.AsNoTracking().ToList();
            }

            // Unchanged
            return _dbContext.Users.ToList();
        }

        public IQueryable<User> GetAllQuarable(bool AsNoTracking = true)
        {
            return _dbContext.Users;
        }

        public User? GetById(int id)
        {
            return _dbContext.Users.Find(id); // Search Localy , If Found Rteurn True , else send => Request Database 
        }

        public int UpdateUser(User user)
        {
            _dbContext.Users.Update(user); // Saved Locally
            return _dbContext.SaveChanges(); // Apply Remotly
        }
    }
}
