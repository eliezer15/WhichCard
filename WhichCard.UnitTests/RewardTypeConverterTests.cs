using System;
using System.Collections.Generic;
using FluentAssertions;
using Newtonsoft.Json;
using WhichCard.Entities;
using Xunit;

namespace WhichCard.UnitTests
{
    public class RewardTypeConverterTests
    {
        List<Reward> testRewards = new List<Reward>()
        {
            new CashbackReward()
            {
                Expiration = DateTime.Now,
                Categories = new List<Category>()
                {
                    new Category { Name = "Gas" }
                },
                CashbackPercentage = 5
            },

            new MilesReward()
            {
                Expiration = DateTime.Now,
                Categories = new List<Category>()
                {
                    new Category { Name = "Gas" }
                },
                MilesPerDollarSpent = 5,
                DollarPricePerMile = 0.01m,
            },

            new SignUpReward()
            {
                Expiration = DateTime.Now,
                Categories = new List<Category>()
                {
                    new Category { Name = "Gas" }
                },
                Miles = new MilesReward
                {
                    MilesPerDollarSpent = 5,
                    DollarPricePerMile = 0.01m
                }
            }
        };

        [Fact]
        public void FromEntry_Success()
        {
			//Arrange
			var subject = new RewardTypeConverter();

            var serializedRewards = subject.ToEntry(testRewards);

            //Act
            var result = subject.FromEntry(serializedRewards);

            //Assert
            var actualRewards = result as List<Reward>;
            actualRewards.Should().NotBeNull();
            actualRewards.Count.Should().Be(3);

            for (var i = 0; i < 3; i++)
            {
                var actual = actualRewards[i];
                var expected = testRewards[i];

                actual.ShouldBeEquivalentTo(expected, opts => opts.Excluding(r => r.Expiration));
            }
        }
    }
}
