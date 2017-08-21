using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
namespace WhichCard.Entities
{
    public class CreditCard : IEntity
    {
        public string Name { get; set; }

        public Color Color { get; set; }

        [DynamoDBProperty(typeof(RewardTypeConverter))]
        public List<Reward> Rewards { get; set; }
    }

	public class Color
	{
		public string RgbValue { get; set; }
	}
}
