using System;
namespace WhichCard.Entities
{
    public class SignUpReward : Reward
    {
        public decimal AmountToSpend { get; set; }

        public CashbackReward Cashback { get; set; }

        public MilesReward Miles { get; set; }
    }
}
