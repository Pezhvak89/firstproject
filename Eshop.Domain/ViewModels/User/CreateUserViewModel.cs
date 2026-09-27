using System.ComponentModel.DataAnnotations;

namespace Eshop.Domain.ViewModels.User;

public class CreateUserViewModel
{
    
    public int UserId { get; set; }
    [Display(Name ="نقش کاربر")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    public int RoleId { get; set; }
    [MaxLength(300)]
    [Display(Name ="نام کاربری")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    public string UserName { get; set; }
    [MaxLength(300)]
    [EmailAddress]
    [Display(Name = "ایمیل")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    public string Email { get; set; }
    [MaxLength(400)]
    [Display(Name = "کلمه عبور")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    public string Password { get; set; }
    [Display(Name = "فعال")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
    public bool IsActive { get; set; }
}
public enum CreateUserResult
{
    Success,
    EmailInvalid,
    Error
}