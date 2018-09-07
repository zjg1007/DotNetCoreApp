using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dnc.Entities.Article
{
    /// <summary>
    /// 订单信息主表 
    /// </summary>
    public class OrderProduct:IEntity
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UID { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string PID { get; set; }
        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime OrderTime { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public double TotalPrices { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public double Freight { get; set; }
        /// <summary>
        /// 订单状态（待付款，待发货，待收货，待评价，交易成功、退款）0,1,2,3,4,5
        /// </summary>
        public int OStatus { get; set; }
        /// <summary>
        /// 买家留言
        /// </summary>
        public string LeaveMessage { get; set; }
        public string Name { get; set; }
        public OrderProduct()
        {
            this.ID = Guid.NewGuid().ToString();
            this.OrderTime = DateTime.Now;
        }
    }
}
