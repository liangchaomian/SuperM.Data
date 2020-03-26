namespace SuperM.MongoDB.Configuration
{
    public class MongoDBConfiguration
    {
        //mongodb://127.0.0.1:27017
        /// <summary>
        /// MongoDB连接字符串
        /// </summary>
        public static string MongoDBConnectionString { get; set; }
        /// <summary>
        /// 库
        /// </summary>
        public static string MongoDBDatatabase { get; set; }
        /// <summary>
        /// 对象
        /// </summary>
        private readonly object lockObj=new object();

        /// <summary>
        /// 设置mongodb连接
        /// </summary>
        /// <param name="ConnStr">连接字符串</param>
        /// <param name="DataBase">库</param>
        public void SetMongoDBConnect(string ConnStr, string DataBase)
        {
            //单例
            lock (lockObj)
            {
                if (
                    string.IsNullOrEmpty(MongoDBConnectionString) &&
                    string.IsNullOrEmpty(MongoDBDatatabase))
                {
                    MongoDBConnectionString = ConnStr;
                    MongoDBDatatabase = DataBase;
                }
            }
        }
    }
}
