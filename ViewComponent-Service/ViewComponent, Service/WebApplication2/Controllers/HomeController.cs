using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;
using System.Diagnostics;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.ViewModels;


namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private IEnumerable<Product> products;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            //HttpContext.Session.SetString("Name", "Gulare");

            //HttpContext.Response.Cookies.Append("Surname", "Abasova", new CookieOptions () 
            //{ MaxAge = TimeSpan.FromMinutes(5)});

            //ViewBag.Name = HttpContext.Session.GetString("Name");
            //ViewBag.Surname = HttpContext.Request.Cookies["Surname"];

            IEnumerable<Slider> sliders = await _context.Sliders.Where(m => !m.IsDeleted).ToListAsync();

            SliderDetail sliderDetail = await _context.SliderDetails.FirstOrDefaultAsync(m => !m.IsDeleted);

            IEnumerable<Product> products = await _context.Products
                .Include(m => m.ProductImages)
                .Include(m => m.Category)
                .Where(m => !m.IsDeleted)
                .Take(4)
                .ToListAsync(); 

            IEnumerable<Category> categories = await _context.Categories.Where(m => !m.IsDeleted).ToListAsync();

            HomeVM homeVM = new()
            {
                Sliders = sliders,
                SliderDetail = sliderDetail,
                Products = products,
                Categories = categories
               
            };
            ViewBag.ProductCount =await _context.Products.CountAsync(m=> !m.IsDeleted);
            return View(homeVM);
        }
     
        public async Task<IActionResult> LoadMore(int skip)
        {
        IEnumerable<Product> products = await _context.Products
                .Include(m => m.ProductImages)
                .Include(m => m.Category)
                .Where(m => !m.IsDeleted)
                .Skip(skip)
                .Take(4)
                .ToListAsync();
        return PartialView("_ProductPartial", products);
        }

    }
}
