using MassTransit;

namespace Framework.Messaging.Contracts;

public interface IMessageConsumer<TMessage> : IConsumer<TMessage> where TMessage : class { }; 

