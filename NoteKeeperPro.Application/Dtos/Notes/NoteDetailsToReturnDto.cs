using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Application.Dtos.ApplicationsUsers;
using NoteKeeperPro.Application.Dtos.Collaborators;
using NoteKeeperPro.Application.Dtos.NotesInfo;
using NoteKeeperPro.Application.Dtos.Tags;

namespace NoteKeeperPro.Application.Dtos.Notes
{
    public class NoteDetailsToReturnDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

        // Owner of the note
        public ApplicationUserDto Owner { get; set; }

        // Metadata info related to the note
        public NoteInfoDetailsToReturnDto NoteInfo { get; set; }

        // List of collaborators on the note
        public ICollection<CollaboratorDetailsToReturnDto> Collaborators { get; set; }

        // List of tags associated with the note
        public ICollection<TagDetailsToReturnDto> Tags { get; set; }
    }
}
