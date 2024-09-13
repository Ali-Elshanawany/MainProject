namespace FinalBoatSystemRental.Core.ViewModels.Owner;

public class PendingOwnerViewModel
{

    public int Id { get; set; }
    public string? BusinessName { get; set; }
    public string? FirstName { get; set; }
    public string? Address { get; set; }
    public decimal WalletBalance { get; set; }
    public DateTime CreatesAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool IsVerified { get; set; }
}
