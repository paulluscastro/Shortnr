using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Shortnr.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shortnr.Api.Data
{
    public class ApiDataContext : DbContext
    {
        public DbSet<Parameter> Parameters { get; set; }
        public DbSet<Url> Urls { get; set; }
        public ApiDataContext(DbContextOptions<ApiDataContext> options) : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ForSqlServerUseIdentityColumns();
            builder.Entity<Parameter>(b =>
            {
                b.HasKey(p => p.Id);
                b.HasIndex(p => p.Key).IsUnique().HasName("IDX_Parameter_Key");
                b.Property(p => p.Id)
                    .HasValueGenerator<GuidValueGenerator>()
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("(newid())")
                    .IsRequired();
                b.Property(p => p.Created).ValueGeneratedOnAdd();
                b.Property(p => p.LastUpdated).ValueGeneratedOnAddOrUpdate();
            });
            builder.Entity<Url>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id)
                    .HasValueGenerator<GuidValueGenerator>()
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("(newid())")
                    .IsRequired();
                b.Property(u => u.OriginalUrl).IsRequired();
                b.Property(u => u.Shortened).IsRequired();
                b.HasIndex(u => u.Shortened).IsUnique().HasName("IDX_Url_Shortened");
                b.Property(u => u.Created).ValueGeneratedOnAdd();
                b.Property(u => u.LastUpdated).ValueGeneratedOnAddOrUpdate();
            });
        }
    }
}
