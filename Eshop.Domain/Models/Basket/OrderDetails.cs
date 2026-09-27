using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eshop.Domain.Models.Products;

namespace Eshop.Domain.Models.Basket;

public class OrderDetails
{
    [Key]
    public int DetailsId { get; set; }
    
    public int OrderId { get; set; }
    
    public int ProductId { get; set; }
    
    public int Count { get; set; }
    
    public int Price { get; set; }
    
    #region Relations
    [ForeignKey("OrderId")]
    public Order? Order { get; set; }
    [ForeignKey("ProductId")]
    public Product? Product { get; set; }
    #endregion
}