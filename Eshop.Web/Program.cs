using Eshop.Data.Context;
using Eshop.Ioc.Container;
using Eshop.Web.MiddleWare;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region RegisterServices

builder.Services.RegisteredServices();

#endregion

#region ConnectionString

builder.Services.AddDbContext<EshopDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EshopConnectionString"));
});

#endregion

#region AddAuthentication

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme=CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath="/Login";
        options.LogoutPath="/Logout";
        options.ExpireTimeSpan=TimeSpan.FromDays(14);
    });

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<AuthMiddleWare>();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
