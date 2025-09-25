using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica.Data;
using Practica.Models;

namespace Practica.Controllers
{
    public class ProductoesController : Controller
    {
        private readonly PracticaContext _context;

        public ProductoesController(PracticaContext context)
        {
            _context = context;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
            var productos = await _context.Producto
                .AsNoTracking()
                .OrderBy(p => p.Nombre)
                .ToListAsync();

            return View(productos);
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var producto = await _context.Producto
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProductoId == id);

            if (producto is null) return NotFound();

            return View(producto);
        }

        // GET: Productoes/Create
        public IActionResult Create() => View();

        // POST: Productoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,Nombre,Precio")] Producto producto)
        {
            Normalizar(producto);

            // Reglas de negocio adicionales
            if (!string.IsNullOrWhiteSpace(producto.Nombre) &&
                await NombreExisteAsync(producto.Nombre))
            {
                ModelState.AddModelError(nameof(Producto.Nombre), "Ya existe un producto con ese nombre.");
            }

            if (producto.Precio <= 0)
            {
                ModelState.AddModelError(nameof(Producto.Precio), "El precio debe ser mayor que 0.");
            }

            if (!ModelState.IsValid) return View(producto);

            try
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Producto creado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "No se pudo guardar el producto. Verifique los datos.");
                return View(producto);
            }
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return NotFound();

            var producto = await _context.Producto.FindAsync(id);
            if (producto is null) return NotFound();

            return View(producto);
        }

        // POST: Productoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,Nombre,Precio")] Producto producto)
        {
            if (id != producto.ProductoId) return NotFound();

            Normalizar(producto);

            if (!string.IsNullOrWhiteSpace(producto.Nombre) &&
                await NombreExisteAsync(producto.Nombre, excludeId: producto.ProductoId))
            {
                ModelState.AddModelError(nameof(Producto.Nombre), "Ya existe un producto con ese nombre.");
            }

            if (producto.Precio <= 0)
            {
                ModelState.AddModelError(nameof(Producto.Precio), "El precio debe ser mayor que 0.");
            }

            if (!ModelState.IsValid) return View(producto);

            try
            {
                _context.Update(producto);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Producto actualizado correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProductoExists(producto.ProductoId))
                    return NotFound();

                ModelState.AddModelError(string.Empty, "Conflicto de concurrencia. Intente nuevamente.");
                return View(producto);
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError(string.Empty, "No se pudo actualizar el producto. Verifique los datos.");
                return View(producto);
            }
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return NotFound();

            var producto = await _context.Producto
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProductoId == id);

            if (producto is null) return NotFound();

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Evitar borrar si tiene detalles de pedido
            var usadoEnDetalles = await _context.DetallePedido
                .AsNoTracking()
                .AnyAsync(d => d.ProductoId == id);

            if (usadoEnDetalles)
            {
                TempData["Error"] = "No se puede eliminar un producto que está siendo usado en pedidos.";
                return RedirectToAction(nameof(Details), new { id });
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Producto eliminado.";
            }

            return RedirectToAction(nameof(Index));
        }

        // ===== Helpers =====

        private static void Normalizar(Producto p)
        {
            if (p is null) return;
            p.Nombre = p.Nombre?.Trim();
            // Precio es decimal; no requiere normalización de texto
        }

        private async Task<bool> NombreExisteAsync(string nombre, int? excludeId = null)
        {
            var query = _context.Producto.AsNoTracking()
                .Where(p => p.Nombre.ToLower() == nombre.ToLower());

            if (excludeId.HasValue)
                query = query.Where(p => p.ProductoId != excludeId.Value);

            return await query.AnyAsync();
        }

        private async Task<bool> ProductoExists(int id)
            => await _context.Producto.AnyAsync(e => e.ProductoId == id);
    }
}
