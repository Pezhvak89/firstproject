using System.Diagnostics;
using Eshop.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly EshopDbContext _context;

        public HomeController(EshopDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.Include(p => p.ProductImages)
                .OrderByDescending(p => p.ProductId).Take(15);
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}
