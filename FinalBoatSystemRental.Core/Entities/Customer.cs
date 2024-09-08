

using System.ComponentModel.DataAnnotations.Schema;

namespace FinalBoatSystemRental.Core.Entities;

public class Customer
{

    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal WalletBalance { get; set; }

    public DateTime CreatesAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //Relations
    public virtual ApplicationUser User { get; set; } = default!;
    public string UserId { get; set; } = string.Empty;

    ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    ICollection<Cancellation> Cancellations { get; set; } = new List<Cancellation>();
}
