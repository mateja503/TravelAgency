﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Data;
using TravelAgency.Repository.General.Implementation;
using TravelAgency.Repository.Interface;

namespace TravelAgency.Repository.Implementation
{
    public class ItineraryTravelPackageRepository : GeneralRepository<ItineraryTravelPackage>, IItineraryTravelPackageRepostitory
    {

        private readonly ApplicationDbContext _db;
        public ItineraryTravelPackageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public Task<ItineraryTravelPackage> Update(int id, ItineraryTravelPackage item)
        {
            throw new NotImplementedException();
        }
    }
}
