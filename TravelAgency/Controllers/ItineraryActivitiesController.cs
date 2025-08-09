using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Data;

namespace TravelAgency.Controllers
{
    public class ItineraryActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItineraryActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItineraryActivities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ItineraryActivities.Include(i => i.Itinerary).Include(i => i.TravelActivity);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ItineraryActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraryActivity = await _context.ItineraryActivities
                .Include(i => i.Itinerary)
                .Include(i => i.TravelActivity)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itineraryActivity == null)
            {
                return NotFound();
            }

            return View(itineraryActivity);
        }

        // GET: ItineraryActivities/Create
        public IActionResult Create()
        {
            ViewData["ItineraryId"] = new SelectList(_context.Itineraries, "Id", "Id");
            ViewData["TravelActivityId"] = new SelectList(_context.TravelActivities, "Id", "ActivityName");
            return View();
        }

        // POST: ItineraryActivities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TravelActivityId,ItineraryId")] ItineraryActivity itineraryActivity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itineraryActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItineraryId"] = new SelectList(_context.Itineraries, "Id", "Id", itineraryActivity.ItineraryId);
            ViewData["TravelActivityId"] = new SelectList(_context.TravelActivities, "Id", "ActivityName", itineraryActivity.TravelActivityId);
            return View(itineraryActivity);
        }

        // GET: ItineraryActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraryActivity = await _context.ItineraryActivities.FindAsync(id);
            if (itineraryActivity == null)
            {
                return NotFound();
            }
            ViewData["ItineraryId"] = new SelectList(_context.Itineraries, "Id", "Id", itineraryActivity.ItineraryId);
            ViewData["TravelActivityId"] = new SelectList(_context.TravelActivities, "Id", "ActivityName", itineraryActivity.TravelActivityId);
            return View(itineraryActivity);
        }

        // POST: ItineraryActivities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TravelActivityId,ItineraryId")] ItineraryActivity itineraryActivity)
        {
            if (id != itineraryActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itineraryActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItineraryActivityExists(itineraryActivity.Id))
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
            ViewData["ItineraryId"] = new SelectList(_context.Itineraries, "Id", "Id", itineraryActivity.ItineraryId);
            ViewData["TravelActivityId"] = new SelectList(_context.TravelActivities, "Id", "ActivityName", itineraryActivity.TravelActivityId);
            return View(itineraryActivity);
        }

        // GET: ItineraryActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraryActivity = await _context.ItineraryActivities
                .Include(i => i.Itinerary)
                .Include(i => i.TravelActivity)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itineraryActivity == null)
            {
                return NotFound();
            }

            return View(itineraryActivity);
        }

        // POST: ItineraryActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itineraryActivity = await _context.ItineraryActivities.FindAsync(id);
            if (itineraryActivity != null)
            {
                _context.ItineraryActivities.Remove(itineraryActivity);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItineraryActivityExists(int id)
        {
            return _context.ItineraryActivities.Any(e => e.Id == id);
        }
    }
}
