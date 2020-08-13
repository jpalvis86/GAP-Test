using Insurance.Repository;
using Insurance.WebAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Text.Json.Serialization;

namespace Insurance.WebAPI
{
    public class Startup
    {
        private const string corsPolicyName = "InsuranceApiPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: corsPolicyName,
                    builder =>
                    {
                        builder.WithOrigins("https://jpantestgap.azurewebsites.net")
                                .WithOrigins("http://localhost:4200")
                                .AllowAnyMethod().AllowAnyHeader();
                    });
            });

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            /*
             * For a Production deployment, 
             * we would use another DB like SQL Server.
             */
            services.AddDbContext<InsuranceDbContext>(
                options => options.UseInMemoryDatabase(databaseName: "InMemoryInsuranceDb")
            );

            InjectServiceDependencies(services);
            InjectRepositories(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Insurance API",
                    Version = "v1",
                    Description = "Insurance API for GAP Technical Test",
                    Contact = new OpenApiContact
                    {
                        Name = "Juan Pablo Alvis",
                        Email = "jpan_2009@hotmail.com",
                        Url = new Uri("https://www.linkedin.com/in/jpalvis86"),
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(corsPolicyName);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Insurance API");
                c.RoutePrefix = string.Empty;
            });
        }


        private static void InjectRepositories(IServiceCollection services)
        {
            services.AddScoped<IInsuranceRepository, InsuranceRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }

        private static void InjectServiceDependencies(IServiceCollection services)
        {
            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
