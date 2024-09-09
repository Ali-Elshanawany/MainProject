namespace FinalBoatSystemRental.Core.ViewModels.Reservation;

public class AdminReservationViewModel
{
    public int Id { get; set; }
    public int NumOfPeople { get; set; }
    public int TotalPrice { get; set; }
    public DateTime ReservationDate { get; set; } = DateTime.Now;
    public string Status { get; set; } = string.Empty;
    public DateTime? CanceledAt { get; set; }
    public DateTime CreatesAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public int CustomerId { get; set; }
    public int BoatId { get; set; }
    public int TripId { get; set; }

    public string CustomerFirstName { get; set; }
    public string BoatName { get; set; }
}
