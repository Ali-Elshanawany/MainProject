using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Core.ViewModels.Trip;

public class AvailableTripsViewModel
{

    public string Name { get; set; } = string.Empty;  
    public string Description { get; set; } = string.Empty;
    public decimal PricePerPerson { get; set; } = decimal.Zero; 
    public int MaxPeople { get; set; }  
    public DateTime CancellationDeadLine { get; set; }   
    public string Status { get; set; } = string.Empty;       
    public DateTime StartedAt { get; set; } = DateTime.Now; 
    public string? BoatName { get; set; }
    public int AvailableSeats { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


}
