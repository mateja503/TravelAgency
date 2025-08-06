using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.DTOs;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.General.Interface;

namespace TravelAgency.Repository.Interface
{
    public interface ITravelPackageRepository : IGeneralRepository<TravelPackage>
    {
        public Task<TravelPackage> Update(int id, TravelPackage item);

        public Task<TravelPackageDto?> GetTravelPackageForDetail(int id);
    }
}
