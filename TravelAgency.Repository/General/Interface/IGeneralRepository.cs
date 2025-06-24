using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Repository.Data;

namespace TravelAgency.Repository.General.Interface
{
    public interface IGeneralRepository<T> where T : class
    {
        Task<List<T>> GetAll();

        Task<T?> GetById(Expression<Func<T, bool>> filter);

        Task<T> Add(T item);

        Task<T?> DeleteById(Expression<Func<T, bool>> filter);
    }
}
