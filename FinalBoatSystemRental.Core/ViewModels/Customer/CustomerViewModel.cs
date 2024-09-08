namespace FinalBoatSystemRental.Core.ViewModels.Customer;

public class CustomerViewModel
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public decimal WalletBalance { get; set; }

    public DateTime CreatesAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
