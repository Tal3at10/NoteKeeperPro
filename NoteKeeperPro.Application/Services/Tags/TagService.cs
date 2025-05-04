using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Tags;
using NoteKeeperPro.Application.Dtos.Tags;
using NoteKeeperPro.Infrastructure.Presistance.Repositories.Tags;

namespace NoteKeeperPro.Application.Services.Tags
{
    internal class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public int CreateTag(TagToCreateDto tag)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTag(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TagToReturnDto> GetAllTags()
        {
            throw new NotImplementedException();
        }

        public TagDetailsToReturnDto? GetTagById(int id)
        {
            throw new NotImplementedException();
        }

        public int UpdateTag(TagToUpdateDto tag)
        {
            throw new NotImplementedException();
        }
    }
}
