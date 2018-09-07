using Dnc.Entities.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dnc.Entities.Article
{
    /// <summary>
    /// 用户收藏商品信息表 
    /// </summary>
    public class CollectionInfo : IEntity
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string PID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ProductsInfo ProductsInfo { get; set; }

        public CollectionInfo()
        {
            this.ID = Guid.NewGuid().ToString();
        }
    }
}
