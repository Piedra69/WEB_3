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
            var cursos = _appDbContext.Cursos.ToListAsync();
            return await cursos;
        }

        public async Task<Curso> RegisterCursoAsync(CursoDto dto)
        {
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

            curso.Titulo = string.IsNullOrWhiteSpace(dto.Titulo) ? curso.Titulo : dto.Titulo;
            curso.Descripcion = string.IsNullOrWhiteSpace(dto.Descripcion) ? curso.Descripcion : dto.Descripcion;

            if (dto.UserId != 0)
                curso.UserId = dto.UserId;

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
