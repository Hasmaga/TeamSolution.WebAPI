using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using TeamSolution.DAO;
using TeamSolution.DAO.Interface;
using TeamSolution.DatabaseContext;
using TeamSolution.Repository;
using TeamSolution.Repository.Interface;

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
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
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
builder.Services.AddScoped<IRoleDAO, RoleDAO>();
builder.Services.AddScoped<IUserDAO, UserDAO>();
builder.Services.AddScoped<IFeedBackDAO, FeedBackDAO>();
builder.Services.AddScoped<IOrderDAO, OrderDAO>();
builder.Services.AddScoped<IOrderDetailDAO, OrderDetailDAO>();
builder.Services.AddScoped<IShipperDetailDAO, ShipperDetailDAO>();
builder.Services.AddScoped<IStatusDAO, StatusDAO>();
builder.Services.AddScoped<IStoreDAO, StoreDAO>();
builder.Services.AddScoped<IStoreModeSetingDAO, StoreModeSetingDAO>();

// Add Scoped Services Repository
builder.Services.AddScoped<IRoleReposotory, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFeebBackRepository, FeedBackRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IShipperDetailRepository, ShipperDetailRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IStoreRepository,  StoreRepository>();
builder.Services.AddScoped<IStoreModeSetingRepository, StoreModeSetingRepository>();

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
