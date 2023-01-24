using Cyrus.DDD;
using Cyrus.Mongodb.Contracts;
using Cyrus.Mongodb.Repository;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using System.Collections;
using System.ComponentModel;
using System.Text;

namespace Cyrus.Mongodb;

public static class DIConfigurations
{
	public static IServiceCollection AddMongoDb(this IServiceCollection services,
											Func<IOptionBuilder, IOptionBuilder> buildOptions)
	{
		services.AddSingleton(buildOptions);

		var _opts = buildOptions(new OptionBuilder()).Build();

		services.AddSingleton(_opts);
		services.AddSingleton<IMongoClient>(opt =>
		{
			var options = opt.GetRequiredService<MongoDbOptions>();
			return new MongoClient(_opts.ConnectionString);
		});

		services.AddTransient(sp =>
		{
			var options = sp.GetRequiredService<MongoDbOptions>();
			var client = sp.GetRequiredService<IMongoClient>();
			return client.GetDatabase(options.Database);
		});

		return services;
	}

	public static IServiceCollection AddMongoRepository<TEntity>
				(this IServiceCollection services,string collectionName) 
						where TEntity : IAggregateRoot
	{
		services.AddTransient<IMongoRepository<TEntity>>(opt =>
		{
			var database = opt.GetRequiredService<IMongoDatabase>();
			return new MongoRepository<TEntity>(database, collectionName);
		});

		return services;
	}
}

public interface IOptionBuilder
{
	IOptionBuilder WithConnectionString(string connectionString);
	IOptionBuilder WithUserNameAndPassword(ConnectionStringScheme scheme, string username, string password, string host, int port = 27017);
	MongoDbOptions Build();
	IOptionBuilder WithDatabase(string database);
}

public class OptionBuilder : IOptionBuilder
{
	private MongoDbOptions _options; 
    public OptionBuilder()
    {
		_options = new MongoDbOptions();

	}
	public MongoDbOptions Build()
	{
		return _options;
	}

	public IOptionBuilder WithConnectionString(string connectionString)
	{
		_options.ConnectionString = connectionString;
		return this;
	}

	public IOptionBuilder WithDatabase(string database)
	{
		_options.Database = database;	
		return this;	
	}

	public IOptionBuilder WithUserNameAndPassword(ConnectionStringScheme scheme, string username, string password, string host, int port = 27017)
	{
		var conn = new StringBuilder();
		conn
		.Append(scheme == ConnectionStringScheme.MongoDBPlusSrv ? "mongodb+srv://" : "mongodb://")
		.Append(username)
		.Append(":")
		.Append(password)
		.Append("@")
		.Append(host)
		.Append(":")
		.Append(port);

		_options.ConnectionString = conn.ToString();
		return this;
	}


}

public class MongoDbOptions
{
	public string ConnectionString { get; set; }
	public string Database { get; set; }
}