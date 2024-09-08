namespace FinalBoatSystemRental.Core.ViewModels.Trip;

public class GetCancellation_PriceViewModel
{

    public decimal Price { get; set; }
    public DateTime CancellationDeadLine { get; set; }
    public DateTime ReservationDate { get; set; }
}
