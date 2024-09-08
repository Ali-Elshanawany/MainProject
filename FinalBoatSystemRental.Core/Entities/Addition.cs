

namespace FinalBoatSystemRental.Core.Entities
{
    public class Addition
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }=string.Empty;
        public int Price { get; set; }

        public DateTime CreatedAt { get; set; }= DateTime.Now;
        public DateTime UpdatedAt { get; set; }= DateTime.Now;

        //Relations
        public Owner Owner { get; set; } = default!;
        public int OwnerId { get; set; }

        public ICollection<BoatBookingAddition> BoatBookingAdditions { get; set; } = default!;




    }
}
