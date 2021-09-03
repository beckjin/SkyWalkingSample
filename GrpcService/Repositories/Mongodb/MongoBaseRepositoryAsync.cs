using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MD.MongoDB
{
    public partial class MongoBaseRepository<T>
    {
        #region 索引

        /// <inheritdoc/>
        public async Task<string> CreateOneIndexAsync(IndexKeysDefinition<T> keys, string indexName, CancellationToken cancellationToken = default)
        {
            var options = new CreateIndexOptions()
            {
                Background = true,
                Name = indexName
            };
            return await CreateOneIndexAsync(keys, options).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<string> CreateOneIndexAsync(IndexKeysDefinition<T> keys, CreateIndexOptions options, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            var index = new CreateIndexModel<T>(keys, options);
            return await myCollection.Indexes.CreateOneAsync(index, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region 新增

        /// <inheritdoc/>
        public async Task<bool> InsertAsync(T model, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            await myCollection.InsertOneAsync(model, cancellationToken: cancellationToken).ConfigureAwait(false);
            return true;

        }

        /// <inheritdoc/>
        public async Task<bool> InsertManyAsync(List<T> models, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            await myCollection.InsertManyAsync(models, cancellationToken: cancellationToken).ConfigureAwait(false);
            return true;
        }

        #endregion

        #region 修改

        /// <inheritdoc/>
        public async Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            UpdateOptions options = new UpdateOptions() { IsUpsert = isUpsert };
            return await myCollection.UpdateOneAsync(filter, update, options, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            UpdateOptions options = new UpdateOptions() { IsUpsert = isUpsert };
            return await myCollection.UpdateOneAsync<T>(filter, update, options, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ReplaceOneResult> ReplaceOneAsync(T entity, FilterDefinition<T> filter, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            return await myCollection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<UpdateResult> UpdateAllAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            UpdateOptions options = new UpdateOptions() { IsUpsert = isUpsert };
            return await myCollection.UpdateManyAsync(filter, update, options, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<UpdateResult> UpdateAllAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            UpdateOptions options = new UpdateOptions() { IsUpsert = isUpsert };
            return await myCollection.UpdateManyAsync<T>(filter, update, options, cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region 删除

        /// <inheritdoc/>
        public async Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            return await myCollection.DeleteOneAsync(filter, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<DeleteResult> DeleteOneAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            return await myCollection.DeleteOneAsync<T>(filter, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            return await myCollection.DeleteManyAsync(filter, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<DeleteResult> DeleteManyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            return await myCollection.DeleteManyAsync<T>(filter, cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region  查询

        /// <inheritdoc/>
        public async Task<List<TEntity>> FindAsync<TEntity>(FilterDefinition<T> filter, FindOptions<T, TEntity> options = null, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            var result = (await myCollection.FindAsync(filter, options, cancellationToken).ConfigureAwait(false)).ToList(cancellationToken);
            return result;
        }

        /// <inheritdoc/>
        public async Task<List<TO>> FindAsync<TI, TO>(FilterDefinition<TI> filter, FindOptions<TI, TO> options = null, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<TI>(collectionName);
            var result = (await myCollection.FindAsync(filter, options, cancellationToken).ConfigureAwait(false)).ToList(cancellationToken);
            return result;
        }

        /// <inheritdoc/>
        public async Task<List<T>> FindAsync(FilterDefinition<T> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            var result = (await myCollection.FindAsync(filter, options, cancellationToken).ConfigureAwait(false)).ToList(cancellationToken);
            return result;
        }

        /// <inheritdoc/>
        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default)
        {
            return await FindAsync((FilterDefinition<T>)filter, options, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<T> FindOneAsync(FilterDefinition<T> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default)
        {
            options ??= new FindOptions<T, T> { Limit = 1 };
            return (await FindAsync(filter, options, cancellationToken).ConfigureAwait(false)).FirstOrDefault();
        }

        /// <inheritdoc/>
        public async Task<TO> FindOneAsync<TI, TO>(FilterDefinition<TI> filter, FindOptions<TI, TO> options = null, CancellationToken cancellationToken = default)
        {
            options ??= new FindOptions<TI, TO>() { Limit = 1 };
            var result = await FindAsync(filter, options, cancellationToken).ConfigureAwait(false);
            return result.FirstOrDefault();
        }

        /// <inheritdoc/>
        public async Task<T> FindOneAsync(Expression<Func<T, bool>> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default)
        {
            options ??= new FindOptions<T, T>() { Limit = 1 };
            var result = await FindAsync(filter, options, cancellationToken).ConfigureAwait(false);
            return result.FirstOrDefault();
        }

        /// <inheritdoc/>
        public async Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            FindOneAndUpdateOptions<T> options = new FindOneAndUpdateOptions<T> { IsUpsert = isUpsert };
            return await myCollection.FindOneAndUpdateAsync(filter, update, options, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default)
        {
            return await FindOneAndUpdateAsync((FilterDefinition<T>)filter, update, isUpsert, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<List<TEntity>> AggregateAsync<TEntity>(PipelineDefinition<T, TEntity> filter, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            var result = (await myCollection.AggregateAsync(filter, options, cancellationToken).ConfigureAwait(false)).ToList(cancellationToken);
            return result;
        }

        /// <inheritdoc/>
        public async Task<List<T>> AggregateAsync(PipelineDefinition<T, T> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            var result = (await myCollection.AggregateAsync(pipeline, options, cancellationToken).ConfigureAwait(false)).ToList(cancellationToken);
            return result;
        }

        /// <inheritdoc/>
        public async Task<long> CountAsync(FilterDefinition<T> filter, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            var aggregateResult = myCollection.Aggregate().Match(filter).Group(new BsonDocument(){
                    {"_id","null"},
                    {"count",new BsonDocument("$sum",1)}
                });
            var results = await aggregateResult.ToListAsync(cancellationToken).ConfigureAwait(false);

            if (results.Count == 1)
                return Convert.ToInt64(results[0].GetElement("count").Value);
            else
                return 0;
        }

        /// <inheritdoc/>
        public async Task<long> CountAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            return await myCollection.CountDocumentsAsync<T>(filter, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<long> SumAsync(FilterDefinition<T> filter, string sumKeyField, string sumField, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);

            var aggregate = myCollection.Aggregate()
                .Match(filter)
                .Group(new BsonDocument { { "_id", "$" + sumKeyField }, { "sum", new BsonDocument("$sum", "$" + sumField) } });

            var results = await aggregate.ToListAsync(cancellationToken).ConfigureAwait(false);

            if (results.Count == 1)
                return Convert.ToInt64(results[0].GetElement("sum").Value);
            else
                return 0;

        }

        /// <inheritdoc/>
        public async Task<long> SumAsync(Expression<Func<T, bool>> filter, string sumKeyField, string sumField, CancellationToken cancellationToken = default)
        {
            return await SumAsync((FilterDefinition<T>)filter, sumKeyField, sumField, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<long> MaxAsync(FilterDefinition<T> filter, string maxKeyField, string maxField, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);

            var aggregate = myCollection.Aggregate()
                .Match(filter)
                .Group(new BsonDocument { { "_id", "$" + maxKeyField }, { "max", new BsonDocument("$max", "$" + maxField) } });

            var results = await aggregate.ToListAsync(cancellationToken).ConfigureAwait(false);

            if (results.Count == 1)
                return (long)results[0].GetElement("max").Value;
            else
                return 0;
        }

        /// <inheritdoc/>
        public async Task<long> MaxAsync(Expression<Func<T, bool>> filter, string maxKeyField, string maxField, CancellationToken cancellationToken = default)
        {
            return await MaxAsync((FilterDefinition<T>)filter, maxKeyField, maxField, cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region 批量写入

        /// <inheritdoc/>
        public async Task<BulkWriteResult> BulkWriteAsync(IEnumerable<WriteModel<T>> models, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            return await myCollection.BulkWriteAsync(models, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<bool> BulkWriteOneAsync(T entity, CancellationToken cancellationToken = default)
        {
            var model = new InsertOneModel<T>(entity);
            var inserResult = await BulkWriteAsync(new[] { model }, cancellationToken).ConfigureAwait(false);
            return inserResult != null && inserResult.InsertedCount == 1;
        }

        #endregion
    }
}
