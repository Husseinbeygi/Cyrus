namespace Cyrus.DDD
{
	public interface IUnitOfWork : IDisposable
	{
		bool IsDisposed { get; }

		Task<int> SaveAsync();
	}
}
