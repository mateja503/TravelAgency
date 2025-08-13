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
    public class ItinerariesController : Controller
    {
        private readonly IItineraryService _itineraryService;

        public ItinerariesController(ApplicationDbContext context, IItineraryService itineraryService)
        {
            _itineraryService = itineraryService;
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Itineraries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Itinerary itinerary)
        {
            if (ModelState.IsValid)
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

            var itinerary = await _itineraryService.GetAll().FirstOrDefaultAsync(m => m.Id == id);
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
