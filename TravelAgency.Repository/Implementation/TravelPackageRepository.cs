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

       
        public async Task<TravelPackage> Update(TravelPackage item)
        {
             _db.TravelPackages.Update(item);
             await _db.SaveChangesAsync();
             return item;
        }
    }
}
