using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica.Data;
using Practica.Models;

namespace Practica.Controllers
{
    public class ClientesController : Controller
    {
        private readonly PracticaContext _context;

        public ClientesController(PracticaContext context) => _context = context;

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var data = await _context.Cliente.AsNoTracking().ToListAsync();
            return View(data);
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();
            var cliente = await _context.Cliente.AsNoTracking().FirstOrDefaultAsync(m => m.ClienteId == id);
            if (cliente is null) return NotFound();
            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create() => View();

        // POST: Clientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,Nombre,Email")] Cliente cliente)
        {
            Normalizar(cliente);

            // 1) Reglas de negocio (email único, si lo quieres)
            if (!string.IsNullOrWhiteSpace(cliente.Email))
            {
                var emailExists = await _context.Cliente.AsNoTracking()
                    .AnyAsync(c => c.Email.ToLower() == cliente.Email.ToLower());

                if (emailExists)
                    ModelState.AddModelError(nameof(Cliente.Email), "Ese correo ya está registrado.");
            }

            // 2) Si algo falla en ModelState, muéstrame TODO el detalle
            if (!ModelState.IsValid)
            {
                AddModelStateDebug(ModelState, "CREATE");
                return View(cliente);
            }

            try
            {
                _context.Add(cliente);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cliente creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                // 3) Si hay constraint/índice UNIQUE en BD, verás el mensaje real aquí
                ModelState.AddModelError(string.Empty, "ERROR BD: " + GetDeepMessage(ex));
                return View(cliente);
            }
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente is null) return NotFound();
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Nombre,Email")] Cliente cliente)
        {
            if (id != cliente.ClienteId) return NotFound();

            Normalizar(cliente);

            // email único excluyendo el mismo registro
            if (!string.IsNullOrWhiteSpace(cliente.Email))
            {
                var emailExists = await _context.Cliente.AsNoTracking()
                    .AnyAsync(c => c.ClienteId != cliente.ClienteId &&
                                   c.Email.ToLower() == cliente.Email.ToLower());

                if (emailExists)
                    ModelState.AddModelError(nameof(Cliente.Email), "Ese correo ya está registrado.");
            }

            if (!ModelState.IsValid)
            {
                AddModelStateDebug(ModelState, "EDIT");
                return View(cliente);
            }

            try
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cliente actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ClienteExists(cliente.ClienteId)) return NotFound();
                ModelState.AddModelError(string.Empty, "Conflicto de concurrencia. Intenta nuevamente.");
                return View(cliente);
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, "ERROR BD: " + GetDeepMessage(ex));
                return View(cliente);
            }
        }

        // DELETE (igual que ya tienes)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente != null)
            {
                _context.Cliente.Remove(cliente);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cliente eliminado.";
            }
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ClienteExists(int id)
            => await _context.Cliente.AnyAsync(e => e.ClienteId == id);

        private static void Normalizar(Cliente c)
        {
            if (c is null) return;
            c.Nombre = c.Nombre?.Trim();
            c.Email = c.Email?.Trim();
        }

        // ---- Helpers de diagnóstico ----
        private static void AddModelStateDebug(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary ms, string origen)
        {
            var all = ms.SelectMany(kv => kv.Value.Errors.Select(e =>
                $"{kv.Key}: {(string.IsNullOrWhiteSpace(e.ErrorMessage) ? e.Exception?.Message : e.ErrorMessage)}"));
            var msg = string.Join(" | ", all);
            if (!string.IsNullOrWhiteSpace(msg))
                ms.AddModelError(string.Empty, $"DEBUG {origen}: {msg}");
        }

        private static string GetDeepMessage(Exception ex)
        {
            while (ex.InnerException != null) ex = ex.InnerException;
            return ex.Message;
        }
    }
}
