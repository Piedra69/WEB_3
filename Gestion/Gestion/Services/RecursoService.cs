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
            return await _appDbContext.Recursos
                .Include(r => r.Curso)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<Recurso> RegisterRecursoAsync(RecursoDto dto)
        {
            var curso = await _appDbContext.Cursos.FirstOrDefaultAsync(c => c.Id == dto.CursoId);
            if (curso == null)
                throw new Exception("No se puede crear el recurso porque el curso no existe");

            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == dto.UserId);
            if (user == null)
                throw new Exception("No se puede crear el recurso porque el usuario no existe");

            var recurso = new Recurso
            {
                CursoId = dto.CursoId,
                UserId = dto.UserId,
                Nombre = dto.Nombre
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

            if (!string.IsNullOrWhiteSpace(dto.Nombre))
                recurso.Nombre = dto.Nombre;

            if (dto.CursoId != 0 && dto.CursoId != recurso.CursoId)
            {
                var curso = await _appDbContext.Cursos.FirstOrDefaultAsync(c => c.Id == dto.CursoId);
                if (curso == null)
                    throw new Exception("No se puede actualizar el recurso porque el curso no existe");
                recurso.CursoId = dto.CursoId;
            }

            if (dto.UserId != 0 && dto.UserId != recurso.UserId)
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == dto.UserId);
                if (user == null)
                    throw new Exception("No se puede actualizar el recurso porque el usuario no existe");
                recurso.UserId = dto.UserId;
            }

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
