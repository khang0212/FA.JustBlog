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
        // GET: Post
        private readonly IPostServices _postServices;
        private readonly ICategoryServices _categoryServices;

        public PostController(IPostServices postServices, ICategoryServices categoryServices)
        {
            _postServices = postServices;
            _categoryServices = categoryServices;
        }
        // GET: Post
        public async Task<ActionResult> Index(int? pageIndex = 1, int? pageSize = 3)
        {
            Expression<Func<Post, bool>> filter = null;

            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = o => o.OrderBy(p => p.Title);
            var posts = await _postServices.GetAsync(filter: filter, orderBy: orderBy,
                pageIndex: pageIndex ?? 1, pageSize: pageSize ?? 3);
            return View(posts);
        }

        public async Task<ActionResult> LastestPosts()
        {
            var lastestPost = await _postServices.GetLatestPostAsync(5);
            return View(lastestPost);
        }
        public async Task<ActionResult> MostViewedPosts()
        {
            var highestView = await _postServices.GetHighestViewCountPostAsync(5);
            return View(highestView);
        }
    }
}