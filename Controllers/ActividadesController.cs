using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NetIdentity.Controllers
{
    
    public class ActividadesController : Controller
    {
        [Authorize(Policy = "SoloFemenino")]
        public IActionResult Index()
        {
            return View();
        }
        //vista->controlador->modelo
        //modelo->controlador->vista
        //vista->controlador
        [Authorize(Policy = "menoresEdad")]
        public IActionResult Deportes()
        {
            return View();
        }

        [Authorize(Policy = "SoloFemenino")]
        public IActionResult Tareas()
        {
            return View();
        }
    }

}
