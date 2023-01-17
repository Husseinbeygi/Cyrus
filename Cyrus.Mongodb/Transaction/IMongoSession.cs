using MongoDB.Driver;

namespace Cyrus.Mongodb.Transaction;

public interface IMongoSession
{
	Task<IClientSessionHandle> StartAsync();
}
