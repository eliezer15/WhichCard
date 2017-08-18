using System.Collections.Generic;
namespace WhichCard.Entities
{
    public class CreditCard
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Color Color { get; set; }

        public IEnumerable<Reward> Rewards { get; set; }
    }
}
