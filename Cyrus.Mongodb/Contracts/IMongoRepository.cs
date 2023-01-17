using Cyrus.DDD;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Cyrus.Mongodb.Contracts;

public interface IMongoRepository<TEntity> : IRepository<TEntity> where TEntity : IAggregateRoot
{
	IMongoCollection<TEntity> Collection { get; init; }

	Task AddAsync
		(TEntity entity, InsertOneOptions options, CancellationToken cancellationToken = default);

	Task AddRangeAsync
			(IEnumerable<TEntity>
			entities, InsertManyOptions options, CancellationToken cancellationToken = default);

	Task RemoveRangeAsync
			(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

	Task RemoveRangeAsync
			(Expression<Func<TEntity, bool>> predicate,
						DeleteOptions options, CancellationToken cancellationToken = default);

	Task AddAsync
			(IClientSessionHandle session, 
			TEntity entity, InsertOneOptions options, CancellationToken cancellationToken = default);

	Task AddRangeAsync
			(IClientSessionHandle session, IEnumerable<TEntity>
			entities, InsertManyOptions options, CancellationToken cancellationToken = default);

	Task RemoveRangeAsync
			(IClientSessionHandle session, 
			Expression<Func<TEntity, bool>> predicate, 
					CancellationToken cancellationToken = default);

	Task RemoveRangeAsync
			(IClientSessionHandle session,Expression<Func<TEntity, bool>> predicate,
						DeleteOptions options, CancellationToken cancellationToken = default);

	Task AddAsync
		(IClientSessionHandle session, 
			TEntity entity, CancellationToken cancellationToken = default);

	Task AddRangeAsync
		(IClientSessionHandle session,IEnumerable<TEntity>
		entities, CancellationToken cancellationToken = default);

	Task UpdateAsync
		(IClientSessionHandle session,
				TEntity entity, CancellationToken cancellationToken = default);

	Task RemoveAsync
		(IClientSessionHandle session,
				TEntity entity, CancellationToken cancellationToken = default);

	Task RemoveRangeAsync
		(IClientSessionHandle session, 
			IEnumerable<TEntity> entities, 
				CancellationToken cancellationToken = default);

	Task<bool> RemoveByIdAsync
		(IClientSessionHandle session,
			Guid id, CancellationToken cancellationToken = default);

}
