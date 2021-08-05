using FA.JustBlog.Data.Infrastructure.BaseRepositories;
using FA.JustBlog.Models;
using FA.JustBlog.Models.Common;
using System;
using System.Threading.Tasks;

namespace FA.JustBlog.Data.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        JustBlogDbContext DbContext { get; }
 
        IGenericRepository<Category> CategoryRepository { get; }

        IGenericRepository<Tag> TagRepository { get; }

        IGenericRepository<Post> PostRepository { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        IGenericRepository<T> GenericRepository<T>() where T : BaseEntity;
    }
}
