﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestionDeStockMagasin.Data;
using GestionDeStockMagasin.Models;


namespace GestionDeStockMagasin.Controllers
{
    public class PersonnelsController : Controller
    {
        private readonly AppDbContext _context;

        public PersonnelsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Personnels
        public async Task<IActionResult> Index()
        {
            //pour verifier si c'est l'utilisateur posede le droit d'entrer avec son role 
            RoleController1 verificateur = new RoleController1();
            if (verificateur.role("Personnels", HttpContext.Session.GetString("Role")) != null)
            {
                TempData["error"] = "you are not allowed";
                return verificateur.role("Personnels", HttpContext.Session.GetString("Role"));
            }



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
            //pour verifier si c'est l'utilisateur posede le droit d'entrer avec son role 
            RoleController1 verificateur = new RoleController1();
            if(verificateur.role("Personnels", HttpContext.Session.GetString("Role")) != null) {
                TempData["error"] = "you are not allowed";
                return   verificateur.role("Personnels",HttpContext.Session.GetString("Role"));
            }


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
                // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
                var results = _context.personnels.Where(p => p.Email.ToLower() == personnel.Email.ToLower() || p.Phone == personnel.Phone).Select(p => new { p.Email,p.Phone }).SingleOrDefault();
                if(results!=null){
                    TempData["error"] = "Ce Personnel existe déjà ";
                    return View(personnel);
                }
                byte[] salt = new byte[128 / 8];
                using (var rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetNonZeroBytes(salt);
                }


                // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: personnel.Password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));; ;
                personnel.Password = hashed;
                _context.Add(personnel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personnel);
        }

        // GET: Personnels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            //pour verifier si c'est l'utilisateur posede le droit d'entrer avec son role 
            RoleController1 verificateur = new RoleController1();
            if (verificateur.role("Personnels", HttpContext.Session.GetString("Role")) != null)
            {
                TempData["error"] = "you are not allowed";
                return verificateur.role("Personnels", HttpContext.Session.GetString("Role"));
            }



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
            var results = _context.personnels.Where(p => (p.Email.ToLower() == personnel.Email.ToLower() || p.Phone == personnel.Phone) && (p.Id != id)).Select(p => new { p.Email,p.Phone }).SingleOrDefault();
                if(results!=null){
                    TempData["error"] = "Ce Personnel existe déjà ";
                    return View(personnel);
                }

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
            //pour verifier si c'est l'utilisateur posede le droit d'entrer avec son role 
            RoleController1 verificateur = new RoleController1();
            if (verificateur.role("Personnels", HttpContext.Session.GetString("Role")) != null)
            {
                TempData["error"] = "you are not allowed";
                return verificateur.role("Personnels", HttpContext.Session.GetString("Role"));
            }



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
