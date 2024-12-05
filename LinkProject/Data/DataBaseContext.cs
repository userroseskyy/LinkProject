

using LinkProject.Areas.Profile.Models.User;
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
       public DbSet<UserProfile> userProfiles {  get; set; }
       public DbSet<UserLink> userLinks {  get; set; }
       public DbSet<Icon> Icons {  get; set; }
       




        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            
           

        }
    }
}

