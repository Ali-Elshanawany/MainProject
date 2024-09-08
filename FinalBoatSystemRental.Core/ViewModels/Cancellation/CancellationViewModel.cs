using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalBoatSystemRental.Core.ViewModels.Cancellation
{
    public class CancellationViewModel
    {
        public int Id { get; set; }

        public DateTime CancellationDate { get; set; } = DateTime.Now;
        public decimal RefundAmount { get; set; }
        // this column to indicate if the customer had his money or not
        public bool RefundedYet { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
