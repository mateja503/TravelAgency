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
    public class ItineraryActivitiesController : Controller
    {
        private readonly IItineraryActivityService _itineraryActivityService;
        private readonly IItineraryService _itineraryService;
        private readonly ITravelActivityService _travelActivityService;


        public ItineraryActivitiesController(IItineraryActivityService itineraryActivityService,
            IItineraryService itineraryService, ITravelActivityService travelActivityService)
        {
            _itineraryActivityService = itineraryActivityService;
            _itineraryService = itineraryService;
            _travelActivityService = travelActivityService;
        }

        // GET: ItineraryActivities
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.ItineraryActivities.Include(i => i.Itinerary).Include(i => i.TravelActivity);
            return View(await _itineraryActivityService.GetAll().ToListAsync());
        }

        // GET: ItineraryActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraryActivity = await _itineraryActivityService.GetAll()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itineraryActivity == null)
            {
                return NotFound();
            }

            return View(itineraryActivity);
        }

        // GET: ItineraryActivities/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ItineraryId"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Id");
            ViewData["TravelActivityId"] = new SelectList(await _travelActivityService.GetAll().ToListAsync(), "Id", "ActivityName");
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
              
                await _itineraryActivityService.Add(itineraryActivity);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItineraryId"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Id");
            ViewData["TravelActivityId"] = new SelectList(await _travelActivityService.GetAll().ToListAsync(), "Id", "ActivityName");
            return View(itineraryActivity);
        }

        // GET: ItineraryActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraryActivity = await _itineraryActivityService.GetAll().FirstOrDefaultAsync(u=>u.ItineraryId == id);
            if (itineraryActivity == null)
            {
                return NotFound();
            }
            ViewData["ItineraryId"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Id");
            ViewData["TravelActivityId"] = new SelectList(await _travelActivityService.GetAll().ToListAsync(), "Id", "ActivityName");
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
                   
                    await _itineraryActivityService.Update(itineraryActivity);
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
            ViewData["ItineraryId"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Id");
            ViewData["TravelActivityId"] = new SelectList(await _travelActivityService.GetAll().ToListAsync(), "Id", "ActivityName");
            return View(itineraryActivity);
        }

        // GET: ItineraryActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itineraryActivity = await _itineraryActivityService.GetAll()
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
            var itineraryActivity = await _itineraryActivityService.GetAll()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itineraryActivity != null)
            {
               await _itineraryActivityService.DeleteById(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ItineraryActivityExists(int id)
        {
            return _itineraryActivityService.GetAll()
                .Any(e => e.Id == id);
        }
    }
}
