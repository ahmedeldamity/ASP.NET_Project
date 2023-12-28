using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.IRepositories;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepositories<T> : IGenericRepositories<T> where T : BaseEntity
    {
        private readonly StoreDbContext _storeDbContext;

        public GenericRepositories(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() => await _storeDbContext.Set<T>().ToListAsync();
        public async Task<T?> GetByIdAsync(int id) => await _storeDbContext.Set<T>().FindAsync(id); // Find return: null or object
        public async Task<IReadOnlyList<T>> GetAllWithSpecificationsAsync(ISpecifications<T> specifications)
        {
            return await SpecificationsEvaluator<T>.GetQuery(_storeDbContext.Set<T>(), specifications).ToListAsync();
        }
        public async Task<T?> GetByIdWithSpecificationsAsync(ISpecifications<T> specifications)
        {
            return await SpecificationsEvaluator<T>.GetQuery(_storeDbContext.Set<T>(), specifications).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(ISpecifications<T> specifications)
        {
            return await SpecificationsEvaluator<T>.GetQuery(_storeDbContext.Set<T>(), specifications).CountAsync();
        }
    }
}
