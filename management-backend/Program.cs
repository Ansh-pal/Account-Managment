using AccountManager.API.Data;
using AccountManager.API.Services;
using AccountManager.API.Helpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<TransactionService>();

// DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT
var jwtKey = builder.Configuration["Jwt:Key"];
builder.Services.AddSingleton(new JwtHelper(jwtKey));

// Services
builder.Services.AddScoped<AuthService>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();