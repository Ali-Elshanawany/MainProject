

using System.ComponentModel.DataAnnotations.Schema;

namespace FinalBoatSystemRental.Core.Entities;

public class Trip
{
    public int Id { get; set; }
    public string Name { get; set; }=string.Empty;
    public string Description { get; set; }= string.Empty;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal PricePerPerson { get; set; }=decimal.Zero;

    public int MaxPeople { get; set; } 

    public DateTime CancellationDeadLine { get; set;}

    public string Status { get; set; } = string.Empty;

    public DateTime StartedAt { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //Relations
    public Owner Owner { get; set; } = default!;
    public int OwnerId { get; set; }
    public Boat Boat { get; set; } = default!;
    public int BoatId { get; set; }

    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();


}
