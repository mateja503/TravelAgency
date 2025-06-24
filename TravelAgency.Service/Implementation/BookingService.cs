using System;
using System.Collections.Generic;
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

        public async Task<List<Booking>> GetAll()
        {
            return await this._bookingRepository.GetAll();
        }

        public async Task<Booking?> GetById(int id)
        {
            return await this._bookingRepository.GetById(u=>u.Id == id);
        }

        public async Task<Booking?> Update(int id, Booking item)
        {
            return await this._bookingRepository.Update(id, item);
        }
    }
}
