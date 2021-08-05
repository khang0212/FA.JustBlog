using FA.JustBlog.Services;
using FA.JustBlog.WebMVC.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlog.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostServices _postServices;
        private readonly ITagServices _tagServices;
        private readonly ICategoryServices _categoryServices;

        public HomeController(IPostServices postServices, ITagServices tagServices, ICategoryServices categoryServices)
        {
            _postServices = postServices;
            _tagServices = tagServices;
            _categoryServices = categoryServices;
        }

        public async Task<ActionResult> Index()
        {
            var posts = await _postServices.GetAllAsync();
            return View(posts);
        }
        public async Task<ActionResult> PostDetails(Guid id)
        {
            var posts = await _postServices.GetByIdAsync(id);
            return View(posts);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [ChildActionOnly]
        public ActionResult AboutCard()
        {
            return PartialView("_PartialAboutCard");
        }
        public ActionResult CategoryMenu()
        {
            var categories = _categoryServices.GetAll().Select(a => new CategoryViewModel
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();
            return PartialView("_CategoryView", categories);
        }
    }
}