using Amazon.Runtime;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon;
namespace WhichCard.Repositories
{
    public class BaseRepository
    {
		protected readonly IDynamoDBContext _context;
        public BaseRepository(BasicAWSCredentials credentials)
		{
			var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);
			_context = new DynamoDBContext(client);
		}

        protected static readonly DynamoDBOperationConfig DefaultOperationConfig = new DynamoDBOperationConfig
        {
            ConsistentRead = true
        };
    }
}
