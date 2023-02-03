using Microsoft.EntityFrameworkCore;

namespace BookStore.Models.domins
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options):base (options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<cartbooks> cartbooks { get; set; }
        public DbSet<admin> Admin { get; set; }
    }
}
