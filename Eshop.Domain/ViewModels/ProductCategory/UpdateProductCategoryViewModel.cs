using System.ComponentModel.DataAnnotations;

namespace Eshop.Domain.ViewModels.ProductCategory;

public class UpdateProductCategoryViewModel
{
    public int CategoryId { get; set; }
    [Display(Name ="عنوان دسته")]
    [Required(ErrorMessage = "لظفا {0} را وارد کنید")]
    [MaxLength(400,ErrorMessage = "تعداد کاراکتر وارد شده مجاز نیست")]
    public string CategoryTittle { get; set; }
}

public enum UpdateProductCategoryResult
{
    Success,
    ProductCategoryNotFound
}