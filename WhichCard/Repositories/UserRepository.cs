using System;
using WhichCard.Entities;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using System.Threading.Tasks;

namespace WhichCard.Repositories
{
    public class UserRepository : IUserRepository
    {
        IDynamoDBContext _context;

        public UserRepository(BasicAWSCredentials credentials)
        {
            var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);
			_context = new DynamoDBContext(client);
        }

        public Task DeleteAsync(User user) => _context.DeleteAsync(user);

        public Task<User> GetAsync(string id)
        {
			return _context.LoadAsync<User>(id, new DynamoDBContextConfig
            {
                ConsistentRead = true
            });
        }

        public Task InsertAsync(User user) => _context.SaveAsync(user);
    }
}
