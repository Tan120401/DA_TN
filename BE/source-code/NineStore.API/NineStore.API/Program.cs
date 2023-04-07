using NineStore.BL.BaseBL;
using NineStore.BL.UserBL;
using NineStore.DL;
using NineStore.DL.BaseDL;
using NineStore.DL.UserDL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corspolicy", build => //1
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

DataContext.ConnectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));


builder.Services.AddScoped<IUserBL, UserBL>();
builder.Services.AddScoped<IUserDL,UserDL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corspolicy"); //2
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
