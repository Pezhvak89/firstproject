using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Eshop.Domain.ViewModels.Product;

public class CreateProductViewModel
{
    
    [Display(Name = "دسته بندی محصول")]
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "لظفا {0} را وارد کنید")]
    [MaxLength(400,ErrorMessage = "تعداد کاراکتر وارد شده مجاز نیست")]
    [Display(Name = "عنوان محصول")]
    public string Title { get; set; }
    [Display(Name = "توضیح مختصر محصول")]
    [Required(ErrorMessage = "لظفا {0} را وارد کنید")]
    [MaxLength(400,ErrorMessage = "تعداد کاراکتر وارد شده مجاز نیست")]
    [DataType(DataType.MultilineText)]
    public string ShortDescription { get; set; }
    [Display(Name = "توضیح محصول")]
    [Required(ErrorMessage = "لظفا {0} را وارد کنید")]
    [DataType(DataType.MultilineText)]
    public string Description { get; set; }
    [Display(Name = "قیمت کالا")]
    [Required(ErrorMessage = "لظفا {0} را وارد کنید")]
    public int Price { get; set; }

    public List<IFormFile>? productImgs { get; set; }
}


public enum CreateProductResult
{
    Success,
    ProductCaategoryNotFound,
    Error
}