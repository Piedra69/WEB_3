using Gestion.Models.DTOs;
using Gestion.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gestion.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly RolService _rolService;

        public RolController(RolService rolService)
        {
            _rolService = rolService;
        }

        /// <summary>
        /// Obtiene todos los roles
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var roles = await _rolService.GetAllRolesAsync();
                return Ok(new
                {
                    success = true,
                    data = roles,
                    count = roles.Count
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtiene un rol por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var rol = await _rolService.GetRoleByIdAsync(id);
                return Ok(new
                {
                    success = true,
                    data = rol
                });
            }
            catch (Exception ex)
            {
                return NotFound(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Crea un nuevo rol
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Datos inválidos",
                        errors = ModelState
                    });
                }

                var rol = await _rolService.CreateRoleAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = rol.Id }, new
                {
                    success = true,
                    data = rol,
                    message = "Rol creado correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Actualiza un rol existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Datos inválidos",
                        errors = ModelState
                    });
                }

                if (id != dto.Id)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "El ID del parámetro no coincide con el ID del body"
                    });
                }

                var rol = await _rolService.UpdateRoleAsync(dto);
                return Ok(new
                {
                    success = true,
                    data = rol,
                    message = "Rol actualizado correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }

        /// <summary>
        /// Elimina un rol
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _rolService.DeleteRoleAsync(id);
                return Ok(new
                {
                    success = true,
                    message = "Rol eliminado correctamente"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    error = ex.Message
                });
            }
        }
    }
}