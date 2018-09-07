using System;
using System.Collections.Generic;
using System.Text;

namespace Dnc.Entities.Article
{
    /// <summary>
    /// 优惠券信息
    /// </summary>
    public class Coupons : IEntity
    {
        public string ID { get; set; }
        /// <summary>
        /// 优惠券名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public double Money { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 是否可以使用
        /// </summary>
        public bool IsNeed { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 使用限制(满金额打折,满金额减,满数量减,满数量打折)
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// 满多少(数量或金额)
        /// </summary>
        public double Meet { get; set; }
        /// <summary>
        /// 减或打折
        /// </summary>
        public double Operation { get; set; }
        /// <summary>
        /// 商品ID（,）
        /// </summary>
        public string PID { get; set; }
        public Coupons()
        {
            this.ID = Guid.NewGuid().ToString();
        }
    }
}
