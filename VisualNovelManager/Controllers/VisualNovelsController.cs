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
            var model = await _context.VisualNovel
                                      .Where(a => a.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                                      .ToListAsync();

            return View(model);
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
            List<SelectListItem> status = new List<SelectListItem>
                {
                    new SelectListItem{Value="Completed", Text = "Completed"},
                    new SelectListItem{Value="Reading", Text = "Reading"},
                    new SelectListItem{Value="On Hold", Text = "On Hold"},
                    new SelectListItem{Value="Dropped", Text = "Dropped"}
                };
            ViewBag.status = status;
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
            List<SelectListItem> status = new List<SelectListItem>
                {
                    new SelectListItem{Value="Completed", Text = "Completed"},
                    new SelectListItem{Value="Reading", Text = "Reading"},
                    new SelectListItem{Value="On Hold", Text = "On Hold"},
                    new SelectListItem{Value="Dropped", Text = "Dropped"}
                };
            ViewBag.status = status;

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
        public async Task<IActionResult> Edit(int id, VisualNovelViewModel visualNovel)
        {
            if (id != visualNovel.GameId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var novel = _context.VisualNovel.Find(visualNovel.GameId);

                    novel.GameTitle = visualNovel.GameTitle;
                    novel.GameAlias = visualNovel.GameAlias;
                    novel.CompletionStatus = visualNovel.CompletionStatus;

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
