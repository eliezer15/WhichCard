using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using WhichCard.Entities;
using WhichCard.Services;
using Xunit;

namespace WhichCard.UnitTests
{
    public class CreditCardRecommendationServiceTests
    {
        private List<CreditCard> creditCards = new List<CreditCard>
        {
            new CreditCard
            {
                Name = "Discover",
                Rewards = new List<Reward>()
                {
                    new CashbackReward
                    {
                        Expiration = DateTime.Now.AddDays(1),
                        Categories = new List<Category>()
                        {
                            new Category { Name = "Gas"},
                            new Category { Name = "Restaurants"}
                        },
                        CashbackPercentage = 5
                    }
                }
            },

            new CreditCard
            {
                Name = "Amex",
                Rewards = new List<Reward>()
                {
                    new MilesReward
                    {
                        Expiration = DateTime.Now.AddDays(1),
                        Categories = new List<Category>()
                        {
                            new Category { Name = "Gas"},
                            new Category { Name = "Amazon"}
                        },
                        MilesPerDollarSpent = 2,
                        DollarPricePerMile = 0.01m,
                    }
                }
            },

            new CreditCard
            {
                Name = "Venture",
                Rewards = new List<Reward>()
                {
                    new SignUpReward
                    {
                        Expiration = DateTime.Now.AddDays(1),
                        Categories = new List<Category>()
                        {
                            new Category { Name = "Restaurants"},
                            new Category { Name = "Amazon"}
                        },
                        Cashback = new CashbackReward
                        {
                            CashbackPercentage = 10
                        },
                        AmountToSpend = 2000
                    }
                }
            }
        };

        [Fact]
        public void GetRecommendations_ReturnsExpectedValues()
        {
			//Arrange
			var categories = new List<Category>()
			{
				new Category { Name = "Gas"},
				new Category { Name = "Amazon"},
				new Category { Name = "Restaurants"}
			};

            var user = new User
            {
                CreditCards = creditCards,
                ShoppingCategories = categories
            };

            var subject = new CreditCardRecommendationService();

            //Act

            var recommendations = subject.GetRecommendations(user);

            //Assert
            recommendations.Count().Should().Be(3);

            //Gas Recommendation
            var gasRecommendation = recommendations.First(r => r.Category.Name == "Gas");
            gasRecommendation.CreditCards.Count.Should().Be(2);

            var firstCard = gasRecommendation.CreditCards.First();
            firstCard.Name.Should().Be("Discover");
            firstCard.Rewards.Count().Should().Be(1);

            var reward = firstCard.Rewards.First();
            reward.Should().BeOfType<CashbackReward>();
            reward.EffectivePercentageReward.Should().Be(5);

            var secondCard = gasRecommendation.CreditCards.Last();
            secondCard.Name.Should().Be("Amex");
            secondCard.Rewards.Count().Should().Be(1);

            reward = secondCard.Rewards.First();
            reward.Should().BeOfType<MilesReward>();
            reward.EffectivePercentageReward.Should().Be(2);

			//Restaurant Recommendation
			var restaurantRecommendation = recommendations.First(r => r.Category.Name == "Restaurants");
			restaurantRecommendation.CreditCards.Count.Should().Be(2);

			firstCard = restaurantRecommendation.CreditCards.First();
			firstCard.Name.Should().Be("Venture");
			firstCard.Rewards.Count().Should().Be(1);

			reward = firstCard.Rewards.First();
            reward.Should().BeOfType<SignUpReward>();
			reward.EffectivePercentageReward.Should().Be(10);

			secondCard = restaurantRecommendation.CreditCards.Last();
			secondCard.Name.Should().Be("Discover");
			secondCard.Rewards.Count().Should().Be(1);

			reward = secondCard.Rewards.First();
            reward.Should().BeOfType<CashbackReward>();
			reward.EffectivePercentageReward.Should().Be(5);

			//Amazon Recommendation
			var amazonRecommendation = recommendations.First(r => r.Category.Name == "Amazon");
			amazonRecommendation.CreditCards.Count.Should().Be(2);

			firstCard = amazonRecommendation.CreditCards.First();
			firstCard.Name.Should().Be("Venture");
			firstCard.Rewards.Count().Should().Be(1);

			reward = firstCard.Rewards.First();
			reward.Should().BeOfType<SignUpReward>();
			reward.EffectivePercentageReward.Should().Be(10);

			secondCard = amazonRecommendation.CreditCards.Last();
			secondCard.Name.Should().Be("Amex");
			secondCard.Rewards.Count().Should().Be(1);

			reward = secondCard.Rewards.First();
            reward.Should().BeOfType<MilesReward>();
			reward.EffectivePercentageReward.Should().Be(2);
        }
    }
}
