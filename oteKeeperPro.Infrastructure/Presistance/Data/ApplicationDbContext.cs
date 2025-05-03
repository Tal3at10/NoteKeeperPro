using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteKeeperPro.Domain.Entities.Collaborators;
using NoteKeeperPro.Domain.Entities.Notes;
using NoteKeeperPro.Domain.Entities.NotesInfo;
using NoteKeeperPro.Domain.Entities.Tags;
using NoteKeeperPro.Domain.Entities.Users;

namespace NoteKeeperPro.Infrastructure.Presistance.Data
{
    // Repository => ApplicationDbContext (DataBase) 

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) // Doesnt Suitable For new Way of Dependency Injection
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=NoteKeeperPro;trusted_connection=true;trustServerCertificate=true;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Apply All Configurations Classes
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteInfo> NoteInfos { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}
