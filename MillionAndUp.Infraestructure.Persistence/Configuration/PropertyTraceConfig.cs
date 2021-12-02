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
    public class PropertyTraceConfig : IEntityTypeConfiguration<PropertyTrace>
    {
        public void Configure(EntityTypeBuilder<PropertyTrace> builder)
        {
            builder.ToTable("PropertyTraces");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(p => p.Tax)
                .IsRequired();

            builder.Property(p => p.Value)
                  .IsRequired();

            builder.Property(p => p.DateSale)
                .IsRequired();

            builder.HasOne(x => x.Property)
           .WithMany(x => x.Traces)
           .HasForeignKey(x => x.IdProperty);

            builder.Property(p => p.Created);

            builder.Property(p => p.CreatedBy)
             .HasMaxLength(30);

            builder.Property(p => p.LastModified);

            builder.Property(p => p.LastModifiedBy)
              .HasMaxLength(30);
        }
    }
}
