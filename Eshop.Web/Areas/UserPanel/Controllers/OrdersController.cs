using System.Security.Claims;
using Eshop.Data.Context;
using Eshop.Domain.Models.Basket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Web.Areas.UserPanel.Controllers;
[Authorize]
[Area("UserPanel")]
public class OrdersController : Controller
{
    private readonly EshopDbContext _Context;

    public OrdersController(EshopDbContext context)
    {
        _Context = context;
    }
    public IActionResult Index()
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

    public IActionResult DeleteOrder(int orderDetailsid)
    {
      var detail= _Context.OrderDetailes
          .Find(orderDetailsid);
      _Context.OrderDetailes.Remove(detail);
      _Context.SaveChanges();
      return RedirectToAction("Index");
    }
  
}