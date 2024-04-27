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
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(a => a.Id)
                .HasConversion(c => c.Value,
                dbId => CusomrerId.Of(dbId));
            builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
            builder.Property(a => a.Email).HasMaxLength(255).IsRequired();
            builder.HasIndex(c => c.Email).IsUnique();

        }
    }
}
