using System;
namespace WhichCard.Entities
{
    public class SignUpReward : Reward
    {
        public decimal AmountToSpend { get; set; }

        public decimal RewardAmount { get; set; }
    }
}
