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
using Eshop.Domain.ViewModels.ProductCategory;

namespace Eshop.Web.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductCategoriesController 
        (IProductCategoryService productCategoryService): Controller
    {
        #region Actions

        #region Index

        public async Task<IActionResult> Index()
        {
            var productCategories=await productCategoryService.GetAllAsync();
            return View(productCategories);
        }

        #endregion

        #region Details

        public async Task<IActionResult> Details(int id)
        {
            var productCategory = await productCategoryService.GetProductCategoryByIdAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        #endregion

        #region Create
        public IActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>  Create(CreateproductCategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            var result= await productCategoryService.CreateProductCategoryAsync(model);

            switch (result)
            {
                case CreateproductCategorResult.Success:
                    return RedirectToAction(nameof(Index));
                    break;
                case CreateproductCategorResult.CategoryDuplicated:
                  ModelState.AddModelError("این دسته قبلا ثبت شده است",model.CategoryTittle);
                    break;
            }
            return RedirectToAction(nameof(Index));
        }
            
          
        #endregion

        #region Update
        
        public async Task<IActionResult> Edit(int id)
        {
            var productCategory = await productCategoryService.GetProductCategoryForEditByIdAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return PartialView(productCategory);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           var result = await productCategoryService.UpdateProductCategoryAsync(model);
           return RedirectToAction(nameof(Index));
           
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int id)
        {
            var result= await productCategoryService.DeleteProductCategoryAsync(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion
        
        #endregion

      
      
    }
}
