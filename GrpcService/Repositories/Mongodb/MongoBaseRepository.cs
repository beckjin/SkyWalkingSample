using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SkyApm.Diagnostics.MongoDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace MD.MongoDB
{
    /// <summary>
    /// MongoDB Driver 封装
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class MongoBaseRepository<T> : IMongoBaseRepository<T> where T : class
    {
        // 数据库名称
        private readonly string databaseName;

        // collection 名称
        private readonly string collectionName;

        // client
        private readonly MongoClient client;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connstr">连接字符串</param>
        /// <param name="databaseName">数据库名，如果 connstr 提供了数据库名，则会覆盖此字段的值</param>
        /// <param name="collectionName">表名</param>
        public MongoBaseRepository(string connstr, string databaseName, string collectionName)
        {
            if (!string.IsNullOrEmpty(connstr))
            {
                BsonSerializer.RegisterSerializationProvider(new BsonSerializationRepository());

                var mongoConnectionUrl = new MongoUrl(connstr);
                if (!string.IsNullOrEmpty(mongoConnectionUrl.DatabaseName))
                {
                    databaseName = mongoConnectionUrl.DatabaseName;
                }

                var setting = MongoClientSettings.FromUrl(mongoConnectionUrl);
                setting.ClusterConfigurator = cb => cb.Subscribe(new DiagnosticsActivityEventSubscriber());
                client = new MongoClient(setting);
                this.databaseName = databaseName;
                this.collectionName = collectionName;
            }
        }

        /// <inheritdoc/>
        public IMongoCollection<T> GetCollection()
        {
            var database = client.GetDatabase(databaseName);
            return database.GetCollection<T>(collectionName);
        }

        #region 索引

        /// <inheritdoc/>
        public string CreateOneIndex(IndexKeysDefinition<T> keys, string indexName, CancellationToken cancellationToken = default)
        {
            var options = new CreateIndexOptions()
            {
                Background = true,
                Name = indexName
            };
            return CreateOneIndex(keys, options);
        }

        /// <inheritdoc/>
        public string CreateOneIndex(IndexKeysDefinition<T> keys, CreateIndexOptions options, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            var index = new CreateIndexModel<T>(keys, options);
            return myCollection.Indexes.CreateOne(index, cancellationToken: cancellationToken);
        }

        #endregion

        #region 新增

        /// <inheritdoc/>
        public bool Insert(T model, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            myCollection.InsertOne(model, cancellationToken: cancellationToken);
            return true;
        }

        /// <inheritdoc/>
        public bool InsertMany(List<T> models, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            myCollection.InsertMany(models, cancellationToken: cancellationToken);
            return true;
        }

        #endregion

        #region 修改

        /// <inheritdoc/>
        public UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            UpdateOptions options = new UpdateOptions() { IsUpsert = isUpsert };
            return myCollection.UpdateOne(filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public UpdateResult UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            UpdateOptions options = new UpdateOptions() { IsUpsert = isUpsert };
            return myCollection.UpdateOne<T>(filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public ReplaceOneResult ReplaceOne(T entity, FilterDefinition<T> filter, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            return myCollection.ReplaceOne(filter, entity, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public UpdateResult UpdateAll(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            UpdateOptions options = new UpdateOptions() { IsUpsert = isUpsert };
            return myCollection.UpdateMany(filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public UpdateResult UpdateAll(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            UpdateOptions options = new UpdateOptions() { IsUpsert = isUpsert };
            return myCollection.UpdateMany<T>(filter, update, options, cancellationToken);
        }

        #endregion

        #region 删除

        /// <inheritdoc/>
        public DeleteResult DeleteOne(FilterDefinition<T> filter, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            return myCollection.DeleteOne(filter, cancellationToken);
        }

        /// <inheritdoc/>
        public DeleteResult DeleteOne(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            return myCollection.DeleteOne<T>(filter, cancellationToken);
        }

        /// <inheritdoc/>
        public DeleteResult DeleteMany(FilterDefinition<T> filter, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            return myCollection.DeleteMany(filter, cancellationToken);
        }

        /// <inheritdoc/>
        public DeleteResult DeleteMany(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            return myCollection.DeleteMany<T>(filter, cancellationToken);
        }

        #endregion

        #region  查询

        /// <inheritdoc/>
        public List<TEntity> Find<TEntity>(FilterDefinition<T> filter, FindOptions<T, TEntity> options = null, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            var result = myCollection.FindSync(filter, options, cancellationToken).ToList(cancellationToken);
            return result;
        }

        /// <inheritdoc/>
        public List<TO> Find<TI, TO>(FilterDefinition<TI> filter, FindOptions<TI, TO> options = null, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<TI>(collectionName);
            var result = myCollection.FindSync(filter, options, cancellationToken).ToList(cancellationToken);
            return result;
        }

        /// <inheritdoc/>
        public List<T> Find(FilterDefinition<T> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            var result = myCollection.FindSync(filter, options, cancellationToken).ToList(cancellationToken);
            return result;
        }

        /// <inheritdoc/>
        public List<T> Find(Expression<Func<T, bool>> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default)
        {
            return Find((FilterDefinition<T>)filter, options, cancellationToken);
        }

        /// <inheritdoc/>
        public T FindOne(FilterDefinition<T> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default)
        {
            options ??= new FindOptions<T, T> { Limit = 1 };
            return Find(filter, options, cancellationToken).FirstOrDefault();
        }

        /// <inheritdoc/>
        public TO FindOne<TI, TO>(FilterDefinition<TI> filter, FindOptions<TI, TO> options = null, CancellationToken cancellationToken = default)
        {
            options ??= new FindOptions<TI, TO>() { Limit = 1 };
            var result = Find(filter, options, cancellationToken);
            return result.FirstOrDefault();
        }

        /// <inheritdoc/>
        public T FindOne(Expression<Func<T, bool>> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default)
        {
            options ??= new FindOptions<T, T>() { Limit = 1 };
            var result = Find(filter, options, cancellationToken);
            return result.FirstOrDefault();
        }

        /// <inheritdoc/>
        public T FindOneAndUpdate(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            FindOneAndUpdateOptions<T> options = new FindOneAndUpdateOptions<T> { IsUpsert = isUpsert };
            return myCollection.FindOneAndUpdate(filter, update, options, cancellationToken);
        }

        /// <inheritdoc/>
        public T FindOneAndUpdate(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default)
        {
            return FindOneAndUpdate((FilterDefinition<T>)filter, update, isUpsert, cancellationToken);
        }

        /// <inheritdoc/>
        public List<TEntity> Aggregate<TEntity>(PipelineDefinition<T, TEntity> filter, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            var result = myCollection.Aggregate(filter, options, cancellationToken).ToList(cancellationToken);
            return result;
        }

        /// <inheritdoc/>
        public List<T> Aggregate(PipelineDefinition<T, T> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            var result = myCollection.Aggregate(pipeline, options, cancellationToken).ToList(cancellationToken);
            return result;
        }

        /// <inheritdoc/>
        public long Count(FilterDefinition<T> filter, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            var aggregateResult = myCollection.Aggregate().Match(filter).Group(new BsonDocument(){
                    {"_id","null"},
                    {"count",new BsonDocument("$sum",1)}
                });
            var results = aggregateResult.ToList(cancellationToken);

            if (results.Count == 1)
                return Convert.ToInt64(results[0].GetElement("count").Value);
            else
                return 0;
        }

        /// <inheritdoc/>
        public long Count(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            return myCollection.CountDocuments<T>(filter, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public long Sum(FilterDefinition<T> filter, string sumKeyField, string sumField, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);

            var aggregate = myCollection.Aggregate()
                .Match(filter)
                .Group(new BsonDocument { { "_id", "$" + sumKeyField }, { "sum", new BsonDocument("$sum", "$" + sumField) } });

            var results = aggregate.ToList(cancellationToken);

            if (results.Count == 1)
                return Convert.ToInt64(results[0].GetElement("sum").Value);
            else
                return 0;
        }

        /// <inheritdoc/>
        public long Sum(Expression<Func<T, bool>> filter, string sumKeyField, string sumField, CancellationToken cancellationToken = default)
        {
            return Sum((FilterDefinition<T>)filter, sumKeyField, sumField, cancellationToken);
        }

        /// <inheritdoc/>
        public long Max(FilterDefinition<T> filter, string maxKeyField, string maxField, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);

            var aggregate = myCollection.Aggregate()
                .Match(filter)
                .Group(new BsonDocument { { "_id", "$" + maxKeyField }, { "max", new BsonDocument("$max", "$" + maxField) } });

            var results = aggregate.ToList(cancellationToken);

            if (results.Count == 1)
                return (long)results[0].GetElement("max").Value;
            else
                return 0;
        }

        /// <inheritdoc/>
        public long Max(Expression<Func<T, bool>> filter, string maxKeyField, string maxField, CancellationToken cancellationToken = default)
        {
            return Max((FilterDefinition<T>)filter, maxKeyField, maxField, cancellationToken);
        }

        #endregion

        #region 批量

        /// <inheritdoc/>
        public BulkWriteResult BulkWrite(IEnumerable<WriteModel<T>> models, CancellationToken cancellationToken = default)
        {
            var database = client.GetDatabase(databaseName);
            var myCollection = database.GetCollection<T>(collectionName);
            return myCollection.BulkWrite(models, cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public bool BulkWriteOne(T entity, CancellationToken cancellationToken = default)
        {
            var model = new InsertOneModel<T>(entity);
            var inserResult = BulkWrite(new[] { model }, cancellationToken);
            return inserResult != null && inserResult.InsertedCount == 1;
        }

        #endregion
    }
}
