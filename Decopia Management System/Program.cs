using System.Text;
using Decopia.API.Data;
using Decopia.API.Helpers;
using Decopia.API.Interfaces;
using Decopia.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
 

var builder = WebApplication.CreateBuilder(args);

 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };
    });

}

 

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5,              
            maxRetryDelay: TimeSpan.FromSeconds(10), 
            errorNumbersToAdd: null         
            )
        )

    );

builder.Services.AddOpenApi();

builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
            "http://localhost:5177",
            "https://localhost:5177",
            "http://localhost:5174",
            "https://localhost:5174",
            "https://decopia.vercel.app"
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors("AllowFrontend"); 

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
