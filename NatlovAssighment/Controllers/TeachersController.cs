﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NatlovAssighment.Data;
using NatlovAssighment.Models;

namespace NatlovAssighment.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TeachersController(ApplicationDbContext context)
        { 
          this._context = context;
        }

        public async Task<IActionResult> Index()
        {
            var teachers = await _context.Teachers.ToListAsync();
            return View(teachers);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (teacher == null)
                return NotFound();

            return View(teacher);
        }

  
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Department,Specialization")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
                return NotFound();

            return View(teacher);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Department,Specialization")] Teacher teacher)
        {
            if (id != teacher.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.Id == id);

            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            // 
            TempData["Message"] = "Teacher deleted successfully";

            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }

    }
}
