using Eshop.Data.Context;
using Eshop.Domain.InterFaces;
using Eshop.Domain.Models.Users;
using Eshop.Domain.ViewModels.User;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Data.Repositories;

public class UserRepository
    (EshopDbContext dbContext):IUserRepository
{
    public void AddUser(User user)
    {
        dbContext.Users.Add(user);
    }

    public bool IsExistEmail(string email)
    {
        return dbContext.Users.Any(u => u.Email == email);
    }

    public void Save()
    {
        dbContext.SaveChanges();
    }

    public void UpdateUser(User user)
    {
        dbContext.Users.Update(user);
    }

    public User GetUserByEmail(string email)
    {
        return dbContext.Users.FirstOrDefault(u => u.Email == email);
    }

    public User getUserByConfirmCode(string code)
    {
        return dbContext.Users.FirstOrDefault(u => u.ConfirmCode == code);
    }

    public User GetUserById(int userId)
    {
        return dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefault(m => m.UserId == userId);
    }

    public EditUserViewModel GetUserForEdit(int userId)
    {
        return  dbContext.Users.
            Select(u=>new EditUserViewModel
            {
                UserId = u.UserId,
                RoleId = u.RoleId,
                UserName = u.UserName,
                Email = u.Email,
                NewPassword = u.Password,
                IsActive = u.IsActive
            }).FirstOrDefault(u=>u.UserId == userId);
    }

    public void DeleteUser(User user)
    {
       dbContext.Users.Remove(user);
    }

    public void DeleteUser(int userId)
    {
        var user=GetUserById(userId);
        DeleteUser(user);
    }

    public List<User> GetUsersForAdmin(int skip, int take, string search)
    {
        if (!string.IsNullOrEmpty(search))
        {
            return dbContext.Users.Include(u=> u.Role).Where(
                u=> u.UserName.Contains(search) || u.Email.Contains(search)
                ).ToList();
        }
        return dbContext.Users.Include(u=> u.Role)
            .OrderBy(u=>u.CreateDate)
            .Skip(skip)
            .Take(take)
            .ToList();
    }

    public int GetUsersCount()
    {
       return dbContext.Users.Count();
    }
}