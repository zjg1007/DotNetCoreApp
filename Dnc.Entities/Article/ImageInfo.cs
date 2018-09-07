using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dnc.Entities.Article
{
    /// <summary>
    ///图片存储表
    /// </summary>
    public class ImageInfo : IEntity
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 图片描述
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图片展示类型（购物车预览图、商品图片展示轮播图、用户商品评价、商品详情页图文简介等。。） 
        /// </summary>
        public int ImgType { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }
        /// <summary>
        /// 图片排序
        /// </summary>
        public int DisplayOrder { get; set; }
        public ImageInfo()
        {
            this.ID = Guid.NewGuid().ToString();
        }
    }
}
