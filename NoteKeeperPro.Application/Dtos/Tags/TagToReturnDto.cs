using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Domain.Entities.Notes;

namespace NoteKeeperPro.Application.Dtos.Tags
{
    public class TagToReturnDto
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        // Soft delete flag
        public bool IsDeleted { get; set; } = false;

        // Notes associated with this tag (many-to-many)
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
