using MongoDB.Bson;
using MongoDB.Driver;
using SuperM.MongoDB.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace SuperM.MongoDB.Repositories
{
    /// <summary>
    /// Implements IRepository for MongoDB.
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public class MongoDbRepository<T> : IMongoDbRepository<T> where T : IMongoModel
    {
        public MongoClient Client
        {
            get
            {
                MongoClient MongoDBClient = new MongoClient(MongoDBConfiguration.MongoDBConnectionString);
                return MongoDBClient;
            }
        }

        public IMongoDatabase Database
        {
            get { return Client.GetDatabase(MongoDBConfiguration.MongoDBDatatabase); }
        }

        public IMongoCollection<T> Collection
        {
            get
            {
                return Database.GetCollection<T>(typeof(T).Name);
            }
        }

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="Model"></param>
        public void Add(T Model)
                  => Collection.InsertOne(Model);
        

        /// <summary>
        /// 异步添加实体
        /// </summary>
        /// <param name="Model"></param>
        public async Task AddAsync(T Model)
                  =>await Collection.InsertOneAsync(Model);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            if (Collection.Find(e => e._id.Equals(entity._id)).Any())
            {
                var filter = Builders<T>.Filter.Eq("_id", entity._id);
                ReplaceOneResult result = Collection.ReplaceOne(filter, entity);
            }
        }

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="Model"></param>
        public void AddList(List<T> Model)
                 =>  Collection.InsertMany(Model);

        /// <summary>
        /// 异步批量添加实体
        /// </summary>
        /// <param name="Model"></param>
        public async Task AddListAsync(List<T> Model)
                 => await Collection.InsertManyAsync(Model);

        /// <summary>
        /// 根据Id查找
        /// </summary>
        public T FirstForId(string _id)
        {
            if (_id == null) { throw new ArgumentNullException("参数无效"); }
            return Collection.Find(s=>s._id== new ObjectId(_id)).FirstOrDefault();
        }

        /// <summary>
        /// 查找第一个
        /// </summary>
        public T FirstOrDefault(Expression<Func<T, bool>> expression)
        {
            if (expression == null) { throw new ArgumentNullException("参数无效"); }
            return Collection.Find(expression)?.FirstOrDefault();
        }

        /// <summary>
        /// 查找符合数据列表
        /// </summary>
        public List<T> FindToList(Expression<Func<T, bool>> expression)
        {
            if (expression == null) { throw new ArgumentNullException("参数无效"); }
            return Collection.Find(expression).ToList();
        }

        /// <summary>
        /// 查找符合数据列表(分页)
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="skip">跳过页数</param>
        /// <param name="take">获取页数</param>
        /// <returns></returns>
        public List<T> FindToPageList(Expression<Func<T, bool>> expression,int skip,int take)
        {
            if (expression == null) { throw new ArgumentNullException("参数无效"); }
            return Collection.Find(expression).Limit(take).ToList();
        }
        

        /// <summary> 
        /// 删除全部匹配数据
        /// </summary>
        public void Delete(Expression<Func<T, bool>> expression)
        {
            if (expression == null) { throw new ArgumentNullException("参数无效"); }
            Collection.DeleteMany(expression);
        }

        /// <summary> 
        /// 异步删除全部匹配数据
        /// </summary>
        public async Task  DeleteAsync(Expression<Func<T, bool>> expression)
        {
            if (expression == null) { throw new ArgumentNullException("参数无效"); }
            await Collection.DeleteManyAsync(expression);
        }


        /// <summary>
        /// 删除一个
        /// </summary>
        public void DeleteOne(Expression<Func<T, bool>> expression)
        {
            if (expression == null) { throw new ArgumentNullException("参数无效"); }
            Collection.DeleteOne(expression);
        }

        /// <summary>
        /// 异步删除一个
        /// </summary>
        public async Task DeleteOneAsync(Expression<Func<T, bool>> expression)
        {
            if (expression == null) { throw new ArgumentNullException("参数无效"); }
            await Collection.DeleteOneAsync(expression);
        }

        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="_id">Id</param>
        public void DeleteById(string _id)
        {
            Collection.DeleteOne(d=>d._id==new ObjectId(_id));
        }

        /// <summary>
        /// 转换为MongoDBId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ObjectId GetObjectId(string Id)
        {
            return new ObjectId(Id);
        }
    }
}
