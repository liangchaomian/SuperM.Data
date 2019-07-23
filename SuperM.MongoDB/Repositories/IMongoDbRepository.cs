using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SuperM.MongoDB.Repositories
{
    public interface IMongoDbRepository<T>  where T : IMongoModel
    {
        /// <summary>
        /// 添加单个实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Add(T model);

        /// <summary>
        /// 异步添加实体
        /// </summary>
        /// <param name="model"></param>
        Task AddAsync(T model);

        /// <summary>
        /// 添加多个实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void AddList(List<T> model);


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// 异步批量添加实体
        /// </summary>
        /// <param name="model"></param>
        Task AddListAsync(List<T> model);

        /// <summary>
        /// 根据Id查找
        /// </summary>
        T FirstForId(string _id);

        /// <summary>
        /// 查找第一个
        /// </summary>
        T FirstOrDefault(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 查找符合数据列表
        /// </summary>
        List<T> FindToList(Expression<Func<T, bool>> expression);
        
        /// <summary>
        /// 查找符合数据列表(分页)
        /// </summary>
        List<T> FindToPageList(Expression<Func<T, bool>> expression, int skip, int take);

        /// <summary>
        /// 删除全部匹配数据
        /// </summary>
        void Delete(Expression<Func<T, bool>> expression);

        /// <summary> 
        /// 异步删除全部匹配数据
        /// </summary>
        Task DeleteAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 删除一个
        /// </summary>
        void DeleteOne(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 异步删除一个
        /// </summary>
        Task DeleteOneAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="_id">Id</param>
        void DeleteById(string _id);

        /// <summary>
        /// 转换为MongoDBId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        ObjectId GetObjectId(string Id);
    }
}
