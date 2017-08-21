using System;
using System.Collections.Generic;

namespace WhichCard.Entities
{
    public class CreditCardRecommendation
    {
        public Category Category { get; set; }

        public List<CreditCard> CreditCards { get; set; } = new List<CreditCard>();
    }

}
