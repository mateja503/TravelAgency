using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Data;
using TravelAgency.Repository.General.Implementation;
using TravelAgency.Repository.General.Interface;
using TravelAgency.Repository.Interface;

namespace TravelAgency.Repository.Implementation
{
    public class ItineraryActivityRepository : GeneralRepository<ItineraryActivity>, IItineraryActivityRepository
    {
        private readonly ApplicationDbContext _db;
        public ItineraryActivityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ItineraryActivity> Update(ItineraryActivity item)
        {
            _db.ItineraryActivities.Update(item);
            await _db.SaveChangesAsync();
            return item;
        }
    }
}
