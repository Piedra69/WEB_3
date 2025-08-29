using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tareas_Cruds.Data;
using Tareas_Cruds.Models;

namespace Tareas_Cruds.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Tareas_Cruds.Data.TareaDbContext _context;

        public IndexModel(Tareas_Cruds.Data.TareaDbContext context)
        {
            _context = context;
        }

        public IList<Tarea> Tarea { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Tarea = await _context.Tareas.ToListAsync();
        }
    }
}
