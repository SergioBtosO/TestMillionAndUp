using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MillionAndUp.Core.Aplication.Interfaces;
using MillionAndUp.Core.Domain.Entities;
using System.Threading;
using MillionAndUp.Core.Domain.Common;
using System.Reflection;
using MillionAndUp.Infraestructure.Shared.Services;

namespace MillionAndUp.Infraestructure.Persistence.Contexts
{
    public class ApplicationDBContext : DbContext
    {
        private readonly IDateTimeService _dateTime;

        public ApplicationDBContext (DbContextOptions<ApplicationDBContext> options, IDateTimeService dateTimeService) : base (options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTimeService;
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<PropertyTrace> PropertyTraces { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach(var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.Created = _dateTime != null ? _dateTime.NowUtc : DateTime.Now ;
                if (entry.State == EntityState.Modified)
                    entry.Entity.LastModified = _dateTime != null ? _dateTime.NowUtc : DateTime.Now;
            }
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
