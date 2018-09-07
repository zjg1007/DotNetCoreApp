using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dnc.Entities.Article
{
    /// <summary>
    /// 商品类别表
    /// </summary>
    public class ProductsCategory: IEntity
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 上级类别ID
        /// </summary>
        public string TopID { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        public string Name { get; set; }
        public ProductsCategory()
        {
            this.ID = Guid.NewGuid().ToString();
        }
    }
}
