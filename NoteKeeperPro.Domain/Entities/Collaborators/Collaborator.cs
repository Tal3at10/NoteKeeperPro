using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Domain.Entities.Users;

namespace NoteKeeperPro.Domain.Entities.Collaborators
{
    public enum PermissionType
    {
        Edit = 1,
        View = 2,
    }
    public class Collaborator : User
    {
      public PermissionType PermissionType { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
