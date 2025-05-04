using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Application.Dtos.Notes;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Notes;
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

        public int CreateNote(NoteToCreateDto note)
        {
            throw new NotImplementedException();
        }

        public bool DeleteNote(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NoteToReturnDto> GetAllNotes()
        {
            throw new NotImplementedException();
        }

        public NoteDetailsToReturnDto? GetNoteById(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateNote(NoteToUpdateDto note)
        {
            throw new NotImplementedException();
        }
    }
}
