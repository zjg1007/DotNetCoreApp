using Dnc.Entities.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dnc.Entities.Article
{
    /// <summary>
    /// 订单明细表
    /// </summary>
    public class OrderProductItems:IEntity
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 订单ID
        /// </summary>
        public string OID { get; set; }
        /// <summary>
        /// 商品ID
        /// </summary>
        public string PID { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary>
        public int BuyCount { get; set; }
        public string Name { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ProductsInfo ProductsInfo { get; set; }
        public OrderProductItems()
        {
            this.ID = Guid.NewGuid().ToString();
        }
    }
}
