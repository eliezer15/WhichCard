using System.Collections.Generic;
using System.Threading.Tasks;
using WhichCard.Entities;

namespace WhichCard.Repositories
{
    public class CreditCardRepository : BaseRepository, ICreditCardRepository
    {
        public Task Deleteasync(CreditCard creditCard) => _context.DeleteAsync(creditCard);

        public Task InsertAsync(CreditCard creditCard) => _context.SaveAsync(creditCard);

        public Task<CreditCard> GetAsync(string userId, string name) 
            => _context.LoadAsync<CreditCard>(userId, name, DefaultOperationConfig);

        public Task<List<CreditCard>> GetByUserAsync(string userId)
        {
            var search = _context.QueryAsync<CreditCard>(userId, DefaultOperationConfig);
            return search.GetRemainingAsync();
        }
    }
}
