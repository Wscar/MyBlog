using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
namespace Bolg.Web.UserContext
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "varchar(128) ")]
        public string Address { get; set; }
        [MaxLength(1)]
        public string Sex { get; set; }
       
    }
}
