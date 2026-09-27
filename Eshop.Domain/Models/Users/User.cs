using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshop.Domain.Models.Basket;

namespace Eshop.Domain.Models.Users;

public class User
{
    [Key]
    public int UserId { get; set; }
    [Required]
    public int RoleId { get; set; }
    [MaxLength(300)]
    [Display(Name ="نام کاربری")]
    public string? UserName { get; set; }
    [MaxLength(300)]
    [Required]
    [EmailAddress]
    [Display(Name = "ایمیل")]
    public string Email { get; set; }
    [MaxLength(400)]
    [Required]
    public string Password { get; set; }
    [Display(Name = "فعال/غیر فعال")]

    public bool IsActive { get; set; }
    [Required]
    public int FailedLoginAttemp { get; set; }

    public DateTime? LockedOutTime { get; set; }
    public string? ConfirmCode { get; set; }
    [Required]
    [Display(Name = "تاریخ ثبت نام")]
    public DateTime CreateDate { get; set; }

    [ForeignKey("RoleId")]
    public Role? Role { get; set; }

    public List<Order>? Orders { get; set; }
}