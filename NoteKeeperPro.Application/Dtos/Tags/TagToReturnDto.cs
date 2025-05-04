using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeperPro.Application.Dtos.Tags
{
    public class TagToReturnDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
