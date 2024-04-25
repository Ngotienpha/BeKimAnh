using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebApplication5.Models;



namespace WebApplication5.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<ViewStatus> ViewStatus { get; set; }
    }

}
