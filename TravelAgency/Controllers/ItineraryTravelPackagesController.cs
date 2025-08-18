using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Data;
using TravelAgency.Service.Implementation;
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
            ViewData["Itineraries"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Name");
            ViewData["TravelPackages"] = new SelectList(await _travelPackageService.GetAll().ToListAsync(), "Id", "Tittle");
            return View();
        }

        // POST: ItineraryTravelPackages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TravelPackageId,ItineraryId")] ItineraryTravelPackage itineraryTravelPackage)
        {
            if (itineraryTravelPackage.TravelPackageId != 0 && itineraryTravelPackage.ItineraryId != 0)
            {
                await _itineraryTravelPackageService.Add(itineraryTravelPackage);
                return RedirectToAction(nameof(Index));
            }
            ViewData["Itineraries"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Name");
            ViewData["TravelPackages"] = new SelectList(await _travelPackageService.GetAll().ToListAsync(), "Id", "Title");
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
               .Include(u => u.Itinerary)
               .Include(u => u.TravelPackage)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (itineraryTravelPackage == null)
            {
                return NotFound();
            }
            ViewData["Itineraries"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Name");
            ViewData["TravelPackages"] = new SelectList(await _travelPackageService.GetAll().ToListAsync(), "Id", "Tittle");
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

            if (itineraryTravelPackage.ItineraryId != 0 && itineraryTravelPackage.TravelPackageId != 0)
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
                .Include(u=>u.TravelPackage).Include(u=>u.Itinerary)
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
            await _itineraryTravelPackageService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ItineraryTravelPackageExists(int id)
        {
            return _itineraryTravelPackageService.GetAll().Any(e => e.Id == id);
        }
    }
}
