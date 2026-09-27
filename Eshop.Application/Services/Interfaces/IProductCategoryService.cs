using Eshop.Domain.Models.Products;
using Eshop.Domain.ViewModels.ProductCategory;

namespace Eshop.Application.Services.Interfaces;

public interface IProductCategoryService
{
    Task<CreateproductCategorResult> CreateProductCategoryAsync(CreateproductCategoryViewModel model);
    Task<UpdateProductCategoryResult> UpdateProductCategoryAsync(UpdateProductCategoryViewModel model);
    Task<UpdateProductCategoryViewModel> GetProductCategoryForEditByIdAsync(int id);
    Task<bool> DeleteProductCategoryAsync(int id);
    Task<List<ProductCategory>> GetAllAsync();
    Task<ProductCategory> GetProductCategoryByIdAsync(int id);
}   