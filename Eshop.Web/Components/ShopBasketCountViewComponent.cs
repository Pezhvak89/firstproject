using System.Security.Claims;
using Eshop.Data.Context;
using Eshop.Domain.Models.Basket;
using Eshop.Domain.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Web.Components;

public class ShopBasketCountViewComponent : ViewComponent
{
    private readonly EshopDbContext _Context;

    public ShopBasketCountViewComponent(EshopDbContext context)
    {
        _Context = context;
    }

    public IViewComponentResult Invoke()
    {
        int userId = int.Parse(HttpContext.User.FindFirstValue("UserId"));
        var order = _Context.Orders
            .FirstOrDefault(o => o.UserId == userId);
        int count = 0;
         if(order != null)
             count = _Context.OrderDetailes.Where(od => od.OrderId == order.OrderId).Sum(od => od.Count);
        return View(count);
    }
}
