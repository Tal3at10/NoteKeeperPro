using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteKeeperPro.Domain.Entities.Notes;
using NoteKeeperPro.Infrastructure.Presistance.Data;

namespace NoteKeeperPro.Infrastructure.Presistance.Repositories.Notes
{
    public class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public NoteRepository(ApplicationDbContext dbContext) // Ask Clr To Create Instnce (Dependeny Injection)
        {
            _dbContext = dbContext;
        }

        public int AddNote(Note note)
        {
            _dbContext.Notes.Add(note); // Saved Locally
            return _dbContext.SaveChanges(); // Apply Remotly
        }

        public int DeleteNote(Note note)
        {
            _dbContext.Notes.Remove(note);
            return _dbContext.SaveChanges(); // Apply Remotly
        }

        public IEnumerable<Note> GetAll(bool AsNoTracking = true)
        {
            if (AsNoTracking)
            {
                // Detached
                return _dbContext.Notes.AsNoTracking().ToList();
            }

            // Unchanged
            return _dbContext.Notes.ToList();
        }

        public Note? GetById(int id)
        {
            return _dbContext.Notes.Find(id); // Search Localy , If Found Rteurn True , else send => Request Database 
        }

        public int UpdateNote(Note note)
        {
            _dbContext.Notes.Update(note); // Saved Locally
            return _dbContext.SaveChanges(); // Apply Remotly
        }
    }
}
