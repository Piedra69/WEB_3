using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvcProyect.Data;
using mvcProyect.Models;

namespace mvcProyect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArtesaniasDBContext _context;

        public HomeController(ArtesaniasDBContext context)
        {
            _context = context;
        }

        // GET: HomeModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.HomeModels.ToListAsync());
        }

        // GET: HomeModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeModel = await _context.HomeModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeModel == null)
            {
                return NotFound();
            }

            return View(homeModel);
        }

        // GET: HomeModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomeModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RequestId,Mensaje")] HomeModel homeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homeModel);
        }

        // GET: HomeModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeModel = await _context.HomeModels.FindAsync(id);
            if (homeModel == null)
            {
                return NotFound();
            }
            return View(homeModel);
        }

        // POST: HomeModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RequestId,Mensaje")] HomeModel homeModel)
        {
            if (id != homeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeModelExists(homeModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(homeModel);
        }

        // GET: HomeModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homeModel = await _context.HomeModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homeModel == null)
            {
                return NotFound();
            }

            return View(homeModel);
        }

        // POST: HomeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homeModel = await _context.HomeModels.FindAsync(id);
            if (homeModel != null)
            {
                _context.HomeModels.Remove(homeModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeModelExists(int id)
        {
            return _context.HomeModels.Any(e => e.Id == id);
        }
    }
}
