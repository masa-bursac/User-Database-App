using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;
using WebApplicationBack.Model;

namespace WebApplicationBack
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // JWT Authentication
            var key = Encoding.ASCII.GetBytes("QKcOa8xPopVOliV6tpvuWmoKn4MOydSeIzUt4W4r1UlU2De7dTUYMlrgv3rU");

            // Add Authentication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                    // Validate the token issuer and audience (if needed, can be turned off here)
                    ValidateIssuer = false,  // Set to true if you want to validate the issuer
                    ValidateAudience = false,  // Set to true if you want to validate the audience
                    ValidateLifetime = true,  // Ensure token is not expired
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero  // Optional: to remove the 5-minute clock skew default

                };
                });

            // Add Authorization policies
            services.AddAuthorization(options =>
            {
                // Policy for Admin role
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireClaim(ClaimTypes.Role, "admin"));
            });

            services.AddControllers();

            services.AddDbContext<MyDbContext>(options =>
              options.UseNpgsql(ConfigurationExtensions.GetConnectionString(Configuration, "MyDbContextConnectionString")).UseLazyLoadingProxies());
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("MyPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
