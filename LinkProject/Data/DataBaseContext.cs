using LinkProject.Models.Role;
using LinkProject.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LinkProject.Data
{
    public class DataBaseContext : IdentityDbContext<User,Role,string>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {

        }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            
           

        }
    }
}

