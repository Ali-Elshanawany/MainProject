
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalBoatSystemRental.Core.Entities;

public class ReservationAddition
{

    public int Id { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    //Relations
    public Reservation Reservation { get; set; } = default!;
    public int ReservationId { get; set; }
    public Addition Addition { get; set; } = default!;
    public int AdditionId { get; set; }

}
