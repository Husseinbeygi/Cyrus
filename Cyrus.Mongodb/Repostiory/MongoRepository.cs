using Cyrus.DDD;
using Cyrus.Mongodb.Contracts;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;

namespace Cyrus.Mongodb.Repostiory
{
    public abstract class MongoRepository<TEntity> :
    IMongoRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        public MongoRepository(IMongoDatabase mongoDatabase, string collection)
        {
            Collection = mongoDatabase.GetCollection<TEntity>(collection);
        }

        public IMongoCollection<TEntity> Collection { get; init; }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await Collection.InsertOneAsync(entity, null, cancellationToken);
        }

        public async Task AddAsync
            (TEntity entity, InsertOneOptions options, CancellationToken cancellationToken = default)
        {
            await Collection.InsertOneAsync(entity, options, cancellationToken);
        }

        public async Task AddAsync(IClientSessionHandle session, TEntity entity, CancellationToken cancellationToken = default)
        {
            await Collection.InsertOneAsync(session, entity, null, cancellationToken);
        }

        public async Task AddAsync(IClientSessionHandle session, TEntity entity, InsertOneOptions options, CancellationToken cancellationToken = default)
        {
            await Collection.InsertOneAsync(session, entity, options, cancellationToken);
        }

        public async Task AddRangeAsync
            (IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await Collection.InsertManyAsync(entities, null, cancellationToken);
        }

        public async Task AddRangeAsync
        (IEnumerable<TEntity> entities, InsertManyOptions options, CancellationToken cancellationToken = default)
        {
            await Collection.InsertManyAsync(entities, options, cancellationToken);
        }

        public async Task AddRangeAsync(IClientSessionHandle session, IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await Collection.InsertManyAsync(session, entities, null, cancellationToken);
        }

        public async Task AddRangeAsync(IClientSessionHandle session, IEnumerable<TEntity> entities, InsertManyOptions options, CancellationToken cancellationToken = default)
        {
            await Collection.InsertManyAsync(session, entities, options, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {

            var docs = await Collection
                            .Find(FilterDefinition<TEntity>.Empty)
                            .ToListAsync(cancellationToken: cancellationToken);
            return docs;
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var doc = await Collection
                            .Find(x => x.Id == id)
                            .SingleOrDefaultAsync(cancellationToken: cancellationToken);

            return doc;
        }

        public async Task<IEnumerable<TEntity>> GetSomeAsync
            (Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            using var doc = await Collection
                            .FindAsync(predicate, cancellationToken: cancellationToken);

            return doc.Current;
        }

        public async Task RemoveAsync
            (TEntity entity, CancellationToken cancellationToken = default)
        {
            await Collection.DeleteOneAsync(e => e.Id == entity.Id,
                                       cancellationToken: cancellationToken);
        }

        public async Task RemoveAsync(IClientSessionHandle session, TEntity entity, CancellationToken cancellationToken = default)
        {
            await Collection.DeleteOneAsync(session, e => e.Id == entity.Id,
                                       cancellationToken: cancellationToken);
        }

        public async Task<bool> RemoveByIdAsync
            (Guid id, CancellationToken cancellationToken = default)
        {
            var res = await Collection.DeleteOneAsync(e => e.Id == id,
                                       cancellationToken: cancellationToken);

            return res.IsAcknowledged;
        }

        public async Task<bool> RemoveByIdAsync(IClientSessionHandle session, Guid id, CancellationToken cancellationToken = default)
        {
            var res = await Collection.DeleteOneAsync(session, e => e.Id == id,
                                       cancellationToken: cancellationToken);

            return res.IsAcknowledged;
        }

        public async Task RemoveRangeAsync
            (IEnumerable<TEntity> entities,
                CancellationToken cancellationToken = default)
        {
            foreach (var item in entities)
            {
                var d =
                    await Collection.DeleteOneAsync(e => e.Id == item.Id, cancellationToken: cancellationToken);
            }
        }

        public async Task RemoveRangeAsync
            (Expression<Func<TEntity, bool>> predicate,
                CancellationToken cancellationToken = default)
        {
            await Collection.DeleteManyAsync(predicate, null, cancellationToken: cancellationToken);
        }

        public async Task RemoveRangeAsync
            (Expression<Func<TEntity, bool>> predicate,
                DeleteOptions options, CancellationToken cancellationToken = default)
        {
            await Collection.DeleteManyAsync(predicate, options,
                                    cancellationToken: cancellationToken);
        }

        public async Task RemoveRangeAsync(IClientSessionHandle session, IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            await Collection.DeleteManyAsync(session, null,
                                    cancellationToken: cancellationToken);
        }

        public async Task RemoveRangeAsync(IClientSessionHandle session, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            await Collection.DeleteManyAsync(session, predicate, null,
                                    cancellationToken: cancellationToken);
        }

        public async Task RemoveRangeAsync(IClientSessionHandle session, Expression<Func<TEntity, bool>> predicate, DeleteOptions options, CancellationToken cancellationToken = default)
        {
            await Collection.DeleteManyAsync(session, predicate, options,
                                    cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await UpdateAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(IClientSessionHandle session, TEntity entity, CancellationToken cancellationToken = default)
        {
            await UpdateAsync(session, entity, cancellationToken);
        }
    }
}