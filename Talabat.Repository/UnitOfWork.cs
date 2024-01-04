using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregiate;
using Talabat.Core.IRepositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _storeDbContext;

        //private Dictionary<string, GenericRepositories<BaseEntity>> _repositories;
        private Hashtable _repositories;

        public UnitOfWork(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
            _repositories = new Hashtable();
        }

        public IGenericRepositories<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var key = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(key))
            {
                var repository = new GenericRepositories<TEntity>(_storeDbContext);

                _repositories.Add(key, repository);
            }

            return (GenericRepositories<TEntity>) _repositories[key] ;
        }
        public async Task<int> CompleteAsync()
        {
            return await _storeDbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _storeDbContext.DisposeAsync();
        }
    }
}
