using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Data;
using TravelAgency.Repository.General.Implementation;
using TravelAgency.Repository.Interface;

namespace TravelAgency.Repository.Implementation
{
    public class TravelActivityRepository : GeneralRepository<TravelActivity>, ITravelActivityRepository
    {
        private readonly ApplicationDbContext _db;

        public TravelActivityRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task<TravelActivity> Update(TravelActivity item)
        {
            throw new NotImplementedException();
        }
    }
}
