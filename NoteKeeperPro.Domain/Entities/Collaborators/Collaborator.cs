using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteKeeperPro.Domain.Entities.Collaborators
{
    public enum PermissionType
    {
        Edit = 1,
        View = 2,
    }
    public class Collaborator
    {
        public int Id { get; set; }
       
        public PermissionType PermissionType { get; set; }

    }
}
