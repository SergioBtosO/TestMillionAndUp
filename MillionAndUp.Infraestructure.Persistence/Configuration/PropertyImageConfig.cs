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
    public class PropertyImageConfig : IEntityTypeConfiguration<PropertyImage>
    {
        public void Configure(EntityTypeBuilder<PropertyImage> builder)
        {
            builder.ToTable("PropertyImages");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Enabled);

            builder.Property(p => p.File)
                .IsRequired();

            builder.HasOne(x => x.Property)
           .WithMany(x => x.Images)
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
