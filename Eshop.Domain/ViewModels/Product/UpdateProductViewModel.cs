using System.ComponentModel.DataAnnotations;
using Eshop.Domain.Models.Products;
using Microsoft.AspNetCore.Http;

namespace Eshop.Domain.ViewModels.Product;

public class UpdateProductViewModel
{
    [Key]
    public int ProductId { get; set; }
    [Display(Name = "دسته بندی محصول")]
    public int CategoryId { get; set; }
    [Required]
    [Display(Name = "عنوان محصول")]
    [MaxLength(400)]
    public string Title { get; set; }
    [Required]
    [Display(Name = "توضیح مختصر محصول")]
    [DataType(DataType.MultilineText)]
    public string ShortDescription { get; set; }
    [Required]
    [Display(Name = "توضیح محصول")]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; }
    [Required]
    [Display(Name = "قیمت کالا")]
    public int Price { get; set; }

    public List<IFormFile>? productImgs { get; set; }
    public List<ProductImage>? ProductImages { get; set; }
}
public enum UpdateProductResult
{
    Success,
    Error,
    ProductCategoryNotFound,
    ProductNotFound
}