using Dnc.Entities.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dnc.Entities.Article
{
    /// <summary>
    /// 商品评论信息
    /// </summary>
    public class Evaluate : IEntity
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string PID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 评论星级（好评中评差评）0,1,2
        /// </summary>
        public int Star { get; set; }
        /// <summary>
        /// 评论标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string Details { get; set; }
        /// <summary>
        /// 评论时间
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 图片展示
        /// </summary>
        public string ImageInfo { get; set; }
        /// <summary>
        /// 该评论有用数量
        /// </summary>
        public int Use { get; set; }
        /// <summary>
        /// 该评论没用数量
        /// </summary>
        public int NoUse { get; set; }
        public bool IsImage { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ProductsInfo ProductsInfo { get; set; }

        public Evaluate()
        {
            this.ID = Guid.NewGuid().ToString();
        }
    }
}
