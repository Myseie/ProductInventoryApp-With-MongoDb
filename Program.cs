using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ProductInventoryApp.Data;
using ProductInventoryApp.Models;


var builder = WebApplication.CreateBuilder(args);

// Konfigurera MongoDB-inställningar
builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));

// Registrera MongoDB-klient och `ProductService`
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var mongoSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
    return new MongoClient(mongoSettings.ConnectionString);
});

builder.Services.AddScoped<ProductService>();

// Lägg till MVC-tjänster
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

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Product}/{action=Index}/{id?}");
});

app.Run();