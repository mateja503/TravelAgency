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
        {//TODO
            return await _db.TravelPackages.Where(u => u.Id == id)
                .Include(u => u.ItineraryTravelPackage)
                    .ThenInclude(u => u.Itinerary)
                    .ThenInclude(u => u.ItineraryActivities)
                    .ThenInclude(u => u.TravelActivity)
                 .Select(u => new TravelPackageDto() 
                 {
                    Tittle = u.Tittle,
                    Description = u.Description,
                    Capacity = u.Capacity,
                    DateRange = u.DateRange

                 
                 })
                 .FirstOrDefaultAsync();
        }

        public Task<TravelPackage> Update(int id, TravelPackage item)
        {
            throw new NotImplementedException();
        }
    }
}
