using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoteKeeperPro.Domain.Entities.ApplicationUsers;
using NoteKeeperPro.Domain.Entities.Collaborators;
using NoteKeeperPro.Domain.Entities.Notes;
using NoteKeeperPro.Domain.Entities.NotesInfo;
using NoteKeeperPro.Domain.Entities.Tags;
using NoteKeeperPro.Infrastructure.Identity;

namespace NoteKeeperPro.Infrastructure.Presistance.Data
{
    // Repository => ApplicationDbContext (DataBase) 
    // ApplicationDbContext : IdentityUser

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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
            base.OnModelCreating (modelBuilder); 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // Apply All Configurations Classes
        }

        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<NoteInfo> NoteInfos { get; set; }
        public DbSet<Tag> Tags { get; set; }

    }
}
