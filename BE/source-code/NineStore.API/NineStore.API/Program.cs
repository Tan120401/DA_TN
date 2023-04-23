using Microsoft.OpenApi.Models;
using NineStore.BL.BaseBL;
using NineStore.BL.UserBL;
using NineStore.DL;
using NineStore.DL.BaseDL;
using NineStore.DL.UserDL;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using NineStore.API.Services;
using NineStore.BL.CategoryBL;
using NineStore.DL.CategoryDL;
using NineStore.BL.ProductBL;
using NineStore.DL.ProductDL;
using NineStore.Common.Entities;
using NineStore.BL.SizeBL;
using NineStore.DL.SizeDL;
using NineStore.BL.ImageProductBL;
using NineStore.DL.ImageProductDL;
using NineStore.BL.CartBL;
using NineStore.DL.CartDL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => //1
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

DataContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));


builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IUserDL, UserDL>();

builder.Services.AddScoped<IProductBL, ProductBL>();
builder.Services.AddScoped<IProductDL, ProductDL>();

builder.Services.AddScoped<ICategoryBL, CategoryBL>();
builder.Services.AddScoped<ICategoryDL, CategoryDL>();

builder.Services.AddScoped<ISizeBL, SizeBL>();
builder.Services.AddScoped<ISizeDL, SizeDL>();

builder.Services.AddScoped<IImageProductBL, ImageProductBL>();
builder.Services.AddScoped<IImageProductDL, ImageProductDL>();

builder.Services.AddScoped<ICartBL, CartBL>();
builder.Services.AddScoped<ICartDL, CartDL>();

builder.Services.AddScoped<IEmailService, EmailService>();

// Convert output to pascalcase
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corspolicy"); //2
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
