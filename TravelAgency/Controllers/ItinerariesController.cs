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
        private readonly IItineraryActivityService _itineraryActivityService;
        private readonly IItineraryTravelPackageService _itineraryTravelPackageService;
    
       
        public ItinerariesController(ApplicationDbContext context,
            IItineraryService itineraryService,
            ITravelActivityService travelActivityService,
            ITravelPackageService travelPackageService,
            IItineraryActivityService itineraryActivityService,
            IItineraryTravelPackageService itineraryTravelPackageService

            )
        {
            _itineraryService = itineraryService;
            _travelActivityService = travelActivityService;
            _travelPackageService = travelPackageService;
            _itineraryActivityService = itineraryActivityService;
            _itineraryTravelPackageService = itineraryTravelPackageService;



        }

        // GET: Itineraries
        public async Task<IActionResult> Index()
        {
            var itineraries = await _itineraryService.GetAll()
             .Include(u => u.ItineraryTravelPackage)
             .ThenInclude(t => t.TravelPackage)
             .Include(u => u.ItineraryActivities)
             .ThenInclude(t => t.TravelActivity)
             .Select(u => new ItineraryDto()
             {
                 Id = u.Id,
                 Name = u.Name,
                 TravelPackage = new TravelPackageDto()
                 {
                     Id = u.ItineraryTravelPackage.Select(t => t.TravelPackage.Id).First(),
                     Tittle = u.ItineraryTravelPackage.Select(t => t.TravelPackage.Tittle).First()
                 },
                 TravelActivity = new TravelActivityDto()
                 {
                     Id = u.ItineraryActivities.Select(t => t.TravelActivity.Id).First(),
                     ActivityName = u.ItineraryActivities.Select(t => t.TravelActivity.ActivityName).First(),
                     SeasonType = u.ItineraryActivities.Select(t => t.TravelActivity.SeasonType).First()
                 }
             }
             )
             .ToListAsync();

            return View(itineraries);
        }

        // GET: Itineraries/Details/5
        public async Task<IActionResult> Details(int? id)
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
                  Id = u.Id,
                  Name = u.Name,
                  TravelPackage = new TravelPackageDto()
                  {
                      Id = u.ItineraryTravelPackage.Select(t => t.TravelPackage.Id).First(),
                      Tittle = u.ItineraryTravelPackage.Select(t => t.TravelPackage.Tittle).First()
                  },
                  TravelActivity = new TravelActivityDto()
                  {
                      Id = u.ItineraryActivities.Select(t => t.TravelActivity.Id).First(),
                      ActivityName = u.ItineraryActivities.Select(t => t.TravelActivity.ActivityName).First(),
                      SeasonType = u.ItineraryActivities.Select(t => t.TravelActivity.SeasonType).First()
                  }
              }
              )
              .FirstOrDefaultAsync();

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
                   Id = u.Id,
                   Name = u.Name,
                   SelectedTravelPackageId = u.ItineraryTravelPackage.Select(t => t.TravelPackage.Id).First(),
                   ItineraryTravelPackageId = u.ItineraryTravelPackage.Select(t => t.Id).First(),
                   TravelPackage = new TravelPackageDto()
                   {
                       Id = u.ItineraryTravelPackage.Select(t => t.TravelPackage.Id).First(),
                       Tittle = u.ItineraryTravelPackage.Select(t => t.TravelPackage.Tittle).First()
                   },
                   SelectedActivityId = u.ItineraryActivities.Select(t => t.TravelActivity.Id).First(),
                   ItineraryActivityId = u.ItineraryActivities.Select(t => t.Id).First(),
                   TravelActivity = new TravelActivityDto()
                   {
                       Id = u.ItineraryActivities.Select(t => t.TravelActivity.Id).First(),
                       ActivityName = u.ItineraryActivities.Select(t => t.TravelActivity.ActivityName).First(),
                       SeasonType = u.ItineraryActivities.Select(t => t.TravelActivity.SeasonType).First()
                   }
               }
               )
               .FirstOrDefaultAsync();


            if (itinerary == null)
            {
                return NotFound();
            }
            ViewData["TravelActivity"] = new SelectList(await _travelActivityService.GetAll().ToListAsync(), "Id", "ActivityName");
            ViewData["TravelPackage"] = new SelectList(await _travelPackageService.GetAll().ToListAsync(), "Id", "Tittle");
            
            return View(itinerary);
        }

        // POST: Itineraries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,int itineraryActivityId,int ItineraryTravelPackageId, [Bind("Id","Name", "SelectedTravelPackageId", "SelectedActivityId")] Itinerary itinerary)
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
                    await _itineraryTravelPackageService.Update(new ItineraryTravelPackage 
                    {
                        Id = ItineraryTravelPackageId,
                        TravelPackageId = itinerary.SelectedTravelPackageId,
                        ItineraryId = id
                    });
                    await _itineraryActivityService.Update(new ItineraryActivity
                    {
                        Id = itineraryActivityId,
                        TravelActivityId = itinerary.SelectedActivityId,
                        ItineraryId = id
                    });
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
