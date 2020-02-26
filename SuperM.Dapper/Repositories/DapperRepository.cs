using MySql.Data.MySqlClient;
using Npgsql;
using SuperM.Dapper.Configuration;
using System;
using System.Data.OracleClient;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using System.Collections.Generic;

namespace SuperM.Dapper.Repositories
{
    /// <summary>
    /// Dapper仓储
    /// </summary>
    public class DapperRepository
    {
        /// <summary>
        /// EF连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="connString"></param>
        public DapperRepository(string connString=null) {
            ConnectionString = string.IsNullOrEmpty(connString) ? DapperConfiguration.DefalutConnectionString : connString;
        }

        /// <summary>
        /// IDbConnection
        /// </summary>
        public IDbConnection DbConnection
        {
            get
            {
                //判断数据库类型
                switch (DapperConfiguration.DapperDataType)
                {
                    case DataBaseType.SqlServer:
                        return new SqlConnection(ConnectionString);
                    case DataBaseType.MySql:
                        return new MySqlConnection(ConnectionString);
                    case DataBaseType.Oracle:
                        return new OracleConnection(ConnectionString);
                    case DataBaseType.PgSql:
                        return new NpgsqlConnection(ConnectionString);
                    default:
                        throw new ArgumentNullException("无效的数据库类型");
                }
            }
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void OpenConnection()
        {
            if (DbConnection.State == ConnectionState.Closed)
                DbConnection.Open();
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseConnection()
        {
            if (DbConnection.State == ConnectionState.Open)
                DbConnection.Close();
        }

        /// <summary>
        /// 执行sql并且返回受影响的行数
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="para">参数</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, object para)
        {
            //打开连接
            OpenConnection();
            //执行Sql
            return DbConnection.Execute(sql, para);
        }

        /// <summary>
        /// 查出多条记录的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object param = null)
        {
            return DbConnection.Query<T>(sql, param);
        }

        /// <summary>
        /// 查出一条记录的实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public T QueryFirstOrDefault<T>(string sql, object param = null)
        {
            return DbConnection.QueryFirst<T>(sql, param);
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        /// <param name="action"></param>
        public void Transaction(Action<IDbConnection> action)
        {
            //打开连接
            OpenConnection();
            //开始事务
            IDbTransaction transaction = DbConnection.BeginTransaction();
            try
            {
                action.Invoke(DbConnection);
                //提交事务
                transaction.Commit();
            }
            catch (Exception ex)
            {
                //出现异常，事务Rollback
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }
    }
}
