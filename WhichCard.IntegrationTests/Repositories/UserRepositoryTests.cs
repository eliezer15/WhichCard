using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WhichCard.Entities;
using WhichCard.Repositories;
using Xunit;

namespace WhichCard.IntegrationTests.Repositories
{
    public class RepositoryTests
    {
        [Fact]
        public async Task DoIt()
        {
            var cards = new List<CreditCard>()
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

			var categories = new List<Category>()
			{
				new Category { Name = "Gas"},
				new Category { Name = "Amazon"},
				new Category { Name = "Restaurants"}
			};

			var user = new User
			{
                Id = "123",
                Email = "eliezerencarnacion@gmail.com",
                FirstName = "Eli",
                LastName = "Encarnacion",
				CreditCards = cards,
				ShoppingCategories = categories
			};

            await new UserRepository().InsertAsync(user);
        }
        
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
                CreditCards = new List<CreditCard> 
                { 
                    new CreditCard { Name = "Venture"},
                    new CreditCard { Name = "Fargo"} 
                }
            };

            var repo = new UserRepository();

            //Create
            await repo.InsertAsync(user);

            //Get
            var fetchedUser = await repo.GetAsync(user.Id);
            fetchedUser.ShouldBeEquivalentTo(user, opts => opts.Excluding(u => u.CreationDate));

            //Delete
            await repo.DeleteAsync(user);

            var getDeleted = await repo.GetAsync(user.Id);
            getDeleted.Should().BeNull();
        }
    }
}
