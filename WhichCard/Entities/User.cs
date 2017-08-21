using System;
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

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public List<Category> ShoppingCategories { get; set; } = new List<Category>();

        public List<CreditCard> CreditCards { get; set; } = new List<CreditCard>();
    }
}
