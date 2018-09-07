using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dnc.Entities.Article
{
    /// <summary>
    /// 商品信息表 
    /// </summary>
    public class ProductsInfo: IEntity
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>   
        /// 商品标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 优惠信息
        /// </summary>
        public string Favorable { get; set; }
        /// <summary>
        /// 商品简介
        /// </summary>
        public string Abstract { get; set; }
        /// <summary>
        /// 类别ID
        /// </summary>
        public string CategoryID { get; set; }
        /// <summary>
        /// 商品销售价格
        /// </summary>
        public string ShoppPrice { get; set; }
        /// <summary>
        /// 商品成本价
        /// </summary>
        public string CostpPrice { get; set; }
        /// <summary>
        /// 是否为精品
        /// </summary>
        public bool IsBest { get; set; }
        /// <summary>
        /// 是否热销
        /// </summary>
        public bool IsHot { get; set; }
        /// <summary>
        /// 是否是新品
        /// </summary>
        public bool IsNew { get; set; }
        /// <summary>
        /// 是否免运费
        /// </summary>
        public bool IsFree { get; set; }
        /// <summary>
        /// 重量 
        /// </summary>
        public double Weight { get; set; }
        /// <summary>
        /// 尺寸
        /// </summary>
        public double Volume { get; set; }
        /// <summary>
        /// 计价单位（件重尺）
        /// </summary>
        public string PayType { get; set; }
        /// <summary>
        /// 商品库存
        /// </summary>
        public int Inventory { get; set; }
        /// <summary>
        /// 警告库存
        /// </summary>
        public int Limit { get; set; }
        /// <summary>
        /// 销量
        /// </summary>
        public int Sell { get; set; }
        /// <summary>
        /// 好评数
        /// </summary>
        public int BestCount { get; set; }
        /// <summary>
        /// 中等评价数
        /// </summary>
        public int MediumcCount { get; set; }
        /// <summary>  
        /// 差评数
        /// </summary>
        public int BadcCount { get; set; }
        /// <summary>
        /// 图片路径列表
        /// </summary>
        public string ImagesUrl { get; set; }
        /// <summary>
        /// 归属的商品类型
        /// </summary>
        public virtual ProductsCategory ProductsCategory { get; set; }
        public ProductsInfo()
        {
            this.ID = Guid.NewGuid().ToString();
        }

    }
}
