namespace Appointments.WebAPI.Models.DTOs;

public class ErrorDto
{
    public string Message { get; init; } = null!;
    public string? Details { get; init; }
}