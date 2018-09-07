using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dnc.Entities.Article
{
    /// <summary>
    /// 搜索记录表
    /// </summary>
    public class SearchHistories: IEntity
    {
        [Key]
        /// <summary>
        /// 记录ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UID { get; set; }
       
        /// <summary>
        /// 搜索词
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 搜索次数
        /// </summary>
        public int Times { get; set; }
        /// <summary>
        /// 搜索时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        public SearchHistories()
        {
            this.ID = Guid.NewGuid().ToString();
            this.UpdateTime = DateTime.Now;
        }
    }
}
