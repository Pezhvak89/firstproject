using Eshop.Domain.Models.Products;

namespace Eshop.Domain.InterFaces;

public interface IProductCategoryRepository
{
    Task CreateAsync(ProductCategory productCategory);
    Task SaveAsync();
    void Update(ProductCategory productCategory);
    void Delete(ProductCategory productCategory);
    Task<ProductCategory> GetByIdAsync(int id);
    Task<List<ProductCategory>> GetAllAsync();
   bool IsCategoryExists(string title);
  Task<bool> ExistsAsync(int productCategoryId);
}