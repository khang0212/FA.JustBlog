using FA.JustBlog.Data;
using FA.JustBlog.Models.Common;
using FA.JustBlog.Services;
using FA.JustBlog.WebMVC.ViewModel;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FA.JustBlog.WebMVC.Areas.Admin.Controllers
{
    public class PostManagementController : Controller
    {
        private JustBlogDbContext db = new JustBlogDbContext();
        private readonly IPostServices _postServices;

        public PostManagementController(IPostServices postServices)
        {
            _postServices = postServices;
        }


        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 2)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["ShortDescSortParm"] = sortOrder == "ShortDesc" ? "shortdesc_desc" : "ShortDesc";
            ViewData["PostContentSortParm"] = sortOrder == "PostContent" ? "postcontent_desc" : "PostContent";
            ViewData["UrlSlugSortParm"] = sortOrder == "UrlSlug" ? "urlSlug_desc" : "UrlSlug";
            ViewData["PublishedSortParm"] = sortOrder == "Published" ? "published_desc" : "Published";
            ViewData["PublishedDateSortParm"] = sortOrder == "PublishedDate" ? "publisheddate_desc" : "PublishedDate";
            ViewData["CategoryIdSortParm"] = sortOrder == "CategoryId" ? "CategoryId_desc" : "CategoryId";
            ViewData["TotalTagsSortParm"] = sortOrder == "TotalTags" ? "totalTags_desc" : "TotalTags";
            ViewData["InsertedAtSortParm"] = sortOrder == "InsertedAt" ? "insertedAt_desc" : "InsertedAt";
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
                case "ShortDesc":
                    orderBy = q => q.OrderBy(c => c.ShortDescription);
                    break;
                case "short_desc":
                    orderBy = q => q.OrderByDescending(c => c.ShortDescription);
                    break;
                case "PostContent":
                    orderBy = q => q.OrderBy(c => c.PostContent);
                    break;
                case "PublishedDate":
                    orderBy = q => q.OrderBy(c => c.Published);
                    break;
                case "publisheddate_desc":
                    orderBy = q => q.OrderByDescending(c => c.Published);
                    break;
                case "postcontent_desc":
                    orderBy = q => q.OrderByDescending(c => c.PostContent);
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
                case "CategoryId":
                    orderBy = q => q.OrderBy(c => c.CategoryId);
                    break;
                case "CategoryId_desc":
                    orderBy = q => q.OrderByDescending(c => c.CategoryId);
                    break;
                case "TotalTags":
                    orderBy = q => q.OrderBy(c => c.Tags.Count);
                    break;
                case "totalTags_desc":
                    orderBy = q => q.OrderByDescending(c => c.Tags.Count);
                    break;
                case "InsertedAt":
                    orderBy = q => q.OrderBy(c => c.InsertedAt);
                    break;
                case "insertedAt_desc":
                    orderBy = q => q.OrderByDescending(c => c.InsertedAt);
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

            var posts = await _postServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(posts);
        }


        public ActionResult Create()
        {
            var postViewModel = new PostViewModel();
            return View(postViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    Id = Guid.NewGuid(),
                    Title = postViewModel.Title,
                    ShortDescription = postViewModel.ShortDescription,
                    PostContent = postViewModel.PostContent,
                    UrlSlug = postViewModel.UrlSlug,
                    Published = postViewModel.Published,
                    CategoryId = postViewModel.CategoryId
                };
                _postServices.Add(post);
                return RedirectToAction("Index");
            }

            return View(postViewModel);
        }

        // GET: Admin/PostManagement/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = _postServices.GetById((Guid)id);
            if (post == null)
            {
                return HttpNotFound();
            }

            var postViewModel = new PostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                ShortDescription = post.ShortDescription,
                PostContent = post.PostContent,
                UrlSlug = post.UrlSlug,
                Published = post.Published,
                CategoryId = post.CategoryId
            };
            return View(postViewModel);
        }

        // POST: Admin/PostManagement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(PostViewModel postViewModel)
        {
            if (ModelState.IsValid)
            {
                var post = await _postServices.GetByIdAsync(postViewModel.Id);
                if (post == null)
                {
                    return HttpNotFound();
                }
                post.Title = postViewModel.Title;
                post.ShortDescription = postViewModel.ShortDescription;
                post.PostContent = postViewModel.PostContent;
                post.UrlSlug = postViewModel.UrlSlug;
                post.Published = postViewModel.Published;
                post.CategoryId = postViewModel.CategoryId;

                _postServices.Update(post);
                return RedirectToAction("Index");
            }
            return View(postViewModel);
        }

        // GET: Admin/PostManagement/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Admin/PostManagement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
