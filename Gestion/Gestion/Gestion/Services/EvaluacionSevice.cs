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
            var evaluaciones = _appDbContext.Evaluaciones.ToListAsync();
            return await evaluaciones;
        }

        public async Task<Evaluacion> RegisterEvaluacionAsync(EvaluacionDto dto)
        {
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
                throw new Exception("Evaluación no encontrada");

            evaluacion.Nota = dto.Nota;
            evaluacion.CursoId = dto.CursoId;
            evaluacion.UserId = dto.UserId;

            _appDbContext.Evaluaciones.Update(evaluacion);
            await _appDbContext.SaveChangesAsync();
            return evaluacion;
        }

        public async Task DeleteEvaluacionAsync(int id)
        {
            var evaluacion = await _appDbContext.Evaluaciones.FirstOrDefaultAsync(e => e.Id == id);

            if (evaluacion == null)
                throw new Exception("Evaluación no encontrada");

            _appDbContext.Evaluaciones.Remove(evaluacion);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
