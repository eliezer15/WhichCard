using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime;
using FluentAssertions;
using WhichCard.Entities;
using WhichCard.Repositories;
using Xunit;

namespace WhichCard.IntegrationTests.Repositories
{
    public class RepositoryTests
    {
        [Fact]
        public async Task Users_CRUD()
        {
            //Arrange
            var user = new User
            {
                Id = "userId",
                Email = "myemail@email.com",
                FirstName = "First",
                LastName = "Last",
                CreditCards = new List<string> { "Venture", "Fargo" }
            };

            var repo = new UserRepository();

            //Create
            await repo.InsertAsync(user);

            //Get
            var fetchedUser = await repo.GetAsync(user.Id);
            fetchedUser.ShouldBeEquivalentTo(user);

            //Delete
            /*
            await repo.DeleteAsync(user);

            var getDeleted = await repo.GetAsync(user.Id);
            getDeleted.Should().BeNull();*/
        }

        [Fact]
        public async Task CreditCards_CRUD()
        {
            //Arrange
            var venture = new CreditCard
            {
                UserId = "userId",
                Name = "Venture",
                Color = new Color { RgbValue = "fff" },
                Rewards = new List<Reward>
                {
                    new CashbackReward
                    {
                        Categories = new List<Category>
                        {
                            new Category { Name = "Restaurants" }
                        },
                        Expiration = System.DateTime.Now,
                        CashbackPercentage = 5
                    }
                }
            };

            var amex = new CreditCard
            {
                UserId = "userId",
                Name = "Amex Blue",
                Color = new Color { RgbValue = "#000000" },
                Rewards = new List<Reward>
                {
                    new SignUpReward
                    {
                        Categories = new List<Category>
                        {
                            new Category { Name = "All" }
                        },
                        Expiration = System.DateTime.Now,
                        AmountToSpend = 2000,
                        RewardAmount = 500
					}
				}
			};

            var repo = new CreditCardRepository();

			//Insert
			await repo.InsertAsync(venture);
			await repo.InsertAsync(amex);

            //Get By User Id
            var userCreditCards = await repo.GetByUserAsync("userId");
            userCreditCards.Count.Should().Be(2);

            var fetchedVentureCard = userCreditCards.First(cc => cc.Name == "Venture");
            VerifyCardsAreEquivalent(fetchedVentureCard, venture);

            var fetchedAmexCard = userCreditCards.First(cc => cc.Name == "Amex Blue");
            VerifyCardsAreEquivalent(fetchedAmexCard, amex);

			//Delete
			await repo.Deleteasync(venture);

			userCreditCards = await repo.GetByUserAsync("userId");
            userCreditCards.Count.Should().Be(1);

			await repo.Deleteasync(amex);

			userCreditCards = await repo.GetByUserAsync("userId");
            userCreditCards.Should().BeEmpty();
		}

        private void VerifyCardsAreEquivalent(CreditCard expected, CreditCard actual)
        {
			expected.ShouldBeEquivalentTo(actual, opts => opts.Excluding(c => c.Rewards));

			expected.Rewards.Count.Should().Be(actual.Rewards.Count);

			expected.Rewards.First().ShouldBeEquivalentTo(actual.Rewards.First(),
				opts => opts.Excluding(r => r.Expiration));

            expected.Rewards.First().Expiration.Should().BeCloseTo(actual.Rewards.First().Expiration, 200);
        }
    }
}
