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
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(a => a.Id)
                .HasConversion(c => c.Value,
                dbId => ProductId.Of(dbId));

            builder.Property(a => a.Name)
                .IsRequired();
        }
    }
}
