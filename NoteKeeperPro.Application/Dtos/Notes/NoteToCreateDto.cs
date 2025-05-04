using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeperPro.Application.Dtos.Notes
{
    public class NoteToCreateDto
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
    }
}
