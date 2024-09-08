

using System.ComponentModel.DataAnnotations.Schema;

namespace FinalBoatSystemRental.Core.Entities;

public class BoatBookingAddition
{

    public int Id { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.Now;

    //Relations 

    public BoatBooking BoatBooking { get; set; } = default!;
    public int BoatBookingId { get; set; }
    public Addition Addition { get; set; } = default!;
    public int AdditionId { get; set; }




}
