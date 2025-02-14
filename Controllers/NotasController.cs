using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SGE.Models;

namespace SGE.Controllers
{
    public class NotasController : Controller
    {
        private readonly Contexto _context;

        public NotasController(Contexto context)
        {
            _context = context;
        }

        // GET: Notas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Notas.Include(n => n.Alunos).Include(n => n.Etapas).Include(n => n.Materias);
            return View(await contexto.ToListAsync());
        }

        // GET: Notas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var notas = await _context.Notas
                .Include(n => n.Alunos)
                .Include(n => n.Etapas)
                .Include(n => n.Materias)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notas == null)
            {
                return NotFound();
            }

            return View(notas);
        }

        // GET: Notas/Create
        public IActionResult Create()
        {
            ViewData["AlunosId"] = new SelectList(_context.Alunos, "Id", "Id");
            ViewData["EtapasId"] = new SelectList(_context.Etapas, "Id", "Id");
            ViewData["MateriasId"] = new SelectList(_context.Materias, "Id", "Id");
            return View();
        }

        // POST: Notas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MateriasId,AlunosId,EtapasId,Nota")] Notas notas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlunosId"] = new SelectList(_context.Alunos, "Id", "Id", notas.AlunosId);
            ViewData["EtapasId"] = new SelectList(_context.Etapas, "Id", "Id", notas.EtapasId);
            ViewData["MateriasId"] = new SelectList(_context.Materias, "Id", "Id", notas.MateriasId);
            return View(notas);
        }

        // GET: Notas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var notas = await _context.Notas.FindAsync(id);
            if (notas == null)
            {
                return NotFound();
            }
            ViewData["AlunosId"] = new SelectList(_context.Alunos, "Id", "Id", notas.AlunosId);
            ViewData["EtapasId"] = new SelectList(_context.Etapas, "Id", "Id", notas.EtapasId);
            ViewData["MateriasId"] = new SelectList(_context.Materias, "Id", "Id", notas.MateriasId);
            return View(notas);
        }

        // POST: Notas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MateriasId,AlunosId,EtapasId,Nota")] Notas notas)
        {
            if (id != notas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotasExists(notas.Id))
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
            ViewData["AlunosId"] = new SelectList(_context.Alunos, "Id", "Id", notas.AlunosId);
            ViewData["EtapasId"] = new SelectList(_context.Etapas, "Id", "Id", notas.EtapasId);
            ViewData["MateriasId"] = new SelectList(_context.Materias, "Id", "Id", notas.MateriasId);
            return View(notas);
        }

        // GET: Notas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Notas == null)
            {
                return NotFound();
            }

            var notas = await _context.Notas
                .Include(n => n.Alunos)
                .Include(n => n.Etapas)
                .Include(n => n.Materias)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notas == null)
            {
                return NotFound();
            }

            return View(notas);
        }

        // POST: Notas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Notas == null)
            {
                return Problem("Entity set 'Contexto.Notas'  is null.");
            }
            var notas = await _context.Notas.FindAsync(id);
            if (notas != null)
            {
                _context.Notas.Remove(notas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotasExists(int id)
        {
          return (_context.Notas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
