using Appointments.WebAPI.Models.DTOs;

namespace Appointments.WebAPI.Application.Services;

public interface IAppointmentSlotsService
{
    Task<AvailableSlotDto[]> GetAvailableSlotsAsync(AvailableSlotsQueryDto query);
}