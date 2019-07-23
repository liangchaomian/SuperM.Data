namespace SuperM.Redis.Configuration
{
    public class RedisConfiguration
    {
        /// <summary>
        /// 写入服务器地址
        /// </summary>
        public static string WriteServerList { get; set; }
        /// <summary>
        /// 读取服务器地址
        /// </summary>
        public static string ReadServerList { get; set; }
        /// <summary>
        /// 最大写入连接数
        /// </summary>
        public static int MaxWritePoolSize { get; set; }
        /// <summary>
        /// 最大读取连接数
        /// </summary>
        public static int MaxReadPoolSize { get; set; }
        /// <summary>
        /// 本地缓存到期时间
        /// </summary>
        public static int LocalCacheTime { get; set; }
        /// <summary>
        /// 自动重启
        /// </summary>
        public static bool AutoStart { get; set; }
        /// <summary>
        /// 是否记录日志,该设置仅用于排查redis运行时出现的问题,如redis工作正常,请关闭该项。
        /// </summary>
        public static bool RecordeLog { get; set; }

        /// <summary>
        /// 初始化配置文件
        /// </summary>
        /// <param name="WriteServerList">写入服务器地址</param>
        /// <param name="ReadServerList">读取服务器地址</param>
        /// <param name="MaxWritePoolSize">最大写入连接数</param>
        /// <param name="MaxReadPoolSize">最大读取连接数</param>
        /// <param name="LocalCacheTime">本地缓存到期时间</param>
        /// <param name="AutoStart">自动重启</param>
        /// <param name="RecordeLog">是否记录日志</param>
        /// <param name=""></param>
        public static void InitConfig(
            string WriteServerListStr,
            string ReadServerListStr,
            int MaxWritePoolCount,
            int MaxReadPoolCount,
            int LocalCacheTimes,
            bool IsAutoStart=true,
            bool IsRecordeLog=false
            )
        {
            WriteServerList = WriteServerListStr;
            ReadServerList = ReadServerListStr;
            MaxWritePoolSize = MaxWritePoolCount;
            MaxReadPoolSize = MaxReadPoolCount;
            LocalCacheTime = LocalCacheTimes;
            AutoStart = IsAutoStart;
            RecordeLog = IsRecordeLog;
        }
    }
}
