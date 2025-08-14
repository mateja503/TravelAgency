using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Implementation;
using TravelAgency.Repository.Interface;
using TravelAgency.Service.Interface;

namespace TravelAgency.Service.Implementation
{
    public class ItineraryService(IItineraryRepository itineraryRepository,
        IItineraryActivityRepository itineraryActivityRepository, IItineraryTravelPackageRepostitory itineraryTravelPackageRepostitory) : IItineraryService
    {
        private readonly IItineraryRepository _itineraryRepository = itineraryRepository;
        private readonly IItineraryActivityRepository _itineraryActivityRepository = itineraryActivityRepository;
        private readonly IItineraryTravelPackageRepostitory _itineraryTravelPackageRepostitory = itineraryTravelPackageRepostitory;
        public async Task<Itinerary?> Add(Itinerary item)
        {
            await this._itineraryRepository.Add(item);

            await _itineraryActivityRepository.Add(new ItineraryActivity { ItineraryId = item.Id, TravelActivityId = item.SelectedActivityId });

            await _itineraryTravelPackageRepostitory.Add(new ItineraryTravelPackage { ItineraryId = item.Id, TravelPackageId = item.SelectedTravelPackageId });
           
            return item;
        }

        public async Task<Itinerary?> DeleteById(int id)
        {
            return await this._itineraryRepository.DeleteById(u => u.Id == id);
        }

        public IQueryable<Itinerary> GetAll()
        {
            return  _itineraryRepository.GetAll()
                .Include(u=>u.ItineraryActivities)
                .Include(u=>u.ItineraryTravelPackage).AsQueryable();
        }

        public async Task<Itinerary?> GetById(int id)
        {
            return await this._itineraryRepository.GetById(u => u.Id == id);
        }

        public async Task<Itinerary?> Update(Itinerary item)
        {
            return await this._itineraryRepository.Update(item);
        }
    }
}
