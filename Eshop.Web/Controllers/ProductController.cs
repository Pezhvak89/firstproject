using System.Security.Claims;
using Eshop.Data.Context;
using Eshop.Domain.Models.Basket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Web.Controllers;

public class ProductController 
(EshopDbContext context): Controller
{
   [Route("Group/{categoryTitle}/{categoryId}")]
   public IActionResult ShowProductByCategory(string categoryTitle, int categoryId)
   {
      ViewData["CategoryTitle"] = categoryTitle;
      var product = context.Products
         .Include(p=>p.ProductImages)
         .Where(p => p.CategoryId == categoryId)
         .OrderByDescending(p => p.ProductId)
         .ToList();
      return View(product);
   }
   [Route("ShowProduct/{productId}")]
   public IActionResult ShowProduct(int productId)
   {
      var product = context.Products
         .Include(p => p.ProductImages)
         .FirstOrDefault(p => p.ProductId == productId);
      if (product == null)
         return NotFound();
      return View(product);
   }

   [Authorize]
   public IActionResult AddToBasket(int  productId)
   {
      int userId = int.Parse(User.FindFirstValue("UserId"));
      var order=context.Orders
         .FirstOrDefault(u=>u.UserId == userId && !u.IsFinaly);
      if (order == null)
      {
         order = new Order
         {
            UserId = userId,
            CreateDate = DateTime.Now,
            IsFinaly = false,
         };
         context.Orders.Add(order);
         context.SaveChanges();
      }
      var details = context.OrderDetailes
         .FirstOrDefault(od=>od.OrderId == order.OrderId && od.ProductId == productId);
      if (details == null)
      {
         details = new OrderDetails
         {
            OrderId = order.OrderId,
            ProductId = productId,
            Count = 1,
            Price = context.Products.Find(productId).Price,
         };
         context.OrderDetailes.Add(details);
      }
      else
      {
         details.Count += 1;
      }
      context.SaveChanges();
      string referer= Request.Headers["Referer"].ToString();
      return Redirect(referer);
   }
}