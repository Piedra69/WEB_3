using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practica.Data;
using Practica.Models;

namespace Practica.Controllers
{
    public class PedidoesController : Controller
    {
        private readonly PracticaContext _context;

        public PedidoesController(PracticaContext context)
        {
            _context = context;
        }

        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
            var pedidos = await _context.Pedido
                .AsNoTracking()
                .Include(p => p.Cliente)
                .Include(p => p.Detalles) // útil si muestras MontoTotal en la vista
                .ToListAsync();

            return View(pedidos);
        }

        // GET: Pedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var pedido = await _context.Pedido
                .AsNoTracking()
                .Include(p => p.Cliente)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(m => m.PedidoId == id);

            if (pedido is null) return NotFound();

            return View(pedido);
        }

        // GET: Pedidoes/Create
        public async Task<IActionResult> Create()
        {
            await CargarClientesAsync();
            // Si quieres fecha por defecto hoy en el formulario:
            ViewBag.FechaPorDefecto = DateTime.Today.ToString("yyyy-MM-dd");
            return View();
        }

        // POST: Pedidoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PedidoId,FechaPedido,ClienteId")] Pedido pedido)
        {
            Normalizar(pedido);

            // Fecha por defecto si viene sin valor
            if (pedido.FechaPedido == default)
                pedido.FechaPedido = DateTime.Today;

            // Validación de negocio: cliente existente
            if (!await _context.Cliente.AsNoTracking().AnyAsync(c => c.ClienteId == pedido.ClienteId))
                ModelState.AddModelError(nameof(Pedido.ClienteId), "Debe seleccionar un cliente válido.");

            if (!ModelState.IsValid)
            {
                await CargarClientesAsync(pedido.ClienteId);
                return View(pedido);
            }

            try
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Pedido creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "No se pudo guardar el pedido. Verifique los datos.");
                await CargarClientesAsync(pedido.ClienteId);
                return View(pedido);
            }
        }

        // GET: Pedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido is null) return NotFound();

            await CargarClientesAsync(pedido.ClienteId);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PedidoId,FechaPedido,ClienteId")] Pedido pedido)
        {
            if (id != pedido.PedidoId) return NotFound();

            Normalizar(pedido);

            if (pedido.FechaPedido == default)
                ModelState.AddModelError(nameof(Pedido.FechaPedido), "La fecha del pedido es obligatoria.");

            // Validación: cliente debe existir
            if (!await _context.Cliente.AsNoTracking().AnyAsync(c => c.ClienteId == pedido.ClienteId))
                ModelState.AddModelError(nameof(Pedido.ClienteId), "Debe seleccionar un cliente válido.");

            if (!ModelState.IsValid)
            {
                await CargarClientesAsync(pedido.ClienteId);
                return View(pedido);
            }

            try
            {
                _context.Update(pedido);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Pedido actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PedidoExists(pedido.PedidoId))
                    return NotFound();

                ModelState.AddModelError(string.Empty, "Conflicto de concurrencia. Intente nuevamente.");
                await CargarClientesAsync(pedido.ClienteId);
                return View(pedido);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "No se pudo actualizar el pedido. Verifique los datos.");
                await CargarClientesAsync(pedido.ClienteId);
                return View(pedido);
            }
        }

        // GET: Pedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var pedido = await _context.Pedido
                .AsNoTracking()
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.PedidoId == id);

            if (pedido is null) return NotFound();

            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Evitar eliminación si tiene detalles
            var tieneDetalles = await _context.DetallePedido
                .AsNoTracking()
                .AnyAsync(d => d.PedidoId == id);

            if (tieneDetalles)
            {
                TempData["Error"] = "No se puede eliminar un pedido que tiene detalles. Elimine los detalles primero.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Pedido eliminado.";
            }

            return RedirectToAction(nameof(Index));
        }

        // ===== Helpers =====

        private async Task CargarClientesAsync(int? clienteId = null)
        {
            var clientes = await _context.Cliente
                .AsNoTracking()
                .Select(c => new
                {
                    c.ClienteId,
                    Texto = string.IsNullOrWhiteSpace(c.Nombre)
                        ? $"Cliente #{c.ClienteId}"
                        : $"{c.Nombre} ({c.Email})"
                })
                .ToListAsync();

            ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "Texto", clienteId);
        }

        private static void Normalizar(Pedido p)
        {
            // si tuvieras otros campos string, trims aquí
            // (FechaPedido y ClienteId no requieren trim)
        }

        private async Task<bool> PedidoExists(int id)
            => await _context.Pedido.AnyAsync(e => e.PedidoId == id);
    }
}
