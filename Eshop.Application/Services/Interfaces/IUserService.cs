
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Domain.Models.Users;
using Eshop.Domain.ViewModels.User;

namespace Eshop.Application.Services.Interfaces
{
    public interface IUserService
    {
        RegisterResult RegisterUser(RegisterUserViewModel model);
        LoginResult LoginUser(LoginUserViewModel model);
        ChangePasswordResult ChangePassword(string email,ChangePasswordViewModel model);
        ForgotPasswordResult Forgotpassword(ForgotPasswordViewModel model);
        ResetPasswordResult ResetPassword(ResetPasswordViewModel model);
        int GetPageUserCount(int pageSize);
        List<User> GetUsersForAdmin(int pageId, int pagesize, string search);
        User GetUserById(int userId);
        CreateUserResult CreateUser(CreateUserViewModel model);
        EditUserViewModel GetUserForEdit(int userId);
        EditUserResult UpdateUser(EditUserViewModel model);
        bool DeleteUser(int userId);
        
        
       
    }
}
