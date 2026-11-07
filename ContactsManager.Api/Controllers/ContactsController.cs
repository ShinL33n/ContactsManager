using ContactsManager.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.Api.Controllers;

[ApiController]
//[Authorize]
public class ContactsController : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<ApiResponse> GetAllAsync()
    {
        return View();
    }
}
