namespace Cyrus.CQRS.Contracts
{
	public interface ICommand
	{
	}
	public interface ICommandHandler<TCommand, TResult>
		where TCommand : ICommand
	{
		TResult Handle(TCommand command);
	}
}