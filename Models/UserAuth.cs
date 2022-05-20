using System;
using System.Collections.Generic;

namespace Autosalon.Models
{
    public partial class UserAuth
    {
        public UserAuth()
        {
            Customers = new HashSet<Customer>();
            Managers = new HashSet<Manager>();
        }

        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Manager> Managers { get; set; }
    }
}
