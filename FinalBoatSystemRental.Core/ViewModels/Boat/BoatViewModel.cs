using System.ComponentModel.DataAnnotations;

namespace FinalBoatSystemRental.Application.Boat.ViewModels;

public class BoatViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [Length(3, 40)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required")]
    [MinLength(20)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Capacity is required")]
    public string Capacity { get; set; } = string.Empty;

    [Required(ErrorMessage = "ReservationPrice is required")]
    public decimal ReservationPrice { get; set; }
}
