using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bolg.Web.Models.ViewModels
{
    public class LogInViewModel
    {   
        [Required()]
        public string Email { get; set; }
        [Required()]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
    }
}
