using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TeamSolution.DatabaseContext;
using TeamSolution.Repository;
using TeamSolution.Repository.Interface;
using TeamSolution.Service;
using TeamSolution.Service.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Configure SQL Server Connection to Azure SQL Database from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));
//options.UseSqlServer(builder.Configuration.GetConnectionString("CloudConnection")));

// Add Identity Service with JWT Token Authentication and Role-based Authorization
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Fix Null Reference Exception by checking if configuration is null before accessing GetSection()
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token")?.Value ?? throw new Exception("Invalid Token in configuration"))), // Fix possible null reference argument for parameter 's' in 'byte[] Encoding.GetBytes(string s)'.
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// Configure Swagger/OpenAPI 
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    option.OperationFilter<SecurityRequirementsOperationFilter>();
});

// Add cross-origin resource sharing
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add Scoped Services DAO
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ICustomerComplainStoreRepository, CustomerComplainStoreRepository>();
builder.Services.AddScoped<IFeedBackRepository, FeedBackRepository>();
builder.Services.AddScoped<IIssueRepository, IssueRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IShipperReportIssueTourRepository, ShipperReportIssueTourRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreServiceRepository, StoreServiceRepository>();
builder.Services.AddScoped<ITourShipperRepository, TourShipperRepository>();

// Add Scoped Services Repository
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICustomerComplainStoreService, CustomerComplainStoreService>();
builder.Services.AddScoped<IFeedBackService, FeedBackService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IShipperReportIssueTourService, ShipperReportIssueTourService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IStoreServiceService, StoreServiceService>();
builder.Services.AddScoped<ITourShipperService, TourShipperService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TeamSolution v1"));

app.UseAuthorization();

app.MapControllers();

app.Run();
