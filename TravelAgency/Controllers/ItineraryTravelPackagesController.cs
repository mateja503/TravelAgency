using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Data;
using TravelAgency.Service.Interface;

namespace TravelAgency.Controllers
{
    public class ItineraryTravelPackagesController : Controller
    {
        private readonly IItineraryTravelPackageService _itineraryTravelPackageService;
        private readonly IItineraryService _itineraryService;
        private readonly ITravelPackageService _travelPackageService;
        public ItineraryTravelPackagesController(
            IItineraryTravelPackageService itineraryTravelPackageService,
            IItineraryService itineraryService,
            ITravelPackageService travelPackageService)
        {
            _itineraryTravelPackageService = itineraryTravelPackageService;
            _itineraryService = itineraryService;
            _travelPackageService = travelPackageService;
        }

        // GET: ItineraryTravelPackages
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.ItineraryTravelPackages.Include(i => i.Itinerary).Include(i => i.TravelPackage);
            var applicationDbContext = await _itineraryTravelPackageService.GetAll().Include(u => u.Itinerary).Include(u => u.TravelPackage).ToListAsync();
            return View(applicationDbContext);
        }

        // GET: ItineraryTravelPackages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraryTravelPackage = await _itineraryTravelPackageService.GetAll()
                .Include(u => u.Itinerary)
                .Include(u => u.TravelPackage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itineraryTravelPackage == null)
            {
                return NotFound();
            }

            return View(itineraryTravelPackage);
        }

        // GET: ItineraryTravelPackages/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ItineraryId"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Id");
            ViewData["TravelPackageId"] = new SelectList(await _travelPackageService.GetAll().ToListAsync(), "Id", "Description");
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
                await _itineraryTravelPackageService.Add(itineraryTravelPackage);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItineraryId"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Id");
            ViewData["TravelPackageId"] = new SelectList(await _travelPackageService.GetAll().ToListAsync(), "Id", "Description");
            return View(itineraryTravelPackage);
        }

        // GET: ItineraryTravelPackages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraryTravelPackage = await _itineraryTravelPackageService.GetAll()
               //.Include(u => u.Itinerary)
               //.Include(u => u.TravelPackage)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (itineraryTravelPackage == null)
            {
                return NotFound();
            }
            ViewData["ItineraryId"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Id");
            ViewData["TravelPackageId"] = new SelectList(await _travelPackageService.GetAll().ToListAsync(), "Id", "Description");
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
                    await _itineraryTravelPackageService.Update(itineraryTravelPackage);
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
            ViewData["ItineraryId"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Id");
            ViewData["TravelPackageId"] = new SelectList(await _travelPackageService.GetAll().ToListAsync(), "Id", "Description");
            return View(itineraryTravelPackage);
        }

        // GET: ItineraryTravelPackages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraryTravelPackage = await _itineraryTravelPackageService.GetAll()
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
            var itineraryTravelPackage =  await _itineraryTravelPackageService.GetAll()
               //.Include(u => u.Itinerary)
               //.Include(u => u.TravelPackage)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (itineraryTravelPackage != null)
            {
                await _itineraryTravelPackageService.DeleteById(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ItineraryTravelPackageExists(int id)
        {
            return _itineraryTravelPackageService.GetAll().Any(e => e.Id == id);
        }
    }
}
