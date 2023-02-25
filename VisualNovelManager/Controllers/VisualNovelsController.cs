using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using VisualNovelManager.Data;
using VisualNovelManager.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Immutable;
using System.Security.Claims;

namespace VisualNovelManager.Controllers
{
    public class VisualNovelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public VisualNovelsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: VisualNovels
        public async Task<IActionResult> Index()
        {
              return View(await _context.VisualNovel.ToListAsync());
        }

        // GET: VisualNovels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VisualNovel == null)
            {
                return NotFound();
            }

            var visualNovel = await _context.VisualNovel
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (visualNovel == null)
            {
                return NotFound();
            }

            return View(visualNovel);
        }

        // GET: VisualNovels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VisualNovels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VisualNovelViewModel visualNovel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = await _userManager.GetUserAsync(User);

                VisualNovel newVN = new()
                {
                    GameTitle = visualNovel.GameTitle,
                    GameAlias = visualNovel.GameAlias,
                    CompletionStatus = visualNovel.CompletionStatus,
                    UserId = identityUser.Id
                };

                _context.Add(newVN);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.ErrorMessage));
            return View(visualNovel);
        }

        // GET: VisualNovels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VisualNovel == null)
            {
                return NotFound();
            }

            var visualNovel = await _context.VisualNovel.FindAsync(id);
            if (visualNovel == null)
            {
                return NotFound();
            }

            return View(visualNovel);
        }

        // POST: VisualNovels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VisualNovel visualNovel)
        {
            if (id != visualNovel.GameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visualNovel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisualNovelExists(visualNovel.GameId))
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
            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.ErrorMessage));
            return View(visualNovel);
        }

        // GET: VisualNovels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VisualNovel == null)
            {
                return NotFound();
            }

            var visualNovel = await _context.VisualNovel
                .FirstOrDefaultAsync(m => m.GameId == id);
            if (visualNovel == null)
            {
                return NotFound();
            }

            return View(visualNovel);
        }

        // POST: VisualNovels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VisualNovel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VisualNovel'  is null.");
            }
            var visualNovel = await _context.VisualNovel.FindAsync(id);
            if (visualNovel != null)
            {
                _context.VisualNovel.Remove(visualNovel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisualNovelExists(int id)
        {
          return _context.VisualNovel.Any(e => e.GameId == id);
        }
    }
}
