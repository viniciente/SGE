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
    public class EtapasController : Controller
    {
        private readonly Contexto _context;

        public EtapasController(Contexto context)
        {
            _context = context;
        }

        // GET: Etapas
        public async Task<IActionResult> Index()
        {
              return _context.Etapas != null ? 
                          View(await _context.Etapas.ToListAsync()) :
                          Problem("Entity set 'Contexto.Etapas'  is null.");
        }

        // GET: Etapas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Etapas == null)
            {
                return NotFound();
            }

            var etapas = await _context.Etapas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (etapas == null)
            {
                return NotFound();
            }

            return View(etapas);
        }

        // GET: Etapas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Etapas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Ano")] Etapas etapas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(etapas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(etapas);
        }

        // GET: Etapas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Etapas == null)
            {
                return NotFound();
            }

            var etapas = await _context.Etapas.FindAsync(id);
            if (etapas == null)
            {
                return NotFound();
            }
            return View(etapas);
        }

        // POST: Etapas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Ano")] Etapas etapas)
        {
            if (id != etapas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(etapas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EtapasExists(etapas.Id))
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
            return View(etapas);
        }

        // GET: Etapas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Etapas == null)
            {
                return NotFound();
            }

            var etapas = await _context.Etapas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (etapas == null)
            {
                return NotFound();
            }

            return View(etapas);
        }

        // POST: Etapas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Etapas == null)
            {
                return Problem("Entity set 'Contexto.Etapas'  is null.");
            }
            var etapas = await _context.Etapas.FindAsync(id);
            if (etapas != null)
            {
                _context.Etapas.Remove(etapas);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EtapasExists(int id)
        {
          return (_context.Etapas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
