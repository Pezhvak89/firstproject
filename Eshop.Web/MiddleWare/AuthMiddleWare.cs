using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Eshop.Web.MiddleWare;

public class AuthMiddleWare
{
    private readonly RequestDelegate _next;

    public AuthMiddleWare(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.ToString().ToLower().StartsWith("/admin"))
        {
            if (context.User.Identity.IsAuthenticated)
            {
                string roleId = context.User.Claims.FirstOrDefault(c=> c.Type=="Role")?.Value;
                if (!string.IsNullOrWhiteSpace(roleId) && int.Parse(roleId) == 1)
                {
                     await _next(context);
                }
                else
                {
                    context.Response.Redirect("/Accsess-Denied");
                } 
            }
            else
            {
                context.Response.Redirect("/Login");
            }
           
        }
        else
        {
            await _next(context);
        }
      
    }
}