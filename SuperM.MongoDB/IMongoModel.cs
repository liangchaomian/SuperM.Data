using MongoDB.Bson;

namespace SuperM.MongoDB
{
    public class IMongoModel
    {
        /// <summary>
        /// 基础ID
        /// </summary>
        public virtual ObjectId _id { get; set; }
    }
}
