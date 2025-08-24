
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Runtime.InteropServices;
using TravelAgency.ApiResponses;
using TravelAgency.Domain.DTOs;
using TravelAgency.Domain.Models;
using TravelAgency.Domain.Shared;
using TravelAgency.Domain.ValueObjects;
using TravelAgency.Service.Interface;
using TravelAgency.ViewModels;

namespace TravelAgency.Controllers
{
    public class TravelPackagesController(ITravelPackageService travelpackageService): Controller
    {
        private readonly ITravelPackageService _travelpackageService = travelpackageService;
       
        // GET: TravelPackages
        public async Task<IActionResult> Index()
        {
            return View(await _travelpackageService.GetAll().ToListAsync());
        }


        public async Task<IActionResult> IntegrationWithOutSideApi() 
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://google.serper.dev/places?q=travel&apiKey=477393229248fdd379532f565dfbe072ee8e318e");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var res = await response.Content.ReadFromJsonAsync<TravelPackageResponse>();
            var random = new Random();

            List<TypeCurrency> randomCurrency = Enum.GetValues(typeof(TypeCurrency))
                     .Cast<TypeCurrency>()
                     .ToList();
          
            var travelPackages = res.places.Select(u => new TravelPackage
            {
                Tittle = u.Title,
                Description = "This is from outside api inregration",
                Capacity = random.Next(1, 101),
                Price = new Price 
                {
                    Amount = Math.Round((float)(random.NextDouble() * (2000 - 100) + 100),2),
                    TypeCurrency = randomCurrency[random.Next(randomCurrency.Count())]
                },
                DateRange = new DateRange 
                {
                    From = DateTime.Today.AddDays(random.Next(1, 30)),
                    To = DateTime.Today.AddDays(random.Next(1,90))
                }

            }).ToList();

            await _travelpackageService.AddRange(travelPackages);

            return RedirectToAction(nameof(Index));
        }

        // GET: TravelPackages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _travelpackageService.GetAll().Where(u => u.Id == id)
                .Include(u => u.ItineraryTravelPackage)
                    .ThenInclude(u => u.Itinerary)
                    .ThenInclude(u => u.ItineraryActivities)
                    .ThenInclude(u => u.TravelActivity)
                 .Select(u => new TravelPackageDto()
                 {
                     Id = u.Id,
                     Tittle = u.Tittle,
                     Description = u.Description,
                     Capacity = u.Capacity,
                     DateRange = new DateRangeDto()
                     {
                         From = u.DateRange.From,
                         To = u.DateRange.To,
                     },
                     Price = new PriceDto()
                     {
                         Amount = u.Price.Amount,
                         Currency = u.Price.TypeCurrency
                     },
                     TravelActivitiesList = u.ItineraryTravelPackage
                        .SelectMany(itp => itp.Itinerary.ItineraryActivities)
                        .Select(ia => ia.TravelActivity)
                         //.Distinct() 
                         .Select(ta => new TravelActivityDto
                         {
                             ActivityName = ta.ActivityName,
                             SeasonType = ta.SeasonType
                         })
                                .ToList()

                 })
                 .FirstOrDefaultAsync();


            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // GET: TravelPackages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TravelPackages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TravelPackageViewModel model)
        {
                var travelPackage = new TravelPackage
                {
                    Tittle = model.Tittle,
                    Description = model.Description,
                    Capacity = model.Capacity,
                    Price = new Price 
                    {
                        Amount = model.Amount,
                        TypeCurrency = model.TypeCurrency
                    },
                    DateRange = new DateRange 
                    {
                        From = model.From,
                        To = model.To,
                    }
                    
                };
                await _travelpackageService.Add(travelPackage);
                return RedirectToAction(nameof(Index));
            
        }

        // GET: TravelPackages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _travelpackageService.GetById(id ?? 0);
            if (travelPackage == null)
            {
                return NotFound();
            }
            return View(travelPackage);
        }

        // POST: TravelPackages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tittle,Description,Capacity,Price,DateRange")] TravelPackage travelPackage)
        {
            if (id != travelPackage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _travelpackageService.Update(travelPackage);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TravelPackageExists(travelPackage.Id))
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
            return View(travelPackage);
        }

        // GET: TravelPackages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _travelpackageService.GetById(id ?? 0);
            if (travelPackage == null)
            {
                return NotFound();
            }

            return View(travelPackage);
        }

        // POST: TravelPackages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _travelpackageService.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private  bool TravelPackageExists(int id)
        {
            return _travelpackageService.GetAll().AnyAsync(e => e.Id == id).Result;
        }    }
}
