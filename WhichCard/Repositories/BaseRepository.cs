using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon;
namespace WhichCard.Repositories
{
    public class BaseRepository
    {
		protected readonly IDynamoDBContext _context;
        public BaseRepository()
		{
			var client = new AmazonDynamoDBClient(RegionEndpoint.USEast1);
			_context = new DynamoDBContext(client);
		}

        protected static readonly DynamoDBOperationConfig DefaultOperationConfig = new DynamoDBOperationConfig
        {
            ConsistentRead = true
        };
    }
}
