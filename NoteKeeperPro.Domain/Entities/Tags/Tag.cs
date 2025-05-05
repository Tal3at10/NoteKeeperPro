using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeperPro.Domain.Entities.Tags
{
    public class Tag
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
