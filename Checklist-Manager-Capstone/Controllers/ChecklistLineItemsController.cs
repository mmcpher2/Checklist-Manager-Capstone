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
    public class ChecklistLineItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChecklistLineItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChecklistLineItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.ChecklistLineItem.ToListAsync());
        }

        // GET: ChecklistLineItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistLineItem = await _context.ChecklistLineItem
                .SingleOrDefaultAsync(m => m.ChecklistLineItemId == id);
            if (checklistLineItem == null)
            {
                return NotFound();
            }

            return View(checklistLineItem);
        }

        // GET: ChecklistLineItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChecklistLineItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChecklistLineItemId,ActionToDo,Completed")] ChecklistLineItemModel checklistLineItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(checklistLineItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(checklistLineItem);
        }

        // GET: ChecklistLineItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistLineItem = await _context.ChecklistLineItem.SingleOrDefaultAsync(m => m.ChecklistLineItemId == id);
            if (checklistLineItem == null)
            {
                return NotFound();
            }
            return View(checklistLineItem);
        }

        // POST: ChecklistLineItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChecklistLineItemId,ActionToDo,Completed")] ChecklistLineItemModel checklistLineItem)
        {
            if (id != checklistLineItem.ChecklistLineItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checklistLineItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChecklistLineItemExists(checklistLineItem.ChecklistLineItemId))
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
            return View(checklistLineItem);
        }

        // GET: ChecklistLineItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistLineItem = await _context.ChecklistLineItem
                .SingleOrDefaultAsync(m => m.ChecklistLineItemId == id);
            if (checklistLineItem == null)
            {
                return NotFound();
            }

            return View(checklistLineItem);
        }

        // POST: ChecklistLineItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checklistLineItem = await _context.ChecklistLineItem.SingleOrDefaultAsync(m => m.ChecklistLineItemId == id);
            _context.ChecklistLineItem.Remove(checklistLineItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChecklistLineItemExists(int id)
        {
            return _context.ChecklistLineItem.Any(e => e.ChecklistLineItemId == id);
        }
    }
}
