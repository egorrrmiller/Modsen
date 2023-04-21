using Microsoft.EntityFrameworkCore;
using Modsen.Auth;
using Modsen.Auth.Interfaces;
using Modsen.Database.Context;
using Modsen.Database.Repository;
using Modsen.Database.Repository.Interfaces;
using Modsen.Domain.Models;
using Modsen.Mapper;
using Modsen.Web.Extensions;
using Modsen.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ModsenContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

builder.Services.Configure<JwtOptionsModel>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IGenerateToken, GenerateToken>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptionsModel>();

builder.Services.AddAuthenticationWithJwtBearer(jwtOptions);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithJwtSecurity();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorExceptionHandling>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();