using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;
using WebApplication2.Models;

namespace WebApplication2.ViewComponents
{
    public class BlogViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;
        public BlogViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()

        {
            IEnumerable<Blog> blogs = await _context.Blogs
            .Where(m => !m.IsDeleted).ToListAsync();

            return await Task.FromResult(View(blogs));
        }
    }
}
