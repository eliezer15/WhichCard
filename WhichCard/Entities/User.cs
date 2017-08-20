using System.Collections.Generic;
using Amazon.DynamoDBv2.DataModel;

namespace WhichCard.Entities
{
    [DynamoDBTable("Users")]
    public class User : IEntity
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// Names of Shopping categories for this user
        /// </summary>
        /// <value>The shopping categories.</value>
        public List<string> ShoppingCategories { get; set; }

        /// <summary>
        /// Names of Credit Cards for this user
        /// </summary>
        /// <value>The credit cards.</value>
        public List<string> CreditCards { get; set; }
    }
}
