using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MillionAndUp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MillionAndUp.Infraestructure.Persistence.Configuration
{
    public class OwnerConfig : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("Owners");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(p => p.Photo);

            builder.Property(p => p.Address)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(p => p.Birthday)
                .IsRequired();

            builder.Property(p => p.Created);

            builder.Property(p => p.CreatedBy)
             .HasMaxLength(30);

            builder.Property(p => p.LastModified);

            builder.Property(p => p.LastModifiedBy)
              .HasMaxLength(30);

        }
    }
}
