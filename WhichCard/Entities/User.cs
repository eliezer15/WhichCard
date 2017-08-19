using System.Collections.Generic;
namespace WhichCard.Entities
{
    public class User
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public IEnumerable<string> ShoppingCategoryIds { get; set; }

        public IEnumerable<string> CreditCardIds { get; set; }
    }
}
