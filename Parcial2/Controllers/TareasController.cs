using Parcial2.Models.Dtos;
using Parcial2.Services;
using Microsoft.AspNetCore.Mvc;
using Parcial2.Models;
namespace Parcial2.Controllers


{
    [ApiController]
    [Route("api/[controller]")]
    public class TareasController : ControllerBase
    {
        private readonly TareasService _tareasService;

        public TareasController(TareasService tareasService)
        {
            _tareasService = tareasService;
        }

        [HttpGet("Listar Tareas")]
        public async Task<IActionResult> Obtener()
        {
            try
            {
                var users = await _tareasService.GetAllTareaAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("Registrar Tarea")]
        public async Task<IActionResult> Register([FromBody] RegistrarTarea dto)
        {
            try
            {
                var tarea = await _tareasService.RegisterTareaAsync(dto);
                return Ok(new { message = "Tarea creada", tarea.Id, tarea.Titulo });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPut("Actulizar Tarea")]
        public async Task<IActionResult> Update([FromBody] RegistrarTarea dto)
        {
            try
            {
                var tarea = await _tareasService.UpdateTareaAsync(dto);
                return Ok(new { message = "Tarea actualizada", tarea.Titulo });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("Buscar Por Titulo")]
        public async Task<IActionResult> BuscarTitulo(string titulo)
        {
            try
            {
                var tarea = await _tareasService.BuscarTituloAsync(titulo);
                return Ok(new { message = "Titulo encontrado", tarea.Titulo });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("Eliminar Tareas")]
        public async Task<IActionResult> Delete(string titulo)
        {
            try
            {
                await _tareasService.DeleteTareaAsync(titulo);
                return Ok(new { message = "La tarea se ha eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}

