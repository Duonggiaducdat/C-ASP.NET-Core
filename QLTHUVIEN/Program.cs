using Microsoft.EntityFrameworkCore;
using QLTHUVIEN.Interfaces;
using QLTHUVIEN.Models;
using QLTHUVIEN.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISachServices, SachService>();
builder.Services.AddScoped<ILoaiSachServices, LoaiSachServices>();
builder.Services.AddScoped<ITacGiaServices, TacGiaServices>();
// Cấu hình DbContext với chuỗi kết nối từ tệp cấu hình
builder.Services.AddDbContext<QLTVContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Thêm các dịch vụ khác vào container
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
