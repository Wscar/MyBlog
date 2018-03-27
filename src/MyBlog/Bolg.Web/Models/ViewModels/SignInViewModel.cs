using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bolg.Web.Models.ViewModels
{
    public class SignInViewModel
    {
        [Required()]
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Required()]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string PassWord { get; set; }
    }
}
