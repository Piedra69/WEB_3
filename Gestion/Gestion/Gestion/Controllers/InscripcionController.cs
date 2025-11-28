using Gestion.Models.DTOs;
using Gestion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscripcionController : ControllerBase
    {
        private readonly InscripcionService _inscripcionService;

        public InscripcionController(InscripcionService inscripcionService)
        {
            _inscripcionService = inscripcionService;
        }

        [HttpGet("obtener")]
        public async Task<IActionResult> Obtener()
        {
            try
            {
                var inscripciones = await _inscripcionService.GetAllInscripcionAsync();
                return Ok(inscripciones);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] InscripcionDto dto)
        {
            try
            {
                var inscripcion = await _inscripcionService.RegisterInscripcionAsync(dto);
                return Ok(new
                {
                    message = "Inscripción creada",
                    inscripcion.Id,
                    inscripcion.CursoId,
                    inscripcion.UserId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] InscripcionDto dto)
        {
            try
            {
                var inscripcion = await _inscripcionService.UpdateInscripcionAsync(dto);
                return Ok(new { message = "Inscripción actualizada", inscripcion.Id });
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
                await _inscripcionService.DeleteInscripcionAsync(id);
                return Ok(new { message = "Inscripción eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
