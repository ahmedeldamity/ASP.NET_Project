using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class Basket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }

        public Basket(string id)
        {
            Id = id;
            Items = new List<BasketItem>();
        }
    }
}
