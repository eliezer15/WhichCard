using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace WhichCard.Entities
{
    [DynamoDBTable("Users")]
    public class User
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public IEnumerable<string> ShoppingCategoryIds { get; set; }

        public IEnumerable<string> CreditCardIds { get; set; }
    }
}
