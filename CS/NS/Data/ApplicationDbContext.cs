using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using NS.Models;
using System.Linq;

namespace NS.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<NS.Models.Part> Part { get; set; }
        public DbSet<NS.Models.Part> Sales { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Sales>()
               //  .ToTable("Entity")  // Is this really neccessary to singularize the entity name?
                 .HasIndex(entity => new
                 {
                     entity.SalesNum,
                     entity.PartNum
                 }, "Unique_SalesNum_PartNum").IsUnique(true);
        }
        //   protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasAnnotation("Scaffolding:ConnectionString", "Data Source=(local);Initial Catalog=NSDatabase;Integrated Security=true");

        //    modelBuilder.Entity<Part>(entity =>
        //    {
        //        entity.Property(e => e.Category).IsRequired();

        //        entity.Property(e => e.Code).IsRequired();

        //        entity.HasIndex(e => e.Code).IsUnique();

        //        entity.Property(e => e.Name).IsRequired();

        //        entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

        //        entity.Property(e => e.Uom)
        //            .IsRequired()
        //            .HasColumnName("UOM");
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}


        //https://stackoverflow.com/questions/62550667/validation-30000-no-type-specified-for-the-decimal-column

        //http://jameschambers.com/2019/06/No-Type-Was-Specified-for-the-Decimal-Column/
        //protected override void OnModelCreating(ModelBuilder builder)
        //{

        //    foreach (var property in builder.Model.GetEntityTypes()
        //        .SelectMany(t => t.GetProperties())
        //        .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        //    {

        //        //property.Relational().ColumnType = "decimal(18,2)";
        //        property.HasColumnType("decimal(18,2)");


        //    }
        //}
    }
}
