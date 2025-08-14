using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Domain.DTOs;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Data;
using TravelAgency.Service.Implementation;
using TravelAgency.Service.Interface;

namespace TravelAgency.Controllers
{
    public class ItinerariesController : Controller
    {
        private readonly IItineraryService _itineraryService;
        private readonly ITravelActivityService _travelActivityService;
        private readonly ITravelPackageService _travelPackageService;
        public ItinerariesController(ApplicationDbContext context,
            IItineraryService itineraryService,
            ITravelActivityService travelActivityService,
            ITravelPackageService travelPackageService)
        {
            _itineraryService = itineraryService;
            _travelActivityService = travelActivityService;
            _travelPackageService = travelPackageService;
        }

        // GET: Itineraries
        public async Task<IActionResult> Index()
        {
            return View(await _itineraryService.GetAll().ToListAsync());
        }

        // GET: Itineraries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itinerary = await _itineraryService.GetAll().FirstOrDefaultAsync(m => m.Id == id);

            if (itinerary == null)
            {
                return NotFound();
            }

            return View(itinerary);
        }

        // GET: Itineraries/Create
        public async Task<IActionResult> Create()
        { 
            ViewData["TravelActivity"] = new SelectList(await _travelActivityService.GetAll().ToListAsync(), "Id", "ActivityName");
            ViewData["TravelPackage"] = new SelectList(await _travelPackageService.GetAll().ToListAsync(), "Id", "Tittle");

            return View();
        }

        // POST: Itineraries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name", "SelectedTravelPackageId", "SelectedActivityId")] Itinerary itinerary)
        {

            if (!string.IsNullOrEmpty(itinerary.Name) && itinerary.SelectedTravelPackageId != 0 && itinerary.SelectedActivityId != 0) 
            {
                await _itineraryService.Add(itinerary);
                return RedirectToAction(nameof(Index));
            }

            return View(itinerary);
           
        }

        // GET: Itineraries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var itinerary = await _itineraryService.GetAll()
                .Where(m => m.Id == id)
               .Include(u => u.ItineraryTravelPackage)
               .ThenInclude(t => t.TravelPackage)
               .Include(u => u.ItineraryActivities)
               .ThenInclude(t => t.TravelActivity)
               .Select(u => new ItineraryDto()
               {
                   Name = u.Name,
                   TravelActivities = u.ItineraryActivities
                       .Select(u => new TravelActivityDto()
                       {
                           ActivityName = u.TravelActivity.ActivityName,
                           SeasonType = u.TravelActivity.SeasonType
                       }).ToList(),
                   TravelPackages = u.ItineraryTravelPackage
                       .Select(u => new TravelPackageDto()
                       {
                           Tittle = u.TravelPackage.Tittle,
                       }).ToList()
               }
               )
               .FirstOrDefaultAsync();

            //var itinerary = await _itineraryService.GetAll().FirstOrDefaultAsync(m => m.Id == id);
            if (itinerary == null)
            {
                return NotFound();
            }
            return View(itinerary);
        }

        // POST: Itineraries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Itinerary itinerary)
        {
            if (id != itinerary.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _itineraryService.Update(itinerary);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItineraryExists(itinerary.Id))
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
            return View(itinerary);
        }

        // GET: Itineraries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itinerary = await _itineraryService.GetAll().FirstOrDefaultAsync(m => m.Id == id);

            if (itinerary == null)
            {
                return NotFound();
            }

            return View(itinerary);
        }

        // POST: Itineraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itinerary = await _itineraryService.GetAll().FirstOrDefaultAsync(m => m.Id == id);
            if (itinerary != null)
            {
                await _itineraryService.DeleteById(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ItineraryExists(int id)
        {
            return _itineraryService.GetAll().AnyAsync(m => m.Id == id).Result;

        }
    }
}
