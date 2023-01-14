namespace Cyrus.DDD
{
	public interface IRepository<T> where T : IAggregateRoot
	{
		Task AddAsync
			(T entity, CancellationToken cancellationToken = default);

		Task AddRangeAsync
			(IEnumerable<T>
			entities, CancellationToken cancellationToken = default);

		Task UpdateAsync
			(T entity, CancellationToken cancellationToken = default);

		Task RemoveAsync
			(T entity, CancellationToken cancellationToken = default);

		Task RemoveRangeAsync
			(IEnumerable<T>
			entities, CancellationToken cancellationToken = default);

		Task<bool> RemoveByIdAsync
			(Guid id, CancellationToken cancellationToken = default);

		Task<IEnumerable<T>> GetAllAsync
			(CancellationToken cancellationToken = default);

		Task<IEnumerable<T>> GetSomeAsync
			(System.Linq.Expressions.Expression<Func<T, bool>>
			predicate, CancellationToken cancellationToken = default);

		Task<T> GetByIdAsync
			(Guid id, CancellationToken cancellationToken = default);
	}
}
