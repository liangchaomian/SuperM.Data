using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;
using SuperM.EF.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

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
            if (EFConfiguration.LoadDll)
            {
                var modelDll = string.IsNullOrEmpty(EFConfiguration.ModelDll) ? "*" : "";
                foreach (string file in Directory.GetFiles(PlatformServices.Default.Application.ApplicationBasePath, $"{modelDll}.dll"))
                {
                    var assemblyName = AssemblyName.GetAssemblyName(file);
                    AppDomain.CurrentDomain.Load(assemblyName);
                }
                var types = AppDomain.CurrentDomain.GetAssemblies()
                  .SelectMany(a => a.GetTypes().Where(t => t.BaseType == typeof(IBaseEntity)))
                  .ToList();
                types.ForEach(item => {
                    modelBuilder.Model.AddEntityType(item);
                });
                base.OnModelCreating(modelBuilder);
            }
        }

    }
}
