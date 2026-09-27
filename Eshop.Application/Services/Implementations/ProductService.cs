using Eshop.Application.Services.Interfaces;
using Eshop.Domain.InterFaces;
using Eshop.Domain.Models.Products;
using Eshop.Domain.ViewModels.Product;

namespace Eshop.Application.Services.Implementations;

public class ProductService
    (IProductRepository productRepository,
        IProductCategoryRepository productCategoryRepository,
        IImageRepository imageRepository):IProductService
{
    public async Task<CreateProductResult> CreateProductAsync(CreateProductViewModel model)
    {
        if (!await productCategoryRepository.ExistsAsync(model.CategoryId))
            return CreateProductResult.ProductCaategoryNotFound;
        Product product = new()
        {
            CategoryId = model.CategoryId,
            Title = model.Title,
            ShortDescription = model.ShortDescription,
            Description = model.Description,
            Price = model.Price
        };
        await productRepository.CreateAsync(product);
        await productRepository.SaveAsync();
         if (model.productImgs != null && model.productImgs .Any())
         {
           foreach (var formFile in model.productImgs)
            {
                string imageName=Guid.NewGuid().ToString()+
                                 Path.GetExtension(formFile.FileName);
                ProductImage productImage = new()
                { 
                    ProductId = product.ProductId,
                   ImageName = imageName
                };
                await imageRepository.CreateProductImageAsync(productImage);
                string savePath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/productImages",imageName);
                       
               using (var stream = System.IO.File.Create(savePath))
                 {
                     formFile.CopyTo(stream);
                 }
        
                await imageRepository.SaveAsync();
             }
        }
        return CreateProductResult.Success;

    }

    public async Task<UpdateProductResult> UpdateProductAsync(UpdateProductViewModel model)
    {
        if (!await productCategoryRepository.ExistsAsync(model.CategoryId))
            return UpdateProductResult.ProductCategoryNotFound;
        Product product = await productRepository.GetByIdAsync(model.ProductId);
        if (product == null)
            return UpdateProductResult.ProductNotFound;
        product.CategoryId = model.CategoryId;
        product.Title = model.Title;
        product.ShortDescription = model.ShortDescription;
        product.Description = model.Description;
        product.Price = model.Price;
        productRepository.Update(product);
        await productRepository.SaveAsync();
        if (model.productImgs != null && model.productImgs.Any())
        {
            foreach (var formFile in model.productImgs)
            {
                string imageName = Guid.NewGuid().ToString() +
                                   Path.GetExtension(formFile.FileName);
                ProductImage productImage = new()
                {
                    ProductId = product.ProductId,
                    ImageName = imageName,
                };
                await imageRepository.CreateProductImageAsync(productImage);
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/productImages", imageName);

                using (var stream = System.IO.File.Create(savePath))
                {
                    formFile.CopyTo(stream);
                }

               await imageRepository.SaveAsync();
            }
        }
        return UpdateProductResult.Success;
    }

    public async Task<UpdateProductViewModel> GetForEditAsync(int productId)
    {
        var product = await productRepository.GetByIdAsync(productId);
        if (product == null)
            return null;
     
        return new UpdateProductViewModel
        {
            ProductId = product.ProductId,
            CategoryId = product.CategoryId,
            Title = product.Title,
            ShortDescription = product.ShortDescription,
            Description = product.Description,
            Price = product.Price,
            ProductImages = product.ProductImages,
        };
    }

    public async Task<DeleteProductResult> DeleteProductAsync(int id)
    {
        Product product = await productRepository.GetByIdAsync(id);
        if(product==null)
            return DeleteProductResult.ProductNotFound;
        List<ProductImage> productImage = new List<ProductImage>();
        productImage.AddRange(product.ProductImages);
        foreach (var image in productImage)
        {
            string deletePath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/productImages",image.ImageName);
            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }
            imageRepository.DeleteProductImage(image);
           await imageRepository.SaveAsync();
        }
        productRepository.Delete(product);
        await productRepository.SaveAsync();
        return DeleteProductResult.Succsess;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        return await productRepository.GetAllAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await productRepository.GetByIdAsync(id);
    }

    public async Task DeleteImageAsync(int id)
    {
        var image = await GetProductImageByIdAsync(id);
        imageRepository.DeleteProductImage(image);
        string deletePath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/productImages",image.ImageName);
        if (System.IO.File.Exists(deletePath))
        {
            System.IO.File.Delete(deletePath);
        }

      await imageRepository.SaveAsync();
    }

    public async Task<ProductImage> GetProductImageByIdAsync(int id)
    {
        return await imageRepository.GetProductImageByIdAsync(id);
    }
}