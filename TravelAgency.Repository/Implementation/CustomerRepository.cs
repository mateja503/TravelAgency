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
    public class CustomerRepository : GeneralRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Customer?> Update( Customer item)
        {
            var res = await GetById(u => u.Id == item.Id);
            if (res == null) {
                return null;
            }
            res.Name = item.Name;
            //res.Address = item.Address;
            _db.Update(res);
            await _db.SaveChangesAsync();
            return res;
        }
    }
}
