using Eshop.Data.Context;
using Eshop.Domain.InterFaces;
using Eshop.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Data.Repositories;

public class ProductCategoryRepository(EshopDbContext context) : IProductCategoryRepository
{
    public async Task CreateAsync(ProductCategory productCategory)
        => await context.ProductCategories.AddAsync(productCategory);

    public async Task SaveAsync()
        => await context.SaveChangesAsync();

    public void Update(ProductCategory productCategory)
        => context.ProductCategories.Update(productCategory);

    public void Delete(ProductCategory productCategory)
        => context.ProductCategories.Remove(productCategory);

    public async Task<ProductCategory> GetByIdAsync(int id)
        => await context.ProductCategories
            .FirstOrDefaultAsync(pc => pc.CategoryId == id);

    public async Task<List<ProductCategory>> GetAllAsync()
        => await context.ProductCategories.ToListAsync();

    public bool IsCategoryExists(string title)
    {
        return context.ProductCategories.Any(pc => pc.CategoryTittle == title);
    }

    public async Task<bool> ExistsAsync(int productCategoryId)
        => await context.ProductCategories.AnyAsync(pc => pc.CategoryId == productCategoryId);
}