using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace MD.MongoDB
{
    /// <summary>
    /// MongoDB Driver 封装
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMongoBaseRepository<T> where T : class
    {
        #region Sync

        #region 索引

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        string CreateOneIndex(IndexKeysDefinition<T> keys, string indexName, CancellationToken cancellationToken = default);

        /// <summary>
        ///  创建索引
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        string CreateOneIndex(IndexKeysDefinition<T> keys, CreateIndexOptions options, CancellationToken cancellationToken = default);

        #endregion

        #region 新增

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">新增的实体</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        bool Insert(T model, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="models">新增的实体</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        bool InsertMany(List<T> models, CancellationToken cancellationToken = default);

        #endregion

        #region 修改

        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="update">更新内容</param>
        /// <param name="isUpsert">数据不存在时是否新增</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        UpdateResult UpdateOne(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default);


        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="update">更新内容</param>
        /// <param name="isUpsert">数据不存在时是否新增</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        UpdateResult UpdateOne(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default);


        /// <summary>
        /// 整体替换
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        ReplaceOneResult ReplaceOne(T entity, FilterDefinition<T> filter, CancellationToken cancellationToken = default);


        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="update">更新内容</param>
        /// <param name="isUpsert">数据不存在时是否新增</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        UpdateResult UpdateAll(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default);


        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="update">更新内容</param>
        /// <param name="isUpsert">数据不存在时是否新增</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        UpdateResult UpdateAll(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default);


        #endregion

        #region 删除

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="filter">删除条件</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        DeleteResult DeleteOne(FilterDefinition<T> filter, CancellationToken cancellationToken = default);


        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="filter">删除条件</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        DeleteResult DeleteOne(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);


        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="filter">删除条件</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        DeleteResult DeleteMany(FilterDefinition<T> filter, CancellationToken cancellationToken = default);


        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="filter">删除条件</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        DeleteResult DeleteMany(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        #endregion

        #region  查询

        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter">查询条件</param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        List<TEntity> Find<TEntity>(FilterDefinition<T> filter, FindOptions<T, TEntity> options = null, CancellationToken cancellationToken = default);


        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <typeparam name="TI"></typeparam>
        /// <typeparam name="TO"></typeparam>
        /// <param name="filter">查询条件</param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        List<TO> Find<TI, TO>(FilterDefinition<TI> filter, FindOptions<TI, TO> options = null, CancellationToken cancellationToken = default);


        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        List<T> Find(FilterDefinition<T> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default);


        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        List<T> Find(Expression<Func<T, bool>> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default);


        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        T FindOne(FilterDefinition<T> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <typeparam name="TI"></typeparam>
        /// <typeparam name="TO"></typeparam>
        /// <param name="filter">查询条件</param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        TO FindOne<TI, TO>(FilterDefinition<TI> filter, FindOptions<TI, TO> options = null, CancellationToken cancellationToken = default);


        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        T FindOne(Expression<Func<T, bool>> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询并更新
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <param name="isUpsert"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        T FindOneAndUpdate(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询并更新
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <param name="isUpsert"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        T FindOneAndUpdate(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default);


        /// <summary>
        /// 统计
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter">查询条件</param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        List<TEntity> Aggregate<TEntity>(PipelineDefinition<T, TEntity> filter, AggregateOptions options = null, CancellationToken cancellationToken = default);


        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="pipeline"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        List<T> Aggregate(PipelineDefinition<T, T> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default);


        /// <summary>
        /// 统计数量
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        long Count(FilterDefinition<T> filter, CancellationToken cancellationToken = default);


        /// <summary>
        /// 统计数量
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        long Count(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);


        /// <summary>
        /// 统计某字段的和
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="sumKeyField">Bson关键词字段</param>
        /// <param name="sumField">Bson被统计字段</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        long Sum(FilterDefinition<T> filter, string sumKeyField, string sumField, CancellationToken cancellationToken = default);


        /// <summary>
        /// 统计某字段的和
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="sumKeyField">Bson关键词字段</param>
        /// <param name="sumField">Bson被统计字段</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        long Sum(Expression<Func<T, bool>> filter, string sumKeyField, string sumField, CancellationToken cancellationToken = default);

        /// <summary>
        /// 统计某字段的最大值
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="maxKeyField"></param>
        /// <param name="maxField"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        long Max(FilterDefinition<T> filter, string maxKeyField, string maxField, CancellationToken cancellationToken = default);


        /// <summary>
        /// 统计某字段的最大值
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="maxKeyField"></param>
        /// <param name="maxField"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        long Max(Expression<Func<T, bool>> filter, string maxKeyField, string maxField, CancellationToken cancellationToken = default);


        #endregion

        #region 批量

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="models"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        BulkWriteResult BulkWrite(IEnumerable<WriteModel<T>> models, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        bool BulkWriteOne(T entity, CancellationToken cancellationToken = default);


        #endregion

        #endregion

        #region Async

        #region 索引

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="indexName"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<string> CreateOneIndexAsync(IndexKeysDefinition<T> keys, string indexName, CancellationToken cancellationToken = default);

        /// <summary>
        ///  创建索引
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> CreateOneIndexAsync(IndexKeysDefinition<T> keys, CreateIndexOptions options, CancellationToken cancellationToken = default);

        #endregion

        #region 新增

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">新增的实体</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<bool> InsertAsync(T model, CancellationToken cancellationToken = default);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="models">新增的实体</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<bool> InsertManyAsync(List<T> models, CancellationToken cancellationToken = default);

        #endregion

        #region 修改

        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="update">更新内容</param>
        /// <param name="isUpsert">数据不存在时是否新增</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<UpdateResult> UpdateOneAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新单条数据
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="update">更新内容</param>
        /// <param name="isUpsert">数据不存在时是否新增</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<UpdateResult> UpdateOneAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 整体替换
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="filter"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<ReplaceOneResult> ReplaceOneAsync(T entity, FilterDefinition<T> filter, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="update">更新内容</param>
        /// <param name="isUpsert">数据不存在时是否新增</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<UpdateResult> UpdateAllAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新多条数据
        /// </summary>
        /// <param name="filter">过滤条件</param>
        /// <param name="update">更新内容</param>
        /// <param name="isUpsert">数据不存在时是否新增</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<UpdateResult> UpdateAllAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default);

        #endregion

        #region 删除

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter, CancellationToken cancellationToken = default);
        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<DeleteResult> DeleteOneAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<DeleteResult> DeleteManyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        #endregion

        #region  查询

        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<List<TEntity>> FindAsync<TEntity>(FilterDefinition<T> filter, FindOptions<T, TEntity> options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <typeparam name="TI"></typeparam>
        /// <typeparam name="TO"></typeparam>
        /// <param name="filter"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<List<TO>> FindAsync<TI, TO>(FilterDefinition<TI> filter, FindOptions<TI, TO> options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<List<T>> FindAsync(FilterDefinition<T> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询多条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<List<T>> FindAsync(Expression<Func<T, bool>> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<T> FindOneAsync(FilterDefinition<T> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <typeparam name="TI"></typeparam>
        /// <typeparam name="TO"></typeparam>
        /// <param name="filter"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<TO> FindOneAsync<TI, TO>(FilterDefinition<TI> filter, FindOptions<TI, TO> options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<T> FindOneAsync(Expression<Func<T, bool>> filter, FindOptions<T, T> options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询并更新
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <param name="isUpsert"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 查询并更新
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="update"></param>
        /// <param name="isUpsert"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, bool isUpsert = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 统计
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<List<TEntity>> AggregateAsync<TEntity>(PipelineDefinition<T, TEntity> filter, AggregateOptions options = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="pipeline"></param>
        /// <param name="options"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<List<T>> AggregateAsync(PipelineDefinition<T, T> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default);

        /// <summary>
        ///  统计数量
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<long> CountAsync(FilterDefinition<T> filter, CancellationToken cancellationToken = default);

        /// <summary>
        /// 统计数量
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<long> CountAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);

        /// <summary>
        /// 统计某字段的和
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="sumKeyField">BSon关键词字段</param>
        /// <param name="sumField">BSon被统计字段</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<long> SumAsync(FilterDefinition<T> filter, string sumKeyField, string sumField, CancellationToken cancellationToken = default);

        /// <summary>
        /// 统计某字段的和
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="sumKeyField">BSon关键词字段</param>
        /// <param name="sumField">BSon被统计字段</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<long> SumAsync(Expression<Func<T, bool>> filter, string sumKeyField, string sumField, CancellationToken cancellationToken = default);

        /// <summary>
        /// 统计某字段的最大值
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="maxKeyField"></param>
        /// <param name="maxField"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<long> MaxAsync(FilterDefinition<T> filter, string maxKeyField, string maxField, CancellationToken cancellationToken = default);

        /// <summary>
        /// 统计某字段的最大值
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="maxKeyField"></param>
        /// <param name="maxField"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<long> MaxAsync(Expression<Func<T, bool>> filter, string maxKeyField, string maxField, CancellationToken cancellationToken = default);

        #endregion

        #region 批量写入

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="models"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<BulkWriteResult> BulkWriteAsync(IEnumerable<WriteModel<T>> models, CancellationToken cancellationToken = default);

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<bool> BulkWriteOneAsync(T entity, CancellationToken cancellationToken = default);

        #endregion

        #endregion
    }
}
