using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.General.Interface;

namespace TravelAgency.Repository.Interface
{
    public interface IItineraryActivityRepository : IGeneralRepository<ItineraryActivity>
    {
        public Task<ItineraryActivity> Update(int id, ItineraryActivity item);
    }
}
