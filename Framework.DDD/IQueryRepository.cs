namespace Cyrus.DDD
{
	public interface IQueryRepository<T> where T : IAggregateRoot
	{
		Task<IEnumerable<T>> GetAllAsync
			(CancellationToken cancellationToken = default);

		Task<IEnumerable<T>> GetSomeAsync
			(System.Linq.Expressions.Expression<Func<T, bool>>
			predicate, CancellationToken cancellationToken = default);

		Task<T> GetByIdAsync
			(Guid id, CancellationToken cancellationToken = default);
	}
}
