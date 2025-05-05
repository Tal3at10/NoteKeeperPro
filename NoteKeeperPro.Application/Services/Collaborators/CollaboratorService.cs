using System;
using System.Collections.Generic;
using System.Linq;
using NoteKeeperPro.Application.Dtos.Collaborators;
using NoteKeeperPro.Domain.Entities.Collaborators;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Collaborators;

namespace NoteKeeperPro.Application.Services.Collaborators
{
    internal class CollaboratorService : ICollaboratorService
    {
        private readonly ICollaboratorRepository _collaboratorRepository;

        public CollaboratorService(ICollaboratorRepository collaboratorRepository)
        {
            _collaboratorRepository = collaboratorRepository;
        }

        public IEnumerable<CollaboratorToReturnDto> GetAllCollaborators()
        {
            var collaborators = _collaboratorRepository.GetAllQuarable()
                .Where(c => c.IsDeleted == false)
                .Select(c => new CollaboratorToReturnDto
                {
                    Id = c.Id,
                    Email = c.Email,
                    Password = c.Password,
                    CreatedAt = c.CreatedAt,
                    PhoneNumber = c.PhoneNumber,
                    UserName = c.UserName,
                    PermissionType = c.PermissionType,

                })
                .ToList();

            return collaborators;
        }

        public CollaboratorDetailsToReturnDto? GetCollaboratorById(int id)
        {
            var collaborator = _collaboratorRepository.GetById(id);

            if (collaborator != null)
            {
                return new CollaboratorDetailsToReturnDto
                {
                    Id = collaborator.Id,
                    Email = collaborator.Email,
                    Password = collaborator.Password,
                    CreatedAt = collaborator.CreatedAt,
                    PhoneNumber = collaborator.PhoneNumber,
                    UserName = collaborator.UserName,
                    PermissionType = collaborator.PermissionType,
                };
            }

            return null;
        }

        public int CreateCollaborator(CollaboratorToCreateDto collaborator)
        {
            var newCollaborator = new Collaborator
            {
                UserName = collaborator.UserName,
                Email = collaborator.Email,
                CreatedAt = DateTime.UtcNow,
                PermissionType = collaborator.PermissionType, 
                Password = collaborator.Password, 
                PhoneNumber = collaborator.Password,
            };

            return _collaboratorRepository.AddCollaborator(newCollaborator);
        }

        public int UpdateCollaborator(CollaboratorToUpdateDto collaborator)
        {
            var updatedCollaborator = new Collaborator
            {
                Id = collaborator.Id,
                UserName = collaborator.UserName,
                Email = collaborator.Email,
                CreatedAt = DateTime.UtcNow,
                PermissionType = collaborator.PermissionType,
                Password = collaborator.Password,
                PhoneNumber = collaborator.Password,
            };

            return _collaboratorRepository.UpdateCollaborator(updatedCollaborator);
        }

        public bool DeleteCollaborator(int id)
        {
            var collaborator = _collaboratorRepository.GetById(id);

            if (collaborator != null)
            {
                collaborator.IsDeleted = true; // Soft   deleted
                return _collaboratorRepository.UpdateCollaborator(collaborator) > 0;
            }

            return false;
        }
    }
}
