using System.Collections.Generic;
using System.Threading.Tasks;
using WhichCard.Entities;

namespace WhichCard.Repositories
{
    public interface ICreditCardRepository
    {
        Task InsertAsync(CreditCard creditCard);

        Task Deleteasync(CreditCard creditCard);

        Task<CreditCard> GetAsync(string userId, string name);

        Task<List<CreditCard>> GetByUserAsync(string userId);
    }
}
