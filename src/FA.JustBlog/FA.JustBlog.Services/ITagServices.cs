using FA.JustBlog.Models.Common;
using FA.JustBlog.Services.BaseServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FA.JustBlog.Services
{
    public interface ITagServices : IBaseService<Tag>
    {

        Task<int> CountTagsForPostAsync(string post);

        Task<IEnumerable<Tag>> GetTagsByPostAsync(string post);

        IEnumerable<Tag> GetHighestViewCountTag(int count);
    }
}
