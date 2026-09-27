using Eshop.Data.Context;
using Eshop.Domain.InterFaces;
using Eshop.Domain.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Data.Repositories;

public class ImageRepository
    (EshopDbContext context):IImageRepository
{
    public async Task CreateProductImageAsync(ProductImage productImage)
 => await context.ProductImages.AddAsync(productImage);

    public async Task SaveAsync()
 => await context.SaveChangesAsync();

    public void DeleteProductImage(ProductImage productImage)
    {
        context.ProductImages.Remove(productImage);
    }

    public async Task<ProductImage> GetProductImageByIdAsync(int id)
    {
        return await context.ProductImages
            .FirstOrDefaultAsync(pi=>pi.ImageId == id);
    }
}