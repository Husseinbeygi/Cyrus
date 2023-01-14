namespace Cyrus.DDD
{
	public interface IAggregateRoot : IEntity
	{
		void ClearDomainEvents();

		IReadOnlyList<IDomainEvent> DomainEvents { get; }
	}
}
