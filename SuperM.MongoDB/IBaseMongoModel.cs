using System;

namespace SuperM.MongoDB
{
    public class IBaseMongoModel: IMongoModel
    {
        /// <summary>
        /// 删除标志
        /// </summary>
        public virtual int DeleteMark { get; set; }

        /// <summary>
        /// 启用标志
        /// </summary>
        public virtual int EnabledMark { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public virtual DateTime CreateDate { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        public virtual string CreateUserId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public virtual DateTime? ModifyDate { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public virtual string ModifyUserId { get; set; }

        /// <summary>
        /// 新增
        /// </summary>
        public virtual void Create(
            string userId,
            string modifyUserId)
        {
            CreateDate = DateTime.Now;
            CreateUserId = userId;
            DeleteMark = 0;
            EnabledMark = 1;
        }

        /// <summary>
        /// 修改
        /// </summary>
        public virtual void Modify(
            string userId)
        {
            ModifyDate = DateTime.Now;
            ModifyUserId = userId;
        }
    }
}
