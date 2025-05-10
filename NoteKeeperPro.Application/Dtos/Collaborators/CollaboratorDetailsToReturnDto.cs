using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Domain.Entities.ApplicationUsers;
using NoteKeeperPro.Domain.Entities.Collaborators;
using NoteKeeperPro.Domain.Entities.Notes;

namespace NoteKeeperPro.Application.Dtos.Collaborators
{
    public class CollaboratorDetailsToReturnDto
    {
        public int Id { get; set; }
        public int NoteId { get; set; }

        public string UserName { get; set; }

       public Note Note { get; set; } = null!;

        // FK to the collaborating user
        public string UserId { get; set; } = null!;

        // Navigation to the collaborating user
       public ApplicationUser User { get; set; } = null!;

        // Access level of the collaborator (Read, Write, etc.)
        public PermissionType PermissionType { get; set; }

        // Soft delete flag for collaboration (e.g., revoked access)
        public bool IsDeleted { get; set; } = false;
    }
}
