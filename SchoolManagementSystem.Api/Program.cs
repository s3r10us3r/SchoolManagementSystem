using Microsoft.IdentityModel.Logging;
using SchoolManagementSystem.Api;
using SchoolManagementSystem.Api.Services.Extensions;
using SchoolManagementSystem.Dal.Extensions;
using SchoolManagementSystem.Shared.Services.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddSharedServices();
builder.Services.AddRepos();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

IdentityModelEventSource.ShowPII = true;


JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication()
    .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        RoleClaimType = JwtSharedClaims.Role,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(GetSecretKey(builder.Configuration))),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

builder.Services.AddHttpLogging(o => {
    o.RequestHeaders.Add("Authorization");
});

if (builder.Environment.IsDevelopment())
{
    builder.Logging.AddDebug();
    builder.Logging.AddConsole();
}

var app = builder.Build();
app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


static string GetSecretKey(IConfiguration configuration)
{
    var secretKey = configuration["Jwt:SecretKey"];
    if (secretKey == null)
        throw new Exception("Secret key is not set");
    return secretKey;
}
