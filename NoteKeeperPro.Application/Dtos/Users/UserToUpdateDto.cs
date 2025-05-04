using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeperPro.Application.Dtos.Users
{
    internal class UserToUpdateDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public required string Email { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
