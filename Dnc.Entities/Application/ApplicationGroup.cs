using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dnc.Entities.Application
{
    public class ApplicationGroup:IEntity
    {
        [Key]
        public string ID { get; set; }
        /// <summary>
        /// 中文名称
        /// </summary>
        [StringLength(20)]
        public string Name { get; set; }
        /// <summary>
        /// 中文描述
        /// </summary>
        [StringLength(500)]
        public string Decription { get; set; }
        /// <summary>
        /// 权限(Admin,Client,Client1,Client2,SystemOrAdmin)
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// 权限ID
        /// </summary>
        public long RoleID { get; set; }

        public ApplicationGroup()
        {
            this.ID = Guid.NewGuid().ToString();
        }

    }
}
