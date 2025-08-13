using Microsoft.EntityFrameworkCore;
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
    public class ItineraryTravelPackageService(IItineraryTravelPackageRepostitory itineraryTravelPackageRepostitory)
    : IItineraryTravelPackageService
    {
        private readonly IItineraryTravelPackageRepostitory _itineraryTravelPackageRepostitory = itineraryTravelPackageRepostitory;
        public async Task<ItineraryTravelPackage?> Add(ItineraryTravelPackage item)
        {
            return await _itineraryTravelPackageRepostitory.Add(item);
        }

        public async Task<ItineraryTravelPackage?> DeleteById(int id)
        {
            return await _itineraryTravelPackageRepostitory.DeleteById(u=>u.Id == id);
        }

        public IQueryable<ItineraryTravelPackage> GetAll()
        {
            return   _itineraryTravelPackageRepostitory.GetAll().AsQueryable();
        }

        public async Task<ItineraryTravelPackage?> GetById(int id)
        {
            return await _itineraryTravelPackageRepostitory.GetById(u=>u.Id == id);
        }

        public async Task<ItineraryTravelPackage?> Update(ItineraryTravelPackage item)
        {
            return await _itineraryTravelPackageRepostitory.Update(item);
        }
    }
}
