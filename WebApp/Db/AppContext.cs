using Microsoft.EntityFrameworkCore;
using WebApp.Db.Entities;

namespace WebApp.Db
{
    public class AppContext : DbContext
    {
        public DbSet<BlogEntry> Blogs { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
    }
}
