using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vidly.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("YourDbContextConnection") ?? throw new InvalidOperationException("Connection string 'YourDbContextConnection' not found."); ;

builder.Services.AddDbContext<VidlyDBContext>(options => options.UseSqlite(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<VidlyDBContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
app.MapControllers();


// shown below is the old way of adding custom routes 
// which are now added in the controllers

// app.MapControllerRoute(
//     name: "MoviesByReleaseDate",
//     pattern: "movies/released/{year}/{month}",
//     new { controller = "Movies", action = "ByReleaseDate" },
//     new { year = @"\d{4}", month = @"\d{2}" }
// );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    ).WithStaticAssets();


app.MapRazorPages();

app.Run();
