using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Data;
using TravelAgency.Repository.Identity;
using TravelAgency.Service.Implementation;
using TravelAgency.Service.Interface;

namespace TravelAgency.Controllers
{

    [Authorize]
    public class BookingsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBookingService _bookingService;
        private readonly IItineraryService _itineraryService;
        private readonly ITravelPackageService _travelPackageService;
        private readonly ICustomerService _customerService;
        private readonly IItineraryTravelPackageService _itineraryTravelPackageService;
        public BookingsController(UserManager<ApplicationUser> userManager,
            IBookingService bookingService, IItineraryService itineraryService,
            ITravelPackageService travelPackageService, ICustomerService customerService,
            IItineraryTravelPackageService itineraryTravelPackageService)
        {
            _userManager = userManager;
            _bookingService = bookingService;
            _itineraryService = itineraryService;
            _travelPackageService = travelPackageService;
            _customerService = customerService;
            _itineraryTravelPackageService = itineraryTravelPackageService;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            bool isAdmin = false;
            if (await _userManager.IsInRoleAsync(currentUser!, "Admin"))
            {
                isAdmin = true;
            }

            var query = _bookingService.GetAll()
                .Include(b => b.Customer)
                .Include(b => b.Itinerary)
                .AsQueryable();

            if (!isAdmin) 
            {
                query = query.Where(u => u.CustomerId == currentUser!.CustomerId);
            }

            var applicationDbContext = await query.ToListAsync();

            return View(applicationDbContext);
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetAll().FirstOrDefaultAsync(m => m.Id == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public async Task<IActionResult> Create()
        {
            //var user = await _userManager.GetUserAsync(User);
            //ViewData["Customers"] = new SelectList(await _customerService.GetAll().ToListAsync(),"Id", "FullName");
            ViewData["Itinerary"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Name");
            //ViewData["TravelPackage"] = new SelectList(await _travelPackageService.GetAll().ToListAsync(), "Id", "Tittle");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ItineraryId,Capacity,DateRange,Status")] Booking booking)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            booking.CustomerId = currentUser!.CustomerId;

            var itineraryTravelPackage = await _itineraryTravelPackageService
                .GetAll().Where(u => u.ItineraryId == booking.ItineraryId)
                .Include(u => u.TravelPackage)
                .Select(u => new 
                {
                    capacityTravelPackage = u.TravelPackage.Capacity,
                    dataRangeTravelPackage = u.TravelPackage.DateRange,
                    travelPackage = u.TravelPackage
                })
                .FirstOrDefaultAsync();

            if (itineraryTravelPackage is null) 
            {
                //ModelState.AddModelError("", "Selected itinerary does not exist."); 
                ViewData["Itinerary"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Name");
                return View();
            }
        
            if (booking.Capacity > itineraryTravelPackage?.capacityTravelPackage) 
            {
                ModelState.AddModelError("Capacity", "Capacity exceeds available travel package capacity.");
                ViewData["Itinerary"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Name");
                return View();
            }

            if (booking.DateRange.From >= itineraryTravelPackage?.dataRangeTravelPackage?.From && booking.DateRange.To <= itineraryTravelPackage.dataRangeTravelPackage.To) 
            {
                ModelState.AddModelError("DateRange", "Selected date range is outside of the travel package range.");
                ViewData["Itinerary"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Name");
                return View();
            }

            itineraryTravelPackage.travelPackage.Capacity -= booking.Capacity;
            await _travelPackageService.Update(itineraryTravelPackage.travelPackage);
            await _bookingService.Add(booking);
            return RedirectToAction(nameof(Index));
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetAll()
                .Include(u=>u.Customer)
                .Include(u=>u.Itinerary).FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }
            //var user = await _userManager.GetUserAsync(User);
            //ViewData["Customers"] = new SelectList(await _customerService.GetAll().ToListAsync(), "Id", "FullName");
            ViewData["Itineraries"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Name");
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,ItineraryId,DateRange,Status")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _bookingService.Update(booking);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
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
            ViewData["Customers"] = new SelectList(await _customerService.GetAll().ToListAsync(), "Id", "FullName");
            ViewData["Itineraries"] = new SelectList(await _itineraryService.GetAll().ToListAsync(), "Id", "Name");
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetAll().FirstOrDefaultAsync(m => m.Id == id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _bookingService.GetAll().FirstOrDefaultAsync(m => m.Id == id);
            if (booking != null)
            {
               await _bookingService.DeleteById(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            //return _context.Bookings.Any(e => e.Id == id);
            return  _bookingService.GetAll().AnyAsync(m => m.Id == id).Result;

        }
    }
}
