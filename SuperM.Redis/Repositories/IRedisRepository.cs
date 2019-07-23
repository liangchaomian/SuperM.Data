using System;
using System.Collections.Generic;

namespace SuperM.Redis.Repositories
{
    /// <summary>
    /// Redis仓储接口
    /// </summary>
    public interface IRedisRepository
    {
        #region -- Item --
        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">值</param>
        /// <param name="dbId">库Id</param>
        /// <returns></returns>
        bool Set<T>(string key, T t, long dbId = 0);

        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">值</param>
        /// <param name="timeSpan">保存时间</param>
        /// <param name="dbId">库Id</param>
        /// <returns></returns>
        bool Set<T>(string key, T t, TimeSpan timeSpan, long dbId = 0);

        /// <summary>
        /// 设置单体
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">值</param>
        /// <param name="dateTime">过期时间</param>
        /// <returns></returns>
        bool Set<T>(string key, T t, DateTime dateTime, long dbId = 0);

        /// <summary>
        /// 获取单体
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="key">键值</param>
        /// <returns></returns>
        T Get<T>(string key, long dbId = 0) where T : class;

        /// <summary>
        /// 移除单体
        /// </summary>
        /// <param name="key">键值</param>
        bool Remove(string key, long dbId = 0);

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        void RemoveAll(long dbId = 0);
        #endregion

        #region -- List --
        /// <summary>
        /// 添加列表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">值</param>
        /// <param name="dbId">库</param>
        void List_Add<T>(string key, T t, long dbId = 0);

        /// <summary>
        /// 移除列表某个值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        bool List_Remove<T>(string key, T t, long dbId = 0);

        /// <summary>
        /// 移除列表所有值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="dbId">库Id</param>
        void List_RemoveAll<T>(string key, long dbId = 0);

        /// <summary>
        /// 获取列表数据条数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="dbId"></param>
        /// <returns></returns>
        long List_Count(string key, long dbId = 0);

        /// <summary>
        /// 获取指定条数列表数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="start">开始编号</param>
        /// <param name="count">条数</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        List<T> List_GetRange<T>(string key, int start, int count, long dbId = 0);

        /// <summary>
        /// 获取列表所有数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="dbId">库数据</param>
        /// <returns></returns>
        List<T> List_GetList<T>(string key, long dbId = 0);

        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        List<T> List_GetList<T>(string key, int pageIndex, int pageSize, long dbId = 0);

        #endregion

        #region -- Set --
        /// <summary>
        /// 添加集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">数值</param>
        /// <param name="dbId">库</param>
        void Set_Add<T>(string key, T t, long dbId = 0);

        /// <summary>
        /// 集合是否包含指定数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">数值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        bool Set_Contains<T>(string key, T t, long dbId = 0);

        /// <summary>
        /// 移除集合某个值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="t">数值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        bool Set_Remove<T>(string key, T t, long dbId = 0);

        #endregion

        #region -- Hash --
        /// <summary>
        /// 判断某个数据是否已经被缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">hashID</param>
        /// <param name="dataKey">键值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        bool Hash_Exist<T>(string key, string dataKey, long dbId = 0);

        /// <summary>
        /// 存储数据到hash表
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">hashID</param>
        /// <param name="dataKey">键值</param>
        /// <param name="t">数值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        bool Hash_Set<T>(string key, string dataKey, T t, long dbId = 0);

        /// <summary>
        /// 移除hash中的某值
        /// </summary>
        /// <param name="key">hashID</param>
        /// <param name="dataKey">键值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        bool Hash_Remove(string key, string dataKey, long dbId = 0);

        /// <summary>
        /// 移除整个hash
        /// </summary>
        /// <param name="key">hashID</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        bool Hash_Remove(string key, long dbId = 0);

        /// <summary>
        /// 从hash表获取数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">hashID</param>
        /// <param name="dataKey">键值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        T Hash_Get<T>(string key, string dataKey, long dbId = 0);

        /// <summary>
        /// 获取整个hash的数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">hashID</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        List<T> Hash_GetAll<T>(string key, long dbId = 0);

        #endregion

        #region -- SortedSet --
        /// <summary>
        ///  添加数据到 SortedSet
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">集合id</param>
        /// <param name="t">数值</param>
        /// <param name="score">排序码</param>
        /// <param name="dbId">库</param>
        bool SortedSet_Add<T>(string key, T t, double score, long dbId = 0);

        /// <summary>
        /// 移除数据从SortedSet
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">集合id</param>
        /// <param name="t">数值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        bool SortedSet_Remove<T>(string key, T t, long dbId = 0);

        /// <summary>
        /// 修剪SortedSet
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="size">保留的条数</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        long SortedSet_Trim(string key, int size, long dbId = 0);

        /// <summary>
        /// 获取SortedSet的长度
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        long SortedSet_Count(string key, long dbId = 0);

        /// <summary>
        /// 获取SortedSet的分页数据
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        List<T> SortedSet_GetList<T>(string key, int pageIndex, int pageSize, long dbId = 0);


        /// <summary>
        /// 获取SortedSet的全部数据
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="key">键值</param>
        /// <param name="dbId">库</param>
        /// <returns></returns>
        List<T> SortedSet_GetListALL<T>(string key, long dbId = 0);

        #endregion

        #region 公用方法
        /// <summary>
        /// 设置缓存过期
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="datetime">过期时间</param>
        /// <param name="dbId">库</param>
        void SetExpire(string key, DateTime datetime, long dbId = 0);
        #endregion
    }
}
