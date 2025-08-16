using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Interface;
using TravelAgency.Service.Interface;

namespace TravelAgency.Service.Implementation
{
    public class BookingService(IBookingRepository bookingRepository) : IBookingService
    {
        private readonly IBookingRepository _bookingRepository = bookingRepository;

        public async Task<Booking?> Add(Booking item)
        {
          return await this._bookingRepository.Add(item);
        }

        public async Task<Booking?> DeleteById(int id)
        {
            return await this._bookingRepository.DeleteById(u => u.Id == id);
        }

        public IQueryable<Booking> GetAll()
        {
            return _bookingRepository.GetAll()
                .Include(u=>u.Customer)
                .Include(u=>u.Itinerary)
                .AsQueryable();
        }

        public async Task<Booking?> GetById(int id)
        {
            return await this._bookingRepository.GetById(u=>u.Id == id);
        }

        public async Task<Booking?> Update(Booking item)
        {
            return await _bookingRepository.Update(item);
        }

    }
}
