using System.Linq.Expressions;
using Appointments.WebAPI.Database;
using Appointments.WebAPI.Models.DTOs;
using Appointments.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointments.WebAPI.Application.Services;

public class AppointmentSlotsService(ApplicationDbContext db) : IAppointmentSlotsService
{
    public async Task<AvailableSlotDto[]> GetAvailableSlotsAsync(AvailableSlotsQueryDto query)
    {
        var (startDate, endDate) = MapToUtcStartEndDates(query.Date);

        var availableSlotsQuery = db.Slots
            .AsNoTracking()
            .Where(slot =>
                slot.Booked == false &&
                slot.StartDate >= startDate && slot.EndDate < endDate &&
                slot.EndDate - slot.StartDate == TimeSpan.FromHours(1) &&
                slot.SalesManager!.Languages.Contains(query.Language) &&
                slot.SalesManager.CustomerRatings.Contains(query.Rating) &&
                query.Products.All(product => slot.SalesManager.Products.Contains(product))
            )
            .Where(HasNoOverlappingBookings)
            .GroupBy(slot => slot.StartDate)
            .Select(group => new AvailableSlotDto
            {
                StartDate = group.Key,
                AvailableCount = group.Count()
            })
            .OrderBy(s => s.StartDate);

        var availableSlots = await availableSlotsQuery.ToArrayAsync();
        return availableSlots;
    }

    private Expression<Func<Slot, bool>> HasNoOverlappingBookings =>
        slot => !db.Slots.Any(bookedSlot =>
            bookedSlot.Booked == true &&
            slot.StartDate < bookedSlot.EndDate &&
            slot.EndDate > bookedSlot.StartDate &&
            bookedSlot.SalesManagerId == slot.SalesManagerId
        );

    private (DateTime, DateTime) MapToUtcStartEndDates(DateTime startDate)
    {
        var startDateTime = startDate.Date.ToUniversalTime();
        var endDateTime = startDateTime.AddDays(1);

        return (startDateTime, endDateTime);
    }
}