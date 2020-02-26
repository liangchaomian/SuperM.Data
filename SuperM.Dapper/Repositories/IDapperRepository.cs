using System;
using System.Collections.Generic;
using System.Data;

namespace SuperM.Dapper.Repositories
{
    /// <summary>
    /// Dapper仓储接口
    /// </summary>
    public interface IDapperRepository
    {
        /// <summary>
        /// 查出多条记录的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<T> Query<T>(string sql, object param = null);

        /// <summary>
        /// 查出一条记录的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        T QueryFirstOrDefault<T>(string sql, object param = null);

        /// <summary>
        /// 事务提交
        /// </summary>
        /// <param name="action"></param>
        void Transaction(Action<IDbConnection> action);
    }
}
