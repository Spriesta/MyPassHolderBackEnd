global using MyPassHolder.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MyPassHolder.Common;
using MyPassHolder.Repositories;
using MyPassHolder.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PassHolderContext>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Securitykey"])),
    };
});

//My services
builder.Services.AddScoped<RegisterService>();
builder.Services.AddScoped<RegisterRepository>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<LoginRepository>();
builder.Services.AddScoped<UserOperationsService>();
builder.Services.AddScoped<UserOperationsRepository>();
//My services

var app = builder.Build();

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
