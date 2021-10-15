using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Week4.EsFinale.Core.Models;

namespace Week4.EsFinale.EF.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(b => b.Id);

            builder
                .Property(b => b.CustomerCode)
                .HasMaxLength(15)
                .IsRequired();

            builder
                .Property(b => b.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder
               .Property(b => b.Surname)
               .HasMaxLength(50)
               .IsRequired();
        }
    }
}