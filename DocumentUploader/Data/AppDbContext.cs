using DocumentUploader.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentUploader.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Models.File> Files { get; set; }

    }
}
