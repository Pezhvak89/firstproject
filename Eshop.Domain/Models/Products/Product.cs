using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Domain.Models.Basket;

namespace Eshop.Domain.Models.Products
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Display(Name = "دسته بندی محصول")]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "عنوان محصول")]
        [MaxLength(400)]
        public string Title { get; set; }
        [Required]
        [Display(Name = "توضیح مختصر محصول")]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }
        [Required]
        [Display(Name = "توضیح محصول")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [Required]
        [Display(Name = "قیمت کالا")]
        public int Price { get; set; }

        [ForeignKey("CategoryId")]
        public ProductCategory? ProductCategory { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public List<OrderDetails>? OrderDetails { get; set; }
    }
}
