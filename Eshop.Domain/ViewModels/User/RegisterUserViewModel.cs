using System.ComponentModel.DataAnnotations;

namespace Eshop.Domain.ViewModels.User;

public class RegisterUserViewModel
{
    [MaxLength(300)]
    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [EmailAddress]
    public string Email { get; set; }
    [MaxLength(400)] 
    [Display(Name = "کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [MaxLength(400)]
    [Display(Name = "تکرار کلمه عبور")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [DataType(DataType.Password)]
    [Compare("Password",ErrorMessage = "پسوردها یکسان نیست")]
    public string RePassword { get; set; }
   
    public bool ConfirmRules { get; set; }
}

public enum RegisterResult
{
    Success,
    Failed,
    EmailInvalid
    
}