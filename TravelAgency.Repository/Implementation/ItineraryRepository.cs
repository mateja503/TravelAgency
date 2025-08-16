using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Data;
using TravelAgency.Repository.General.Implementation;
using TravelAgency.Repository.Interface;

namespace TravelAgency.Repository.Implementation
{
    public class ItineraryRepository : GeneralRepository<Itinerary>, IItineraryRepository
    {
        private readonly ApplicationDbContext _db;
        public ItineraryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Itinerary> Update(Itinerary item)
        {

            _db.Itineraries.Update(item);
            await _db.SaveChangesAsync();
            return item;
        }
    }
}
