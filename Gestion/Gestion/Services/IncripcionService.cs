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
            return await _appDbContext.Inscripciones
                .Include(i => i.Curso)
                .Include(i => i.User)
                .ToListAsync();
        }
        public async Task<Inscripcion> RegisterInscripcionAsync(InscripcionDto dto)
        {
            var curso = await _appDbContext.Cursos.FirstOrDefaultAsync(c => c.Id == dto.CursoId);
            if (curso == null)
                throw new Exception("No se puede crear la inscripción porque el curso no existe");

            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == dto.UserId);
            if (user == null)
                throw new Exception("No se puede crear la inscripción porque el usuario no existe");
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
            if (dto.CursoId != 0 && dto.CursoId != inscripcion.CursoId)
            {
                var curso = await _appDbContext.Cursos.FirstOrDefaultAsync(c => c.Id == dto.CursoId);
                if (curso == null)
                    throw new Exception("No se puede actualizar la inscripción porque el curso no existe");
                inscripcion.CursoId = dto.CursoId;
            }
            if (dto.UserId != 0 && dto.UserId != inscripcion.UserId)
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Id == dto.UserId);
                if (user == null)
                    throw new Exception("No se puede actualizar la inscripción porque el usuario no existe");
                inscripcion.UserId = dto.UserId;
            }
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
