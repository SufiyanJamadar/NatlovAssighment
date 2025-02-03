using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NatlovAssighment.Data;
using NatlovAssighment.Models;

namespace NatlovAssighment.Controllers
{
    public class CourseSchedulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseSchedulesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CourseSchedules
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CourseSchedules.Include(c => c.Course).Include(c => c.Teacher);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CourseSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSchedule = await _context.CourseSchedules
                .Include(c => c.Course)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseSchedule == null)
            {
                return NotFound();
            }

            return View(courseSchedule);
        }

        // GET: CourseSchedules/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id");
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,CourseId,TeacherId,StartTime,EndTime")] CourseSchedule courseSchedule)
        //{

        //        _context.Add(courseSchedule);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));


        //}



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CourseId,TeacherId,StartTime,EndTime")] CourseSchedule courseSchedule)
        {
           
                try
                {
                    _context.Add(courseSchedule);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    // Log the error (uncomment ex variable name and write a log)
                    ModelState.AddModelError("", "Unable to save changes. " + ex.Message);
                }
            
            return View(courseSchedule);
        }


        // GET: CourseSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSchedule = await _context.CourseSchedules.FindAsync(id);
            if (courseSchedule == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "Id", "Name", courseSchedule.CourseId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Id", courseSchedule.TeacherId);
            return View(courseSchedule);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CourseId,TeacherId,StartTime,EndTime")] CourseSchedule courseSchedule)
        {
            if (id != courseSchedule.Id)
            {
                return NotFound();
            }

           
                try
                {
                    _context.Update(courseSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseScheduleExists(courseSchedule.Id))
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

        // GET: CourseSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseSchedule = await _context.CourseSchedules
                .Include(c => c.Course)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseSchedule == null)
            {
                return NotFound();
            }

            return View(courseSchedule);
        }

        // POST: CourseSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseSchedule = await _context.CourseSchedules.FindAsync(id);
            if (courseSchedule != null)
            {
                _context.CourseSchedules.Remove(courseSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseScheduleExists(int id)
        {
            return _context.CourseSchedules.Any(e => e.Id == id);
        }
    }
}
