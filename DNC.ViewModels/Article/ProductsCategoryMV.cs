using Dnc.Entities.Article;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DNC.ViewModels.Article
{
    public class ProductsCategoryMV
    {
        [Display(Name = "ID")]
        public string ID { get; set; }
        /// <summary>
        /// 上级类别ID
        /// </summary>
        public string TopID { get; set; }
        [Display(Name = "类别")]
        [Required(ErrorMessage = "{0}不能为空。")]
        /// <summary>
        /// 类别名称
        /// </summary>
        public string Name { get; set; }
        public ProductsCategoryMV() { }
        public ProductsCategoryMV(ProductsCategory bo)
        {
            this.ID = bo.ID;
            this.TopID = bo.ID;
            this.Name = bo.Name;
        }
        public void MapBo(ProductsCategory bo)
        {
            bo.ID = this.ID;
            bo.TopID = this.ID;
            bo.Name = this.Name;
        }
    }
}
