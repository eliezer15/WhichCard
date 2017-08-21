using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

namespace WhichCard.Entities
{
    /// <summary>
    /// Converts Rewards to strings so they can be saved by Dynamo
    /// </summary>
    public class RewardTypeConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            var serializedRewards = entry.AsString();
            var dynamoRewards = 
                JsonConvert.DeserializeObject<List<DynamoDBReward>>(serializedRewards);

            return dynamoRewards.Select(d => ToReward(d)).ToList();
        }

        public DynamoDBEntry ToEntry(object value)
        {
            var rewards = value as List<Reward>;
            var dynamoRewards = rewards.Select(r => ToDynamo(r)).ToList();

            return new Primitive
            {
                Value = JsonConvert.SerializeObject(dynamoRewards)
            };
        }

        private DynamoDBReward ToDynamo(Reward reward)
        {
            if (reward == null) return null;

            var dynamoReward = new DynamoDBReward
            {
                Categories = reward.Categories,
                Expiration = reward.Expiration,
                Type = reward.GetType().ToString()
            };

            if (reward is CashbackReward cashback)
            {
                dynamoReward.CashbackPercentage = cashback.CashbackPercentage;
            }
            else if (reward is MilesReward miles)
            {
                dynamoReward.MilesPerDollarSpent = miles.MilesPerDollarSpent;
                dynamoReward.DollarPricePerMile = miles.DollarPricePerMile;
            }
            else if (reward is SignUpReward signup)
            {
                dynamoReward.Miles = ToDynamo(signup.Miles);
                dynamoReward.Cashback = ToDynamo(signup.Cashback);
            }

            return dynamoReward;
        }

        private Reward ToReward(DynamoDBReward dynamoReward)
        {
            if (dynamoReward == null) return null;

            if (dynamoReward.Type.Contains(nameof(CashbackReward)))
            {
                return ToCashbackReward(dynamoReward);
            }

            if (dynamoReward.Type.Contains(nameof(MilesReward)))
			{
                return ToMilesReward(dynamoReward);
			}

            if (dynamoReward.Type.Contains(nameof(SignUpReward)))
			{
                return ToSignupReward(dynamoReward);
			}

            throw new ArgumentException($"Invalid dynamoReward.Type: {dynamoReward.Type}");
        }

        private CashbackReward ToCashbackReward(DynamoDBReward dynamoReward)
        {
            if (dynamoReward == null) return null;
            
            return new CashbackReward
            {
                Categories = dynamoReward.Categories,
                Expiration = dynamoReward.Expiration,
                CashbackPercentage = dynamoReward.CashbackPercentage
            };
        }

		private MilesReward ToMilesReward(DynamoDBReward dynamoReward)
		{
            if (dynamoReward == null) return null;
            return new MilesReward
			{
				Categories = dynamoReward.Categories,
				Expiration = dynamoReward.Expiration,
				MilesPerDollarSpent = dynamoReward.MilesPerDollarSpent,
                DollarPricePerMile = dynamoReward.DollarPricePerMile,
			};
		}

        private SignUpReward ToSignupReward(DynamoDBReward dynamoReward)
		{
            return new SignUpReward
			{
				Categories = dynamoReward.Categories,
				Expiration = dynamoReward.Expiration,
                Miles = ToMilesReward(dynamoReward.Miles),
                Cashback = ToCashbackReward(dynamoReward.Cashback)
			};
		}

        /// <summary>
        /// A flattend reward that can hold values for each reward type
        /// </summary>
        private class DynamoDBReward
        {
            //Fields for all rewards
            public List<Category> Categories { get; set; }

            public DateTime Expiration { get; set; }

            public string Type { get; set; }

            //Fields for Cashback rewards
            public decimal CashbackPercentage { get; set; }

            //Fields for MilesReward
            public decimal MilesPerDollarSpent { get; set; }

            public decimal DollarPricePerMile { get; set; }

            //Fields for SignupReward
            public decimal AmountToSpend { get; set; }

            public DynamoDBReward Cashback { get; set; }

            public DynamoDBReward Miles { get; set; }
        }
    }
}
