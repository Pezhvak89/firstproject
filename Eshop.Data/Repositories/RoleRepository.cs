using Eshop.Data.Context;
using Eshop.Domain.InterFaces;
using Eshop.Domain.Models.Users;

namespace Eshop.Data.Repositories;

public class RoleRepository
    (EshopDbContext context):IRoleRepository
{
    public List<Role> GetAllRoles()
    {
        return context.Roles.ToList();
    }
}