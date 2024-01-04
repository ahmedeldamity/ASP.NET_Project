using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregiate
{
    public class OrderItem: BaseEntity
    {
        public OrderItem()
        {
            // we create this constractor because EF need it while migration
        }
        public OrderItem(ProductItemOrdered product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductItemOrdered Product { get; set; } // this is a navigation property
                                                        // so EF will mapped it to database
                                                        // but we don't need that
                                                        // we need take his properties and mapped it in OrderItem table
                                                        // so will make configration for that :)
        public decimal Price { get; set; }  
        public int Quantity { get; set; }  
    }
}
