using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Core.ViewModels.Owner;

public class OwnerViewModel
{
    public string? BusinessName { get; set; }
    public string? Address { get; set; }

    public decimal WalletBalance { get; set; }
}
