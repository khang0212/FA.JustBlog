using FA.JustBlog.Data.Infrastructure;
using FA.JustBlog.Models.Common;
using FA.JustBlog.Services.BaseServices;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace FA.JustBlog.Services
{
    public class CategoryServices : BaseServices<Category>, ICategoryServices
    {
        public CategoryServices(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<Category>> GetCategoriesByPostAsync(string posts)
        {
            return await _unitOfWork.CategoryRepository.GetQuery().Where(p => p.Posts.Any(t => t.Title == posts)).ToListAsync();
        }
    }
}
