using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FA.JustBlog.Data;
using FA.JustBlog.Models.Common;
using FA.JustBlog.Services;
using FA.JustBlog.WebMVC.ViewModel;

namespace FA.JustBlog.WebMVC.Areas.Admin.Controllers
{
    public class TagManagementController : Controller
    {
        private JustBlogDbContext db = new JustBlogDbContext();
        private readonly ITagServices _tagService;

        public TagManagementController(ITagServices tagServices)
        {
            _tagService = tagServices;
        }


        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 2)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["UrlSlugSortParm"] = sortOrder == "UrlSlug" ? "urlSlug_desc" : "UrlSlug";
            ViewData["DescriptionSortParm"] = sortOrder == "Description" ? "description_desc" : "Description";
            ViewData["TotalPostsSortParm"] = sortOrder == "TotalPosts" ? "totalPost_desc" : "TotalPosts";
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

            Expression<Func<Tag, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = c => c.Name.Contains(searchString);
            }

      
            Func<IQueryable<Tag>, IOrderedQueryable<Tag>> orderBy = null;

            switch (sortOrder)
            {
                case "name_desc":
                    orderBy = q => q.OrderByDescending(c => c.Name);
                    break;
                case "UrlSlug":
                    orderBy = q => q.OrderBy(c => c.UrlSlug);
                    break;
                case "urlSlug_desc":
                    orderBy = q => q.OrderByDescending(c => c.UrlSlug);
                    break;
                case "Description":
                    orderBy = q => q.OrderBy(c => c.Description);
                    break;
                case "description_desc":
                    orderBy = q => q.OrderByDescending(c => c.Description);
                    break;
                case "TotalPosts":
                    orderBy = q => q.OrderBy(c => c.Posts.Count);
                    break;
                case "totalPost_desc":
                    orderBy = q => q.OrderByDescending(c => c.Posts.Count);
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
                    orderBy = q => q.OrderBy(c => c.Name);
                    break;
            }

            var tags = await _tagService.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(tags);
        }


        public ActionResult Create()
        {
            var tagViewModel = new TagViewModel();
            return View(tagViewModel);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                var tag = new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = tagViewModel.Name,
                    UrlSlug = tagViewModel.UrlSlug,
                    Description = tagViewModel.Description
                };
                _tagService.Add(tag);
                return RedirectToAction("Index");
            }

            return View(tagViewModel);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tag = _tagService.GetById((Guid)id);
            if (tag == null)
            {
                return HttpNotFound();
            }

            var tagViewModel = new TagViewModel()
            {
                Name = tag.Name,
                UrlSlug = tag.UrlSlug,
                Description = tag.Description
            };
            return View(tagViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                var tag = await _tagService.GetByIdAsync(tagViewModel.Id);
                if (tag == null)
                {
                    return HttpNotFound();
                }
                tag.Name = tagViewModel.Name;
                tag.UrlSlug = tagViewModel.UrlSlug;
                tag.Description = tagViewModel.Description;

                _tagService.Update(tag);
                return RedirectToAction("Index");
            }
            return View(tagViewModel);
        }

        
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            Tag tag = _tagService.GetById(id);
            var result = false;
            if (tag != null)
            {
                result = _tagService.Delete(tag.Id);
            }

            if (result)
            {
                TempData["Message"] = "Delete Successful";
            }
            else
            {
                TempData["Message"] = "Delete Failed";
            }
            return RedirectToAction("Index");
        }


    }
}