using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshop.Domain.Models.Users;

namespace Eshop.Domain.Models.Basket;

public class Order
{
    [Key]
    public int OrderId { get; set; }
    
    public int UserId { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public bool IsFinaly { get; set; }

    #region Relations
    [ForeignKey("UserId")]
    public User? User { get; set; }

    public List<OrderDetails>? OrderDetails { get; set; }
    

    #endregion
}