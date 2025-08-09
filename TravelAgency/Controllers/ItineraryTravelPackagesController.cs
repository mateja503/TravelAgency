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
    public class ItineraryTravelPackagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItineraryTravelPackagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ItineraryTravelPackages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ItineraryTravelPackages.Include(i => i.Itinerary).Include(i => i.TravelPackage);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ItineraryTravelPackages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraryTravelPackage = await _context.ItineraryTravelPackages
                .Include(i => i.Itinerary)
                .Include(i => i.TravelPackage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itineraryTravelPackage == null)
            {
                return NotFound();
            }

            return View(itineraryTravelPackage);
        }

        // GET: ItineraryTravelPackages/Create
        public IActionResult Create()
        {
            ViewData["ItineraryId"] = new SelectList(_context.Itineraries, "Id", "Id");
            ViewData["TravelPackageId"] = new SelectList(_context.TravelPackages, "Id", "Description");
            return View();
        }

        // POST: ItineraryTravelPackages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TravelPackageId,ItineraryId")] ItineraryTravelPackage itineraryTravelPackage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itineraryTravelPackage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItineraryId"] = new SelectList(_context.Itineraries, "Id", "Id", itineraryTravelPackage.ItineraryId);
            ViewData["TravelPackageId"] = new SelectList(_context.TravelPackages, "Id", "Description", itineraryTravelPackage.TravelPackageId);
            return View(itineraryTravelPackage);
        }

        // GET: ItineraryTravelPackages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraryTravelPackage = await _context.ItineraryTravelPackages.FindAsync(id);
            if (itineraryTravelPackage == null)
            {
                return NotFound();
            }
            ViewData["ItineraryId"] = new SelectList(_context.Itineraries, "Id", "Id", itineraryTravelPackage.ItineraryId);
            ViewData["TravelPackageId"] = new SelectList(_context.TravelPackages, "Id", "Description", itineraryTravelPackage.TravelPackageId);
            return View(itineraryTravelPackage);
        }

        // POST: ItineraryTravelPackages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TravelPackageId,ItineraryId")] ItineraryTravelPackage itineraryTravelPackage)
        {
            if (id != itineraryTravelPackage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itineraryTravelPackage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItineraryTravelPackageExists(itineraryTravelPackage.Id))
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
            ViewData["ItineraryId"] = new SelectList(_context.Itineraries, "Id", "Id", itineraryTravelPackage.ItineraryId);
            ViewData["TravelPackageId"] = new SelectList(_context.TravelPackages, "Id", "Description", itineraryTravelPackage.TravelPackageId);
            return View(itineraryTravelPackage);
        }

        // GET: ItineraryTravelPackages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraryTravelPackage = await _context.ItineraryTravelPackages
                .Include(i => i.Itinerary)
                .Include(i => i.TravelPackage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itineraryTravelPackage == null)
            {
                return NotFound();
            }

            return View(itineraryTravelPackage);
        }

        // POST: ItineraryTravelPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itineraryTravelPackage = await _context.ItineraryTravelPackages.FindAsync(id);
            if (itineraryTravelPackage != null)
            {
                _context.ItineraryTravelPackages.Remove(itineraryTravelPackage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItineraryTravelPackageExists(int id)
        {
            return _context.ItineraryTravelPackages.Any(e => e.Id == id);
        }
    }
}
