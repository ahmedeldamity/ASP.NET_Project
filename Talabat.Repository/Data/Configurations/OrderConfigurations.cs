using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregiate;

namespace Talabat.Repository.Data.Configurations
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(orderItem => orderItem.ShipingAddress, address => address.WithOwner());

            builder.Property(O => O.Status)
                .HasConversion(
                    OrderStatusFromUserToDB => OrderStatusFromUserToDB.ToString(),
                    OrderStatusFromDBToUser => (OrderStatus) Enum.Parse(typeof(OrderStatus), OrderStatusFromDBToUser)
                    );
            // here we store in OrderStatus in database as string 
            // and when we bring it from batabase will bring it as Enum of type OrderStatus
            // so if (property which recieve OrderState from batabase) was int => then this property will be number (0 | 1 | 2 ..)
            // and if was string => then this property will be (pending | Payment Succeded | Payment Failed)

            builder.Property(O => O.Subtotal)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(O => O.DeliveryMethodId).IsRequired(false);
        }
    }
}
