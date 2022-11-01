using ComputerService.Data;
using ComputerService.Interfaces;
using ComputerService.Middleware;
using ComputerService.Services;
using ComputerService.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddMvc();
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

builder.Services.AddSwaggerGen();

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
    .AddScoped<IAccessoryService, AccessoryService>()
    .AddScoped<IAddressService, AddressService>()
    .AddScoped<IClientService, ClientService>()
    .AddScoped<IDeviceService, DeviceService>()
    .AddScoped<IOrderService, OrderService>()
    .AddScoped<IOrderAccessoryService, OrderAccessoryService>()
    .AddScoped<IOrderDetailsService, OrderDetailsService>()
    .AddScoped<IUserService, UserService>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();
