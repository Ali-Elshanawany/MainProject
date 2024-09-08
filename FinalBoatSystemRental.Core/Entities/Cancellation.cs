

using System.ComponentModel.DataAnnotations.Schema;

namespace FinalBoatSystemRental.Core.Entities;

public class Cancellation
{

    public int Id { get; set; }

    public DateTime CancellationDate { get; set; } = DateTime.Now;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal RefundAmount { get; set; }
    // this column to indicate if the customer had his money or not
    public bool RefundedYet { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;


    //Relation

    public Customer Customer { get; set; } = default!;
    public int CustomerId { get; set; }

    public int? BoatBookingId { get; set; }
    public int? ReservationId { get; set; }
    public int BoatId { get; set; }


}
