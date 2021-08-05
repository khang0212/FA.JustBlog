using FA.JustBlog.Data.Infrastructure;
using FA.JustBlog.Models.Common;
using FA.JustBlog.Services.BaseServices;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FA.JustBlog.Services
{
    public class TagServices : BaseServices<Tag>, ITagServices
    {
        public TagServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<int> CountTagsForPostAsync(string post)
        {
            return await _unitOfWork.TagRepository.GetQuery().CountAsync(p => p.Posts.Any(t => t.Title == post));
        }

        public IEnumerable<Tag> GetHighestViewCountTag(int count)
        {
            return _unitOfWork.TagRepository.GetQuery().OrderByDescending(p => p.Count).Take(count).ToList();
        }

        public async Task<IEnumerable<Tag>> GetTagsByPostAsync(string post)
        {
            return await _unitOfWork.TagRepository.GetQuery().Where(p => p.Posts.Any(t => t.Title == post)).ToListAsync();
        }
    }
}
