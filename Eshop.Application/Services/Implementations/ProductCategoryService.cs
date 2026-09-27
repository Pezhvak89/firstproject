using Eshop.Application.Services.Interfaces;
using Eshop.Domain.InterFaces;
using Eshop.Domain.Models.Products;
using Eshop.Domain.ViewModels.ProductCategory;

namespace Eshop.Application.Services.Implementations;

public class ProductCategoryService(IProductCategoryRepository productCategoryRepository) : IProductCategoryService
{
    public async Task<CreateproductCategorResult> CreateProductCategoryAsync(CreateproductCategoryViewModel model)
    {
        ProductCategory productCategory = new()
        {
            CategoryTittle = model.CategoryTittle
        };
        if (productCategoryRepository.IsCategoryExists(model.CategoryTittle))
            return CreateproductCategorResult.CategoryDuplicated;
        await productCategoryRepository.CreateAsync(productCategory);
        await  productCategoryRepository.SaveAsync();
        return CreateproductCategorResult.Success;
    }


    public async Task<UpdateProductCategoryResult> UpdateProductCategoryAsync(UpdateProductCategoryViewModel model)
    {
        ProductCategory productCategory = await productCategoryRepository.GetByIdAsync(model.CategoryId);
        if (productCategory == null)
            return UpdateProductCategoryResult.ProductCategoryNotFound;
        
        productCategory.CategoryTittle = model.CategoryTittle;
        
        productCategoryRepository.Update(productCategory);
        await productCategoryRepository.SaveAsync();
        return UpdateProductCategoryResult.Success;
    }

    public async Task<UpdateProductCategoryViewModel> GetProductCategoryForEditByIdAsync(int id)
    {
        ProductCategory productCategory = await productCategoryRepository.GetByIdAsync(id);
        if (productCategory == null)
            return null;
        UpdateProductCategoryViewModel model = new()
        {
            CategoryId =  productCategory.CategoryId,
            CategoryTittle = productCategory.CategoryTittle,
        };
        return model;
    }

    public async Task<bool> DeleteProductCategoryAsync(int id)
    {
        ProductCategory productCategory = await productCategoryRepository.GetByIdAsync(id);
        if (productCategory == null)
            return false;
        productCategoryRepository.Delete(productCategory);
        await productCategoryRepository.SaveAsync();
        return true;
    }

    public async Task<List<ProductCategory>> GetAllAsync()
    {
       return await productCategoryRepository.GetAllAsync();
    }

    public async Task<ProductCategory> GetProductCategoryByIdAsync(int id)
    {
        return await productCategoryRepository.GetByIdAsync(id);
    }
}