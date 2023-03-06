using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VisualNovelManager.Data;
using VisualNovelManager.Models;

namespace VisualNovelManager.Controllers
{
    public class VNListsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public VNListsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: VNLists
        public async Task<IActionResult> Index()
        {
              return View(await _context.VNList.ToListAsync());
        }

        // GET: VNLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VNList == null)
            {
                return NotFound();
            }

            var vNList = await _context.VNList
                .FirstOrDefaultAsync(m => m.ListId == id);
            if (vNList == null)
            {
                return NotFound();
            }

            return View(vNList);
        }

        // GET: VNLists/Create
        public IActionResult Create()
        {
            List<VisualNovel> AllAvailableVN = new List<VisualNovel>();
            AllAvailableVN = _context.VisualNovel
                                      .Where(a => a.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                                      .ToList();
            ViewBag.AllAvailableVN = AllAvailableVN;
            return View();
        }

        // POST: VNLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VNListCreateViewModel vNList)
        {
            if (ModelState.IsValid)
            {
                // Grab currently logged in user
                IdentityUser identityUser = await _userManager.GetUserAsync(User);

                VNList newVnList = new()
                {
                    UserId = identityUser.Id,
                    ListName = vNList.ListName,
                    ListDescription = vNList.ListDescription,
                };

                _context.Add(newVnList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vNList);
        }

        // GET: VNLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VNList == null)
            {
                return NotFound();
            }

            var vNList = await _context.VNList.FindAsync(id);
            if (vNList == null)
            {
                return NotFound();
            }
            return View(vNList);
        }

        // POST: VNLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListId,UserId,ListName,ListDescription,List")] VNList vNList)
        {
            if (id != vNList.ListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vNList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VNListExists(vNList.ListId))
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
            return View(vNList);
        }

        // GET: VNLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VNList == null)
            {
                return NotFound();
            }

            var vNList = await _context.VNList
                .FirstOrDefaultAsync(m => m.ListId == id);
            if (vNList == null)
            {
                return NotFound();
            }

            return View(vNList);
        }

        // POST: VNLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VNList == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VNList'  is null.");
            }
            var vNList = await _context.VNList.FindAsync(id);
            if (vNList != null)
            {
                _context.VNList.Remove(vNList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VNListExists(int id)
        {
          return _context.VNList.Any(e => e.ListId == id);
        }
    }
}
