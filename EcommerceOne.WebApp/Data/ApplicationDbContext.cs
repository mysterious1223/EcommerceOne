using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EcommerceOne.WebApp.Models;

namespace EcommerceOne.WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {


        public DbSet<Category> Category {get; set;}
        public DbSet<SubCategory> SubCategory {get; set;}     
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        
    }
}
