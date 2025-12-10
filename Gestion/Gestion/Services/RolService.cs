using Gestion.Data;
using Gestion.Models;
using Gestion.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gestion.Services
{
    public class RolService
    {
        private readonly AppDbContext _appDbContext;

        public RolService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<RolDto>> GetAllRolesAsync()
        {
            var roles = await _appDbContext.Roles.ToListAsync();
            return roles.Select(r => new RolDto
            {
                Id = r.Id,
                Nombre = r.Nombre
            }).ToList();
        }

        public async Task<RolDto> GetRoleByIdAsync(int id)
        {
            var rol = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (rol == null)
                throw new Exception("Rol no encontrado");

            return new RolDto
            {
                Id = rol.Id,
                Nombre = rol.Nombre
            };
        }

        public async Task<RolDto> CreateRoleAsync(RolDto dto)
        {
            var rol = new Rol { Nombre = dto.Nombre };
            _appDbContext.Roles.Add(rol);
            await _appDbContext.SaveChangesAsync();

            return new RolDto
            {
                Id = rol.Id,
                Nombre = rol.Nombre
            };
        }

        public async Task<RolDto> UpdateRoleAsync(RolDto dto)
        {
            var rol = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Id == dto.Id);
            if (rol == null)
                throw new Exception("Rol no encontrado");

            rol.Nombre = string.IsNullOrWhiteSpace(dto.Nombre) ? rol.Nombre : dto.Nombre;
            _appDbContext.Roles.Update(rol);
            await _appDbContext.SaveChangesAsync();

            return new RolDto
            {
                Id = rol.Id,
                Nombre = rol.Nombre
            };
        }

        public async Task DeleteRoleAsync(int id)
        {
            var rol = await _appDbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (rol == null)
                throw new Exception("Rol no encontrado");

            _appDbContext.Roles.Remove(rol);
            await _appDbContext.SaveChangesAsync();
        }
    }
}