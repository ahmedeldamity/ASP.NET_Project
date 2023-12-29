using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.IRepositories
{
    public interface IBasketRepository
    {
        Task<Basket?> GetBasketAsync(string basketId);
        Task<Basket?> CreateOrUpdateBasketAsync(Basket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
