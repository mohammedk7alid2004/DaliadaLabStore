using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication4.Data;
using WebApplication4.Models;
using WebApplication4.Services;

var builder = WebApplication.CreateBuilder(args);

// تسجيل DbContext
builder.Services.AddDbContext<DalidaAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("con")));

// إعدادات الهوية (Identity)
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DalidaAppDbContext>()
    .AddDefaultTokenProviders();
// تسجيل الخدمات




// إضافة الخدمات المخصصة
builder.Services.AddScoped<CartItemServices>(); // أضف هذا السطر
builder.Services.AddScoped<IBrands, Brands>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<OrderServices>();

// إعدادات الجلسة (Session)
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(35));

builder.Services.AddControllersWithViews();
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// يجب أن يأتي UseAuthentication قبل UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

// إعدادات الجلسة
app.UseSession();

// تعيين مسار التحكم الافتراضي
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();