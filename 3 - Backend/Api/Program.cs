using Api;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Common.Domain;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using Api.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

var ClientAuthorityEndPoint = builder.Configuration.GetSection($"ClientAuthorityEndPoint:Default").Value.Split(',');

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "AllowSpecificOrigins",
//                      policy =>
//                      {
//                          policy.WithOrigins(ClientAuthorityEndPoint);
//                      });
//});
builder.Services.AddCors();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
ConfigContainer.Config(builder.Services);

//builder.Services.AddDbContext<DataContext>(
//   options => options.UseSqlServer(builder.Configuration.GetSection($"ConfigConnectionString:Default").Value)
//   );

// var serverVersion = new MySqlServerVersion(new Version(11, 0, 2));
// builder.Services.AddDbContext<DataContext>(
//     options => options.UseMySql(builder.Configuration.GetSection($"ConfigConnectionString:MariaDB").Value, serverVersion)
//     );

//builder.Services.AddControllers();
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).AddNewtonsoftJson(opt =>
{
   //opt.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm";
   opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtSettingsBase>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<EmailConfigBase>(builder.Configuration.GetSection("EmailConfig"));

var keyJwt = Encoding.ASCII.GetBytes((builder.Configuration.GetSection($"JwtSettings:Secret").Value + ""));
builder.Services.AddAuthentication(x =>
{
   x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
   x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
   x.RequireHttpsMetadata = false;
   x.SaveToken = true;
   x.TokenValidationParameters = new TokenValidationParameters
   {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(keyJwt),
      ValidateIssuer = false,
      ValidateAudience = false
   };
});

// AddHealthChecks
builder.Services.AddHealthChecks(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
app.UseSwagger(c =>
{
   c.SerializeAsV2 = true;
});

app.UseSwaggerUI(c =>
{
   c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gerador");

});
//}

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
//app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();
app.UseHealthChecks();

app.MapControllers();

app.Run();
