using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.OrderModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configrations
{
    public class OrderConfigrations : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.OwnsOne(A => A.ShippingAddress, Addres => Addres.WithOwner());

            builder.HasMany(I=>I.OrderItems).
                    WithOne().
                    OnDelete(DeleteBehavior.Cascade);

            builder.Property(P => P.PaymentStatus).
                   HasConversion(S => S.ToString(), S => Enum.Parse<PaymentStatus>(S));

            builder.HasOne(D=>D.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);

            builder.Property(S => S.SubTotal).
                   HasColumnType("decimal(1)");
        }
    }
}
