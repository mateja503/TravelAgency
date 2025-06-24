using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Repository.Data;
using TravelAgency.Repository.General.Interface;

namespace TravelAgency.Repository.General.Implementation
{
    public class GeneralRepository<T> : IGeneralRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        public GeneralRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<T> Add(T item)
        {
            var res = await _db.Set<T>().AddAsync(item);
            await _db.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<T?> DeleteById(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _db.Set<T>();
            query = query.Where(filter);
            var res = await query.SingleOrDefaultAsync();
            return res;
        }

        public async Task<List<T>> GetAll()
        {
            return await _db.Set<T>().ToListAsync();

        }

        public async Task<T?> GetById(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _db.Set<T>();
            query = query.Where(filter);
            var res = await query.SingleOrDefaultAsync();
            return res;
        }


    }
}
