using System;
using System.Collections.Generic;

namespace WhichCard.Entities
{
    public abstract class Reward
    {
        public List<Category> Categories { get; set; }

        public DateTime Expiration { get; set; }

        public decimal EffectivePercentageReward { get; set; }

        public string Type { get; set; }
    }
}
