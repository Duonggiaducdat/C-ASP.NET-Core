using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using QLTHUVIEN.Interfaces;
using QLTHUVIEN.Models;
using QLTHUVIEN.Services;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ MVC với các View
builder.Services.AddControllersWithViews();
// Đăng ký HttpClient
builder.Services.AddHttpClient();
// Thêm các dịch vụ vào container
builder.Services.AddScoped<ISachServices, SachService>();
builder.Services.AddScoped<ILoaiSachServices, LoaiSachServices>();
builder.Services.AddScoped<ITacGiaServices, TacGiaServices>();
builder.Services.AddScoped<IPhieuMuonServices, PhieuMuonServices>();
builder.Services.AddScoped<ITaiKhoanServiecs, TaiKhoanServeces>();

// Cấu hình DbContext với chuỗi kết nối từ tệp cấu hình
builder.Services.AddDbContext<QLTVContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Cấu hình xác thực bằng cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/TaiKhoan/Login"; // Đường dẫn tới trang đăng nhập
        options.LogoutPath = "/logout"; // Đường dẫn để đăng xuất
        options.AccessDeniedPath = "/TaiKhoan/AccessDenied"; // Đường dẫn khi không có quyền (nếu cần)
    });

var app = builder.Build();

// Cấu hình pipeline HTTP request
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Thêm middleware xác thực
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=home}/{action=index}/{id?}");

app.Run();

//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.EntityFrameworkCore;
//using QLTHUVIEN.Interfaces;
//using QLTHUVIEN.Models;
//using QLTHUVIEN.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Thêm dịch vụ MVC với các View
//builder.Services.AddControllersWithViews(options =>
//{
//    // Đăng ký filter toàn cục
//    options.Filters.Add(new SessionFilter("Username")); // Đảm bảo SessionFilter không yêu cầu tiêm phụ thuộc không hợp lệ
//});

//// Thêm các dịch vụ vào container
//builder.Services.AddScoped<ISachServices, SachService>();
//builder.Services.AddScoped<ILoaiSachServices, LoaiSachServices>();
//builder.Services.AddScoped<ITacGiaServices, TacGiaServices>();
//builder.Services.AddScoped<IPhieuMuonServices, PhieuMuonServices>();

//// Cấu hình DbContext với chuỗi kết nối từ tệp cấu hình
//builder.Services.AddDbContext<QLTVContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//// Cấu hình Session
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thay đổi thời gian timeout nếu cần
//    options.Cookie.HttpOnly = true; // Chỉ cho phép cookie được sử dụng qua HTTP
//    options.Cookie.IsEssential = true; // Cookie là cần thiết cho ứng dụng
//});

//// Cấu hình xác thực bằng cookie
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.LoginPath = "/TaiKhoan/Login"; // Đường dẫn tới trang đăng nhập
//        options.AccessDeniedPath = "/TaiKhoan/AccessDenied"; // Đường dẫn khi không có quyền (nếu cần)
//    });

//var app = builder.Build();

//// Cấu hình pipeline HTTP request
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//// Thêm middleware xác thực và session
//app.UseAuthentication();
//app.UseSession();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=TaiKhoan}/{action=Login}/{id?}");

//app.Run();
