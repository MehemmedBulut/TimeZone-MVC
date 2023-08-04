using Microsoft.EntityFrameworkCore;
using TimeZoneBack.Models;

namespace TimeZoneBack.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<Arrival> Arrivals { get; set; }
        public DbSet<PopularItem> PopularItems { get; set; }
        public  DbSet<Bio> Bios { get; set; }
        public  DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Contact> Contacts { get; set; }

    }
}
