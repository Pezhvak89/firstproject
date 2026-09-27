using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Domain.ViewModels.User
{
    public class ChangePasswordViewModel
    {
        [MaxLength(400)]
        [Display(Name = "کلمه عبور فعلی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
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

    public enum ChangePasswordResult
    {
        Success,
        Failed,
        InvalidPassword,
    }
}
