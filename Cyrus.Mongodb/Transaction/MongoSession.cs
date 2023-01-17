using MongoDB.Driver;

namespace Cyrus.Mongodb.Transaction;

public class MongoSession : IMongoSession
{
	private readonly IMongoClient _client;

	public MongoSession(IMongoClient client)
	{
		_client = client;
	}

	public async Task<IClientSessionHandle> StartAsync()
	{
		return await _client.StartSessionAsync();
	}
}
