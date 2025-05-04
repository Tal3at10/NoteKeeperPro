using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NoteKeeperPro.Domain.Entities.Tags;
using NoteKeeperPro.Infrastructure.Presistance.Data;

namespace NoteKeeperPro.Infrastructure.Presistance.Repositories.Tags
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TagRepository(ApplicationDbContext dbContext) // Ask CLR to create instance (Dependency Injection)
        {
            _dbContext = dbContext;
        }

        public int AddTag(Tag tag)
        {
            _dbContext.Tags.Add(tag); // Saved locally
            return _dbContext.SaveChanges(); // Apply remotely
        }

        public int DeleteTag(Tag tag)
        {
            _dbContext.Tags.Remove(tag);
            return _dbContext.SaveChanges(); // Apply remotely
        }

        public IEnumerable<Tag> GetAll(bool asNoTracking = true)
        {
            if (asNoTracking)
            {
                return _dbContext.Tags.AsNoTracking().ToList(); // Detached
            }

            return _dbContext.Tags.ToList(); // Unchanged
        }

        public Tag GetById(int id)
        {
            return _dbContext.Tags.Find(id); // Search locally first, then fallback to DB
        }

        public int UpdateTag(Tag tag)
        {
            _dbContext.Tags.Update(tag); // Saved locally
            return _dbContext.SaveChanges(); // Apply remotely
        }
    }
}
