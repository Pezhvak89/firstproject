using Eshop.Application.Services.Interfaces;
using Eshop.Domain.InterFaces;
using Eshop.Domain.Models.Users;

namespace Eshop.Application.Services.Implementations;

public class RoleService
    (IRoleRepository roleRepository):IRoleService
{
    public List<Role> GetAllRoles()
    {
        return roleRepository.GetAllRoles();
    }
}