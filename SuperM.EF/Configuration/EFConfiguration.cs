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
    }
}
