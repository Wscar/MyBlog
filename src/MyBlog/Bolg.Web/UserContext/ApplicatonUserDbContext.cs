﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bolg.Web.UserContext
{
    public class ApplicatonUserDbContext: IdentityDbContext<ApplicationUser, ApplicationUserRole,int>
    {
        public ApplicatonUserDbContext(DbContextOptions<ApplicatonUserDbContext> optinos) : base(optinos)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
