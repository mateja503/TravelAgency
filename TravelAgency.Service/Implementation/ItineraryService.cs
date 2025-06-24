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
    public class ItineraryService(IItineraryRepository itineraryRepository) : IItineraryService
    {
        private readonly IItineraryRepository _itineraryRepository = itineraryRepository;
        public async Task<Itinerary?> Add(Itinerary item)
        {
            return await this._itineraryRepository.Add(item);
        }

        public async Task<Itinerary?> DeleteById(int id)
        {
            return await this._itineraryRepository.DeleteById(u => u.Id == id);
        }

        public async Task<List<Itinerary>> GetAll()
        {
            return await this._itineraryRepository.GetAll();
        }

        public async Task<Itinerary?> GetById(int id)
        {
            return await this._itineraryRepository.GetById(u => u.Id == id);
        }

        public async Task<Itinerary?> Update(int id, Itinerary item)
        {
            return await this._itineraryRepository.Update(id,item);
        }
    }
}
