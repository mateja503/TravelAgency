using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.DTOs;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Interface;
using TravelAgency.Service.Interface;

namespace TravelAgency.Service.Implementation
{
    public class TravelPackageService(ITravelPackageRepository travelPackageRepository) : ITravelPackageService
    {
        private readonly ITravelPackageRepository _travelPackageRepository = travelPackageRepository;
        public async Task<TravelPackage?> Add(TravelPackage item)
        {
            return await this._travelPackageRepository.Add(item);
        }

        public async Task<TravelPackage?> DeleteById(int id)
        {
            return await this._travelPackageRepository.DeleteById(u=>u.Id == id);
        }

        public async Task<List<TravelPackage>> GetAll()
        {
            return await this._travelPackageRepository.GetAll();
        }

        public async Task<TravelPackage?> GetById(int id)
        {
            return await _travelPackageRepository.GetById(u=>u.Id == id);
        }

        public async Task<TravelPackageDto> GetTravelPackageDetail(int id) 
        {
            var travelPackege = await _travelPackageRepository.GetTravelPackageForDetail(id);
            throw new NotImplementedException();
        }
        public async Task<TravelPackage?> Update(int id, TravelPackage item)
        {
            return await this._travelPackageRepository.Update(id,item);
        }
    }
}
