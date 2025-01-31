using System.Text.Json.Serialization;

namespace Appointments.WebAPI.Models.DTOs;

public class AvailableSlotDto
{
    [JsonPropertyName("available_count")]
    public int AvailableCount { get; init; }
    
    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; init; }
}