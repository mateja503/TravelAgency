using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Service.General
{
    public interface IGeneralService<T> where T : class
    {
        public Task<List<T>> GetAll();

        public Task<T?> GetById(int id);

        public Task<T?> DeleteById(int id);

        public Task<T?> Add(T item);

        public Task<T?> Update(int id, T item);



    }
}
