using Gestion.Models.DTOs;
using Gestion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecursoController : ControllerBase
    {
        private readonly RecursoService _recursoService;

        public RecursoController(RecursoService recursoService)
        {
            _recursoService = recursoService;
        }

        [HttpGet("obtener")]
        public async Task<IActionResult> Obtener()
        {
            try
            {
                var recursos = await _recursoService.GetAllRecursoAsync();
                return Ok(recursos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RecursoDto dto)
        {
            try
            {
                var recurso = await _recursoService.RegisterRecursoAsync(dto);
                return Ok(new { message = "Recurso creado", recurso.Id, recurso.Nombre });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] RecursoDto dto)
        {
            try
            {
                var recurso = await _recursoService.UpdateRecursoAsync(dto);
                return Ok(new { message = "Recurso actualizado", recurso.Nombre });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _recursoService.DeleteRecursoAsync(id);
                return Ok(new { message = "Recurso eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
