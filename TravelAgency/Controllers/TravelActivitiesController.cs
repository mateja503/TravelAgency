using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Domain.Shared;
using TravelAgency.Repository.Data;

namespace TravelAgency.Controllers
{
    public class TravelActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TravelActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TravelActivities
        public async Task<IActionResult> Index()
        {
            return View(await _context.TravelActivities.ToListAsync());
        }

        // GET: TravelActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelActivity = await _context.TravelActivities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelActivity == null)
            {
                return NotFound();
            }

            return View(travelActivity);
        }

        // GET: TravelActivities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TravelActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SeasonType,ActivityName")] TravelActivity travelActivity)
        
        {
            
            if (ModelState.IsValid)
            {
                _context.Add(travelActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(travelActivity);
        }

        // GET: TravelActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelActivity = await _context.TravelActivities.FindAsync(id);
            if (travelActivity == null)
            {
                return NotFound();
            }
            return View(travelActivity);
        }

        // POST: TravelActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SeasonType,ActivityName")] TravelActivity travelActivity)
        {
            if (id != travelActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(travelActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelActivityExists(travelActivity.Id))
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
            return View(travelActivity);
        }

        // GET: TravelActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelActivity = await _context.TravelActivities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (travelActivity == null)
            {
                return NotFound();
            }

            return View(travelActivity);
        }

        // POST: TravelActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var travelActivity = await _context.TravelActivities.FindAsync(id);
            if (travelActivity != null)
            {
                _context.TravelActivities.Remove(travelActivity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TravelActivityExists(int id)
        {
            return _context.TravelActivities.Any(e => e.Id == id);
        }
    }
}
