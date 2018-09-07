using Dnc.Entities.Article;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DNC.ApiModel.Article
{
    public class ProductsInfoMV
    {
        [Display(Name = "商品ID")]
        public string ID { get; set; }
        [Display(Name = "商品名称")]
        [Required(ErrorMessage = "{0}不能为空。")]
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name { get; set; }
        [Display(Name = "商品标题")]
        [Required(ErrorMessage = "{0}不能为空。")]
        /// <summary>   
        /// 商品标题
        /// </summary>
        public string Title { get; set; }
        [Display(Name = "优惠信息")]
        [Required(ErrorMessage = "{0}不能为空。")]
        /// <summary>
        /// 优惠信息
        /// </summary>
        public string Favorable { get; set; }
        [Display(Name = "商品简介")]
        [Required(ErrorMessage = "{0}不能为空。")]
        /// <summary>
        /// 商品简介
        /// </summary>
        public string Abstract { get; set; }
        /// <summary>
        /// 类别ID
        /// </summary>
        public string CategoryID { get; set; }
        [Display(Name = "商品销售价格")]
        [Required(ErrorMessage = "{0}不能为空。")]
        /// <summary>
        /// 商品销售价格
        /// </summary>
        public string ShoppPrice { get; set; }
        [Display(Name = "商品成本价")]
        [Required(ErrorMessage = "{0}不能为空。")]
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
        [Display(Name = "重量")]
        [Required(ErrorMessage = "{0}不能为空。")]
        /// <summary>
        /// 重量 
        /// </summary>
        public double Weight { get; set; }
        [Display(Name = "尺寸")]
        [Required(ErrorMessage = "{0}不能为空。")]
        /// <summary>
        /// 尺寸
        /// </summary>
        public double Volume { get; set; }
        [Display(Name = "计价单位（件重尺）")]
        [Required(ErrorMessage = "{0}不能为空。")]
        /// <summary>
        /// 计价单位（件重尺）
        /// </summary>
        public string PayType { get; set; }
        [Display(Name = "商品库存")]
        [Required(ErrorMessage = "{0}不能为空。")]
        /// <summary>
        /// 商品库存
        /// </summary>
        public int Inventory { get; set; }
        [Display(Name = "警告库存")]
        [Required(ErrorMessage = "{0}不能为空。")]
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
        /// 归属的商品类型
        /// </summary>
        public  ProductsCategory ProductsCategory { get; set; }
        /// <summary>
        /// 商品类别id
        /// </summary>
        public string ProductsCategoryID { get; set; }
        /// <summary>
        /// 商品类别名称
        /// </summary>
        public string ProductsCategoryName { get; set; }
        /// <summary>
        /// 图片路径列表
        /// </summary>
        public string ImagesUrl { get; set; }
        public ProductsInfoMV() { }
        public ProductsInfoMV(ProductsInfo bo)
        {
            this.ID = bo.ID.ToString();
            this.Name = bo.Name;
            this.Title = bo.Title;
            this.Favorable = bo.Favorable;
            this.Abstract = bo.Abstract;
            this.CategoryID = bo.CategoryID;
            this.ShoppPrice = bo.ShoppPrice;
            this.CostpPrice = bo.CostpPrice;
            this.IsBest = bo.IsBest;
            this.IsHot = bo.IsHot;
            this.IsNew = bo.IsNew;
            this.IsFree = bo.IsFree;
            this.Weight = bo.Weight;
            this.Volume = bo.Volume;
            this.PayType = bo.PayType;
            this.Inventory = bo.Inventory;
            this.Limit = bo.Limit;
            this.Sell = bo.Sell;
            this.BestCount = bo.BestCount;
            this.ImagesUrl = bo.ImagesUrl;
            this.MediumcCount = bo.MediumcCount;
            if (bo.ProductsCategory != null)
            {
                this.ProductsCategoryName = bo.ProductsCategory.Name;
                this.ProductsCategoryID = bo.ProductsCategory.ID;
            }
        }

        public void MapBo(ProductsInfo bo)
        {
            bo.ID = this.ID;
            bo.Name = this.Name;
            bo.Title = this.Title;
            bo.Favorable = this.Favorable;
            bo.Abstract = this.Abstract;
            bo.CategoryID = this.CategoryID;
            bo.ShoppPrice = this.ShoppPrice;
            bo.CostpPrice = this.CostpPrice;
            bo.IsBest = this.IsBest;
            bo.IsHot = this.IsHot;
            bo.IsNew = this.IsNew;
            bo.IsFree = this.IsFree;
            bo.Weight = this.Weight;
            bo.Volume = this.Volume;
            bo.PayType = this.PayType;
            bo.Inventory = this.Inventory;
            bo.Limit = this.Limit;
            bo.Sell = this.Sell;
            bo.BestCount = this.BestCount;
            bo.ImagesUrl = this.ImagesUrl;
            bo.MediumcCount = this.MediumcCount;
            
            if (this.ProductsCategory != null)
            {
                bo.ProductsCategory.Name = this.ProductsCategory.Name;
                bo.ProductsCategory.ID = this.ProductsCategory.ID;
            }
        }
    }
}
