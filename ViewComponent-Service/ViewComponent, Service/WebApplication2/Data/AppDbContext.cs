using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }

        public DbSet<SliderDetail> SliderDetails { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   modelBuilder.Entity<Blog>().HasQueryFilter(m => !m.IsDeleted);
            modelBuilder.Entity<Blog>().HasData(
                new Blog
                {
                   Id = 1,
                    Image = "blog-feature-img-1.jpg",
                    Title = "Flower Power",
                    Description = "This is the first blog post.",
                    CreatedDate = new DateTime(2025,12,22),
                    IsDeleted = false
                },
                 new Blog
                 {
                     Id = 2,
                     Image = "blog-feature-img-3.jpg",
                     Title = "Local Florists",
                     Description = "This is the first blog post.",
                     CreatedDate =new  DateTime(2025, 12, 22),
                     IsDeleted = false
                 },
                  new Blog
                  {
                      Id = 3,
                      Image = "blog-feature-img-4.jpg",
                      Title = "Flower Beauty",
                      Description = "This is the first blog post.",
                      CreatedDate = new DateTime(2025, 12, 22),
                      IsDeleted = false
                  }
                );
                
        }
    }
}
