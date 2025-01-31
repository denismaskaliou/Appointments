using Appointments.WebAPI.Models.DTOs;

namespace Appointments.WebAPI.Application.Services;

public interface IAppointmentsSlotsService
{
    Task<AvailableSlotDto[]> GetAvailableSlotsAsync(AvailableSlotsQueryDto query);
}