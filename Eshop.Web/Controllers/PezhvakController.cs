using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eshop.Web.Controllers;
[Authorize]
public class PezhvakController : Controller
{
    public IActionResult Test1()
    {
        return Content("Hello World");
    }
    public IActionResult Test2()
    {
        return Content("Hello World2");
    }
}