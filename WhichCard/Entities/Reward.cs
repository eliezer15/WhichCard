using System;
using System.Collections.Generic;

namespace WhichCard.Entities
{
    public abstract class Reward
    {
        public string Id { get; set; }

        public IEnumerable<Category> Category { get; set; }

        public DateTime Expiration { get; set; }
    }
}
