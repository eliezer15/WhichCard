using System;
using System.Collections.Generic;
using System.Linq;
using WhichCard.Entities;

namespace WhichCard.Services
{
    public class CreditCardRecommendationService : ICreditCardRecommendationService
    {
        private const int RewardsPerCategory = 2;

        public IEnumerable<CreditCardRecommendation> GetRecommendations(User user)
        {
            return user.ShoppingCategories.Select(category => GetRecommendation(user.CreditCards, category));
        }

        private CreditCardRecommendation GetRecommendation(List<CreditCard> cards, Category category)
        {
            var rewardsToCards = GetRewardsToCreditCards(cards);
            var rewards = rewardsToCards.Keys.ToList();

            var topRewards = GetTopRewards(rewards, category);

            var topCreditCards = new List<CreditCard>();
            foreach (var reward in topRewards)
            {
                var card = rewardsToCards[reward];
                card.Rewards = new List<Reward> { reward };
                topCreditCards.Add(card);
            }

            return new CreditCardRecommendation
            {
                Category = category,
                CreditCards = topCreditCards
            };
        }

		private Dictionary<Reward, CreditCard> GetRewardsToCreditCards(List<CreditCard> cards)
		{
			var retVal = new Dictionary<Reward, CreditCard>();

			foreach (var card in cards)
			{
				foreach (var reward in card.Rewards)
				{
					retVal.Add(reward, card);
				}
			}

			return retVal;
		}

        private IEnumerable<Reward> GetTopRewards(List<Reward> rewards, Category category)
        {
            var applicableRewards = GetApplicableRewards(rewards, category);

            applicableRewards.ForEach(r => r.EffectivePercentageReward = GetEffectivePercentageReward(r));

            var orderedRewards = applicableRewards.OrderByDescending(r => r.EffectivePercentageReward);

            return orderedRewards.Take(RewardsPerCategory);
        }

        private List<Reward> GetApplicableRewards(List<Reward> rewards, Category category)
        {
            return rewards.Where(r => r.Categories.Any(c => c.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase)))
                .ToList();
        }

        private decimal GetEffectivePercentageReward(Reward reward)
        {
            if (reward is CashbackReward cashBack)
            {
                return cashBack.CashbackPercentage;
            }

            if (reward is MilesReward miles)
            {
                return (miles.DollarPricePerMile * miles.MilesPerDollarSpent) * 100;
            }

            if (reward is SignUpReward signUp)
            {
                return GetEffectivePercentageReward(signUp.Miles)
                    + GetEffectivePercentageReward(signUp.Cashback);
            }

            return 0;
        }
    }
}
