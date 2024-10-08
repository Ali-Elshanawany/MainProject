﻿namespace FinalBoatSystemRental.Core.ViewModels.BoatBooking
{
    public class BoatBookingViewModel
    {
        public int Id { get; set; }

        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? CanceledAt { get; set; }
        public DateTime CreatesAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now; 
        public int CustomerId { get; set; }
        public int BoatId { get; set; }
    }
}
