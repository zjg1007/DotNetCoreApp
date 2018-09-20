using Dnc.Entities.Application;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DNC.ViewModels.Article
{
    public class ApplicationUserMV
    {
        public string ID { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(20)]
        [Display(Name = "昵称")]
        public string Name { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [StringLength(20)]
        [Display(Name = "密码")]
        public string Password { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = "头像")]
        public string Photo { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        public string Phone { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        [Display(Name = "注册时间")]
        public DateTime RegisterTime { get; set; }
        /// <summary>
        /// 最后访问时间
        /// </summary>
        [Display(Name = "最后访问时间")]
        public DateTime LastVisitTime { get; set; }
        /// <summary>
        /// 安全问题
        /// </summary>
        [Display(Name = "安全问题")]
        public string Safeques { get; set; }
        /// <summary>
        /// 安全问题答案
        /// </summary>
        [Display(Name = "安全问题答案")]
        public string SafeAnswer { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        [Display(Name = "是否被封号")]
        public bool Locked { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 用户积分
        /// </summary>
        [Display(Name = "用户积分")]
        public int Integral { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [Display(Name = "出生日期")]
        public DateTime BirthDate { get; set; }
        public string ApplicationGroupID { get; set; }
        public string ApplicationGroupName { get; set; }
        /// <summary>
        /// 是否封号
        /// </summary>
        public string isName { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        public  ApplicationGroup ApplicationGroup { get; set; }
        public ApplicationUserMV() { }
        public ApplicationUserMV(ApplicationUser db)
        {
            this.ID = db.ID;
            this.Name = db.Name;
            this.UserName = db.UserName;
            this.Password = db.Password;
            this.Photo = db.Photo;
            this.Phone = db.Phone;
            this.RegisterTime = db.RegisterTime;
            this.LastVisitTime = db.LastVisitTime;
            this.Safeques = db.Safeques;
            this.SafeAnswer = db.SafeAnswer;
            this.Locked = db.Locked;
            this.Email = db.Email;
            this.Integral = db.Integral;
            this.BirthDate = db.BirthDate;
            if (db.Locked)
            {
                this.isName = "是";
            } else
            {
                this.isName = "否";
            }
            if (db.ApplicationGroup != null)
            {
                this.ApplicationGroup = db.ApplicationGroup;
                this.ApplicationGroupID = db.ApplicationGroup.ID;
                this.ApplicationGroupName = db.ApplicationGroup.Name;
            }
        }
        public void Mapo(ApplicationUser db)
        {
            db.ID = this.ID;
            db.Name = this.Name;
            db.UserName = this.UserName;
            db.Password = this.Password;
            db.Photo = this.Photo;
            db.Phone = this.Phone;
            db.RegisterTime = this.RegisterTime;
            db.LastVisitTime = this.LastVisitTime;
            db.Safeques = this.Safeques;
            db.SafeAnswer = this.SafeAnswer;
            db.Locked = this.Locked;
            db.Email = this.Email;
            db.Integral = this.Integral;
            db.BirthDate = this.BirthDate;
            if (this.ApplicationGroup != null)
            {
                db.ApplicationGroup = this.ApplicationGroup;
                db.ApplicationGroup.ID = this.ApplicationGroupID;
                db.ApplicationGroup.Name = this.ApplicationGroupName;
            }
        }
    }
}
