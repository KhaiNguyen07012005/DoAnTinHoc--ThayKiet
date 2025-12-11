using DoAnTinHoc.Data;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// ensure data folder exists
var dataFolder = Path.Combine(builder.Environment.ContentRootPath, "data");
Directory.CreateDirectory(dataFolder);

var productsPath = Path.Combine(dataFolder, "products.json");
var categoriesPath = Path.Combine(dataFolder, "categories.json");

// register repositories via factory so dependencies resolved correctly
builder.Services.AddSingleton(sp => new CategoryRepository(categoriesPath));
builder.Services.AddSingleton(sp => new ProductRepository(productsPath, sp.GetRequiredService<CategoryRepository>()));

var app = builder.Build();

// ⭐ Quan trọng: ĐỌC PORT TỪ HOSTING ⭐
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();

app.Run();
