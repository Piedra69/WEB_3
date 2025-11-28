using Gestion.Data;
using Gestion.Models;
using Gestion.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Gestion.Services
{
    public class InscripcionService
    {
        private readonly AppDbContext _appDbContext;

        public InscripcionService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Inscripcion>> GetAllInscripcionAsync()
        {
            var inscripciones = _appDbContext.Inscripciones.ToListAsync();
            return await inscripciones;
        }

        public async Task<Inscripcion> RegisterInscripcionAsync(InscripcionDto dto)
        {
            var inscripcion = new Inscripcion
            {
                CursoId = dto.CursoId,
                UserId = dto.UserId
            };

            _appDbContext.Inscripciones.Add(inscripcion);
            await _appDbContext.SaveChangesAsync();
            return inscripcion;
        }

        public async Task<Inscripcion> UpdateInscripcionAsync(InscripcionDto dto)
        {
            var inscripcion = await _appDbContext.Inscripciones.FirstOrDefaultAsync(i => i.Id == dto.Id);

            if (inscripcion == null)
                throw new Exception("Inscripción no encontrada");

            inscripcion.CursoId = dto.CursoId;
            inscripcion.UserId = dto.UserId;

            _appDbContext.Inscripciones.Update(inscripcion);
            await _appDbContext.SaveChangesAsync();
            return inscripcion;
        }

        public async Task DeleteInscripcionAsync(int id)
        {
            var inscripcion = await _appDbContext.Inscripciones.FirstOrDefaultAsync(i => i.Id == id);

            if (inscripcion == null)
                throw new Exception("Inscripción no encontrada");

            _appDbContext.Inscripciones.Remove(inscripcion);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
