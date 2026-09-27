using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eshop.Data.Context;
using Eshop.Domain.Models.Products;
using Eshop.Domain.ViewModels.Product;

namespace Eshop.Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductsController 
        (IProductService productService,
            IProductCategoryService categoryService): Controller
    {

        #region Index
        
        public async Task<IActionResult> Index()
        {
            var product=await  productService.GetAllAsync();
            return View(product);
        }

        #endregion

        #region Detailes

        
        public async Task<IActionResult> Details(int id)
        {

            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        #endregion

        #region Create
        
        public async Task<IActionResult> Create()
        {
            var productCategories =await categoryService.GetAllAsync();
            ViewData["CategoryId"] = new SelectList(productCategories, "CategoryId", "CategoryTittle");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var productCategories =await categoryService.GetAllAsync();
                ViewData["CategoryId"] = new SelectList(productCategories, "CategoryId", "CategoryTittle");
                return View(model);
                
            }
            var result = await productService.CreateProductAsync(model);
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Edit
        
        public async Task<IActionResult> Edit(int id)
        {

            var product = await productService.GetForEditAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productCategories =await categoryService.GetAllAsync();
            ViewData["CategoryId"] = new SelectList(productCategories, "CategoryId", "CategoryTittle");
            return View(product);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductViewModel model)
        {

            if (!ModelState.IsValid)
            {
                var productCategories =await categoryService.GetAllAsync();
                ViewData["CategoryId"] = new SelectList(productCategories, "CategoryId", "CategoryTittle");
                return View(model);
            }
            var result = await productService.UpdateProductAsync(model);
            return RedirectToAction(nameof(Index));
          
        }

        #endregion

        #region DeleteImage

        public async Task DeleteImage(int id)
        {
            await productService.DeleteImageAsync(id);

        }

        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            var product = await productService.DeleteProductAsync(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
