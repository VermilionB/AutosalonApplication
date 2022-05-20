using System;
using System.Collections.Generic;

namespace Autosalon.Models
{
    public partial class Manager
    {
        public Manager()
        {
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }
        public Guid AuthId { get; set; }
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public int? AmountOfSaledCars { get; set; }

        public virtual UserAuth Auth { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
