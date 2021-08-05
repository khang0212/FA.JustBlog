﻿using FA.JustBlog.Models.Common;
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
    public class CategoryManagementController : Controller
    {
        private readonly ICategoryServices _categoryServices;

        public CategoryManagementController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }


        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString,
            int? pageIndex = 1, int pageSize = 2)
        {
            ViewData["CurrentPageSize"] = pageSize;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["UrlSlugSortParm"] = sortOrder == "UrlSlug" ? "urlSlug_desc" : "UrlSlug";
            ViewData["TotalPostsSortParm"] = sortOrder == "TotalPosts" ? "totalPosts_desc" : "TotalPosts";
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


            Expression<Func<Category, bool>> filter = null;

            if (!string.IsNullOrEmpty(searchString))
            {
                filter = c => c.Name.Contains(searchString);
            }


            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null;

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
                case "TotalPosts":
                    orderBy = q => q.OrderBy(c => c.Posts.Count);
                    break;
                case "totalPosts_desc":
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

            var categories = await _categoryServices.GetAsync(filter: filter, orderBy: orderBy, pageIndex: pageIndex ?? 1, pageSize: pageSize);

            return View(categories);
        }



        public ActionResult Create()
        {
            var categoryViewModel = new CategoryViewModel();
            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Id = Guid.NewGuid(),
                    Name = categoryViewModel.Name,
                    UrlSlug = categoryViewModel.UrlSlug,
                    Description = categoryViewModel.Description,
                };
                _categoryServices.Add(category);
                return RedirectToAction("Index");
            }

            return View(categoryViewModel);
        }

        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var category = _categoryServices.GetById((Guid)id);
            if (category == null)
            {
                return HttpNotFound();
            }

            var categoryViewModel = new CategoryViewModel()
            {
                Name = category.Name,
                UrlSlug = category.UrlSlug,
                Description = category.Description
            };
            return View(categoryViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public async Task<ActionResult> Edit(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryServices.GetByIdAsync(categoryViewModel.Id);
                if (category == null)
                {
                    return HttpNotFound();
                }
                category.Name = categoryViewModel.Name;
                category.UrlSlug = categoryViewModel.UrlSlug;
                category.Description = categoryViewModel.Description;

                _categoryServices.Update(category);
                return RedirectToAction("Index");
            }
            return View(categoryViewModel);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(Guid id)
        {
            Category category = _categoryServices.GetById(id);
            var result = _categoryServices.Delete(category.Id);
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