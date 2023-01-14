namespace Cyrus.DDD
{
	public interface IQueryUnitOfWork : IDisposable
	{
		bool IsDisposed { get; }
	}
}
