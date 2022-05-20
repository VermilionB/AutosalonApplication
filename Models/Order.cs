using System;
using System.Collections.Generic;

namespace Autosalon.Models
{
    public partial class Order
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid AutomobileId { get; set; }
        public Guid ManagerId { get; set; }
        public DateTime? Date { get; set; }
        public int? TotalPrice { get; set; }
        public string Status { get; set; } = null!;

        public virtual Automobile Automobile { get; set; } = null!;
        public virtual Customer Customer { get; set; } = null!;
        public virtual Manager Manager { get; set; } = null!;
    }
}
