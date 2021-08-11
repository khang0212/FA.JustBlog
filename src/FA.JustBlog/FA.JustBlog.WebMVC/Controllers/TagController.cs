using FA.JustBlog.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlog.WebMVC.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagServices _tagServices;
        private readonly IPostServices _postServices;

        public TagController(ITagServices tagServices, IPostServices postServices)
        {
            _tagServices = tagServices;
            _postServices = postServices;
        }



        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        
        public ActionResult PopularTags()
        {
            var popularTags = Task.Run(() => _tagServices.GetMostViewedTag(10)).Result;
            return PartialView("_PopularTags", popularTags);
        }
        public async Task<ActionResult> DetailTags(Guid id)
        {
            var getByID = await _tagServices.GetByIdAsync(id);
            if (getByID == null)
            {
                return HttpNotFound();
            }
            var post = await _postServices.GetPostsByTagAsync(getByID.Id);
            ViewBag.TagName = getByID.Name;
            return View(post);
        }

    }
}