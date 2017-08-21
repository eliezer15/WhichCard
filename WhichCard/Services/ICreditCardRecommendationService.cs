using System;
using System.Collections.Generic;
using WhichCard.Entities;

namespace WhichCard.Services
{
    public interface ICreditCardRecommendationService
    {
        IEnumerable<CreditCardRecommendation> GetRecommendations(User user);
    }
}
