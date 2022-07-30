using Application.BusinessLogic.Mapper;
using Application.BusinessLogic.Services.Interfaces;
using Application.BusinessLogic.Services.Implementations;
using Application.Model.Database;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDatabaseContext>(
    opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    opt=>opt.MigrationsAssembly("Application")));

var mappingConfig = new MapperConfiguration(mc => 
    mc.AddProfile(new MapperProfile()));
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,

                    };
                });
builder.Services.AddTransient<ITokenService,TokenService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddSingleton(mapper);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
