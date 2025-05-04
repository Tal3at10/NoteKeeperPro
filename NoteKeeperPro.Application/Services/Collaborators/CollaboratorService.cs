using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Application.Dtos.Collaborators;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Collaborators;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Users;

namespace NoteKeeperPro.Application.Services.Collaborators
{
    internal class CollaboratorService : ICollaboratorService
    {
        private readonly ICollaboratorRepository _collaboratorRepository;

        public CollaboratorService(ICollaboratorRepository collaboratorRepository)
        {
            _collaboratorRepository = collaboratorRepository;
        }

        public int CreateCollaborator(CollaboratorToCreateDto collaborator)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCollaborator(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CollaboratorToReturnDto> GetAllCollaborators()
        {
            throw new NotImplementedException();
        }

        public CollaboratorDetailsToReturnDto? GetCollaboratorById(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateCollaborator(CollaboratorToUpdateDto collaborator)
        {
            throw new NotImplementedException();
        }
    }
}
