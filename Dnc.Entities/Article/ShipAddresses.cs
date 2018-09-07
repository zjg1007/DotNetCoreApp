using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dnc.Entities.Article
{
    /// <summary>
    /// 收货地址管理表 
    /// </summary>
    public class ShipAddresses : IEntity
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 是否为默认地址/ 
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string ProvinceName { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string DistrictnName { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }
        public ShipAddresses()
        {
            this.ID = Guid.NewGuid().ToString();
        }
    }
}
