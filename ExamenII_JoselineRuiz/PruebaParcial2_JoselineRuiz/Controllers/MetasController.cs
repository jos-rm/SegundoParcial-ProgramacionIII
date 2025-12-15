using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaParcial2_JoselineRuiz.Data;
using PruebaParcial2_JoselineRuiz.Models;

namespace PruebaParcial2_JoselineRuiz.Controllers
{
    public class MetasController : Controller
    {
        private readonly AppDbContext _context;

        public MetasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Metas
        public async Task<IActionResult> Index()
        {
            var metas = await _context.Meta
                .Include(m => m.Tareas)
                .ToListAsync();

            return View(metas);
        }

        // GET: Metas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meta = await _context.Meta
                .Include(m => m.Tareas)
                .FirstOrDefaultAsync(m => m.IdMeta == id);

            if (meta == null)
            {
                return NotFound();
            }

            return View(meta);
        }

        // GET: Metas/Create
        public IActionResult Create()
        {
            //cargar los Enums como SelectList
            ViewBag.Categorias = new SelectList(Enum.GetValues(typeof(CategoriaMeta)));
            ViewBag.Prioridades = new SelectList(Enum.GetValues(typeof(PrioridadMeta)));
            ViewBag.Estados = new SelectList(Enum.GetValues(typeof(EstadoMeta)));

            return View();
        }

        // POST: Metas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMeta,Titulo,Descripcion,Categoria,FechaCreacion,FechaLimite,Prioridad,Estado")] Meta meta)
        {
            try
            {
                _context.Add(meta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                throw;
            }

            //si hay error, volver a cargar los datos
            ViewBag.Categorias = new SelectList(Enum.GetValues(typeof(CategoriaMeta)), meta.Categoria);
            ViewBag.Prioridades = new SelectList(Enum.GetValues(typeof(PrioridadMeta)), meta.Prioridad);
            ViewBag.Estados = new SelectList(Enum.GetValues(typeof(EstadoMeta)), meta.Estado);

            return View(meta);
        }

        // GET: Metas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meta = await _context.Meta.FindAsync(id);
            if (meta == null)
            {
                return NotFound();
            }

            //cargar los datos para edición
            ViewBag.Categorias = new SelectList(Enum.GetValues(typeof(CategoriaMeta)), meta.Categoria);
            ViewBag.Prioridades = new SelectList(Enum.GetValues(typeof(PrioridadMeta)), meta.Prioridad);
            ViewBag.Estados = new SelectList(Enum.GetValues(typeof(EstadoMeta)), meta.Estado);

            return View(meta);
        }

        // POST: Metas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdMeta,Titulo,Descripcion,Categoria,FechaCreacion,FechaLimite,Prioridad,Estado")] Meta meta)
        {
            if (id != meta.IdMeta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MetaExists(meta.IdMeta))
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

            ViewBag.Categorias = new SelectList(Enum.GetValues(typeof(CategoriaMeta)), meta.Categoria);
            ViewBag.Prioridades = new SelectList(Enum.GetValues(typeof(PrioridadMeta)), meta.Prioridad);
            ViewBag.Estados = new SelectList(Enum.GetValues(typeof(EstadoMeta)), meta.Estado);

            return View(meta);
        }

        // GET: Metas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meta = await _context.Meta
                .Include(m => m.Tareas)
                .FirstOrDefaultAsync(m => m.IdMeta == id);

            if (meta == null)
            {
                return NotFound();
            }

            return View(meta);
        }

        // POST: Metas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meta = await _context.Meta.FindAsync(id);
            if (meta != null)
            {
                _context.Meta.Remove(meta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool MetaExists(int id)
        {
            return _context.Meta.Any(e => e.IdMeta == id);
        }
    }
}