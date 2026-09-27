using Eshop.Data.Context;
using Eshop.Domain.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Web.Components;

public class ProductCategoryViewComponent:ViewComponent
{
 private readonly EshopDbContext _Context;

 public ProductCategoryViewComponent(EshopDbContext context)
 {
     _Context = context;
 }
 public IViewComponentResult Invoke()
 {
     var category = _Context.ProductCategories
         .Select(c => new ProductGroupShowViewModel
         {
             CategoryId = c.CategoryId,
             CategoryTittle = c.CategoryTittle,
             ProductCount = _Context.Products.Count(p => p.CategoryId == c.CategoryId)
         }).ToList();
      return View(category);
   }
}