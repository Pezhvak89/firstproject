using System.ComponentModel.DataAnnotations;

namespace Eshop.Domain.Models.Users;

public class Role
{
    [Key]
    public int RoleId { get; set; }
    [MaxLength(400)]
    [Required]
    public string RoleTitle { get; set; }

    public List<User>? Users { get; set; }
}