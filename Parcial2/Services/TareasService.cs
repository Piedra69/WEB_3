using System.Text;
using System;
using Parcial2.Models;
using System.Security.Cryptography;

using Parcial2.Data;
using Parcial2.Models;
using Parcial2.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Parcial2.Services
{
    public class TareasService
    {
        private readonly AppDbContext _appDbContext;

        public TareasService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }



        public async Task<List<Tarea>> GetAllTareaAsync()
        {
            var tarea = _appDbContext.Tareas.ToListAsync();
            return await tarea;
        }

        public async Task<Tarea> RegisterTareaAsync(RegistrarTarea dto)
        {
            if (await _appDbContext.Tareas.AnyAsync(t => t.Titulo == dto.Titulo))
                throw new Exception("Titulo ya registrado");

            var tarea = new Tarea
            {
                Titulo = dto.Titulo,
                Descripion = dto.Descripion,
                Estado=dto.Estado,
              
            };

            _appDbContext.Tareas.Add(tarea);
            await _appDbContext.SaveChangesAsync();

            return tarea;
        }


        public async Task<Tarea> UpdateTareaAsync(RegistrarTarea dto)
        {
            var tarea = await _appDbContext.Tareas.FirstOrDefaultAsync(t => t.Titulo == dto.Titulo);
            if (tarea == null)
                throw new Exception("Titulo no encontrado");

            tarea.Titulo = dto.Titulo ?? tarea.Titulo;
            if (!string.IsNullOrEmpty(dto.Descripion))
  

            _appDbContext.Tareas.Update(tarea);
            await _appDbContext.SaveChangesAsync();

            return tarea;
        }

        public async Task<Tarea> BuscarTituloAsync(string titulo)
        {
            var tarea = await _appDbContext.Tareas.FirstOrDefaultAsync(t => t.Titulo == titulo);
            if (tarea == null)
                throw new Exception("Titulo no encontrado");

            return tarea;
        }

        public async Task DeleteTareaAsync(string titulo)
        {
            var tarea = await _appDbContext.Tareas.FirstOrDefaultAsync(t => t.Titulo == titulo);
            if (tarea == null)
                throw new Exception("Titulo no encontrado");

            _appDbContext.Tareas.Remove(tarea);
            await _appDbContext.SaveChangesAsync();
        }

     
    }
}
