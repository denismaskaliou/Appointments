using System.Text.Json.Serialization;

namespace Appointments.WebAPI.Models.DTOs;

public class AvailableSlotsQueryDto
{
    [JsonPropertyName("date")]
    public DateTime Date { get; init; }
    
    [JsonPropertyName("products")]
    public string[] Products { get; init; } = [];
    
    [JsonPropertyName("language")]
    public string Language { get; init; } = null!;
    
    [JsonPropertyName("rating")]
    public string Rating { get; init; } = null!;
}