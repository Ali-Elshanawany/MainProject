namespace FinalBoatSystemRental.Core.ViewModels.Boat;

public class UpdateBoatStatusViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public decimal ReservationPrice { get; set; }
    public string Status { get; set; } = string.Empty;
    public int MaxCancellationDateInDays { get; set; }
    public DateTime CreatesAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int OwnerId { get; set; }
}
