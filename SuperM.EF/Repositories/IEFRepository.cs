using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SuperM.EF.Repositories
{
    public interface IEFRepository
    {
        /// <summary>
        /// 释放上下文
        /// </summary>
        void Dispose();

        /// <summary>
        /// 根据主键查询单个
        /// </summary>
        /// <typeparam name="keyValue">主键</typeparam>
        /// <returns></returns>
        T Find<T>(string keyValue) where T : class;

        /// <summary>
        /// 根据表达式查询单个
        /// </summary>
        /// <typeparam name="condition">表达式</typeparam>
        /// <returns></returns>
        T Find<T>(Expression<Func<T, bool>> condition) where T : class;

        /// <summary>
        /// 根据主键查询单个(异步)
        /// </summary>
        /// <typeparam name="keyValue">主键</typeparam>
        /// <returns></returns>
        Task<T> FindAsync<T>(string keyValue) where T : class;

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        IQueryable<T> IQueryable<T>() where T : class;

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class;

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        List<T> FindList<T>() where T : class;

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        List<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class;

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="page">页数</param>
        /// <param name="size">行数</param>
        /// <param name="totalcount">总数</param>
        /// <returns></returns>
        IQueryable<T> IQueryableByPage<T>(int page, int size, out int totalcount) where T : class;

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <param name="page">页数</param>
        /// <param name="size">行数</param>
        /// <param name="totalcount">总数</param>
        /// <returns></returns>
        IQueryable<T> IQueryableByPage<T>(Expression<Func<T, bool>> condition, int page, int size, out int totalcount) where T : class;

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="page">页数</param>
        /// <param name="size">行数</param>
        /// <param name="totalcount">总数</param>
        /// <returns></returns>
        List<T> FindListByPage<T>(int page, int size, out int totalcount) where T : class;

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <param name="page">页数</param>
        /// <param name="size">行数</param>
        /// <param name="totalcount">总数</param>
        /// <returns></returns>
        List<T> FindListByPage<T>(Expression<Func<T, bool>> condition, int page, int size, out int totalcount) where T : class;

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        int GetListCount<T>(Expression<Func<T, bool>> condition) where T : class;

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool AddEntity<T>(T entity) where T : class;

        /// <summary>
        /// 异步新增
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<int> AddEntityAsync<T>(T entity) where T : class;

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        bool DeleteEntity<T>(T entity) where T : class;

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        T UpdateEnetity<T>(T entity) where T : class;

        /// <summary>
        /// 批量增加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        int AddEntityList<T>(List<T> entityList) where T : class;

        /// <summary>
        /// 异步批量增加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> AddEntityListAsync<T>(List<T> entityList) where T : class;

        /// <summary>
        /// 根据条件批量删除
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        int DeleteEntityList<T>(Expression<Func<T, bool>> condition) where T : class;

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        List<T> UpdateEnetityList<T>(List<T> entity) where T : class;

        /// <summary>
        /// 开始事物
        /// </summary>
        /// <returns></returns>
        IDbContextTransaction BeginTran();

        /// <summary>
        /// 事物提交
        /// </summary>
        void TranCommit();

        /// <summary>
        /// 事物回滚
        /// </summary>
        void TranRoolBack();

        /// <summary>
        /// 执行事物
        /// </summary>
        /// <param name="dbTransAction"></param>
        void Trans(Action<IDbContextTransaction> dbTransAction);

        /// <summary>
        /// 执行Sql语句、存储过程
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        int ExecuteSql(string sql);

        /// <summary>
        /// 执行Sql语句（有参数）、存储过程
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        int ExecuteSql(string sql, params object[] parameters);
    }
}
