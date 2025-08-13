
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Models;
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

        // GET: TravelPackages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var travelPackage = await _travelpackageService.GetTravelPackageDetail(id ?? 0);


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
            if (ModelState.IsValid)
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
            return View(model);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tittle,Description,Capacity")] TravelPackage travelPackage)
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
            var travelPackage = await  _travelpackageService.GetById(id);
            if (travelPackage != null)
            {
                await _travelpackageService.DeleteById(id);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetPropertiesHTML() 
        {
            return View();
        }

        private  bool TravelPackageExists(int id)
        {
            return _travelpackageService.GetAll().AnyAsync(e => e.Id == id).Result;
        }    }
}
