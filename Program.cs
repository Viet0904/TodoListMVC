using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Authentication.Cookies;

using Microsoft.EntityFrameworkCore;
using TodoListMVC.Data;
using TodoListMVC.Services.Implementations;
using TodoListMVC.Services.Interfaces;
var builder = WebApplication.CreateBuilder(args);


// 1. Đăng ký DbContext (đã có sẵn)
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// 2. Đăng ký Service (Dependency Injection) - QUAN TRỌNG
// Scoped: Một instance mới được tạo cho mỗi request HTTP
builder.Services.AddScoped<IAccountService, AccountService>();

// 3. Cấu hình Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Redirect về đây nếu chưa đăng nhập
        options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
        options.SlidingExpiration = true; // Gia hạn cookie khi user thao tác
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Add services to the container.
builder.Services
    .AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
