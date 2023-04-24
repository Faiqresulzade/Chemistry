using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAcces.Context
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<PersonInfo> PersonInfo { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<HomeSlider> HomeSliders { get; set; }
    }
}
