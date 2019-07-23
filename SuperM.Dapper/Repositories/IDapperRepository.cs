using System;
using System.Data;

namespace SuperM.Dapper.Repositories
{
    /// <summary>
    /// Dapper仓储接口
    /// </summary>
    public interface IDapperRepository
    {
        /// <summary>
        /// 事务提交
        /// </summary>
        /// <param name="action"></param>
        void Transaction(Action<IDbConnection> action);
    }
}
