using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using FA.JustBlog.Data;
using System.Collections.Generic;
using FA.JustBlog.Models.Common;
using FA.JustBlog.Services;
using FA.JustBlog.WebMVC.ViewModel;
using System.Data;
using System.Web.Mvc;

namespace FA.JustBlog.WebMVC.Areas.Admin.Controllers
{
    public class PostManagementController : Controller
    {
        private JustBlogDbContext db = new JustBlogDbContext();
        private readonly IPostServices _postService;
        private readonly ICategoryServices _categoryService;
        private readonly ITagServices _tagService;

        public PostManagementController(IPostServices postServices, ICategoryServices categoryServices, ITagServices tagServices)
        {
            _postService = postServices;
            _categoryService = categoryServices;
            _tagService = tagServices;
        }

        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 2)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["UrlSlugSortParm"] = sortOrder == "UrlSlug" ? "urlSlug_desc" : "UrlSlug";
            ViewData["PublishedSortParm"] = sortOrder == "Published" ? "published_desc" : "Published";
            ViewData["PublishedDateSortParm"] = sortOrder == "PublishedDate" ? "publisheddate_desc" : "PublishedDate";
            ViewData["UpdatedAtSortParm"] = sortOrder == "UpdatedAt" ? "updatedAt_desc" : "UpdatedAt";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            Expression<Func<Post, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = c => c.Title.Contains(searchString);
            }

            Func<IQueryable<Post>, IOrderedQueryable<Post>> orderBy = null;

            switch (sortOrder)
            {
                case "title_desc":
                    orderBy = q => q.OrderByDescending(c => c.Title);
                    break;
                case "UrlSlug":
                    orderBy = q => q.OrderBy(c => c.UrlSlug);
                    break;
                case "urlSlug_desc":
                    orderBy = q => q.OrderByDescending(c => c.UrlSlug);
                    break;
                case "Published":
                    orderBy = q => q.OrderBy(c => c.Published);
                    break;
                case "published_desc":
                    orderBy = q => q.OrderByDescending(c => c.Published);
                    break;
                case "PublishedDate":
                    orderBy = q => q.OrderBy(c => c.PublishedDate);
                    break;
                case "publisheddate_desc":
                    orderBy = q => q.OrderByDescending(c => c.PublishedDate);
                    break;
                case "UpdatedAt":
                    orderBy = q => q.OrderBy(c => c.UpdatedAt);
                    break;
                case "updatedAt_desc":
                    orderBy = q => q.OrderByDescending(c => c.UpdatedAt);
                    break;
                default:
                    orderBy = q => q.OrderBy(c => c.Title);
                    break;
            }

            var posts = await _postService.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(posts);
        }




        public ActionResult Create()
        {

            ViewBag.CategoryId = new SelectList(_categoryService.GetAll(), "Id", "Name");
            var postViewModel = new PostViewModel();
            postViewModel.Tags = _tagService.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            return View(postViewModel);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var p = new Post
                {
                    Id = Guid.NewGuid(),
                    Title = postViewModel.Title,
                    ShortDescription = postViewModel.ShortDescription,
                    PostContent = postViewModel.PostContent,
                    UrlSlug = postViewModel.UrlSlug,
                    Published = postViewModel.Published,
                    CategoryId = postViewModel.CategoryId,
                    ImageUrl = postViewModel.ImageUrl,
                    Tags = await GetTagById(postViewModel.SelectedTagIds)
                };

                var result = await _postService.AddAsync(p);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name", postViewModel.CategoryId);
            postViewModel.Tags = _tagService.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            return View(postViewModel);
        }


        public async Task<ActionResult> Edit(Guid? id)
        {
            var p = await _postService.GetByIdAsync((Guid)id);
            var postViewModel = new PostViewModel()
            {
                Id = p.Id,
                Title = p.Title,
                UrlSlug = p.UrlSlug,
                ShortDescription = p.ShortDescription,
                ImageUrl = p.ImageUrl,
                PostContent = p.PostContent,
                Published = p.Published,
                CategoryId = p.CategoryId,
                SelectedTagIds = p.Tags.Select(k => k.Id)
            };
            ViewBag.Categories = new SelectList(_categoryService.GetAll(), "Id", "Name", postViewModel.CategoryId);
            postViewModel.Tags = _tagService.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            ViewBag.TagList = _tagService.GetAll();
            return View(postViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var post = await _postService.GetByIdAsync(postViewModel.Id);
                if (post == null)
                {
                    return HttpNotFound();
                }
                post.Title = postViewModel.Title;
                post.UrlSlug = postViewModel.UrlSlug;
                post.ShortDescription = postViewModel.ShortDescription;
                post.ImageUrl = postViewModel.ImageUrl;
                post.PostContent = postViewModel.PostContent;
                post.Published = postViewModel.Published;
                post.CategoryId = postViewModel.CategoryId;
                await UpdateTagById(postViewModel.SelectedTagIds, post);

                var result = await _postService.UpdateAsync(post);
                if (result)
                {
                    TempData["Message"] = "Update successful!";
                }
                else
                {
                    TempData["Message"] = "Update failed!";

                }
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name", postViewModel.CategoryId);
            postViewModel.Tags = _tagService.GetAll().Select(t => new SelectListItem { Value = t.Id.ToString(), Text = t.Name });
            ViewBag.TagList = _tagService.GetAll();
            return View(postViewModel);
        }

        private async Task<ICollection<Tag>> GetTagById(IEnumerable<Guid> tagId)
        {
            var tags = new List<Tag>();
            var tagEntities = await _tagService.GetAllAsync();
            foreach (var item in tagEntities)
            {
                if (tagId.Any(x => x == item.Id))
                {
                    tags.Add(item);
                }
            }
            return tags;
        }

        private async Task UpdateTagById(IEnumerable<Guid> tagId, Post p)
        {
            var tags = p.Tags;
            foreach (var i in tags.ToList())
            {
                p.Tags.Remove(i);
            }
            p.Tags = await GetTagById(tagId);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            Post post = _postService.GetById(id);
            var result = false;
            if (post != null)
            {
                result = _postService.Delete(post.Id);
            }

            if (result)
            {
                TempData["Message"] = "Done !";
            }
            else
            {
                TempData["Message"] = "Uncompleted !";
            }
            return RedirectToAction("Index");
        }

    }
}
