
using ApiTest.Data.Context;
using ApiTest.Data.Model;
using ApiTest.InterFaces;
using ApiTest.Presntisses.Repository;
using ApiTest.Presntisses.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ApplicationContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("CS"));
            });
            // Register Identity Services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<ApplicationContext>();

            builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>));
            builder.Services.AddScoped<IEmployeeServices, EmpServices>();
            builder.Services.AddScoped<IDepartmentServices, DeptServices>();
            // Use CORS  Allow All User Use End Points
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            // Allow JWT Token

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;  // Default Jwt Not Cookie
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;  // Show UnAuthorized
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:IssuerIp"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:AudienceIp"],
                  //  ValidateLifetime = true,                
                   // ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecritKey"])),

                 //   ClockSkew = TimeSpan.Zero
                };
            });

            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseCors("MyPolicy");
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
