using FitHub.Libary.DataBase.Context;
using FitHub.Service;
using LinqToDB.AspNet;
using LinqToDB.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
var x = Convert.ToInt32(configuration.GetSection("mysql:x").Value);
var y = Convert.ToInt32(configuration.GetSection("mysql:y").Value);
var z = Convert.ToInt32(configuration.GetSection("mysql:z").Value);
var serverVersion = new MySqlServerVersion(new Version(x, y, z));

builder.Services.AddDbContext<FitHubContext>(options =>
{
    options.UseMySql(configuration.GetConnectionString("DefaultConnection"), serverVersion, mysqlOptions =>
{
    mysqlOptions.EnableRetryOnFailure();
});
    options.EnableSensitiveDataLogging();
    options.EnableDetailedErrors();
});
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseCors();
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
//     app.UseSwaggerUI(c =>
//    {
//        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
//        c.RoutePrefix = string.Empty;
//    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
