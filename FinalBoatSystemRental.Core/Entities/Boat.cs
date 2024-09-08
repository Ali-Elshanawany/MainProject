



using System.ComponentModel.DataAnnotations.Schema;

namespace FinalBoatSystemRental.Core.Entities
{
    public class Boat
    {
      
        public int Id { get; set; }

        public string Name { get; set; }=string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Capacity { get; set; } 

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ReservationPrice { get; set; }

        public string Status { get; set; } = string.Empty;

        public int MaxCancellationDateInDays { get; set; }

        public DateTime CreatesAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        //Relations 
        public int OwnerId { get; set; }

        public Owner Owner { get; set; } = default!;

        public ICollection<Reservation> Reservations { get; set; }=new List<Reservation>();

        public ICollection<Cancellation> Cancellations { get; set; } = new List<Cancellation>();


    }
}
