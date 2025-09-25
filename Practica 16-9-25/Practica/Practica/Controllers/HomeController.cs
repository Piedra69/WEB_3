using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Practica.Controllers
{
    public class HomeController : Controller
    {
        // Credenciales DEMO: usuario -> (password, rol)
        private static readonly Dictionary<string, (string Pass, string Rol)> Creds = new()
        {
            ["admin"] = ("1234", "admin"),
            ["cliente"] = ("4321", "cliente"),
            ["empleado"] = ("1111", "empleado")
        };

        [HttpGet]
        public IActionResult Index()
        {
            // Renderiza el formulario
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string usuario, string password)
        {
            // Validación básica (sin models)
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Completa usuario y contraseña.";
                return View();
            }

            var key = usuario.Trim().ToLowerInvariant();
            var pass = password.Trim();

            if (Creds.TryGetValue(key, out var info) && info.Pass == pass)
            {
                ViewBag.Mensaje = info.Rol switch
                {
                    "admin" => "Hola Admin",
                    "cliente" => "Hola Cliente",
                    "empleado" => "Hola Empleado",
                    _ => "Hola"
                };
                // Mostramos el saludo en la misma vista
                return View();
            }

            ViewBag.Error = "Usuario o contraseña inválidos.";
            return View();
        }
    }
}
