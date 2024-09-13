
namespace FinalBoatSystemRental.Core.ViewModels.Trip;

public class TripViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;


    [Required(ErrorMessage = "Description is required")]
    public string Description { get; set; } = string.Empty;


    [Required(ErrorMessage = "Price is required")]
    public decimal PricePerPerson { get; set; } = decimal.Zero;

    [Required(ErrorMessage = "Max Capacity  is required")]

    public int MaxPeople { get; set; }

    [Required(ErrorMessage = "Cancellation DeadLine is required")]
    public DateTime CancellationDeadLine { get; set; }

    [Required(ErrorMessage = "Status is required")]
    public string Status { get; set; } = string.Empty;

    [Required(ErrorMessage = "Starting Time DeadLine is required")]
    public DateTime StartedAt { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Boat is required")]
    public int BoatId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}

