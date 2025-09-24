using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Practica.Data;
using Practica.Models;

namespace Practica.Controllers
{
    public class DetallePedidoesController : Controller
    {
        private readonly PracticaContext _context;

        public DetallePedidoesController(PracticaContext context)
        {
            _context = context;
        }

        // GET: DetallePedidoes
        public async Task<IActionResult> Index()
        {
            var query = _context.DetallePedido
                .AsNoTracking()
                .Include(d => d.Pedido)
                .Include(d => d.Producto);

            return View(await query.ToListAsync());
        }

        // GET: DetallePedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var detallePedido = await _context.DetallePedido
                .AsNoTracking()
                .Include(d => d.Pedido)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.DetallePedidoId == id);

            if (detallePedido is null) return NotFound();

            return View(detallePedido);
        }

        // GET: DetallePedidoes/Create
        public async Task<IActionResult> Create()
        {
            await CargarCombosAsync();
            return View();
        }

        // POST: DetallePedidoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DetallePedidoId,PedidoId,ProductoId,Cantidad,PrecioUnitario")] DetallePedido detallePedido)
        {
            await ValidarNegocioAsync(detallePedido);

            if (!ModelState.IsValid)
            {
                await CargarCombosAsync(detallePedido.PedidoId, detallePedido.ProductoId);
                return View(detallePedido);
            }

            try
            {
                _context.Add(detallePedido);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Detalle agregado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "No se pudo guardar el detalle. Verifica los datos.");
                await CargarCombosAsync(detallePedido.PedidoId, detallePedido.ProductoId);
                return View(detallePedido);
            }
        }

        // GET: DetallePedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var detallePedido = await _context.DetallePedido.FindAsync(id);
            if (detallePedido is null) return NotFound();

            await CargarCombosAsync(detallePedido.PedidoId, detallePedido.ProductoId);
            return View(detallePedido);
        }

        // POST: DetallePedidoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DetallePedidoId,PedidoId,ProductoId,Cantidad,PrecioUnitario")] DetallePedido detallePedido)
        {
            if (id != detallePedido.DetallePedidoId) return NotFound();

            await ValidarNegocioAsync(detallePedido);

            if (!ModelState.IsValid)
            {
                await CargarCombosAsync(detallePedido.PedidoId, detallePedido.ProductoId);
                return View(detallePedido);
            }

            try
            {
                _context.Update(detallePedido);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Detalle actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await DetallePedidoExists(detallePedido.DetallePedidoId))
                    return NotFound();

                ModelState.AddModelError(string.Empty, "Conflicto de concurrencia. Intenta nuevamente.");
                await CargarCombosAsync(detallePedido.PedidoId, detallePedido.ProductoId);
                return View(detallePedido);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "No se pudo actualizar el detalle. Verifica los datos.");
                await CargarCombosAsync(detallePedido.PedidoId, detallePedido.ProductoId);
                return View(detallePedido);
            }
        }

        // GET: DetallePedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var detallePedido = await _context.DetallePedido
                .AsNoTracking()
                .Include(d => d.Pedido)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.DetallePedidoId == id);

            if (detallePedido is null) return NotFound();

            return View(detallePedido);
        }

        // POST: DetallePedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detallePedido = await _context.DetallePedido.FindAsync(id);
            if (detallePedido != null)
            {
                _context.DetallePedido.Remove(detallePedido);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Detalle eliminado.";
            }
            return RedirectToAction(nameof(Index));
        }

        // ========= Helpers =========

        private async Task CargarCombosAsync(int? pedidoId = null, int? productoId = null)
        {
            var pedidos = await _context.Set<Pedido>()
                .AsNoTracking()
                .Select(p => new
                {
                    p.PedidoId,
                    Texto = "Pedido #" + p.PedidoId + " - " + p.FechaPedido.ToString("yyyy-MM-dd")
                })
                .ToListAsync();

            var productos = await _context.Set<Producto>()
                .AsNoTracking()
                .Select(p => new
                {
                    p.ProductoId,
                    p.Nombre
                })
                .ToListAsync();

            ViewData["PedidoId"] = new SelectList(pedidos, "PedidoId", "Texto", pedidoId);
            ViewData["ProductoId"] = new SelectList(productos, "ProductoId", "Nombre", productoId);
        }

        /// <summary>
        /// Reglas de negocio adicionales y refuerzo de DataAnnotations.
        /// </summary>
        private async Task ValidarNegocioAsync(DetallePedido d)
        {
            // Verificar FK Pedido
            var pedidoExiste = await _context.Set<Pedido>()
                .AsNoTracking()
                .AnyAsync(p => p.PedidoId == d.PedidoId);

            if (!pedidoExiste)
                ModelState.AddModelError(nameof(DetallePedido.PedidoId), "Debe seleccionar un pedido válido.");

            // Verificar FK Producto
            var producto = await _context.Set<Producto>()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ProductoId == d.ProductoId);

            if (producto is null)
                ModelState.AddModelError(nameof(DetallePedido.ProductoId), "Debe seleccionar un producto válido.");

            // Cantidad mínima
            if (d.Cantidad < 1)
                ModelState.AddModelError(nameof(DetallePedido.Cantidad), "La cantidad debe ser al menos 1.");

            // Precio > 0 (si no viene, usar el del producto)
            if (d.PrecioUnitario <= 0)
            {
                if (producto != null && producto.Precio > 0)
                {
                    // Ajuste automático: tomar el precio del producto
                    d.PrecioUnitario = producto.Precio;
                }
                else
                {
                    ModelState.AddModelError(nameof(DetallePedido.PrecioUnitario), "El precio unitario debe ser mayor a 0.");
                }
            }
        }

        private async Task<bool> DetallePedidoExists(int id)
            => await _context.DetallePedido.AnyAsync(e => e.DetallePedidoId == id);
    }
}
