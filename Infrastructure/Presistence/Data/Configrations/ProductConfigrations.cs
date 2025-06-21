using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configrations
{
    public class ProductConfigrations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            

            builder.HasOne(T => T.ProductType)
                   .WithMany()
                   .HasForeignKey(FK => FK.TypeId);

            builder.HasOne(B => B.ProductBrand)
                   .WithMany()
                   .HasForeignKey(FK => FK.BrandId);

            builder.Property(P => P.Price)
                   .HasColumnType("decimal(18,2)");
        }
    }
}
