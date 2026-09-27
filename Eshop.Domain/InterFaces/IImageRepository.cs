using Eshop.Domain.Models.Products;

namespace Eshop.Domain.InterFaces;

public interface IImageRepository
{
    Task CreateProductImageAsync(ProductImage productImage);
    Task SaveAsync();
    void DeleteProductImage(ProductImage productImage);
    Task<ProductImage> GetProductImageByIdAsync(int id);
}