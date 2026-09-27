using Eshop.Application.Services.Implementations;
using Eshop.Application.Services.Interfaces;
using Eshop.Data.Repositories;
using Eshop.Domain.InterFaces;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Eshop.Ioc.Container;

public static class IocContainer
{
    public static void RegisteredServices(this IServiceCollection service)
    {
        #region Services

        service.AddScoped<IUserService, UserService>();
        service.AddScoped<IRoleService, RoleService>();
        service.AddScoped<IProductCategoryService, ProductCategoryService>();
        service.AddScoped<IProductService, ProductService>();
        
        // service.AddSingleton<IEmailSender, EmailSender>();

        #endregion
        #region Repositories
        service.AddScoped<IUserRepository, UserRepository>();
        service.AddScoped<IRoleRepository, RoleRepository>();
        service.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<IImageRepository, ImageRepository>();
        #endregion
    }
}