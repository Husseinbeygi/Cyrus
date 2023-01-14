using Cyrus.CQRS.Contracts;

namespace Cyrus.CQRS
{
	public interface IMessages
	{
		void Dispatch(ICommand command);
		T Dispatch<T>(IQuery<T> query);
		Task DispatchAsync(ICommandAsync command, CancellationToken cancellationToken = default);
		Task<T> DispatchAsync<T>(ICommandAsync<T> command, CancellationToken cancellationToken = default);
		Task<T> DispatchAsync<T>(IQueryAsync<T> query, CancellationToken cancellationToken = default);
		Task PublishAsync(IEventAsync @event, CancellationToken cancellationToken = default);
	}
}