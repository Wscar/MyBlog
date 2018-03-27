using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bolg.Web.Models.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage = "邮箱格式不正确")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "邮箱")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "昵称")]
        public string Nickname { get; set; }
        [Required()]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string PassWord { get; set; }
        [Required()]
        [DataType(DataType.Password)]
        [Compare("PassWord", ErrorMessage = "两次密码不一致")]
        [Display(Name = "确认密码")]
        public string ConfirmedPassWord { get; set; }
    }
}
