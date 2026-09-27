using Eshop.Application.Generators;
using Eshop.Application.Security;
using Eshop.Application.Services.Interfaces;
using Eshop.Domain.InterFaces;
using Eshop.Domain.Models.Users;
using Eshop.Domain.ViewModels.User;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Eshop.Application.Services.Implementations;

public class UserService(
    IUserRepository userRepository): IUserService
{
    public RegisterResult RegisterUser(RegisterUserViewModel model)
    {
        if (IsEmailExist(model.Email))
            return RegisterResult.EmailInvalid;
        try
        {
            User user = new User
            {
                RoleId = 2,
                UserName = null,
                Email = model.Email,
                Password = SecretHasher.Hash(model.Password),
                IsActive = true,
                FailedLoginAttemp = 0,
                CreateDate = DateTime.Now,
            };
            userRepository.AddUser(user);
            userRepository.Save();
            return RegisterResult.Success;
        }
        catch (Exception ex)
        {
            return RegisterResult.Failed;
        }
    }

    public bool IsEmailExist(string email)
    {
        return userRepository.IsExistEmail(email);
    }

    public LoginResult LoginUser(LoginUserViewModel model)
    {
        User user = userRepository.GetUserByEmail(model.Email.ToLower());
        if (user == null)
        {
            return LoginResult.Failed();
        }

        if (!SecretHasher.Verify(model.Password, user.Password))
        {
            user.FailedLoginAttemp++;
            userRepository.UpdateUser(user);
            userRepository.Save();
            if (user.FailedLoginAttemp >= 3)
            {
                if (user.LockedOutTime.HasValue && user.LockedOutTime.Value > DateTime.Now)
                    return LoginResult.Locked(user.LockedOutTime);
                user.LockedOutTime = DateTime.Now.AddMinutes(15);
                userRepository.UpdateUser(user);
                userRepository.Save();
                return LoginResult.Locked(user.LockedOutTime);
            }

            return LoginResult.Failed();
        }
        else
        {
            if (user.LockedOutTime.HasValue && user.LockedOutTime.Value > DateTime.Now)
            {
                return LoginResult.Locked(user.LockedOutTime);
            }
            else
            {
                user.FailedLoginAttemp = 0;
                user.LockedOutTime = null;
                userRepository.UpdateUser(user);
                userRepository.Save();
                var userdto = new LoginUserDto
                {
                    UserId = user.UserId,
                    RoleId = user.RoleId
                };
                return LoginResult.Success(userdto);
            }



        }
    }

    public ChangePasswordResult ChangePassword(string email, ChangePasswordViewModel model)
    {
        User user = userRepository.GetUserByEmail(email);
        if (user == null)
            return ChangePasswordResult.Failed;
        if (!SecretHasher.Verify(model.OldPassword, user.Password))
            return ChangePasswordResult.InvalidPassword;
        user.Password = SecretHasher.Hash(model.Password);
        userRepository.UpdateUser(user);
        userRepository.Save();
        return ChangePasswordResult.Success;
    }

    public ForgotPasswordResult Forgotpassword(ForgotPasswordViewModel model)
    {
        User user = userRepository.GetUserByEmail(model.Email.ToLower());
        if (user == null)
            return ForgotPasswordResult.UserNotFound;
        user.ConfirmCode = NumberGenerators.RandomNumberGenerator().ToString();
        userRepository.UpdateUser(user);
        userRepository.Save();
        //sendEmail
        
        // string body = $"<h1>کد یک بار مصرف:{user.ConfirmCode}</h1>";
        // emailSender.SendEmail(user.Email,"فراموشی کلمه عبور",body);

        return ForgotPasswordResult.Success;
    }

    public ResetPasswordResult ResetPassword(ResetPasswordViewModel model)
    {
        User user = userRepository.getUserByConfirmCode(model.ConfirmCode);
        if (user == null)
            return ResetPasswordResult.UserNotFound;
        user.Password = SecretHasher.Hash(model.Password);
        user.ConfirmCode = null;
        userRepository.UpdateUser(user);
        userRepository.Save();
        return ResetPasswordResult.Success;
    }

    public int GetPageUserCount(int pageSize)
    {
       int countuser=userRepository.GetUsersCount();
        return countuser/pageSize;
    }

    public List<User> GetUsersForAdmin(int pageId, int pagesize, string search)
    {
       int skip=(pageId-1)*pagesize;
        return userRepository.GetUsersForAdmin(skip, pagesize,search);
    }

    public User GetUserById(int userId)
    {
        return userRepository.GetUserById(userId);
    }

    public CreateUserResult CreateUser(CreateUserViewModel model)
    {
        if (IsEmailExist(model.Email))
            return CreateUserResult.EmailInvalid;
        User user = new User
        {
            RoleId = model.RoleId,
            UserName = model.UserName,
            Email = model.Email,
            Password = SecretHasher.Hash(model.Password),
            IsActive = model.IsActive,
            CreateDate = DateTime.Now,
        };
        userRepository.AddUser(user);
        userRepository.Save();
        return CreateUserResult.Success;
    }

    public EditUserViewModel GetUserForEdit(int userId)
    {
        return userRepository.GetUserForEdit(userId);
    }

    public EditUserResult UpdateUser(EditUserViewModel model)
    {
        var user=userRepository.GetUserById(model.UserId);
        if (user == null)
            return EditUserResult.UserNotFound;
        user.RoleId=model.RoleId;
        user.UserName=model.UserName;
        user.Email=model.Email;
        user.IsActive = model.IsActive;
        if(!string.IsNullOrWhiteSpace(model.NewPassword))
            user.Password = SecretHasher.Hash(model.NewPassword);
        userRepository.UpdateUser(user);
        userRepository.Save();
        return EditUserResult.Success;
    }

    public bool DeleteUser(int userId)
    {
        var user=userRepository.GetUserById(userId);
        if (user == null)
            return false;
        userRepository.DeleteUser(user);
        userRepository.Save();
        return true;
    }
}