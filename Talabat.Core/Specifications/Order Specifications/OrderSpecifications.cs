using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregiate;

namespace Talabat.Core.Specifications.Order_Specifications
{
    public class OrderSpecifications: BaseSpecifications<Order>
    {
        public OrderSpecifications(string buyerEmail)
        {
            WhereCriteria = O => O.BuyerEmail == buyerEmail;

            IncludeCriterias.Add(O => O.DeliveryMethod);

            IncludeCriterias.Add(O => O.Items);

            OrderByDesc = O => O.OrderData;
        }

        public OrderSpecifications(int orderId, string buyerEmail)
        {
            WhereCriteria = O => O.Id == orderId && O.BuyerEmail == buyerEmail;

            IncludeCriterias.Add(O => O.DeliveryMethod);

            IncludeCriterias.Add(O => O.Items);
        }
    }
}
