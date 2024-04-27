using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models;
using Ordering.Domain.StronglyTypedIds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastrucure.Data.EntityTypeConfigurations
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(a => a.Id)
           .HasConversion(c => c.Value,
            dbId => OrderItemId.Of(dbId));


            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            builder.Property(oi=>oi.Price).IsRequired();
            builder.Property(oi=>oi.Quantity).IsRequired();
        }
    }
}
