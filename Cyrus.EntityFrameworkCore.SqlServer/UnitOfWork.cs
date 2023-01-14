using Cyrus.DDD;

namespace Cyrus.EntityFrameworkCore.SqlServer;

public abstract class UnitOfWork<TDbContext> :
	IUnitOfWork where TDbContext : Microsoft.EntityFrameworkCore.DbContext
{
	public UnitOfWork(TDbContext databaseContext) : base()
	{
		DatabaseContext = databaseContext;
	}

	// **********
	protected TDbContext DatabaseContext { get; }
	// **********

	// **********
	/// <summary>
	/// To detect redundant calls
	/// </summary>
	public bool IsDisposed { get; protected set; }
	// **********

	/// <summary>
	/// Public implementation of Dispose pattern callable by consumers.
	/// </summary>
	public void Dispose()
	{
		Dispose(true);

		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
	/// </summary>
	protected virtual void Dispose(bool disposing)
	{
		if (IsDisposed)
		{
			return;
		}

		if (disposing)
		{
			// TODO: dispose managed state (managed objects).

			if (DatabaseContext != null)
			{
				DatabaseContext.Dispose();
			}
		}

		// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
		// TODO: set large fields to null.

		IsDisposed = true;
	}

	public async Task<int> SaveAsync()
	{
		int result =
			await DatabaseContext.SaveChangesAsync();

		return result;
	}

	~UnitOfWork()
	{
		Dispose(false);
	}
}
