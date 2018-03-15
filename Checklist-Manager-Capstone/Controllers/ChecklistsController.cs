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
    public class ChecklistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChecklistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Checklists
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Checklist.Include(c => c.ChecklistLineItems).Include(c => c.ChecklistTitle);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Checklists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklist = await _context.Checklist
                .Include(c => c.ChecklistLineItems)
                .Include(c => c.ChecklistTitle)
                .SingleOrDefaultAsync(m => m.CheckListId == id);
            if (checklist == null)
            {
                return NotFound();
            }

            return View(checklist);
        }

        // GET: Checklists/Create
        public IActionResult Create()
        {
            ViewData["ChecklistLineItemId"] = new SelectList(_context.ChecklistLineItem, "ChecklistLineItemId", "ActionToDo");
            ViewData["ChecklistTitleId"] = new SelectList(_context.ChecklistTitle, "ChecklistTitleId", "Title");
            return View();
        }

        // POST: Checklists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CheckListId,ChecklistTitleId,ChecklistLineItemId")] Checklist checklist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checklist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChecklistLineItemId"] = new SelectList(_context.ChecklistLineItem, "ChecklistLineItemId", "ActionToDo", checklist.ChecklistLineItemId);
            ViewData["ChecklistTitleId"] = new SelectList(_context.ChecklistTitle, "ChecklistTitleId", "Title", checklist.ChecklistTitleId);
            return View(checklist);
        }

        // GET: Checklists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklist = await _context.Checklist.SingleOrDefaultAsync(m => m.CheckListId == id);
            if (checklist == null)
            {
                return NotFound();
            }
            ViewData["ChecklistLineItemId"] = new SelectList(_context.ChecklistLineItem, "ChecklistLineItemId", "ActionToDo", checklist.ChecklistLineItemId);
            ViewData["ChecklistTitleId"] = new SelectList(_context.ChecklistTitle, "ChecklistTitleId", "Title", checklist.ChecklistTitleId);
            return View(checklist);
        }

        // POST: Checklists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CheckListId,ChecklistTitleId,ChecklistLineItemId")] Checklist checklist)
        {
            if (id != checklist.CheckListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checklist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChecklistExists(checklist.CheckListId))
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
            ViewData["ChecklistLineItemId"] = new SelectList(_context.ChecklistLineItem, "ChecklistLineItemId", "ActionToDo", checklist.ChecklistLineItemId);
            ViewData["ChecklistTitleId"] = new SelectList(_context.ChecklistTitle, "ChecklistTitleId", "Title", checklist.ChecklistTitleId);
            return View(checklist);
        }

        // GET: Checklists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklist = await _context.Checklist
                .Include(c => c.ChecklistLineItems)
                .Include(c => c.ChecklistTitle)
                .SingleOrDefaultAsync(m => m.CheckListId == id);
            if (checklist == null)
            {
                return NotFound();
            }

            return View(checklist);
        }

        // POST: Checklists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checklist = await _context.Checklist.SingleOrDefaultAsync(m => m.CheckListId == id);
            _context.Checklist.Remove(checklist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChecklistExists(int id)
        {
            return _context.Checklist.Any(e => e.CheckListId == id);
        }
    }
}
