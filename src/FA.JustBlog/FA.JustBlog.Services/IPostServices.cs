﻿using FA.JustBlog.Models.Common;
using FA.JustBlog.Services.BaseServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FA.JustBlog.Services
{
    public interface IPostServices : IBaseService<Post>
    {
        Task<IEnumerable<Post>> GetPublisedPostsAsync(bool published = true);

        Task<IEnumerable<Post>> GetLatestPostAsync(int size);

        Task<IEnumerable<Post>> GetPostsByMonthAsync(DateTime monthYear);

        Task<Post> GetPostsByTimeAndUrlSlugAsync(int year, int month, string urlSlug);

        Task<int> CountPostsForCategoryAsync(string category);

        Task<IEnumerable<Post>> GetPostsByCategoryAsync(string category);

        Task<IEnumerable<Post>> GetPostsByCategoryAsync(Guid id);

        Task<int> CountPostsForTagAsync(string tag);

        Task<IEnumerable<Post>> GetPostsByTagAsync(string tag);

        Task<IEnumerable<Post>> GetPostsByTagAsync(Guid tagId);

        Task<IEnumerable<Post>> GetMostViewedPost(int count);

        Task<IEnumerable<Post>> GetMostViewedPostAsync(int size);
    }
}
