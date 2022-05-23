using System;
using System.Collections.Generic;

namespace Autosalon.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
            Requests = new HashSet<Request>();
        }

        public Guid Id { get; set; }
        public Guid AuthId { get; set; }
        public string Surname { get; set; } = null!;
        public string? Name { get; set; }

        public virtual UserAuth Auth { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
