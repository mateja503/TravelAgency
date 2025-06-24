using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Repository.Interface;
using TravelAgency.Service.General;
using TravelAgency.Service.Interface;

namespace TravelAgency.Service.Implementation
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        public async Task<Customer?> Add(Customer item)
        {
            return await this._customerRepository.Add(item);
        }

        public async Task<Customer?> DeleteById(int id)
        {
            return await this._customerRepository.DeleteById(u=>u.Id == id);
        }

        public async Task<List<Customer>> GetAll()
        {
            return await this._customerRepository.GetAll();
        }

        public async Task<Customer?> GetById(int id)
        {
            return await this._customerRepository.GetById(u=>u.Id == id);
        }

        public async Task<Customer?> Update(int id, Customer item)
        {
            return await this._customerRepository.Update(id,item);
        }
    }
}
