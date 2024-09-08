using System.ComponentModel.DataAnnotations;

namespace FinalBoatSystemRental.Core.ViewModels.Addition;

public class AdditionViewModel
{
    [Required(ErrorMessage = "Id is required")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is Required")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is Required")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Price is Required")]
    [Range(0,int.MaxValue,ErrorMessage ="Must Be a Positive Number") ]
    public int Price { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}
