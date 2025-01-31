namespace Appointments.WebAPI.Models.Entities;

public class Slot
{
    public int Id { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public bool Booked { get; init; }
    public int SalesManagerId { get; init; }
    
    public SalesManager? SalesManager { get; init; }
}