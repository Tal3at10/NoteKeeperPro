using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteKeeperPro.Domain.Entities.Collaborators;
using NoteKeeperPro.Infrastructure.Presistance.Data;

namespace NoteKeeperPro.Infrastructure.Presistance.Repositories.Collaborators
{
    public class CollaboratorRepository : ICollaboratorRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CollaboratorRepository(ApplicationDbContext dbContext) // Ask Clr To Create Instnce (Dependeny Injection)
        {
            _dbContext = dbContext;
        }

        public int AddCollaborator(Collaborator collaborator)
        {
            _dbContext.Collaborators.Add(collaborator); // Saved Locally
            return _dbContext.SaveChanges(); // Apply Remotly
        }

        public int DeleteCollaborator(Collaborator collaborator)
        {
            _dbContext.Collaborators.Remove(collaborator);
            return _dbContext.SaveChanges(); // Apply Remotly
        }

        public IEnumerable<Collaborator> GetAll(bool AsNoTracking = true)
        {
            if (AsNoTracking)
            {
                // Detached
                return _dbContext.Collaborators.AsNoTracking().ToList();
            }

            // Unchanged
            return _dbContext.Collaborators.ToList();
        }

        public IQueryable<Collaborator> GetAllQuarable(bool AsNoTracking = true)
        {
            return _dbContext.Collaborators;
        }

        public Collaborator? GetById(int id)
        {
            return _dbContext.Collaborators.Find(id); // Search Localy , If Found Rteurn True , else send => Request Database 
        }

        public int UpdateCollaborator(Collaborator collaborator)
        {
            _dbContext.Collaborators.Update(collaborator); // Saved Locally
            return _dbContext.SaveChanges(); // Apply Remotly
        }
    }
}
