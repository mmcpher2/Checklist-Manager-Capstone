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
    public class LineItemJoinerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LineItemJoinerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LineItemJoinerModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LineItemJoiner.Include(l => l.ChecklistLineItems).Include(l => l.Checklists);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LineItemJoinerModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItemJoinerModel = await _context.LineItemJoiner
                .Include(l => l.ChecklistLineItems)
                .Include(l => l.Checklists)
                .SingleOrDefaultAsync(m => m.LineItemJoinerId == id);
            if (lineItemJoinerModel == null)
            {
                return NotFound();
            }

            return View(lineItemJoinerModel);
        }

        // GET: LineItemJoinerModels/Create
        public IActionResult Create()
        {
            ViewData["ChecklistLineItemId"] = new SelectList(_context.ChecklistLineItem, "ChecklistLineItemId", "ActionToDo");
            ViewData["ChecklistId"] = new SelectList(_context.Checklist, "CheckListId", "ChecklistTitle");
            return View();
        }

        // POST: LineItemJoinerModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LineItemJoinerId,ChecklistId,ChecklistLineItemId")] LineItemJoinerModel lineItemJoinerModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lineItemJoinerModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChecklistLineItemId"] = new SelectList(_context.ChecklistLineItem, "ChecklistLineItemId", "ActionToDo", lineItemJoinerModel.ChecklistLineItemId);
            ViewData["ChecklistId"] = new SelectList(_context.Checklist, "CheckListId", "ChecklistTitle", lineItemJoinerModel.ChecklistId);
            return View(lineItemJoinerModel);
        }

        // GET: LineItemJoinerModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItemJoinerModel = await _context.LineItemJoiner.SingleOrDefaultAsync(m => m.LineItemJoinerId == id);
            if (lineItemJoinerModel == null)
            {
                return NotFound();
            }
            ViewData["ChecklistLineItemId"] = new SelectList(_context.ChecklistLineItem, "ChecklistLineItemId", "ActionToDo", lineItemJoinerModel.ChecklistLineItemId);
            ViewData["ChecklistId"] = new SelectList(_context.Checklist, "CheckListId", "ChecklistTitle", lineItemJoinerModel.ChecklistId);
            return View(lineItemJoinerModel);
        }

        // POST: LineItemJoinerModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LineItemJoinerId,ChecklistId,ChecklistLineItemId")] LineItemJoinerModel lineItemJoinerModel)
        {
            if (id != lineItemJoinerModel.LineItemJoinerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lineItemJoinerModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineItemJoinerModelExists(lineItemJoinerModel.LineItemJoinerId))
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
            ViewData["ChecklistLineItemId"] = new SelectList(_context.ChecklistLineItem, "ChecklistLineItemId", "ActionToDo", lineItemJoinerModel.ChecklistLineItemId);
            ViewData["ChecklistId"] = new SelectList(_context.Checklist, "CheckListId", "ChecklistTitle", lineItemJoinerModel.ChecklistId);
            return View(lineItemJoinerModel);
        }

        // GET: LineItemJoinerModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineItemJoinerModel = await _context.LineItemJoiner
                .Include(l => l.ChecklistLineItems)
                .Include(l => l.Checklists)
                .SingleOrDefaultAsync(m => m.LineItemJoinerId == id);
            if (lineItemJoinerModel == null)
            {
                return NotFound();
            }

            return View(lineItemJoinerModel);
        }

        // POST: LineItemJoinerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lineItemJoinerModel = await _context.LineItemJoiner.SingleOrDefaultAsync(m => m.LineItemJoinerId == id);
            _context.LineItemJoiner.Remove(lineItemJoinerModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LineItemJoinerModelExists(int id)
        {
            return _context.LineItemJoiner.Any(e => e.LineItemJoinerId == id);
        }
    }
}
