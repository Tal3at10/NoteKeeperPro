using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Domain.Entities.Notes;

namespace NoteKeeperPro.Infrastructure.Presistance.Repositories.Notes
{
    public interface INoteRepository
    {
        IEnumerable<Note> GetAll(bool AsNoTracking = true);
        IQueryable<Note> GetAllQuarable(bool AsNoTracking = true);
        Note GetById(int id);
        int AddNote(Note note);
        int UpdateNote(Note note);
        int DeleteNote(Note note);
    }
}
