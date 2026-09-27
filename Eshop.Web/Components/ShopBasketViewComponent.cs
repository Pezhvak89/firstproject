using System.Security.Claims;
using Eshop.Data.Context;
using Eshop.Domain.Models.Basket;
using Eshop.Domain.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Web.Components;

public class ShopBasketViewComponent:ViewComponent
{
 private readonly EshopDbContext _Context;

 public ShopBasketViewComponent(EshopDbContext context)
 {
     _Context = context;
 }
 public IViewComponentResult Invoke()
 {
     int userId = int.Parse(HttpContext.User.FindFirstValue("UserId"));
     var order=_Context.Orders
         .FirstOrDefault(o => o.UserId == userId);
     List<OrderDetails> list = new List<OrderDetails>();
     if (order != null)
     {
         list.AddRange(_Context.OrderDetailes
             .Where(od=>od.OrderId==order.OrderId)
             .Include(p=>p.Product)
             .ThenInclude(p=>p.ProductImages));
     }
      return View(list);
   }
}