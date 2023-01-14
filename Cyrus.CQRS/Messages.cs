using Cyrus.CQRS.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.ConstrainedExecution;

namespace Cyrus.CQRS
{
	public sealed class Messages : IMessages
	{
		private readonly IServiceProvider _provider;

		public Messages(IServiceProvider provider)
		{
			_provider = provider;
		}

		public void Dispatch(ICommand command)
		{
			Type type = typeof(ICommandHandler<,>);
			Type[] typeArgs = { command.GetType() };
			Type handlerType = type.MakeGenericType(typeArgs);

			dynamic handler = _provider.GetService(handlerType);
			handler.Handle((dynamic)command);
		}

		public T Dispatch<T>(IQuery<T> query)
		{
			Type type = typeof(IQueryHandler<,>);
			Type[] typeArgs = { query.GetType(), typeof(T) };
			Type handlerType = type.MakeGenericType(typeArgs);

			dynamic handler = _provider.GetService(handlerType);

			T result = handler.Handle((dynamic)query);

			return result;
		}

		public async Task<T> DispatchAsync<T>(IQueryAsync<T> query,
		CancellationToken cancellationToken)
		{
			Type type = typeof(IQueryHandlerAsync<,>);
			Type[] typeArgs = { query.GetType(), typeof(T) };
			Type handlerType = type.MakeGenericType(typeArgs);

			dynamic handler = _provider.GetService(handlerType);

			T result = await handler.HandleAsync((dynamic)query);

			return result;
		}

		public async Task DispatchAsync(ICommandAsync command, CancellationToken cancellationToken = default)
		{
			Type type = typeof(ICommandHandlerAsync<,>);
			Type[] typeArgs = { command.GetType() };
			Type handlerType = type.MakeGenericType(typeArgs);

			dynamic handler = _provider.GetService(handlerType);
			await handler.HandleAsync((dynamic)command);
		}

		public async Task<T> DispatchAsync<T>(ICommandAsync<T> command, CancellationToken cancellationToken = default)
		{
			Type type = typeof(ICommandHandlerAsync<,>);
			Type[] typeArgs = { command.GetType(), typeof(T) };
			Type handlerType = type.MakeGenericType(typeArgs);

			dynamic handler = _provider.GetService(handlerType);
			T result = await handler.HandleAsync((dynamic)command);

			return result;
		}

		public async Task PublishAsync(IEventAsync @event, CancellationToken cancellationToken)
		{
			try
			{
				Type type = typeof(IEventHandlerAsync<>);
				Type typeArgs = @event.GetType();
				Type handlerType = type.MakeGenericType(typeArgs);

				dynamic _services = _provider.GetServices(handlerType);

				foreach (var item in _services)
				{
					if (item is null)
						continue;

					await item.HandleAsync((dynamic)@event);
				}

			}
			catch (Exception ex)
			{

				throw;
			}

		}
	}
}
