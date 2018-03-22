using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Bolg.Web.UserContext
{
    public class ApplicationUser : IdentityUser<int>
    {
        [Column(TypeName = "varchar(128) ")]
        public string Address { get; set; }
        [MaxLength(1)]
        public string Sex { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Nickname { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        [Column(TypeName ="varchar(256)")]
        public string Avater { get; set; }
    } 
}
