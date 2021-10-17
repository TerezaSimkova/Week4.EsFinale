using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Week4.EsFinale.Core.Models;

namespace Week4.EsFinale.EF.Configurations
{
   public class OrderConfiguration  : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(b => b.Id);

            builder
                .Property(b => b.OrderCode)
                .HasMaxLength(20)
                .IsRequired();

            builder
                .Property(b => b.ProductCode)
                .HasMaxLength(15)
                .IsRequired();

            builder
                 .HasOne(o => o._customer).WithMany(c => c.orders).HasForeignKey(o => o.IdCustomer);
        }
    }
}
