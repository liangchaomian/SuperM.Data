using Microsoft.EntityFrameworkCore;
using SuperM.EF.Configuration;
using System;

namespace SuperM.EF
{
    public class DataBaseContext : DbContext, IDisposable
    {
        /// <summary>
        /// EF连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        
        /// <summary>
        /// 设置连接字符串
        /// </summary>
        /// <param name="connString"></param>
        public DataBaseContext(string connString="")
        {
            ConnectionString = string.IsNullOrEmpty(connString) ? EFConfiguration.DefalutConnectionString : connString;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (EFConfiguration.EFDataType)
            {
                case DataBaseType.SqlServer:
                    optionsBuilder
                        .UseSqlServer(ConnectionString, u => u.UseRowNumberForPaging());
                    break;
                case DataBaseType.MySql:
                    optionsBuilder
                        .UseMySQL(ConnectionString);
                    break;
                case DataBaseType.Oracle:
                    optionsBuilder
                        .UseOracle(ConnectionString);
                    break;
                case DataBaseType.PgSql:
                    optionsBuilder
                        .UseNpgsql(ConnectionString);
                    break;
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
