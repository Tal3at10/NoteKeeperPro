using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeperPro.Domain.Entities.NotesInfo
{
    public class NoteInfo
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModifiedAt { get; set; }
        public int WordCount { get; set; }
        public int CharchterCount { get; set; }
    }
}
