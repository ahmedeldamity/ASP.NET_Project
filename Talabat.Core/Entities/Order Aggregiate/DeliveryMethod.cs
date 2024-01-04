using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregiate
{
    public class DeliveryMethod: BaseEntity
    {
        public DeliveryMethod()
        {
            // we create this constractor because EF need it while migration
        }
        public DeliveryMethod(string shortName, string description, decimal cost, string deliveryTime)
        {
            ShortName = shortName;
            Description = description;
            Cost = cost;
            DeliveryTime = deliveryTime;
        }

        public string ShortName { get; set; } // the name of delivery way
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string DeliveryTime { get; set; } // the time delivery will take to bring the order
    }
}
