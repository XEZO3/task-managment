using CV.DAL.Data;
using Domain.Models;
using EComerce.ActionFilter;
using EComerce.Common.Extention;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using task_managment.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ValidationFilter>();
builder.Services.AddDbContext<dbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnection"));
});
builder.Services.AddAutoMapper(typeof(Users));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt => {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("tokenValidation:key"))),
            ValidIssuer = builder.Configuration.GetValue<string>("tokenValidation:issuer"),
            ValidateIssuer = true,
            ValidateAudience = false,
        };
    });
builder.Services.AddAuthentication();
builder.RegisterServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

//app.Use(async (context, next) =>
//{
//    // Do work that can write to the Response.
//   // context.Request.Headers.TryGetValue("lang", out StringValues lang);
   
//    await next.Invoke();
//    // Do logging or other work that doesn't write to the Response.
//});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
