using CleanArchitect.Application.Interfaces;
using CleanArchitect.Infrastructure.DbContexts;
using CleanArchitect.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EBikeShopDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("EBikeShopConnection")
    )
);

builder.Services.AddScoped<IBikeService, BikeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryGetAll, CategoryGetAllService>();

// Add services to the container. CON CUA BU VAI CHUONG!!
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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
