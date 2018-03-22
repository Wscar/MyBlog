using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bolg.Web.Models.ViewModels
{
    public class RegisterViewModel
    {   
        [Required(ErrorMessage ="邮箱不符合规范")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage ="请输入用户名")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Required(ErrorMessage ="两次密码不一致")]        
        [DataType(DataType.Password)]
        public string ConfirmPassWord { get; set; }
    }
}
