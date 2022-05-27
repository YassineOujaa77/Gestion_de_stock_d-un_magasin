using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using stock_management.Data;
using stock_management.Models;

namespace stock_management.Controllers
{
    public class PersonnelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonnelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Personnels
        public async Task<IActionResult> Index()
        {
              return _context.personnels != null ? 
                          View(await _context.personnels.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.personnels'  is null.");
        }

        // GET: Personnels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.personnels == null)
            {
                return NotFound();
            }

            var personnel = await _context.personnels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personnel == null)
            {
                return NotFound();
            }

            return View(personnel);
        }

        // GET: Personnels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personnels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Email,Password,Adresse,Role,Phone")] Personnel personnel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personnel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personnel);
        }

        // GET: Personnels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.personnels == null)
            {
                return NotFound();
            }

            var personnel = await _context.personnels.FindAsync(id);
            if (personnel == null)
            {
                return NotFound();
            }
            return View(personnel);
        }

        // POST: Personnels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Email,Password,Adresse,Role,Phone")] Personnel personnel)
        {
            if (id != personnel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personnel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonnelExists(personnel.Id))
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
            return View(personnel);
        }

        // GET: Personnels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.personnels == null)
            {
                return NotFound();
            }

            var personnel = await _context.personnels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personnel == null)
            {
                return NotFound();
            }

            return View(personnel);
        }

        // POST: Personnels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.personnels == null)
            {
                return Problem("Entity set 'ApplicationDbContext.personnels'  is null.");
            }
            var personnel = await _context.personnels.FindAsync(id);
            if (personnel != null)
            {
                _context.personnels.Remove(personnel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonnelExists(int id)
        {
          return (_context.personnels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
