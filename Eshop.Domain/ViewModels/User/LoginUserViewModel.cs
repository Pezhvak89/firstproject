using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace Eshop.Domain.ViewModels.User;

public class LoginUserViewModel
{
    // public int? UserId { get; set; }
    // public int? RoleId { get; set; }
    public string? ReturnUrl { get; set; }
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

    public bool Rememberme { get; set; }
}
public class LoginResult
{
    public bool IsSuccess { get; set; }
    public bool IsLocked { get; set; }
    public DateTime? LockedOut { get; set; }
    public LoginUserDto User { get; set; }
    private LoginResult()
    {
       
    }

    public static LoginResult Success(LoginUserDto user)
        => new LoginResult() { IsSuccess = true, User = user };

    public static LoginResult Failed()
        => new LoginResult() { IsSuccess = false };
    public static LoginResult Locked(DateTime? lockedOut)
    =>new LoginResult(){IsLocked = true, LockedOut = lockedOut};
    
}