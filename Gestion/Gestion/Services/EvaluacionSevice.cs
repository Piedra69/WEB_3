using Gestion.Data;
using Gestion.Models;
using Gestion.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Services
{
    public class EvaluacionService
    {
        private readonly AppDbContext _appDbContext;

        public EvaluacionService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Evaluacion>> GetAllEvaluacionAsync()
        {
            return await _appDbContext.Evaluaciones.ToListAsync();
        }

        public async Task<Evaluacion> RegisterEvaluacionAsync(EvaluacionDto dto)
        {
            var cursoExists = await _appDbContext.Cursos.AnyAsync(c => c.Id == dto.CursoId);
            if (!cursoExists)
                throw new Exception("No se puede crear la evaluacion porque el curso no existe");

            var userExists = await _appDbContext.Users.AnyAsync(u => u.Id == dto.UserId);
            if (!userExists)
                throw new Exception("No se puede crear la evaluacion porque el usuario no existe");

            var evaluacion = new Evaluacion
            {
                CursoId = dto.CursoId,
                UserId = dto.UserId,
                Nota = dto.Nota
            };

            _appDbContext.Evaluaciones.Add(evaluacion);
            await _appDbContext.SaveChangesAsync();
            return evaluacion;
        }

        public async Task<Evaluacion> UpdateEvaluacionAsync(EvaluacionDto dto)
        {
            var evaluacion = await _appDbContext.Evaluaciones.FirstOrDefaultAsync(e => e.Id == dto.Id);
            if (evaluacion == null)
                throw new Exception("Evaluacion no encontrada");

            if (dto.CursoId != 0 && dto.CursoId != evaluacion.CursoId)
            {
                var cursoExists = await _appDbContext.Cursos.AnyAsync(c => c.Id == dto.CursoId);
                if (!cursoExists)
                    throw new Exception("No se puede actualizar la evaluacion porque el curso no existe");
                evaluacion.CursoId = dto.CursoId;
            }

            if (dto.UserId != 0 && dto.UserId != evaluacion.UserId)
            {
                var userExists = await _appDbContext.Users.AnyAsync(u => u.Id == dto.UserId);
                if (!userExists)
                    throw new Exception("No se puede actualizar la evaluacion porque el usuario no existe");
                evaluacion.UserId = dto.UserId;
            }

            evaluacion.Nota = dto.Nota;
            _appDbContext.Evaluaciones.Update(evaluacion);
            await _appDbContext.SaveChangesAsync();
            return evaluacion;
        }

        public async Task DeleteEvaluacionAsync(int id)
        {
            var evaluacion = await _appDbContext.Evaluaciones.FirstOrDefaultAsync(e => e.Id == id);
            if (evaluacion == null)
                throw new Exception("Evaluacion no encontrada");

            _appDbContext.Evaluaciones.Remove(evaluacion);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
