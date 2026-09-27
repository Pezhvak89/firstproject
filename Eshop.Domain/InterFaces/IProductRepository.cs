using Eshop.Domain.Models.Products;
using Eshop.Domain.ViewModels.Product;

namespace Eshop.Domain.InterFaces;

public interface IProductRepository
{
    Task CreateAsync(Product product);
    Task SaveAsync();
    void Update(Product product);
    void Delete(Product product);
    Task<Product> GetByIdAsync(int productId);
    Task<List<Product>> GetAllAsync();
    // Task<UpdateProductViewModel> GetForEditAsync(int productId);
}