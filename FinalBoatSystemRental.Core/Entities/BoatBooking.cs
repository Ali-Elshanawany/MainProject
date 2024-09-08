
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalBoatSystemRental.Core.Entities
{
    public class BoatBooking
    {

        public int Id { get; set; }

        public DateTime BookingDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }=string.Empty;
        public DateTime? CanceledAt { get; set; }
        public DateTime CreatesAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        //Relations 
        public Customer Customer { get; set; } = default!;
        public int CustomerId { get; set; }
        public Boat Boat { get; set; } = default!;
        public int BoatId { get; set; }

        public ICollection<BoatBookingAddition> BoatBookingAdditions { get; set; } = default!;


    }
}
