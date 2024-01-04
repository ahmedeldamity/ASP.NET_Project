using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregiate;
using Talabat.Core.IRepositories;

namespace Talabat.Core
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        IGenericRepositories<TEntity> Repository<TEntity> () where TEntity : BaseEntity;

        Task<int> CompleteAsync(); 
        
    }
}
