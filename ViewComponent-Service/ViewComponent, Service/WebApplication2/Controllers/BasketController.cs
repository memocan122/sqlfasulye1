using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication2.Data;
using WebApplication2.Models;
using WebApplication2.ViewModels.BasketVMs;

namespace WebApplication2.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;

        public BasketController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(HttpContext.Request.Cookies["basket"]);

            List<BasketDetailVM> basketDetails = new();

            foreach (var item in basket)
            {
                Product product = await _context.Products.Include(m => m.ProductImages).Include(m => m.Category).FirstOrDefaultAsync(m => m.Id == item.Id
                && !m.IsDeleted);

                basketDetails.Add(new BasketDetailVM
                {
                    Id = item.Id,
                    Count = item.Count,
                    Image = product.ProductImages.FirstOrDefault(m => m.IsMain)?.Image,
                    Name = product.Name,
                    Category = product.Category.Name,
                    Price = product.Price,
                    TotalPrice = product.Price * item.Count
                });
            }

            return View(basketDetails);
        }

        //[HttpPost]
        //public async Task<IActionResult> Add(int? id)
        //{
        //    if (id is null) return BadRequest();

        //    bool isExist = await _context.Products.AnyAsync(p => p.Id == id);

        //    if (!isExist) return NotFound();

        //    List<BasketVM> basket;

        //    if (HttpContext.Request.Cookies["basket"] != null)
        //    {
        //        basket = JsonConvert.DeserializeObject<List<BasketVM>>(HttpContext.Request.Cookies["basket"]);
        //    }
        //    else
        //    {
        //        basket = new();
        //    }

        //    var isProductExist = basket.FirstOrDefault(m => m.Id == id);

        //    if (isProductExist == null)
        //    {
        //        basket.Add(new BasketVM()
        //        {
        //            Id = (int)id,
        //            Count = 1
        //        });
        //    }
        //    else
        //    {
        //        isProductExist.Count++;
        //    }

        //    HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket));

        //    return RedirectToAction("Index", "Home");
        //}
        //-----------------------------------------------------------------------//
        [HttpPost]
        public async Task<IActionResult> Add(int? id)
        {
            if (id == null) return BadRequest();

            Product product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

            if (product == null) return NotFound();

            List<BasketVM> basket;

            if (HttpContext.Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(
                    HttpContext.Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            BasketVM exist = basket.FirstOrDefault(b => b.Id == id);

            if (exist == null)
            {
                basket.Add(new BasketVM
                {
                    Id = product.Id,
                    Count = 1
                });
            }
            else
            {
                exist.Count++;
            }

            HttpContext.Response.Cookies.Append(
                "basket",
                JsonConvert.SerializeObject(basket)
            );

            return Ok(); 
        }

    }
}
