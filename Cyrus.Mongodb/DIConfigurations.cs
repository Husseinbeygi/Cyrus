using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver.Core.Configuration;
using System.ComponentModel;

namespace Cyrus.Mongodb;

public static class DIConfigurations
{
	public static IServiceCollection AddMongoDb(this IServiceCollection services,
											Func<IMongoDbOptions,IMongoDbOptions> buildOptions)
	{

		

		return services;
	}
}


public interface IMongoDbOptions 
{
	IMongoDbOptions WithConnectionString(string connectionString);
	IMongoDbOptions WithUserNameAndPassword(ConnectionStringScheme scheme,string username,string password,int port = 27017);

}

public class MongoDbOptions : IMongoDbOptions
{
	public IMongoDbOptions WithConnectionString(string connectionString)
	{
		throw new NotImplementedException();
	}

	public IMongoDbOptions WithUserNameAndPassword(ConnectionStringScheme scheme, string username, string password, int port = 27017)
	{
		throw new NotImplementedException();
	}
}