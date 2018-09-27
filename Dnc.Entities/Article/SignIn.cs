using Dnc.Entities.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dnc.Entities.Article
{
    /// <summary>
    /// 签到表
    /// </summary>
    public class SignIn : IEntity
    {
        public string ID { get; set; }
        /// <summary>
        /// 用户信息从表
        /// </summary>
        public virtual ApplicationUser ApplicationUser { get; set; }
        /// <summary>
        /// 签到时间
        /// </summary>
        public DateTime NewTime { get; set; }

        public string Name { get; set; }
        public SignIn()
        {
            this.ID = Guid.NewGuid().ToString();
            this.NewTime = DateTime.Now;
        }
    }
}
