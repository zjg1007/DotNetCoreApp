using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{/// <summary>
/// 用户表
/// </summary>
    public class ApplicationUser:IEntity
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(20)]
        public string Name { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(20)]
        public string Password { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
       
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterTime { get; set; }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        public DateTime LastVisitTime { get; set; }
        /// <summary>
        /// 安全问题
        /// </summary>
        public string Safeques { get; set; }
        /// <summary>
        /// 安全问题答案
        /// </summary>
        public string SafeAnswer { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool Locked { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 用户积分
        /// </summary>
        public int Integral { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime BirthDate { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        public virtual ApplicationGroup ApplicationGroup { get; set; }
        public ApplicationUser()
        {
            this.ID = Guid.NewGuid().ToString();
            this.RegisterTime = DateTime.Now;
        }

    }
}
