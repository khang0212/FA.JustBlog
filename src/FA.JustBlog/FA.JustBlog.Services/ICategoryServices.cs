using FA.JustBlog.Models.Common;
using FA.JustBlog.Services.BaseServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FA.JustBlog.Services
{
    public interface ICategoryServices : IBaseService<Category>
    {
        Task<IEnumerable<Category>> GetCategoriesByPostAsync(string categories);
    }
}
