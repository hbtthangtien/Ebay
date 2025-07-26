using Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Ebay.Controllers
{
    public class AuthenticateController : Controller
    {
        private readonly IAuthService _auth;
        public AuthenticateController(IAuthService auth) => _auth = auth;

        [HttpGet("/Login")]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("/Login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string? returnUrl = null)
        {
            var userDto = await _auth.AuthenticateAsync(email, password);
            if (userDto is null)
            {
                ModelState.AddModelError("", "Sai email hoặc mật khẩu");
                return View();
            }
            HttpContext.Session.SetString("Id", userDto.Id.ToString());
            HttpContext.Session.SetString("UserEmail", userDto.Email);
            HttpContext.Session.SetString("UserRole", userDto.Role ?? "Customer");
            HttpContext.Session.SetString("Username", userDto.Username??"No Name");
            return LocalRedirect(returnUrl ?? "/");
        }

        [HttpGet("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
