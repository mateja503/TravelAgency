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
    public class TravelActivityService(ITravelActivityRepository travelActivityRepository) : ITravelActivityService
    {
        private readonly ITravelActivityRepository _travelActivityRepository = travelActivityRepository;

        public async Task<TravelActivity?> Add(TravelActivity item)
        {
            return await this._travelActivityRepository.Add(item);
        }

        public async Task<TravelActivity?> DeleteById(int id)
        {
            return await this._travelActivityRepository.DeleteById(u=>u.Id == id);
        }

        public async Task<List<TravelActivity>> GetAll()
        {
            return await this._travelActivityRepository.GetAll();
        }

        public async  Task<TravelActivity?> GetById(int id)
        {
            return await this._travelActivityRepository.GetById(u => u.Id == id);
        }

        public async Task<TravelActivity?> Update(int id, TravelActivity item)
        {
            return await this._travelActivityRepository.Update(id,item);
        }
    }
}
