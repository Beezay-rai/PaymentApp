using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PaymentApp.Areas.Admin;
using PaymentApp.Areas.Admin.Interfaces;
using PaymentApp.Areas.Admin.Repositories;
using PaymentApp.Data;
using PaymentApp.DataModel;
using PaymentApp.Model;
using PaymentApp.Utility;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<PaymentContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<AppUserDetail, UserRoles>().AddEntityFrameworkStores<PaymentContext>().AddDefaultTokenProviders();


builder.Services.AddAuthentication(option =>
    {
        option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(option =>
    {
        option.SaveToken = true;
        option.RequireHttpsMetadata = false;
        option.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWTConfig:ValidIssuer"],
            ValidAudience = builder.Configuration["JWTConfig:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTConfig:IssuerSigningKey"])),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        BearerFormat = "Bearer",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Name = "Bearer",
        Description = "Testing my Security scheme",
        In = ParameterLocation.Header,

    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{ }
        }
    });

   

});




//builder.Services.AddLogging();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("./Logs/log-.text", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.Configure<Appsetting>(builder.Configuration.GetSection("AppSettings"));
builder.Services.Configure<AdminConfig>(builder.Configuration.GetSection("AdminConfig"));



builder.Services.AddScoped<ITransaction, TransactionRepository>();
builder.Services.AddScoped<IMyDapper, MyDapperRepository>();
builder.Services.AddScoped<DatabaseUtilities>();



Log.Information("App Initialized");


var app = builder.Build();

app.UseStaticFiles();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


using (var scope = app.Services.CreateScope())
{
 
    var serviceProvider = scope.ServiceProvider;
    CreateAdmin( serviceProvider);
}
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


// Create Admin Role and Admin user
void CreateAdmin(IServiceProvider serviceProvider)
{
    var config = serviceProvider.GetService<IConfiguration>();
    var _usermanager = serviceProvider.GetService<UserManager<AppUserDetail>>();
    var signInManager = serviceProvider.GetService<SignInManager<AppUserDetail>>();
    var roleManager = serviceProvider.GetService<RoleManager<UserRoles>>();
    var adminSection = config.GetSection("AdminConfig");
    //Creating Admin role
    var checkRoleExistTask = roleManager.RoleExistsAsync(adminSection["Role"]);
    checkRoleExistTask.Wait();
    if (!checkRoleExistTask.Result)
    {
        roleManager.CreateAsync(new UserRoles()
        {
            Name = adminSection["Role"] ,
            Details="Application Administrator",
        });
    }

    var adminModel = new AppUserDetail()
    {
        UserName = adminSection["Username"],
        Email = adminSection["Email"],
        EmailConfirmed = true,
        Password    = adminSection["Password"],
        Role= adminSection["Role"]
    };
    var checkingTask = _usermanager.FindByEmailAsync(adminModel.Email);
    checkingTask.Wait();
    var checkedresult = checkingTask.Result;
    if (checkedresult == null)
    {
        var createAdminUser = _usermanager.CreateAsync(adminModel, adminSection["Password"]);
        createAdminUser.Wait();
        if(createAdminUser.Result.Succeeded)
        {
            var assignRoleToAdmin = _usermanager.AddToRoleAsync(adminModel,adminModel.Role);
            assignRoleToAdmin.Wait();
        }




    }


}
