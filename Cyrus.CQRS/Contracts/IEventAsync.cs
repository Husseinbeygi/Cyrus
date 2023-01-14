namespace Cyrus.CQRS.Contracts
{
	public interface IEventAsync
	{

	}

	public interface IEventHandlerAsync<TEvent>
								where TEvent : IEventAsync
	{
		Task HandleAsync(TEvent @event, CancellationToken cancellationToken = default);
	}

}