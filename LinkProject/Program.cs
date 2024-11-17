using LinkProject.Data;
using LinkProject.Models.Role;
using LinkProject.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



//Add DataBase
var connectionString = builder.Configuration.GetConnectionString("DefultConnectionString");
builder.Services.AddDbContext<DataBaseContext>(option => option.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

//AddIdentity
builder.Services.AddIdentity<User,Role>()
    .AddEntityFrameworkStores<DataBaseContext>()
    .AddDefaultTokenProviders();    

var app = builder.Build(); 

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();


app.MapStaticAssets();

   app.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();




app.Run();
