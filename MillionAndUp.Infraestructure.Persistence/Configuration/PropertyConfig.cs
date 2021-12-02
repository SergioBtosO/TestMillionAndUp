using Microsoft.EntityFrameworkCore;
using MillionAndUp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Infraestructure.Persistence.Configuration
{
    public class PropertyConfig : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Properties");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(p => p.Address)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(p => p.CodeInternal)
                .HasMaxLength(30)
                .IsRequired(); 

            builder.Property(p => p.Price)
                .IsRequired();

            builder.Property(p => p.Year)
                .IsRequired();

            builder.Property(p => p.Created);

            builder.Property(p => p.CreatedBy)
             .HasMaxLength(30);

            builder.Property(p => p.LastModified);

            builder.Property(p => p.LastModifiedBy)
              .HasMaxLength(30);

            builder.HasOne(x => x.Owner)
            .WithMany(x => x.Properties)
            .HasForeignKey(x => x.IdOwner);
        }
    }
}
