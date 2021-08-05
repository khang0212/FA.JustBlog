using System.Data.Entity;

namespace FA.JustBlog.Data
{
    public class DbInitializer : CreateDatabaseIfNotExists<JustBlogDbContext>
    {
        protected override void Seed(JustBlogDbContext context)
        {
            
        }
    }
}
