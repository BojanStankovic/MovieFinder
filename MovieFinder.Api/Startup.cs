using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using MovieFinder.Api.Setup;
using MovieFinder.Business.Models;
using MovieFinder.Dal;

namespace MovieFinder.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Test cors with:
            //fetch("https://localhost:44373/api/movies/{validImdbId}", {headers: {"Content-Type": "application/json"}}).then(a => a.text()).then(console.log)
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins
                        (
                            Configuration.GetSection("CorsOrigins")?["Local"],
                            Configuration.GetSection("CorsOrigins")?["Develop"]
                         )
                        .WithHeaders(HeaderNames.AccessControlAllowHeaders, "content-type");
                });
            });

            services.AddControllers();

            // Registers context
            services.AddDbContext<MovieFinderDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieFinder", Version = "v1" });
            });
            
            services.Configure<ApiKeys>(Configuration.GetSection("ApiKeys"));
            services.Configure<CacheSettings>(Configuration.GetSection("CacheSettings"));

            services.RegisterServices();
            services.AddMemoryCache(options =>
            {
                options.SizeLimit = int.Parse(Configuration.GetSection("CacheSettings")?["SizeLimit"] ?? throw new Exception("Missing cache SizeLimit in appsettings.json"));
                options.CompactionPercentage = double.Parse(Configuration.GetSection("CacheSettings")?["CompactionPercentage"] ?? throw new Exception("Missing cache CompactionPercentage in appsettings.json"));
            });
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieFinder v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
