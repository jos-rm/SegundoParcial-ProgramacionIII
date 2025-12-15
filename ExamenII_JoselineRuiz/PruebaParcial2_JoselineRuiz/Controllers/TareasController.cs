using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaParcial2_JoselineRuiz.Data;
using PruebaParcial2_JoselineRuiz.Models;

namespace PruebaParcial2_JoselineRuiz.Controllers
{
    public class TareasController : Controller
    {
        private readonly AppDbContext _context;

        public TareasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Incluye la información de Meta para mostrarla en la lista
            var tareas = await _context.Tarea
                .Include(t => t.Meta)
                .ToListAsync();

            return View(tareas);
        }

        // GET: Tareas/Create
        public IActionResult Create()
        {
            //carga las Metas en un SelectList para el dropdown
            ViewBag.Metas = new SelectList(_context.Meta, "IdMeta", "Titulo");

            //carga los Enums como SelectList
            ViewBag.Estados = new SelectList(Enum.GetValues(typeof(EstadoTarea)));
            ViewBag.Dificultades = new SelectList(Enum.GetValues(typeof(DificultadTarea)));

            return View();
        }

        // POST: Tareas/Create
        // POST: Tareas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTarea,Descripcion,FechaCreacion,FechaLimite,Estado,Dificultad,TiempoEstimadoHoras,MetaId")] Tarea tarea)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(tarea);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Ocurrió un error al guardar la tarea.");
                }
            }

            //si hay error, volver a cargar los datos
            ViewBag.Metas = new SelectList(_context.Meta, "IdMeta", "Titulo", tarea.MetaId);
            ViewBag.Estados = new SelectList(Enum.GetValues(typeof(EstadoTarea)), tarea.Estado);
            ViewBag.Dificultades = new SelectList(Enum.GetValues(typeof(DificultadTarea)), tarea.Dificultad);

            return View(tarea);
        }

        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tarea.FindAsync(id);
            if (tarea == null)
            {
                return NotFound();
            }

            //cargar los datos para edición
            ViewBag.Metas = new SelectList(_context.Meta, "IdMeta", "Titulo", tarea.MetaId);
            ViewBag.Estados = new SelectList(Enum.GetValues(typeof(EstadoTarea)), tarea.Estado);
            ViewBag.Dificultades = new SelectList(Enum.GetValues(typeof(DificultadTarea)), tarea.Dificultad);

            return View(tarea);
        }

        // POST: Tareas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTarea,Descripcion,FechaCreacion,FechaLimite,Estado,Dificultad,TiempoEstimadoHoras,MetaId")] Tarea tarea)
        {
            if (id != tarea.IdTarea)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.IdTarea))
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

            ViewBag.Metas = new SelectList(_context.Meta, "IdMeta", "Titulo", tarea.MetaId);
            ViewBag.Estados = new SelectList(Enum.GetValues(typeof(EstadoTarea)), tarea.Estado);
            ViewBag.Dificultades = new SelectList(Enum.GetValues(typeof(DificultadTarea)), tarea.Dificultad);

            return View(tarea);
        }

        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tarea
                .Include(t => t.Meta)
                .FirstOrDefaultAsync(m => m.IdTarea == id);

            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tarea
                .Include(t => t.Meta)
                .FirstOrDefaultAsync(m => m.IdTarea == id);

            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarea = await _context.Tarea.FindAsync(id);
            if (tarea != null)
            {
                _context.Tarea.Remove(tarea);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TareaExists(int id)
        {
            return _context.Tarea.Any(e => e.IdTarea == id);
        }
    }
}