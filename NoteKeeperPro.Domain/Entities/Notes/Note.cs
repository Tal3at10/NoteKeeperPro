using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Domain.Entities.ApplicationUsers;
using NoteKeeperPro.Domain.Entities.Collaborators;
using NoteKeeperPro.Domain.Entities.NotesInfo;
using NoteKeeperPro.Domain.Entities.Tags;
using NoteKeeperPro.Infrastructure.Identity;
namespace NoteKeeperPro.Domain.Entities.Notes
{
    public class Note
    {
        public int Id { get; set; }

        public required string Title { get; set; }
        public required string Content { get; set; }

        // Timestamp of note creation
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Timestamp of last update to the note
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Soft delete flag
        public bool IsDeleted { get; set; } = false;

        // FK to the user who owns this note
        public string OwnerId { get; set; } = null!;

        // Navigation to the owner user
        public ApplicationUser Owner { get; set; } = null!;

        // Navigation to metadata info (e.g. word count)
        public NoteInfo NoteInfo { get; set; } = null!;

        // Users collaborating on this note
        public ICollection<Collaborator> Collaborators { get; set; } = new List<Collaborator>();

        // Tags associated with the note
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }

}
