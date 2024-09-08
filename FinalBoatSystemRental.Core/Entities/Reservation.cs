

namespace FinalBoatSystemRental.Core.Entities;

public class Reservation
{

    public int Id { get; set; }
    public int NumOfPeople { get; set; }
    public int TotalPrice { get; set; }
    public DateTime ReservationDate { get; set; }= DateTime.Now;
    public string Status { get; set; }= string.Empty;
    public DateTime? CanceledAt { get; set; }
    public DateTime CreatesAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //Relations
    public Customer Customer { get; set; } = default!;
    public int CustomerId { get; set; }

    public Boat Boat { get; set; } = default!;
    public int BoatId { get; set; }

    public Trip Trip { get; set; } = default!;
    public int TripId { get; set; }

    public ICollection<Cancellation> Cancellations { get; set; } = new List<Cancellation>();
    public ICollection<ReservationAddition> ReservationAdditions { get; set; } = new List<ReservationAddition>();


}
