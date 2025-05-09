using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Application.Dtos.Notes;

namespace NoteKeeperPro.Application.Services.Notes
{
    public interface INoteService
    {
        IEnumerable<NoteToReturnDto> GetAllNotes();
        NoteDetailsToReturnDto? GetNoteById(int id);
        int CreateNote(NoteToCreateDto note);
        int UpdateNote(NoteToUpdateDto note);
        bool DeleteNote(int id);
    }
}
