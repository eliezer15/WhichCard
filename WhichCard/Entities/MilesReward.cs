using System;
namespace WhichCard.Entities
{
    public class MilesReward : Reward
    {
        public decimal AmountToSpend { get; set; }

        public int MilesAwarded { get; set; }
    }
}
