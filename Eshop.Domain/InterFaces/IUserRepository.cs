using Eshop.Domain.Models.Users;
using Eshop.Domain.ViewModels.User;

namespace Eshop.Domain.InterFaces;

public interface IUserRepository
{
    void AddUser(User user);
    bool IsExistEmail(string email);
    void Save();
    void UpdateUser(User user);
    User GetUserByEmail(string email);
    User getUserByConfirmCode(string code);
    User GetUserById(int userId);
    EditUserViewModel GetUserForEdit(int userId);
    void DeleteUser(User user);
    void DeleteUser(int  userId);
    #region AdminPanel
    List<User> GetUsersForAdmin(int skip, int take,string search);
    int GetUsersCount();
    #endregion

}