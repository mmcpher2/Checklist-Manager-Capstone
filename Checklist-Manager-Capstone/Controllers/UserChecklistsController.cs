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
    public class UserChecklistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserChecklistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserChecklists
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserChecklists.Include(u => u.Checklists);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: UserChecklists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userChecklist = await _context.UserChecklists
                .Include(u => u.Checklists)
                .SingleOrDefaultAsync(m => m.UserChecklistId == id);
            if (userChecklist == null)
            {
                return NotFound();
            }

            return View(userChecklist);
        }

        // GET: UserChecklists/Create
        public IActionResult Create()
        {
            ViewData["ChecklistId"] = new SelectList(_context.Checklist, "CheckListId", "CheckListId");
            return View();
        }

        // POST: UserChecklists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserChecklistId,ChecklistId")] UserChecklist userChecklist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userChecklist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChecklistId"] = new SelectList(_context.Checklist, "CheckListId", "CheckListId", userChecklist.ChecklistId);
            return View(userChecklist);
        }

        // GET: UserChecklists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userChecklist = await _context.UserChecklists.SingleOrDefaultAsync(m => m.UserChecklistId == id);
            if (userChecklist == null)
            {
                return NotFound();
            }
            ViewData["ChecklistId"] = new SelectList(_context.Checklist, "CheckListId", "CheckListId", userChecklist.ChecklistId);
            return View(userChecklist);
        }

        // POST: UserChecklists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserChecklistId,ChecklistId")] UserChecklist userChecklist)
        {
            if (id != userChecklist.UserChecklistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userChecklist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserChecklistExists(userChecklist.UserChecklistId))
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
            ViewData["ChecklistId"] = new SelectList(_context.Checklist, "CheckListId", "CheckListId", userChecklist.ChecklistId);
            return View(userChecklist);
        }

        // GET: UserChecklists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userChecklist = await _context.UserChecklists
                .Include(u => u.Checklists)
                .SingleOrDefaultAsync(m => m.UserChecklistId == id);
            if (userChecklist == null)
            {
                return NotFound();
            }

            return View(userChecklist);
        }

        // POST: UserChecklists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userChecklist = await _context.UserChecklists.SingleOrDefaultAsync(m => m.UserChecklistId == id);
            _context.UserChecklists.Remove(userChecklist);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserChecklistExists(int id)
        {
            return _context.UserChecklists.Any(e => e.UserChecklistId == id);
        }
    }
}
