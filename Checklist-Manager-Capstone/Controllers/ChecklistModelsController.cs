using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NursingChecklistManager.Data;
using NursingChecklistManager.Models;
using NursingChecklistManager.Models.ChecklistViewModels;

namespace NursingChecklistManager.Controllers
{
    public class ChecklistModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChecklistModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChecklistModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.UserChecklists.ToListAsync());
        }

        // GET: ChecklistModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistModel = await _context.Checklist
                .SingleOrDefaultAsync(m => m.CheckListId == id);
            if (checklistModel == null)
            {
                return NotFound();
            }

            return View(checklistModel);
        }

        // GET: ChecklistModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ChecklistModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateChecklistViewModel CreateChecklistModel)
        {
            if (ModelState.IsValid)
            
            {
                ChecklistModel Checklist = new ChecklistModel
                {
                    ChecklistTitle = CreateChecklistModel.Title
                };

                ChecklistLineItemModel LineItem = new ChecklistLineItemModel
                {
                    ActionToDo = CreateChecklistModel.LineItem
                };

                _context.Add(Checklist);
                _context.Add(LineItem);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(CreateChecklistModel);
        }

        // GET: ChecklistModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistModel = await _context.Checklist.SingleOrDefaultAsync(m => m.CheckListId == id);
            if (checklistModel == null)
            {
                return NotFound();
            }
            return View(checklistModel);
        }

        // POST: ChecklistModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CheckListId,ChecklistTitle")] ChecklistModel checklistModel)
        {
            if (id != checklistModel.CheckListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checklistModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChecklistModelExists(checklistModel.CheckListId))
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
            return View(checklistModel);
        }

        // GET: ChecklistModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklistModel = await _context.Checklist
                .SingleOrDefaultAsync(m => m.CheckListId == id);
            if (checklistModel == null)
            {
                return NotFound();
            }

            return View(checklistModel);
        }

        // POST: ChecklistModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checklistModel = await _context.Checklist.SingleOrDefaultAsync(m => m.CheckListId == id);
            _context.Checklist.Remove(checklistModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChecklistModelExists(int id)
        {
            return _context.Checklist.Any(e => e.CheckListId == id);
        }
    }
}
