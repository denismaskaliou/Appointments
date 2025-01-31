using Appointments.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Appointments.WebAPI.Database.Configurations;

public class SlotConfiguration : IEntityTypeConfiguration<Slot>
{
    public void Configure(EntityTypeBuilder<Slot> builder)
    {
        builder.ToTable("slots");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.StartDate).HasColumnName("start_date");
        builder.Property(e => e.EndDate).HasColumnName("end_date");
        builder.Property(e => e.Booked).HasColumnName("booked");
        builder.Property(e => e.SalesManagerId).HasColumnName("sales_manager_id");

        builder
            .HasOne(e => e.SalesManager)
            .WithMany(m => m.Slots)
            .HasForeignKey(e => e.SalesManagerId);
    }
}