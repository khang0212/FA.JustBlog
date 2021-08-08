using FA.JustBlog.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlog.WebMVC.Controllers
{

    public class CategoriesController : Controller
    {
        private readonly IPostServices _postService;
        private readonly ICategoryServices _categoryService;

        public CategoriesController(IPostServices postServices, ICategoryServices categoryServices)
        {
            _postService = postServices;
            _categoryService = categoryServices;
        }

        public async Task<ActionResult> CategoriesDetail(Guid id)
        {
            var c = await _categoryService.GetByIdAsync(id);
            ViewBag.CategoryName = c.Name;
            var p = await _postService.GetPostsByCategoryAsync(id);
            return View(p);
        }
    }
}