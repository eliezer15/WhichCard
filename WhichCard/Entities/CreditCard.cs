using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
namespace WhichCard.Entities
{
    [DynamoDBTable("CreditCards")]
    public class CreditCard : IEntity
    {
        [DynamoDBHashKey]
        public string UserId { get; set; }

        [DynamoDBRangeKey]
        public string Name { get; set; }

        public Color Color { get; set; }

        public List<Reward> Rewards { get; set; }
    }

	public class Color
	{
		public string RgbValue { get; set; }
	}
}
