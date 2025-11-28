using Gestion.Models.DTOs;
using Gestion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase
    {
        private readonly CursoService _cursoService;

        public CursoController(CursoService cursoService)
        {
            _cursoService = cursoService;
        }

        [HttpGet("obtener")]
        public async Task<IActionResult> Obtener()
        {
            try
            {
                var cursos = await _cursoService.GetAllCursoAsync();
                return Ok(cursos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] CursoDto dto)
        {
            try
            {
                var curso = await _cursoService.RegisterCursoAsync(dto);
                return Ok(new { message = "Curso creado", curso.Id, curso.Titulo });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] CursoDto dto)
        {
            try
            {
                var curso = await _cursoService.UpdateCursoAsync(dto);
                return Ok(new { message = "Curso actualizado", curso.Titulo });
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
                await _cursoService.DeleteCursoAsync(id);
                return Ok(new { message = "Curso eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
