using System;
using System.Collections.Generic;
using System.Linq;
using NoteKeeperPro.Application.Dtos.Notes;
using NoteKeeperPro.Domain.Entities.M_M_RelationShips;
using NoteKeeperPro.Domain.Entities.Notes;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Notes;

namespace NoteKeeperPro.Application.Services.Notes
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public IEnumerable<NoteToReturnDto> GetAllNotes(string SearchValue)
        {
            var notes = _noteRepository.GetAllQuarable()
                .Where(n => n.IsDeleted == false && (string.IsNullOrEmpty(SearchValue) || n.Title.ToLower().Contains(SearchValue.ToLower()))) // should add n.Tags.Contains(SearchValue)
                .Select(n => new NoteToReturnDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                    // hb2a include more fields here
                })
                .ToList();

            return notes;
        }

        public NoteDetailsToReturnDto? GetNoteById(int id)
        {
            var note = _noteRepository.GetById(id);

            if (note != null)
            {
                return new NoteDetailsToReturnDto
                {
                    Id = note.Id,
                    Title = note.Title,
                    Content = note.Content,
                    // Optionally map collaborators and tags here
                };
            }

            return null;
        }

        public int CreateNote(NoteToCreateDto note)
        {
            var newNote = new Note
            {
                Title = note.Title,
                Content = note.Content,
                OwnerId = note.Owner.Id,
                NoteCollaborators = note.Collaborators?
                    .Select(c => new NoteCollaborator
                    {
                        CollaboratorId = c.Id
                    }).ToList() ?? new List<NoteCollaborator>(),

                NoteTags = note.Tags?
                    .Select(t => new NoteTag
                    {
                        TagId = t.Id
                    }).ToList() ?? new List<NoteTag>()
            };

            return _noteRepository.AddNote(newNote);
        }

        public int UpdateNote(NoteToUpdateDto note)
        {
            var updatedNote = _noteRepository.GetById(note.Id);
            if (updatedNote == null) return 0;

            updatedNote.Title = note.Title;
            updatedNote.Content = note.Content;
            updatedNote.UpdatedAt = DateTime.UtcNow;

            // Optionally update collaborators
            if (note.Collaborators != null)
            {
                updatedNote.NoteCollaborators = note.Collaborators
                    .Select(c => new NoteCollaborator
                    {
                        CollaboratorId = c.Id
                    }).ToList();
            }

            // Optionally update tags
            if (note.Tags != null)
            {
                updatedNote.NoteTags = note.Tags
                    .Select(t => new NoteTag
                    {
                        TagId = t.Id
                    }).ToList();
            }

            return _noteRepository.UpdateNote(updatedNote);
        }

        public bool DeleteNote(int id)
        {
            var note = _noteRepository.GetById(id);

            if (note != null)
            {
                note.IsDeleted = true; // Soft delete
                note.UpdatedAt = DateTime.UtcNow;
                return _noteRepository.UpdateNote(note) > 0;
            }

            return false;
        }
    }
}
