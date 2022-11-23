using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorStore.Model.Services.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BlazorStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserServices _userServices;

        public AccountController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("/account/authenticate")]
        public async Task<IActionResult> Authenticate(LoginViewModel loginViewModel)
        {
            if (string.IsNullOrWhiteSpace(loginViewModel.ReturnUrl))
            {
                loginViewModel.ReturnUrl = "~/";
            }
            else if (!loginViewModel.ReturnUrl.StartsWith("~/"))
            {
                loginViewModel.ReturnUrl = "~/" + loginViewModel.ReturnUrl;
            }

            var user = await _userServices.GetUserByCredentialsAsync(loginViewModel.Email, loginViewModel.Password);
            if (user!=null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                };
                if (user.IsAdmin)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "admin"));
                }

                // Creamos el principal:
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Generamos la cookie:
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                // Redirigimos a la Home:
                return LocalRedirect(loginViewModel.ReturnUrl);
            }
            return LocalRedirect("/login?error=Invalid+credentials&returnUrl=" + loginViewModel.ReturnUrl);
        }

        [HttpPost("/account/logout")]
        public async Task<IActionResult> Logout(string returnUrl = "~/")
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect(returnUrl);
        }
    }

    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; } = "popo";
    }

}
