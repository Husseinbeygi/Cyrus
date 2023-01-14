namespace Cyrus.CQRS.Contracts
{
	public interface IQueryAsync<TResult>
	{
	}


	public interface IQueryHandlerAsync<IQueryAsync, TResult>
		where IQueryAsync : IQueryAsync<TResult>
	{
		Task<TResult> HandleAsync
			(IQueryAsync query, CancellationToken cancellationToken = default);
	}
}