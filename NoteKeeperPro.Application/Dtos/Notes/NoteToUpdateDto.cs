using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Application.Dtos.Collaborators;
using NoteKeeperPro.Application.Dtos.Tags;

namespace NoteKeeperPro.Application.Dtos.Notes
{
    public class NoteToUpdateDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }

        // OwnerId of the note
        public string OwnerId { get; set; }

        // List of collaborators on the note
        public ICollection<CollaboratorDetailsToReturnDto> Collaborators { get; set; }

        // List of tags associated with the note
        public ICollection<TagDetailsToReturnDto> Tags { get; set; }


    }
}
