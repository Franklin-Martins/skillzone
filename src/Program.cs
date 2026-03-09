using Api.Data;
using Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

Configuration.JwtKey = builder.Configuration.GetValue<string>("JwtKey")
    ?? throw new Exception("JwtKey not configured");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => {
    options.UseNpgsql(connectionString);
});

builder.Services.AddTransient<TokenService>();

ConfigureAuthentication(builder);
builder.Services
    .AddControllers()
    .ConfigureApiBehaviorOptions(x =>
    {
        x.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        x.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureAuthentication(WebApplicationBuilder builder)
{
    var key = Encoding.UTF8.GetBytes(Configuration.JwtKey);

    builder.Services
        .AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true
            };
        });

}
