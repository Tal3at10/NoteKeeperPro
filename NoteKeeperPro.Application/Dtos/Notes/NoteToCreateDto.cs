using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Application.Dtos.ApplicationsUsers;

namespace NoteKeeperPro.Application.Dtos.Notes
{
    public class NoteToCreateDto
    {
        public required string Title { get; set; }
        public required string Content { get; set; }

        // Owner of the note
        public ApplicationUserDto Owner { get; set; }
    }
}
