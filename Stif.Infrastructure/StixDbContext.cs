using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stix.Core.Entities;
using System.Text.Json;

namespace Stix.Infrastructure
{
    public class StixDbContext : DbContext
    {
        public StixDbContext(DbContextOptions<StixDbContext> option) : base(option)
        {
        }
        DbSet<Vulnerability> Vulnerabilities { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<GranularMarking>(ConfigureGranularMarkings);
            //modelBuilder.Entity<ExternalReference>(ConfigureExternalReference);
            modelBuilder.Entity<Vulnerability>(ConfigureVulnerability);
        }

        //private void ConfigureGranularMarkings(EntityTypeBuilder<GranularMarking> builder)
        //{
        //    builder.HasKey(p => p.Id);
        //}

        //  private void ConfigureExternalReference(EntityTypeBuilder<ExternalReference> builder)
        //  {
        //      builder.HasKey(p => p.Id);

        //      builder.Property(p => p.Hashes).HasConversion(
        //v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
        //v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions)null),
        //new ValueComparer<Dictionary<string, string>>(
        //    (c1, c2) => c1.SequenceEqual(c2),
        //    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
        //    c => c.ToDictionary(k => k.Key, v => v.Value)));
        //  }

        private void ConfigureVulnerability(EntityTypeBuilder<Vulnerability> builder)
        {
            builder.HasKey(p => p.Id);

            //    builder.Property(p => p.Labels).HasConversion(
            //v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            //v => JsonSerializer.Deserialize<List<string>?>(v, (JsonSerializerOptions)null),
            //new ValueComparer<List<string>>(
            //    (c1, c2) => c1.SequenceEqual(c2),
            //    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            //    c => c.ToList()));

            //    builder.Property(p => p.ObjectMarkingRefs).HasConversion(
            //v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
            //v => JsonSerializer.Deserialize<List<string>?>(v, (JsonSerializerOptions)null),
            //new ValueComparer<List<string>>(
            //    (c1, c2) => c1.SequenceEqual(c2),
            //    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            //    c => c.ToList()));
        }
    }
}
