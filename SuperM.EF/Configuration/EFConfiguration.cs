namespace SuperM.EF.Configuration
{
    public static class EFConfiguration
    {
        /// <summary>
        /// EF默认连接字符串
        /// </summary>
        public static string DefalutConnectionString { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DataBaseType EFDataType { get; set; }

        /// <summary>
        /// 是否开启加载Dll
        /// </summary>
        public static bool LoadDll { get; set; } = false;

        /// <summary>
        /// 模型Dll名，
        /// </summary>
        public static string ModelDll { get; set; }

        /// <summary>
        /// 初始化EF配置
        /// </summary>
        public static void InitEfConfig(string conn,int dataType,bool loadDll=false,string modelDll="") {
            DefalutConnectionString = conn;
            EFDataType = (DataBaseType)dataType;
            LoadDll = loadDll;
            ModelDll = modelDll;
        }
    }
}
