using System;
namespace WhichCard.Entities
{
    public class MilesReward : Reward
    {
        public decimal MilesPerDollarSpent { get; set; }

        public decimal DollarPricePerMile { get; set; }
    }
}
