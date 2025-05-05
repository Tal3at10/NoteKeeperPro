using System;
using System.Collections.Generic;
using System.Linq;
using NoteKeeperPro.Application.Dtos.Notes;
using NoteKeeperPro.Domain.Entities.Notes;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Notes;

namespace NoteKeeperPro.Application.Services.Notes
{
    internal class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public IEnumerable<NoteToReturnDto> GetAllNotes()
        {
            var notes = _noteRepository.GetAllQuarable()
                .Where(n => n.IsDeleted == false)
                .Select(n => new NoteToReturnDto
                {
                    Id = n.Id,
                    Title = n.Title,
                    Content = n.Content,
                   
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
               
            };

            return _noteRepository.AddNote(newNote);
        }

        public int UpdateNote(NoteToUpdateDto note)
        {
            var updatedNote = new Note
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
               
            };

            return _noteRepository.UpdateNote(updatedNote);
        }

        public bool DeleteNote(int id)
        {
            var note = _noteRepository.GetById(id);

            if (note != null)
            {
                note.IsDeleted = true; // Soft delete
                return _noteRepository.UpdateNote(note) > 0;
            }

            return false;
        }
    }
}
