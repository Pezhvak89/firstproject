using Eshop.Domain.Models.Users;

namespace Eshop.Domain.InterFaces;

public interface IRoleRepository
{
    List<Role> GetAllRoles();
}