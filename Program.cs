
using CappyPop_Full_HTML.Models.HomeViewModel;
using CappyPop_Full_HTML.Models.Services;
using CappyPop_Full_HTML.Models.Tables;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<bobateashopContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
                      ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IVnPayService, VnPayService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.UseStaticFiles();

// Authentication and Authorization Middleware
app.UseAuthentication(); // Xác thực trước khi phân quyền
app.UseAuthorization();

// Endpoint Mapping
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
