using Gestion.Models.DTOs;
using Gestion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvaluacionController : ControllerBase
    {
        private readonly EvaluacionService _evaluacionService;

        public EvaluacionController(EvaluacionService evaluacionService)
        {
            _evaluacionService = evaluacionService;
        }

        [HttpGet("obtener")]
        public async Task<IActionResult> Obtener()
        {
            try
            {
                var evaluaciones = await _evaluacionService.GetAllEvaluacionAsync();
                return Ok(evaluaciones);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] EvaluacionDto dto)
        {
            try
            {
                var evaluacion = await _evaluacionService.RegisterEvaluacionAsync(dto);
                return Ok(new { message = "Evaluación creada", evaluacion.Id, evaluacion.Nota });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] EvaluacionDto dto)
        {
            try
            {
                var evaluacion = await _evaluacionService.UpdateEvaluacionAsync(dto);
                return Ok(new { message = "Evaluación actualizada", evaluacion.Nota });
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
                await _evaluacionService.DeleteEvaluacionAsync(id);
                return Ok(new { message = "Evaluación eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
