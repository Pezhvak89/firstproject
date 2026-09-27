using System.ComponentModel.DataAnnotations;

namespace Eshop.Domain.ViewModels.User;

public class ForgotPasswordViewModel
{
    [MaxLength(300)]
    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [EmailAddress]
    public string Email { get; set; }
}
public enum  ForgotPasswordResult
{
    Success,
    UserNotFound
}