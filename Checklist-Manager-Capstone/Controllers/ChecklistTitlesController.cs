using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NursingChecklistManager.Data;
using NursingChecklistManager.Models;

namespace NursingChecklistManager.Controllers
{
    public class ChecklistTitlesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChecklistTitlesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChecklistTitles
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChecklistTitle.ToListAsync());
        }

        // GET: ChecklistTitles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistTitle = await _context.ChecklistTitle
                .SingleOrDefaultAsync(m => m.ChecklistTitleId == id);
            if (checklistTitle == null)
            {
                return NotFound();
            }

            return View(checklistTitle);
        }

        // GET: ChecklistTitles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChecklistTitles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChecklistTitleId,Title")] ChecklistTitle checklistTitle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checklistTitle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(checklistTitle);
        }

        // GET: ChecklistTitles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistTitle = await _context.ChecklistTitle.SingleOrDefaultAsync(m => m.ChecklistTitleId == id);
            if (checklistTitle == null)
            {
                return NotFound();
            }
            return View(checklistTitle);
        }

        // POST: ChecklistTitles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChecklistTitleId,Title")] ChecklistTitle checklistTitle)
        {
            if (id != checklistTitle.ChecklistTitleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checklistTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChecklistTitleExists(checklistTitle.ChecklistTitleId))
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
            return View(checklistTitle);
        }

        // GET: ChecklistTitles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistTitle = await _context.ChecklistTitle
                .SingleOrDefaultAsync(m => m.ChecklistTitleId == id);
            if (checklistTitle == null)
            {
                return NotFound();
            }

            return View(checklistTitle);
        }

        // POST: ChecklistTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checklistTitle = await _context.ChecklistTitle.SingleOrDefaultAsync(m => m.ChecklistTitleId == id);
            _context.ChecklistTitle.Remove(checklistTitle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChecklistTitleExists(int id)
        {
            return _context.ChecklistTitle.Any(e => e.ChecklistTitleId == id);
        }
    }
}
