using Eshop.Domain.Models.Products;
using Eshop.Domain.ViewModels.Product;
using Eshop.Domain.ViewModels.ProductCategory;

namespace Eshop.Application.Services.Interfaces;

public interface IProductService
{
  Task<CreateProductResult> CreateProductAsync(CreateProductViewModel model);
  Task<UpdateProductResult> UpdateProductAsync(UpdateProductViewModel model);
  Task<UpdateProductViewModel> GetForEditAsync(int productId);
  Task<DeleteProductResult> DeleteProductAsync(int id);
  Task<List<Product>> GetAllAsync();
  Task<Product> GetProductByIdAsync(int id);
  Task DeleteImageAsync(int id);
  Task<ProductImage> GetProductImageByIdAsync(int id);
}