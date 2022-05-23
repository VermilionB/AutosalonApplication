using System;
using System.Collections.Generic;

namespace Autosalon.Models
{
    public partial class Request
    {
        public Request(Guid id, Guid automobileId, Guid userId)
        {
            Id = id;
            AutomobileId = automobileId;
            UserId = userId;
        }
        public Guid Id { get; set; }
        public Guid AutomobileId { get; set; }
        public Guid UserId { get; set; }

        public virtual Automobile Automobile { get; set; } = null!;
        public virtual Customer User { get; set; } = null!;
    }
}
