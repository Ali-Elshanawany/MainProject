using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;


namespace FinalBoatSystemRental.Core.Entities;

public class Owner
{

    public int Id { get; set; }

    public string? BusinessName { get; set; }
    public string? Address { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal WalletBalance { get; set; }
    public DateTime CreatesAt { get; set; }= DateTime.Now;
    public DateTime UpdatedAt { get; set; }= DateTime.Now;

    public bool IsVerified { get; set; }

    //FK to AspnetUsers
    public virtual ApplicationUser User { get; set; } = default!;
    public string UserId { get; set; }
    public ICollection<Boat> Boats { get; set; } = new List<Boat>();
    public ICollection<Addition> Additions { get; set; } = new List<Addition>();
    public ICollection<Trip> Trips { get; set; } = new List<Trip>();


}
