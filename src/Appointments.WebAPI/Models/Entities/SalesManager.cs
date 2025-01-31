namespace Appointments.WebAPI.Models.Entities;

public class SalesManager
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string[] Languages { get; init; } = [];
    public string[] Products { get; init; } = [];
    public string[] CustomerRatings { get; init; } = [];
    
    public List<Slot> Slots { get; init; } = [];
}