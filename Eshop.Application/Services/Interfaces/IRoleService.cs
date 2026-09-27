using Eshop.Domain.Models.Users;

namespace Eshop.Application.Services.Interfaces;

public interface IRoleService
{
    List<Role> GetAllRoles();
}