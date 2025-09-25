using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica.Data;
using Practica.Models;

namespace Practica.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly PracticaContext _context;
        private static readonly string[] RolesPermitidos = new[] { "admin", "cliente", "empleado" };

        public UsuariosController(PracticaContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuario
                .AsNoTracking()
                .OrderBy(u => u.Nombre)
                .ToListAsync();

            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var usuario = await _context.Usuario
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (usuario is null) return NotFound();

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create() => View();

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Email,Password,Rol")] Usuario usuario)
        {
            Normalizar(usuario);
            await ValidarNegocioAsync(usuario);

            if (!ModelState.IsValid) return View(usuario);

            try
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Usuario creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "No se pudo guardar el usuario. Verifica los datos.");
                return View(usuario);
            }
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario is null) return NotFound();

            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Email,Password,Rol")] Usuario usuario)
        {
            if (id != usuario.Id) return NotFound();

            // Cargar actual para preservar Password si viene vacío
            var actual = await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (actual is null) return NotFound();

            // Si no envían password, conservar el actual
            if (string.IsNullOrWhiteSpace(usuario.Password))
                usuario.Password = actual.Password;

            Normalizar(usuario);
            await ValidarNegocioAsync(usuario, excludeId: id);

            if (!ModelState.IsValid) return View(usuario);

            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Usuario actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UsuarioExists(usuario.Id))
                    return NotFound();

                ModelState.AddModelError(string.Empty, "Conflicto de concurrencia. Intenta nuevamente.");
                return View(usuario);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "No se pudo actualizar el usuario. Verifica los datos.");
                return View(usuario);
            }
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var usuario = await _context.Usuario
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (usuario is null) return NotFound();

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuario.Remove(usuario);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Usuario eliminado.";
            }

            return RedirectToAction(nameof(Index));
        }

        // ===== Helpers =====

        private static void Normalizar(Usuario u)
        {
            if (u is null) return;
            u.Nombre = u.Nombre?.Trim();
            u.Email = u.Email?.Trim().ToLowerInvariant();
            u.Rol = u.Rol?.Trim().ToLowerInvariant();
            // Nota: En producción, aquí deberías hashear la contraseña antes de guardar.
        }

        private async Task ValidarNegocioAsync(Usuario u, int? excludeId = null)
        {
            // Email único (case-insensitive)
            if (!string.IsNullOrWhiteSpace(u.Email))
            {
                var query = _context.Usuario.AsNoTracking()
                    .Where(x => x.Email.ToLower() == u.Email.ToLower());

                if (excludeId.HasValue)
                    query = query.Where(x => x.Id != excludeId.Value);

                var emailExiste = await query.AnyAsync();
                if (emailExiste)
                    ModelState.AddModelError(nameof(Usuario.Email), "Ese correo ya está registrado.");
            }

            // Rol permitido
            if (string.IsNullOrWhiteSpace(u.Rol) ||
                !RolesPermitidos.Contains(u.Rol.ToLower()))
            {
                ModelState.AddModelError(nameof(Usuario.Rol),
                    "Rol inválido. Usa: admin, cliente o empleado.");
            }

            // Password no vacío al crear (cuando no hay excludeId)
            if (!excludeId.HasValue && string.IsNullOrWhiteSpace(u.Password))
            {
                ModelState.AddModelError(nameof(Usuario.Password), "La contraseña es obligatoria.");
            }
        }

        private async Task<bool> UsuarioExists(int id)
            => await _context.Usuario.AnyAsync(e => e.Id == id);
    }
}
