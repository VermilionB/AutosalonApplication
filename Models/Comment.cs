using System;
using System.Collections.Generic;

namespace Autosalon.Models
{
    public partial class Comment
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string? Text { get; set; }
        public DateTime Date { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
