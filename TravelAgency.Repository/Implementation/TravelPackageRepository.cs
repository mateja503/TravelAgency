using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.DTOs;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Data;
using TravelAgency.Repository.General.Implementation;
using TravelAgency.Repository.Interface;

namespace TravelAgency.Repository.Implementation
{
    public class TravelPackageRepository : GeneralRepository<TravelPackage>, ITravelPackageRepository
    {
        private readonly ApplicationDbContext _db;

        public TravelPackageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<TravelPackageDto?> GetTravelPackageForDetail(int id) 
        {
            return await _db.TravelPackages.Where(u => u.Id == id)
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
        }

        public Task<TravelPackage> Update(TravelPackage item)
        {
            throw new NotImplementedException();
        }
    }
}
