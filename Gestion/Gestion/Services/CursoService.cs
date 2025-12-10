using Gestion.Data;
using Gestion.Models;
using Gestion.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Services
{
    public class CursoService
    {
        private readonly AppDbContext _appDbContext;
        public CursoService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<Curso>> GetAllCursoAsync()
        {
            return await _appDbContext.Cursos.ToListAsync();
        }
        public async Task<Curso> RegisterCursoAsync(CursoDto dto)
        {
            var userExists = await _appDbContext.Users
                .AnyAsync(u => u.Id == dto.UserId);

            if (!userExists)
                throw new Exception("No se puede crear el curso porque el usuario no existe");
            var curso = new Curso
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                UserId = dto.UserId
            };
            _appDbContext.Cursos.Add(curso);
            await _appDbContext.SaveChangesAsync();
            return curso;
        }
        public async Task<Curso> UpdateCursoAsync(CursoDto dto)
        {
            var curso = await _appDbContext.Cursos
                .FirstOrDefaultAsync(c => c.Id == dto.Id);
            if (curso == null)
                throw new Exception("Curso no encontrado");
            if (dto.UserId != 0 && dto.UserId != curso.UserId)
            {
                var userExists = await _appDbContext.Users
                    .AnyAsync(u => u.Id == dto.UserId);
                if (!userExists)
                    throw new Exception("No se puede actualizar el curso porque el usuario indicado no existe");
                curso.UserId = dto.UserId;
            }
            if (!string.IsNullOrWhiteSpace(dto.Titulo))
                curso.Titulo = dto.Titulo;
            if (!string.IsNullOrWhiteSpace(dto.Descripcion))
                curso.Descripcion = dto.Descripcion;
            _appDbContext.Cursos.Update(curso);
            await _appDbContext.SaveChangesAsync();
            return curso;
        }
        public async Task DeleteCursoAsync(int id)
        {
            var curso = await _appDbContext.Cursos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (curso == null)
                throw new Exception("Curso no encontrado");
            _appDbContext.Cursos.Remove(curso);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
