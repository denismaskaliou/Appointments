using Appointments.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointments.WebAPI.Database.Configurations;

public class SalesManagerConfiguration : IEntityTypeConfiguration<SalesManager>
{
    public void Configure(EntityTypeBuilder<SalesManager> builder)
    {
        builder.ToTable("sales_managers");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Name).HasColumnName("name");

        builder
            .Property(t => t.Languages)
            .HasColumnName("languages")
            .HasColumnType("varchar(100)[]");

        builder
            .Property(t => t.Products)
            .HasColumnName("products")
            .HasColumnType("varchar(100)[]");

        builder
            .Property(t => t.CustomerRatings)
            .HasColumnName("customer_ratings")
            .HasColumnType("varchar(100)[]");
    }
}