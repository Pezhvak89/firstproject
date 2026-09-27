using System.ComponentModel.DataAnnotations;

namespace Eshop.Domain.ViewModels.User;

public class ResetPasswordViewModel
{
    [MaxLength(6)]
    [Display(Name = "کد یکبار مصرف")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string ConfirmCode { get; set; }
    [MaxLength(400)]
    [Display(Name = "کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Password)]
        
    public string Password { get; set; }
    [MaxLength(400)]
    [Display(Name = "تکرار کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "پسوردها یکسان نیست")]
    public string RePassword { get; set; }
}
public enum ResetPasswordResult
{
    UserNotFound,
    Failed,
    Success
}