using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TradeAssociationWebsite.DB;
using TradeAssociationWebsite.Repositories;
using TradeAssociationWebsite.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
//read connection string from appsettings.json
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

// SignUp IRepository and Repository
builder.Services.AddScoped<IUserRepository, UserReporitory>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();


// Đọc cấu hình từ appsettings.json
var configuration = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json")
	.Build();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
