using Gestion.Data;
using Gestion.Models;
using Gestion.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Services
{
    public class RecursoService
    {
        private readonly AppDbContext _appDbContext;

        public RecursoService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Recurso>> GetAllRecursoAsync()
        {
            var recursos = _appDbContext.Recursos.ToListAsync();
            return await recursos;
        }

        public async Task<Recurso> RegisterRecursoAsync(RecursoDto dto)
        {
            var recurso = new Recurso
            {
                CursoId = dto.CursoId,
                Nombre = dto.Nombre,
                UserId = dto.UserId
            };

            _appDbContext.Recursos.Add(recurso);
            await _appDbContext.SaveChangesAsync();
            return recurso;
        }

        public async Task<Recurso> UpdateRecursoAsync(RecursoDto dto)
        {
            var recurso = await _appDbContext.Recursos.FirstOrDefaultAsync(r => r.Id == dto.Id);

            if (recurso == null)
                throw new Exception("Recurso no encontrado");

            recurso.Nombre = string.IsNullOrEmpty(dto.Nombre) ? recurso.Nombre : dto.Nombre;
            recurso.CursoId = dto.CursoId;
            recurso.UserId = dto.UserId;

            _appDbContext.Recursos.Update(recurso);
            await _appDbContext.SaveChangesAsync();
            return recurso;
        }

        public async Task DeleteRecursoAsync(int id)
        {
            var recurso = await _appDbContext.Recursos.FirstOrDefaultAsync(r => r.Id == id);

            if (recurso == null)
                throw new Exception("Recurso no encontrado");

            _appDbContext.Recursos.Remove(recurso);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
