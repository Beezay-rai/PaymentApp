using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PaymentApp.Areas.Admin;
using PaymentApp.Areas.Admin.Interfaces;
using PaymentApp.Areas.Admin.Repositories;
using PaymentApp.DataModel;
using PaymentApp.Model;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
OpenApiServer server = new OpenApiServer() { Url= "https://app.swaggerhub.com/home" };
OpenApiServer bija = new OpenApiServer() { Url= "Testing" };
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PaymentContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddLogging();
builder.Services.Configure<Appsetting>(builder.Configuration.GetSection("AppSettings"));





builder.Services.AddScoped<ITransaction, TransactionRepository>();
builder.Services.AddScoped<IMyDapper, MyDapperRepository>();
builder.Services.AddScoped<DatabaseUtilities>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
