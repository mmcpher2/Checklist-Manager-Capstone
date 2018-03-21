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
    public class UserChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserChecklistController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserChecklistModels
        public async Task<IActionResult> Index()
        {

            var applicationDbContext = _context.UserChecklists.Include(u => u.Checklists).ThenInclude(o => o.Checklists);
            
            NCMDashboardViewModel NCMDashboard = new NCMDashboardViewModel
            {
                UserChecklists = await applicationDbContext.ToListAsync()
            };
            return View(NCMDashboard);
            

        }

        // GET: UserChecklistModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userChecklistModel = await _context.UserChecklists
                .Include(u => u.Checklists)
                .SingleOrDefaultAsync(m => m.UserChecklistId == id);
            if (userChecklistModel == null)
            {
                return NotFound();
            }

            return View(userChecklistModel);
        }

        // GET: UserChecklistModels/Create
        public IActionResult Create()
        {
            ViewData["LineItemJoinerId"] = new SelectList(_context.LineItemJoiner, "LineItemJoinerId", "LineItemJoinerId");
            return View();
        }

        // POST: UserChecklistModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserChecklistId,LineItemJoinerId")] UserChecklistModel userChecklistModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userChecklistModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LineItemJoinerId"] = new SelectList(_context.LineItemJoiner, "LineItemJoinerId", "LineItemJoinerId", userChecklistModel.LineItemJoinerId);
            return View(userChecklistModel);
        }

        // GET: UserChecklistModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userChecklistModel = await _context.UserChecklists.SingleOrDefaultAsync(m => m.UserChecklistId == id);
            if (userChecklistModel == null)
            {
                return NotFound();
            }
            ViewData["LineItemJoinerId"] = new SelectList(_context.LineItemJoiner, "LineItemJoinerId", "LineItemJoinerId", userChecklistModel.LineItemJoinerId);
            return View(userChecklistModel);
        }

        // POST: UserChecklistModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserChecklistId,LineItemJoinerId")] UserChecklistModel userChecklistModel)
        {
            if (id != userChecklistModel.UserChecklistId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userChecklistModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserChecklistModelExists(userChecklistModel.UserChecklistId))
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
            ViewData["LineItemJoinerId"] = new SelectList(_context.LineItemJoiner, "LineItemJoinerId", "LineItemJoinerId", userChecklistModel.LineItemJoinerId);
            return View(userChecklistModel);
        }

        // GET: UserChecklistModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userChecklistModel = await _context.UserChecklists
                .Include(u => u.Checklists)
                .SingleOrDefaultAsync(m => m.UserChecklistId == id);
            if (userChecklistModel == null)
            {
                return NotFound();
            }

            return View(userChecklistModel);
        }

        // POST: UserChecklistModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userChecklistModel = await _context.UserChecklists.SingleOrDefaultAsync(m => m.UserChecklistId == id);
            _context.UserChecklists.Remove(userChecklistModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserChecklistModelExists(int id)
        {
            return _context.UserChecklists.Any(e => e.UserChecklistId == id);
        }
    }
}
