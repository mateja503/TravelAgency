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
    public class ItineraryActivityService(IItineraryActivityRepository itineraryActivityRepository) : IItineraryActivityService
    {
        private readonly IItineraryActivityRepository _itineraryActivityRepository = itineraryActivityRepository;
        public async Task<ItineraryActivity?> Add(ItineraryActivity item)
        {
            return await this._itineraryActivityRepository.Add(item);
        }

        public async Task<ItineraryActivity?> DeleteById(int id)
        {
            return await this._itineraryActivityRepository.DeleteById(u=>u.Id == id);
        }

        public async Task<List<ItineraryActivity>> GetAll()
        {
            return await this._itineraryActivityRepository.GetAll();
        }

        public async Task<ItineraryActivity?> GetById(int id)
        {
            return await this._itineraryActivityRepository.GetById(u=>u.Id == id);
        }

        public async Task<ItineraryActivity?> Update(int id, ItineraryActivity item)
        {
            return await this._itineraryActivityRepository.Update(id,item);
        }
    }
}
