using ComputerService;
using ComputerService.Data;
using ComputerService.Interfaces;
using ComputerService.Middleware;
using ComputerService.Security;
using ComputerService.Services;
using ComputerService.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Serilog;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers(options =>
    {
        options.InputFormatters.Insert(0, JsonPatchInputFormatter.GetJsonPatchInputFormatter());
    })
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });

builder.Services.AddMvc();

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
    policy =>
    {
        policy
            .WithOrigins("https://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddAutoMapper(typeof(Program));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(o =>
    {
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException())),
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});

builder.Services.AddSwaggerGenNewtonsoftSupport();

builder.Services.AddDbContext<ComputerServiceContext>(options =>
{
    options
        .EnableSensitiveDataLogging()
        .UseLazyLoadingProxies()
        .UseSqlServer(builder.Configuration["DefaultDataBase"]);
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// registers the Interfaces service with the concrete type Services
builder.Services
    .AddScoped<IPaginationService, PaginationService>()
    .AddScoped<IUriService, UriService>()
    .AddScoped<IAuthenticationService, AuthenticationService>()
    .AddScoped<IPasswordHashingService, PasswordHashingService>()
    .AddScoped<TokenManagerMiddleware>()
    .AddScoped<ITokenManager, TokenManager>()
    .AddScoped<IAccessoryService, AccessoryService>()
    .AddScoped<IAddressService, AddressService>()
    .AddScoped<ICustomerService, CustomerService>()
    .AddScoped<IDeviceService, DeviceService>()
    .AddScoped<IOrderService, OrderService>()
    .AddScoped<IOrderAccessoryService, OrderAccessoryService>()
    .AddScoped<IOrderDetailsService, OrderDetailsService>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<IUserTrackingService, UserTrackingService>();

// Add validators
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();

//Configure Serilog-Logging
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Debug()
    .CreateLogger();

//clear all existing logging providers.
builder.Logging.ClearProviders();
//add Serilog to the logging providers and LoggerConfiguration object as its sole argument
builder.Logging.AddSerilog(logger);

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<TokenManagerMiddleware>();

app.MapControllers();

app.Run();