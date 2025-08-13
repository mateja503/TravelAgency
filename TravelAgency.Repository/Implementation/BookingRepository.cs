using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Data;
using TravelAgency.Repository.General.Implementation;
using TravelAgency.Repository.Interface;

namespace TravelAgency.Repository.Implementation
{
    public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Booking?> Update(Booking item)
        {
            var res = await GetById(u => u.Id == item.Id);
            if (res == null)
            {
                return null;
            }

            res.CustomerId = item.CustomerId;
            res.TravelPackageId = item.TravelPackageId;
            res.ItineraryId = item.ItineraryId;
            res.Status = item.Status;
            res.DateRange.From = item.DateRange.From;
            res.DateRange.To = item.DateRange.To;

            _db.Update(res);
            await _db.SaveChangesAsync();
            return res;
        }
    }
}
