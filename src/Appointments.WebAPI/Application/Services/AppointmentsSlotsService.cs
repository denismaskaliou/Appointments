using System.Linq.Expressions;
using Appointments.WebAPI.Database;
using Appointments.WebAPI.Models.DTOs;
using Appointments.WebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Appointments.WebAPI.Application.Services;

public class AppointmentsSlotsService(ApplicationDbContext db) : IAppointmentsSlotsService
{
    public async Task<AvailableSlotDto[]> GetAvailableSlotsAsync(AvailableSlotsQueryDto query)
    {
        var (startDate, endDate) = ToUtcStartAndEndDate(query.Date);

        var sqlQuery = db.Slots
            .Where(slot =>
                slot.Booked == false &&
                slot.StartDate >= startDate && slot.EndDate < endDate &&
                slot.SalesManager!.Languages.Contains(query.Language) &&
                slot.SalesManager.CustomerRatings.Contains(query.Rating) &&
                query.Products.All(product => slot.SalesManager.Products.Contains(product))
            )
            .Where(DoNotHaveBookedOverlappingSlots())
            .GroupBy(slot => slot.StartDate)
            .Select(group => new AvailableSlotDto
            {
                StartDate = group.Key,
                AvailableCount = group.Count()
            })
            .OrderBy(s => s.StartDate);

        var queryText = sqlQuery.ToQueryString();
        var availableSlots = await sqlQuery.ToArrayAsync();
        return availableSlots;
    }

    private Expression<Func<Slot, bool>> DoNotHaveBookedOverlappingSlots() =>
        slot => !db.Slots.Any(bookedSlot =>
            bookedSlot.SalesManagerId == slot.SalesManagerId &&
            bookedSlot.Booked == true &&
            slot.StartDate < bookedSlot.EndDate && slot.EndDate > bookedSlot.StartDate
        );

    private (DateTime, DateTime) ToUtcStartAndEndDate(DateTime startDate)
    {
        var startDateTime = startDate.Date.ToUniversalTime();
        var endDateTime = startDateTime.AddDays(1);

        return (startDateTime, endDateTime);
    }
}