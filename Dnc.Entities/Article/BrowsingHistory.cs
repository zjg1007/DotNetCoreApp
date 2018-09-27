using Dnc.Entities.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dnc.Entities.Article
{
    /// <summary>
    /// 浏览商品足迹
    /// </summary>
    public class BrowsingHistory : IEntity
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 用户信息从表
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }
        /// <summary>
        /// 商品信息从表
        /// </summary>
        public virtual ProductsInfo ProductsInfo { get; set; }
        /// <summary>
        /// 商品优惠信息(绑定多个优惠券,初步设定是以逗号分隔,)
        /// </summary>
        public string PreferentialInfo { get; set; }
        /// <summary>
        /// 无效参数，忽略
        /// </summary>
        public string Name { get; set; }
        public BrowsingHistory()
        {
            this.ID = Guid.NewGuid().ToString();
        }
    }
}
