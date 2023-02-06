using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using WebApi.Extensions;
using Core;
using WebApi.Helpers;
using Caching;

namespace ProjectTemplate
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(o => o.AddSerilog());
            services.AddSharedServices(Configuration);
            services.AddAppServices(Configuration);
            services.AddWebScrappers(Configuration);
            services.AddApplicationSqlServer(Configuration);
            services.AddRepoServices(Configuration);
            services.AddRedis(Configuration);
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddCustomSwagger(Configuration);

            services.AddControllers();

            services.AddHealthChecks()

                .AddRedis(Configuration.GetSection("RedisSettings:RedisConnectionString").Value,
                name: "RedisHealt-check",
                failureStatus: HealthStatus.Unhealthy,
                tags: new string[] { "api", "Redis" })

                .AddSqlServer(Configuration.GetConnectionString("IdentityConnection"),
                name: "identityDb-check",
                failureStatus: HealthStatus.Unhealthy,
                tags: new string[] { "api", "SqlDb" })

                .AddSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                name: "applicationDb-check",
                failureStatus: HealthStatus.Unhealthy,
                tags: new string[] { "api", "SqlDb" });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyAdmins", policy => policy.RequireRole("SuperAdmin", "Admin"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddSerilog();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseErrorHandlingMiddleware();

            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service Api V1"); });

            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }
    }
}
