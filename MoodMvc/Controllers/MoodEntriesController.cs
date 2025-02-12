using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoodMvc.Data;
using MoodMvc.Models;

namespace MoodMvc.Controllers
{
    [Authorize]

    public class MoodEntriesController : Controller
    {
        private readonly MoodMvcContext _context;

        public MoodEntriesController(MoodMvcContext context)
        {
            _context = context;
        }

        // GET: MoodEntries
        public async Task<IActionResult> Index()
        {
            return View(await _context.MoodEntry.ToListAsync());
        }

        // GET: MoodEntries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moodEntry = await _context.MoodEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moodEntry == null)
            {
                return NotFound();
            }

            return View(moodEntry);
        }

        // GET: MoodEntries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MoodEntries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Date,Mood,JournalEntry")] MoodEntry moodEntry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moodEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moodEntry);
        }

        // GET: MoodEntries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moodEntry = await _context.MoodEntry.FindAsync(id);
            if (moodEntry == null)
            {
                return NotFound();
            }
            return View(moodEntry);
        }

        // POST: MoodEntries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Date,Mood,JournalEntry")] MoodEntry moodEntry)
        {
            if (id != moodEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moodEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MoodEntryExists(moodEntry.Id))
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
            return View(moodEntry);
        }

        // GET: MoodEntries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moodEntry = await _context.MoodEntry
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moodEntry == null)
            {
                return NotFound();
            }

            return View(moodEntry);
        }

        // POST: MoodEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moodEntry = await _context.MoodEntry.FindAsync(id);
            if (moodEntry != null)
            {
                _context.MoodEntry.Remove(moodEntry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MoodEntryExists(int id)
        {
            return _context.MoodEntry.Any(e => e.Id == id);
        }

        public async Task<IActionResult> History()
        {
            string userId = User.Identity.Name; // Get the currently logged-in user
            var moodHistory = await _context.MoodEntry
                                            .Where(m => m.UserId == userId) // Fetch only user's moods
                                            .OrderByDescending(m => m.Date)
                                            .ToListAsync();

            return View(moodHistory);
        }
    }
}
