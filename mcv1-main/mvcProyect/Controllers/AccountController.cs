using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mvcProyect.Data;
using mvcProyect.Models;
using System.Security.Claims;

namespace mvcProyect.Controllers
{
    public class AccountController : Controller
    {
        private readonly ArtesaniasDBContext _context;

        public AccountController(ArtesaniasDBContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Activo);
            if (usuario != null && BCrypt.Net.BCrypt.Verify(password, usuario.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Email),
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.Rol),
                    new Claim("NombreCompleto", usuario.NombreCompleto),
                    new Claim("FechaNacimiento", usuario.FechaNacimiento.ToString("yyyy-MM-dd"))
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Email o contraseña incorrectos";
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string nombreCompleto, DateTime fechaNacimiento)
        {
            var edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad)) edad--;

            if (edad < 18)
            {
                ModelState.AddModelError("", "Debes tener al menos 18 años para registrarte.");
                return View();
            }

            if (_context.Usuarios.Any(u => u.Email == email))
            {
                ModelState.AddModelError("", "El email ya está registrado");
                return View();
            }

            var usuario = new Usuario
            {
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                NombreCompleto = nombreCompleto,
                FechaNacimiento = fechaNacimiento,
                Rol = "Usuario",
                Activo = true
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Role, usuario.Rol),
                new Claim("FechaNacimiento", usuario.FechaNacimiento.ToString("yyyy-MM-dd"))
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() => View();
    }
}
