using Eshop.Data.Context;
using Eshop.Domain.InterFaces;
using Eshop.Domain.Models.Products;
using Eshop.Domain.ViewModels.Product;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Data.Repositories;

public class ProductRepository(EshopDbContext context) : IProductRepository
{
    public async Task CreateAsync(Product product)
        => await context.Products.AddAsync(product);

    public async Task SaveAsync()
        => await context.SaveChangesAsync();

    public void Update(Product product)
        => context.Products.Update(product);

    public void Delete(Product product)
        => context.Products.Remove(product);

    public async Task<Product> GetByIdAsync(int productId)
        => await context.Products
            .Include(p=>p.ProductImages)
            .Include(p => p.ProductCategory)
            .FirstOrDefaultAsync(p => p.ProductId == productId);

    public async Task<List<Product>> GetAllAsync()
        => await context.Products
            .Include(p => p.ProductImages)
            .Include(p=>p.ProductCategory)
            .ToListAsync();

//     public async Task<UpdateProductViewModel> GetForEditAsync(int productId)
//   => await context.Products
//       .Include(p => p.ProductImages)
//       .Select(p=> new UpdateProductViewModel
//       {
//           ProductId = p.ProductId,
//           CategoryId = p.CategoryId,
//           Title = p.Title,
//           ShortDescription = p.ShortDescription,
//           Description = p.Description,
//           Price = p.Price,
//           ProductImages =  p.ProductImages
//       })
//       .FirstOrDefaultAsync(p => p.ProductId == productId);
 }