using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SuperM.EF.Repositories
{
    public class EFRepository: IEFRepository
    {
        /// <summary>
        /// 获取 当前使用的数据访问上下文对象
        /// </summary>
        private DbContext EfdDbContext { get; set; }

        /// <summary>
        /// 事务对象
        /// </summary>
        private IDbContextTransaction DBTransaction { get; set; }

        /// <summary>
        /// 初始化上下文（默认）
        /// </summary>
        public EFRepository(string connString=null)
        {
            EfdDbContext = new DataBaseContext(connString);
        }

        /// <summary>
        /// 初始化上下文
        /// </summary>
        public EFRepository(DataBaseType dataBaseType,string connString = null)
        {
            EfdDbContext = new DataBaseContext(connString, dataBaseType);
        }

        /// <summary>
        /// 初始化上下文
        /// </summary>
        /// <param name="dataBaseContext">数据库上下文</param>
        public EFRepository(DataBaseContext dataBaseContext)
        {
            EfdDbContext = dataBaseContext;
        }

        /// <summary>
        /// 初始化上下文
        /// </summary>
        /// <param name="toLower">DataTable 是否转小写</param>
        /// <param name="dataBaseContext">数据库上下文</param>
        public EFRepository(bool toLower, DataBaseContext dataBaseContext)
        {
            EfdDbContext = dataBaseContext;
        }
        /// <summary>
        /// 获取上下文
        /// </summary>
        /// <returns></returns>
        public DbContext GetDbContext() {
            var context = EfdDbContext;
            if(EfdDbContext==null)
                return new DataBaseContext(null);
            return EfdDbContext;
        }

        /// <summary>
        /// 释放上下文
        /// </summary>
        public void Dispose()
        {
            EfdDbContext.Dispose();
        }

        #region 查询
        /// <summary>
        /// 根据主键查询单个
        /// </summary>
        /// <typeparam name="keyValue">主键</typeparam>
        /// <returns></returns>
        public T Find<T>(string keyValue) where T : class
            => EfdDbContext.Set<T>().Find(keyValue);

        /// <summary>
        /// 根据表达式查询单个
        /// </summary>
        /// <typeparam name="condition">表达式</typeparam>
        /// <returns></returns>
        public T Find<T>(Expression<Func<T, bool>> condition) where T : class
            => EfdDbContext.Set<T>().Where(condition).FirstOrDefault();

        /// <summary>
        /// 根据主键查询单个(异步)
        /// </summary>
        /// <typeparam name="keyValue">主键</typeparam>
        /// <returns></returns>
        public async Task<T> FindAsync<T>(string keyValue) where T : class
            => await EfdDbContext.Set<T>().FindAsync(keyValue);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public IQueryable<T> IQueryable<T>() where T : class
            => EfdDbContext.Set<T>();

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        public IQueryable<T> IQueryable<T>(Expression<Func<T, bool>> condition) where T : class
            => EfdDbContext.Set<T>().Where(condition);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <returns></returns>
        public List<T> FindList<T>() where T : class
            => EfdDbContext.Set<T>().ToList();

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        public List<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class
            => EfdDbContext.Set<T>().Where(condition).ToList();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="page">页数</param>
        /// <param name="size">行数</param>
        /// <param name="totalcount">总数</param>
        /// <returns></returns>
        public IQueryable<T> IQueryableByPage<T>(int page,int size, out int totalcount) where T : class
        {
            var PageData = EfdDbContext.Set<T>();
            totalcount = PageData.Count();
            return PageData.Skip((page - 1) * size).Take(size);
        }


        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <param name="page">页数</param>
        /// <param name="size">行数</param>
        /// <param name="totalcount">总数</param>
        /// <returns></returns>
        public IQueryable<T> IQueryableByPage<T>(Expression<Func<T, bool>> condition, int page, int size, out int totalcount) where T : class
        {
            var PageData = EfdDbContext.Set<T>();
            totalcount = PageData.Count();
            return PageData.Where(condition).Skip((page - 1) * size).Take(size);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="page">页数</param>
        /// <param name="size">行数</param>
        /// <typeparam name="totalcount">总数</typeparam>
        /// <returns></returns>
        public List<T> FindListByPage<T>(int page, int size, out int totalcount) where T : class
        {
            var PageData= EfdDbContext.Set<T>();
            totalcount = PageData.Count();
            return PageData.Skip((page - 1) * size).Take(size).ToList();
        }

        /// <summary>
        /// 分页条件查询
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <param name="page">页数</param>
        /// <param name="size">行数</param>
        /// <param name="totalcount">总数</param>
        /// <returns></returns>
        public List<T> FindListByPage<T>(Expression<Func<T, bool>> condition, int page, int size, out int totalcount) where T : class
        {
            var PageList = EfdDbContext.Set<T>().Where(condition);
            totalcount = PageList.Count();
            return PageList.Skip((page - 1) * size).Take(size).ToList();
        }

        /// <summary>
        /// 获取总数
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="condition">表达式</param>
        /// <returns></returns>
        public int GetListCount<T>(Expression<Func<T, bool>> condition) where T : class 
            => EfdDbContext.Set<T>().Where(condition).Count();
        #endregion

        #region 增删改
        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool AddEntity<T>(T entity) where T : class
        {
            EfdDbContext.Set<T>().Add(entity);
            return EfdDbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 异步新增
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> AddEntityAsync<T>(T entity) where T : class
        {
            await EfdDbContext.Set<T>().AddAsync(entity);
            return await EfdDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public bool DeleteEntity<T>(T entity) where T : class
        {
            EfdDbContext.Set<T>().Attach(entity);
            EfdDbContext.Set<T>().Remove(entity);
            return EfdDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public T UpdateEnetity<T>(T entity) where T : class
        {
            var EFEntity = EfdDbContext.Update(entity).Entity;
            EfdDbContext.SaveChanges();
            return EFEntity;
        }

        #endregion

        #region 批量增删改

        /// <summary>
        /// 批量增加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int AddEntityList<T>(List<T> entityList) where T : class
        {
            entityList.ForEach(e =>
            {
                EfdDbContext.Set<T>().Add(e);
            });
            return EfdDbContext.SaveChanges();
        }

        /// <summary>
        /// 异步批量增加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> AddEntityListAsync<T>(List<T> entityList) where T : class
        {
            entityList.ForEach(async e =>
            {
                await EfdDbContext.Set<T>().AddAsync(e);
            });
            return await EfdDbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 根据条件批量删除
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public int DeleteEntityListByCondition<T>(Expression<Func<T, bool>> condition) where T : class
        {
            var entityList = EfdDbContext.Set<T>().Where(condition).ToList();
            entityList.ForEach(e =>
            {
                EfdDbContext.Set<T>().Remove(e);
            });
            return EfdDbContext.SaveChanges();
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public List<T> UpdateEnetityList<T>(List<T> entity) where T : class
        {
            return EfdDbContext.Update(entity).Entity;
        }
        #endregion

        #region 执行（事物、存储过程、Sql语句）

        /// <summary>
        /// 开始事物
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction BeginTran()
        {
            if (DBTransaction == null)
                DBTransaction = EfdDbContext.Database.BeginTransaction();
            return DBTransaction;
        }

        /// <summary>
        /// 事物提交
        /// </summary>
        public void TranCommit()
        {
            if (DBTransaction == null)
                return;
            DBTransaction.Commit();
        }

        /// <summary>
        /// 事物回滚
        /// </summary>
        public void TranRoolBack()
        {
            if (DBTransaction == null)
                return;
            DBTransaction.Rollback();
        }

        /// <summary>
        /// 执行事物
        /// </summary>
        /// <param name="dbTransAction"></param>
        public void Trans(Action<IDbContextTransaction> dbTransAction)
        {
            if (DBTransaction == null)
                DBTransaction = EfdDbContext.Database.BeginTransaction();
            dbTransAction.Invoke(DBTransaction);
            DBTransaction.Commit();
        }

        /// <summary>
        /// 执行Sql语句、存储过程
        /// </summary>
        /// <param name="sql">sql</param>
        /// <returns></returns>
        public int ExecuteSql(string sql)
            => EfdDbContext.Database.ExecuteSqlCommand(sql);

        /// <summary>
        /// 执行Sql语句（有参数）、存储过程
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public int ExecuteSql(string sql, params object[] parameters)
            => EfdDbContext.Database.ExecuteSqlCommand(sql, parameters);

        #endregion


    }
}
