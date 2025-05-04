using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Application.Dtos.NotesInfo;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.NotesInfo;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Notes;

namespace NoteKeeperPro.Application.Services.NotesInfo
{
    internal class NoteInfoService : INoteInfoService
    {
        private readonly INoteInfoRepository _noteInfoRepository;

        public NoteInfoService(INoteInfoRepository noteInfoRepository)
        {
            _noteInfoRepository = noteInfoRepository;
        }

        public int CreateNoteInfo(NoteInfoToCreateDto noteInfo)
        {
            throw new NotImplementedException();
        }

        public bool DeleteNoteInfo(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NoteInfoToReturnDto> GetAllNoteInfos()
        {
            throw new NotImplementedException();
        }

        public NoteInfoDetailsToReturnDto? GetNoteInfoById(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateNoteInfo(NoteInfoToUpdateDto noteInfo)
        {
            throw new NotImplementedException();
        }
    }
}
