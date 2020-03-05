using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Instagroceries.Models;

namespace Instagroceries.Controllers
{
    public class Test2Controller : Controller
    {
        private readonly ApplicationDbContext _context;

        public Test2Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Test2
        public async Task<IActionResult> Index()
        {
            return View(await _context.Test2s.ToListAsync());
        }

        // GET: Test2/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test2 = await _context.Test2s
                .FirstOrDefaultAsync(m => m.Test2ID == id);
            if (test2 == null)
            {
                return NotFound();
            }

            return View(test2);
        }

        // GET: Test2/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Test2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Test2ID,LastName")] Test2 test2)
        {
            if (ModelState.IsValid)
            {
                test2.Test2ID = Guid.NewGuid();
                _context.Add(test2);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(test2);
        }

        // GET: Test2/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test2 = await _context.Test2s.FindAsync(id);
            if (test2 == null)
            {
                return NotFound();
            }
            return View(test2);
        }

        // POST: Test2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Test2ID,LastName")] Test2 test2)
        {
            if (id != test2.Test2ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(test2);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Test2Exists(test2.Test2ID))
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
            return View(test2);
        }

        // GET: Test2/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test2 = await _context.Test2s
                .FirstOrDefaultAsync(m => m.Test2ID == id);
            if (test2 == null)
            {
                return NotFound();
            }

            return View(test2);
        }

        // POST: Test2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var test2 = await _context.Test2s.FindAsync(id);
            _context.Test2s.Remove(test2);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Test2Exists(Guid id)
        {
            return _context.Test2s.Any(e => e.Test2ID == id);
        }
    }
}
