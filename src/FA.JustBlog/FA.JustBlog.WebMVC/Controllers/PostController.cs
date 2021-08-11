using FA.JustBlog.Models.Common;
using FA.JustBlog.Services;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlog.WebMVC.Controllers
{
    public class PostController : Controller
    {

        private readonly IPostServices _postService;
        private readonly ICategoryServices _categoryService;

        public PostController(IPostServices postServices, ICategoryServices categoryServices)
        {
            _postService = postServices;
            _categoryService = categoryServices;
        }

        public async Task<ActionResult> Index(int? pageIndex = 1, int? pageSize = 3)
        {
            Expression<Func<Post, bool>> filter = null;

            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = o => o.OrderBy(p => p.Title);
            var posts = await _postService.GetAsync(filter: filter, orderBy: orderBy,
                pageIndex: pageIndex ?? 1, pageSize: pageSize ?? 3);

            return View(posts);

        }


        //public ActionResult LastestPosts()
        //{
        //    var lastest = Task.Run(() => _postService.GetLatestPostAsync(5)).Result;
        //    ViewBag.PartialViewTitle = "Lastest Posts";
        //    return PartialView("_ListPosts", lastest);
        //}
        public ActionResult MostViewedPosts()
        {
            var most = Task.Run(() => _postService.GetMostViewedPost(5)).Result;
            ViewBag.PartialViewTitle = "Most Viewed Posts";
            return PartialView("MostViewedPosts", most);
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var post = await _postService.GetByIdAsync(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }
    }
}