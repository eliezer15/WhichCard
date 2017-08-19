using System;
using System.Collections.Generic;
using WhichCard.Entities;

namespace WhichCard.Repositories
{
    public interface ICreditCardRepository
    {
        void Insert(CreditCard creditCard);

        void Delete(string id);

        CreditCard Get(string id);

        IEnumerable<CreditCard> GetAll(IEnumerable<string> ids);

        IEnumerable<CreditCard> GetByRewardsCategory(string categoryId);
    }
}
