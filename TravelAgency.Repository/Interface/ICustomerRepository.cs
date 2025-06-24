using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.General.Interface;

namespace TravelAgency.Repository.Interface
{
    public interface ICustomerRepository : IGeneralRepository<Customer>
    {
        public Task<Customer?> Update(int id, Customer item);
    }
}
