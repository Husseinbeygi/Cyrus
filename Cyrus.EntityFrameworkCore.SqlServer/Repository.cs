using Cyrus.DDD;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cyrus.EntityFrameworkCore.SqlServer;

public abstract class Repository<TEntity> :
	IRepository<TEntity> where TEntity : class, IAggregateRoot
{
	public Repository
		(DbContext databaseContext) : base()
	{
		DatabaseContext =
			databaseContext ??
			throw new ArgumentNullException(paramName: nameof(databaseContext));

		DbSet =
			DatabaseContext.Set<TEntity>();
	}

	// **********
	protected DbSet<TEntity> DbSet { get; }
	// **********

	// **********
	protected DbContext DatabaseContext { get; }
	// **********

	public virtual
		async
		Task
		AddAsync(TEntity entity,
		CancellationToken cancellationToken = default)
	{
		if (entity == null)
		{
			throw new ArgumentNullException(paramName: nameof(entity));
		}

		await DbSet.AddAsync
			(entity: entity, cancellationToken: cancellationToken);
	}

	public virtual
		async
		Task
		AddRangeAsync(IEnumerable<TEntity> entities,
		CancellationToken cancellationToken = default)
	{
		if (entities == null)
		{
			throw new ArgumentNullException(paramName: nameof(entities));
		}

		await DbSet.AddRangeAsync
			(entities: entities, cancellationToken: cancellationToken);
	}

	public virtual
		async
		Task RemoveAsync
		(TEntity entity, CancellationToken cancellationToken = default)
	{
		if (entity == null)
		{
			throw new ArgumentNullException(paramName: nameof(entity));
		}

		await Task.Run(() =>
		{
			DbSet.Remove(entity: entity);

			//var attachedEntity =
			//	DatabaseContext.Attach(entity: entity);

			//attachedEntity.State =
			//	Microsoft.EntityFrameworkCore.EntityState.Deleted;
		}, cancellationToken: cancellationToken);
	}

	public virtual
		async
		Task<bool> RemoveByIdAsync
		(Guid id, CancellationToken cancellationToken = default)
	{
		TEntity entity =
			await FindAsync(id: id, cancellationToken: cancellationToken);

		if (entity == null)
		{
			return false;
		}

		await RemoveAsync
			(entity: entity, cancellationToken: cancellationToken);

		return true;
	}

	public virtual
		async
		Task
		RemoveRangeAsync(IEnumerable<TEntity> entities,
		CancellationToken cancellationToken = default)
	{
		if (entities == null)
		{
			throw new ArgumentNullException(paramName: nameof(entities));
		}

		foreach (var entity in entities)
		{
			await RemoveAsync
				(entity: entity, cancellationToken: cancellationToken);
		}
	}

	public virtual
		async
		Task UpdateAsync
		(TEntity entity, CancellationToken cancellationToken = default)
	{
		await Task.Run(() =>
		{
			var attachedEntity =
				DatabaseContext.Attach(entity: entity);

			if (attachedEntity.State != EntityState.Modified)
			{
				attachedEntity.State =
					EntityState.Modified;
			}
		}, cancellationToken: cancellationToken);
	}

	public virtual
		async
		Task
		<IEnumerable<TEntity>>
		GetAllAsync(CancellationToken cancellationToken = default)
	{
		// ToListAsync -> Extension Method -> using Microsoft.EntityFrameworkCore;
		var result =
			await
			DbSet.ToListAsync(cancellationToken: cancellationToken)
			;

		return result;
	}

	public virtual
		async
		Task<IEnumerable<TEntity>>
		Find(Expression<Func<TEntity, bool>> predicate,
		CancellationToken cancellationToken = default)
	{
		// ToListAsync -> Extension Method -> using Microsoft.EntityFrameworkCore;
		var result =
			await
			DbSet
			.Where(predicate: predicate)
			.ToListAsync(cancellationToken: cancellationToken)
			;

		return result;
	}

	public virtual
		async
		Task<TEntity> FindAsync
		(Guid id, CancellationToken cancellationToken = default)
	{
		var result =
			await DbSet.FindAsync(keyValues: new object[] { id },
			cancellationToken: cancellationToken);

		return result;
	}

	public virtual Task<IEnumerable<TEntity>> GetSomeAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public virtual async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
	{
		TEntity entity =
			await FindAsync(id: id, cancellationToken: cancellationToken);

		return entity;
	}
}
