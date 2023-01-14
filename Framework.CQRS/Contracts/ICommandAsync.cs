namespace Cyrus.CQRS.Contracts
{
	public interface ICommandAsync
	{
	}


	public interface ICommandAsync<T> : ICommandAsync
	{
	}

	public interface ICommandHandlerAsync<TEvent, TResult>
	where TEvent : ICommandAsync
	{
		Task<TResult> HandleAsync(TEvent command);
	}

}