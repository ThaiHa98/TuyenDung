using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TuyenDung.API.Helper;
using TuyenDung.Data.DataContext;
using TuyenDung.Service.Repository.Interface;
using TuyenDung.Service.Repository.Repository;
using TuyenDung.Service.Services.InterfaceIServices;
using TuyenDung.Service.Services.RepositoryServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MyDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
});

builder.Services.AddScoped<IUserIServices, UserServices>();
builder.Services.AddScoped<IUserInterface, UserRepository>();
builder.Services.AddScoped<Token>();
builder.Services.AddScoped<IJobsInterface, JobsRepository>();
builder.Services.AddScoped<IJobsIService, JobsService>();
builder.Services.AddScoped<IJob_seekersIService, Job_seekersService>();
builder.Services.AddScoped<IEmployersInterface, EmployersRepository>();
builder.Services.AddScoped<IEmployersIService, EmployersService>();
builder.Services.AddScoped<IApplicationsIService, ApplicationsService>();
builder.Services.AddScoped<IFormCvInterface,FormCvRepository>();
builder.Services.AddScoped<IFormCvIService, FormCvService>();
builder.Services.AddScoped<IMessagesIService, MessagesService>();
builder.Services.AddScoped<IApplicationsInterface, ApplicationsRepository>();

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

var configuration = builder.Configuration;

if (configuration == null)
{
    throw new Exception("Configuration is null.");
}

var redisConnectionString = builder.Configuration.GetConnectionString("Redis");

if (string.IsNullOrEmpty(redisConnectionString))
{
    throw new Exception("Redis connection string is missing or empty.");
}

builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    redisOptions.Configuration = redisConnectionString;
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "TuyenDungAPI", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Giải quyết các hành động xung đột
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});


builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Token256"]))
    };
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.Cookie.Name = "authenticationToken"; //Tên của cookie
    options.Cookie.HttpOnly = true;
    options.LoginPath = "/account/login"; //đường dẫn đến trang đăng nhập
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

app.UseStaticFiles();
