using System;
using System.Collections.Generic;

namespace WhichCard.Entities
{
    public class Reward
    {
        public List<Category> Categories { get; set; }

        public DateTime Expiration { get; set; }

        public string Type { get; set; }
    }
}
